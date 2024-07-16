using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthMobile
{
    //Newly Added Service
    public class AuthMobileService: AuthMobileAppService
    {

        [Authorize("AbpIdentity.Users.Delete")]
        public string GetCheckToken()
        {
            return "Hello";
        }

        [Authorize]
        
        public string GetCheckTokenusers()
        {
            return "Hello";
        }
    }
}
