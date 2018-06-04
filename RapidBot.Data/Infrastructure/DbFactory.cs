using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidBot.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        RapidBotEntities dbContext;

        public RapidBotEntities Init()
        {
            return dbContext ?? (dbContext = new RapidBotEntities());
        }

        protected override void DisposeCore()
        {
            if(dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
