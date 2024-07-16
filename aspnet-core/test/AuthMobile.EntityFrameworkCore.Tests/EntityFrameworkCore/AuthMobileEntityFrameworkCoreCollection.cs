using Xunit;

namespace AuthMobile.EntityFrameworkCore;

[CollectionDefinition(AuthMobileTestConsts.CollectionDefinitionName)]
public class AuthMobileEntityFrameworkCoreCollection : ICollectionFixture<AuthMobileEntityFrameworkCoreFixture>
{

}
