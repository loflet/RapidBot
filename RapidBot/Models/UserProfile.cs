using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RapidBot.Models
{
    public class UserProfile
    {
        public string displayName { get; set; }
        public string mail { get; set; }
        public string jobTitle { get; set; }
        public string givenName { get; set; }
        public string userPrincipalName { get; set; }
    }
}