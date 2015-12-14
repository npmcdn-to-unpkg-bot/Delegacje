using System;
using System.Reflection;

namespace CrazyAppsStudio.Delegacje.App.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}