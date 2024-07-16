using AuthMobile.Samples;
using Xunit;

namespace AuthMobile.EntityFrameworkCore.Applications;

[Collection(AuthMobileTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<AuthMobileEntityFrameworkCoreTestModule>
{

}
