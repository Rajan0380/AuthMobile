using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace AuthMobile;

[Dependency(ReplaceServices = true)]
public class AuthMobileBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "AuthMobile";
}
