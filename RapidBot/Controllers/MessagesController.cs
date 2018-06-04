﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using AutoMapper;
using RapidBot.Service;
using RapidBot.Controllers;
using Autofac;
using Microsoft.Bot.Builder.Dialogs.Internals;
using System;

namespace RapidBot
{
    [BotAuthentication]
    public class MessagesController : BaseController
    {
        [NonSerialized]
        private IMapper Mapper;
        [NonSerialized]
        private ICustomerService customerService;

        private readonly ILifetimeScope scope;

        public MessagesController(ILifetimeScope scope)
        {
            this.scope = scope;
            //Mapper = _mapper;
            //customerService = _customerService;
        }
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                using (var scope = DialogModule.BeginLifetimeScope(this.scope, activity))
                {
                    //Mapper = scope.Resolve<IMapper>();
                    //customerService = scope.Resolve<ICustomerService>();
                    var dialog = scope.Resolve<IDialog<object>>();
                    await Conversation.SendAsync(activity, () => dialog);
                }
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}