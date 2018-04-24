using RapidBot.Data.Interfaces;
using RapidBot.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RapidBot.Helpers
{
    public sealed class Singleton
    {
        private static IRapidBotUoW _rapidBotUoW;

        private Singleton() { }
        public static IRapidBotUoW GetIRapidBotUoW
        {
            get
            {
                if(_rapidBotUoW == null)
                {
                    _rapidBotUoW = new RapidBotUoW();
                }
                return _rapidBotUoW;
            }
        }
    }
}