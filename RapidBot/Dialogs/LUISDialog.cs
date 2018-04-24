using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using RapidBot.Data;
using RapidBot.Data.Interfaces;
using RapidBot.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RapidBot.Dialogs
{
    //Add your Luis model Id and subscription key below
    [LuisModel("5a864d10-9d6e-4ed9-b0b5-825cc9d6c726", "7bacaea6dfe14a2f9e7c04259cdf21ef")]
    [Serializable]
    public class LUISDialog : LuisDialog<object>
    {
        [NonSerialized]
        private RapidBotUoW _rapidBotUoW = new RapidBotUoW();

        //Type command such as "hi" or "hello" to enter this intent
        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Hey, namaste! So good to see you!");
            context.Wait(MessageReceived);
        }

        //Type command such as "get user" or "show my profile" to enter into this intent
        [LuisIntent("UserManager")]
        public async Task UserManager(IDialogContext context, LuisResult result)
        {
            Customer UserManager = new Customer();
            string email = context.UserData.GetValue<string>(ContextConstants.UserEmail);
            UserManager = _rapidBotUoW.CustomerRepository.Get(w => w.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            await context.PostAsync($"User's first name is {UserManager.FirstName} and last name is {UserManager.LastName}");
            context.Done("Complete");
        }

        //Type command such as "Show me my orders" or "Show my orders" or "my orders" to enter into this intent
        [LuisIntent("ShowOrder")]
        public async Task ShowOrders(IDialogContext context, LuisResult result)
        {
            List<Order> orderList = new List<Order>();
            int userId = context.UserData.GetValue<int>(ContextConstants.UserIdKey);
            string firstName = context.UserData.GetValue<string>(ContextConstants.FirstName);
            orderList = _rapidBotUoW.OrderRepository.Get(w => w.CustomerId.Equals(userId)).ToList();
            IMessageActivity messageOrderList = context.MakeMessage();
            if(orderList.Count > 0 && orderList != null)
            {
                messageOrderList.Text += $"{firstName} you have total {orderList.Count} orders. ...  \n ";
                int orderCount = 1;
                foreach(var order in orderList)
                {
                    messageOrderList.Text += $"  ### Order Id {order.Id} contains following orders: -  \n ";
                    List<OrderItem> orderItemList = order.OrderItems.ToList();
                    int orderItemCount = 0;
                    foreach(var orderItem in orderItemList)
                    {
                        messageOrderList.Text += $" \n {++orderItemCount}. You ordered {orderItem.Product.ProductName} of price {orderItem.UnitPrice} with quantity as {orderItem.Quantity}, so total price is {orderItem.Quantity * orderItem.UnitPrice}  \n ";
                        //orderItemCount++;
                    }
                    messageOrderList.Text += $"  \n The total order price is Rs. {order.TotalAmount}  \n ";
                    orderCount++;
                }
                
            }
            await context.PostAsync(messageOrderList);
            context.Done("Complete");
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I am yet to be trained to understand what you want to say");
            context.Wait(MessageReceived);
        }
    }
}