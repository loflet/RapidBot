using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using AuthBot;
using AuthBot.Dialogs;
using System.Net.Http;
using System.Net.Http.Headers;
using RapidBot.Models;
using RapidBot.Data;
using RapidBot.Helpers;
using System.Linq;
using AutoMapper;
using RapidBot.Service;
using RapidBot.Service.DTOs;

namespace RapidBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        [NonSerialized]
        private readonly IMapper Mapper;
        //[NonSerialized]
        private readonly ICustomerService customerService;
        public RootDialog(IMapper _mapper, ICustomerService _customerService)
        {
            Mapper = _mapper;
            customerService = _customerService;
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(ProcessMessageAsync);

            //return Task.CompletedTask;
        }

        public async Task ProcessMessageAsync(IDialogContext context, IAwaitable<IMessageActivity> item)
        {
            var message = await item;
            if (string.IsNullOrEmpty(await context.GetAccessToken("https://graph.microsoft.com")))
            {
                await context.Forward(new AzureAuthDialog("https://graph.microsoft.com"), this.ResumeAfterAuth, message, System.Threading.CancellationToken.None);
            }
            else
            {
                //var token = await context.GetAccessToken("https://graph.microsoft.com");
                //await context.PostAsync(token);
                //context.Wait(ProcessMessageAsync);

                await context.Forward(new LUISDialog(), this.ResumeAfterOptionDialog, message, System.Threading.CancellationToken.None);
            }
        }

        private async Task ResumeAfterAuth(IDialogContext context, IAwaitable<string> result)
        {
            try
            {


                HttpClient client = new HttpClient();
                //AD resposnse message   
                var message = await result;
                await context.PostAsync(message);
                context.Wait(ProcessMessageAsync);
                var token = await context.GetAccessToken("https://graph.microsoft.com");

                client.BaseAddress = new Uri("https://graph.microsoft.com/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage responseMessage = await client.GetAsync("v1.0/me");

                UserProfile userProfile = new UserProfile();
                if (responseMessage.IsSuccessStatusCode)
                {
                    userProfile = await responseMessage.Content.ReadAsAsync<UserProfile>();
                }

                //Customer UserManager = new Customer();
                //UserManager = Singleton.GetIRapidBotUoW.CustomerRepository.Get(w => w.Email.Equals(userProfile.userPrincipalName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                CustomerDto UserManager = new CustomerDto();
                UserManager = customerService.GetCustomerByEmail(userProfile.userPrincipalName);

                if (UserManager != null)
                {
                    context.UserData.SetValue(ContextConstants.UserEmail, UserManager.Email);
                    context.UserData.SetValue(ContextConstants.UserIdKey, UserManager.Id);
                    context.UserData.SetValue(ContextConstants.FirstName, UserManager.FirstName);
                }
                else
                {
                    await context.PostAsync($"You don't have authorize to access this. Logging you out..");
                    await context.Logout();
                }
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        public async Task ProcessAuthResultAsync(IDialogContext context, IAwaitable<string> result)
        {
            var token = await context.GetAccessToken("https://graph.microsoft.com");
            var message = await result;
            await context.PostAsync(message);
            context.Wait(ProcessMessageAsync);
        }
        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(ProcessMessageAsync);
            }
        }
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            context.Wait(MessageReceivedAsync);
        }
    }
}