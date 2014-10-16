using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;

namespace SalesforceEventSync.Controllers
{
    public class AppAuthFlowMetadata : FlowMetadata
    {
        private static readonly IAuthorizationCodeFlow flow = new GoogleAuthorizationCodeFlow(
            new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "48938331338-v8lgvsugfbpjujbonvgt7kddp4e20ce0.apps.googleusercontent.com",
                    ClientSecret = "jczdSaVUrQsGAY4I10JaO55w"
                },
                Scopes = new[] { CalendarService.Scope.Calendar },
                DataStore = new FileDataStore("SalesforceEventSync")
            });

        public override string GetUserId(System.Web.Mvc.Controller controller)
        {
            return controller.User.Identity.Name;
        }

        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }
}