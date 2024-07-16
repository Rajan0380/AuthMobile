using Volo.Abp.Modularity;

namespace AuthMobile;

[DependsOn(
    typeof(AuthMobileApplicationModule),
    typeof(AuthMobileDomainTestModule)
)]
public class AuthMobileApplicationTestModule : AbpModule
{

}
