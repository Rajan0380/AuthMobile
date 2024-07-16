using AuthMobile.Localization;
using Volo.Abp.Application.Services;

namespace AuthMobile;

/* Inherit your application services from this class.
 */
public abstract class AuthMobileAppService : ApplicationService
{
    protected AuthMobileAppService()
    {
        LocalizationResource = typeof(AuthMobileResource);
    }
}
