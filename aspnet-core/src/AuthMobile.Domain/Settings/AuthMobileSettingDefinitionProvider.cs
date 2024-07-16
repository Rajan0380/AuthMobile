using Volo.Abp.Settings;

namespace AuthMobile.Settings;

public class AuthMobileSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AuthMobileSettings.MySetting1));
    }
}
