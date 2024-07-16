using AuthMobile.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AuthMobile.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AuthMobileController : AbpControllerBase
{
    protected AuthMobileController()
    {
        LocalizationResource = typeof(AuthMobileResource);
    }
}
