using AuthMobile.Samples;
using Xunit;

namespace AuthMobile.EntityFrameworkCore.Domains;

[Collection(AuthMobileTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<AuthMobileEntityFrameworkCoreTestModule>
{

}
