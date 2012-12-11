using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2;
using N2.Definitions;
using N2.Details;
using N2.Integrity;
using N2.Web.UI;

namespace N2Bootstrap.Library.Models
{
    [PageDefinition("Root Page (fallback)",
        Description = "A fallback root page used to organize start pages. This root can be replaced or inherited in a web application project.",
        SortOrder = 0,
        InstallerVisibility = N2.Installation.InstallerHint.PreferredRootPage,
        IconUrl = "{ManagementUrl}/Resources/icons/page_gear.png",
        TemplateUrl = "{ManagementUrl}/Myself/Root.aspx")]
    [RestrictParents(AllowedTypes.None)]
    [TabContainer("Search", "Search", 200)]
    [TabContainer("Email", "Email", 100)]
    [FieldSetContainer("EmailField", "Email", 100, ContainerName = "Email")]
    [WithManageableSearch(ContainerName = "Search")]
    public class RootPage : ContentItem, IRootPage, ISystemNode
    {
        [EditableText("Host", 1, ContainerName = "EmailField")]
        public virtual string Host { get; set; }

        [EditableNumber("Port", 2, ContainerName = "EmailField")]
        public virtual int Port { get; set; }

        [EditableText("User", 3, ContainerName = "EmailField")]
        public virtual string User { get; set; }

        [EditableText("Password", 4, ContainerName = "EmailField")]
        public virtual string Password { get; set; }

        [EditableCheckBox("Use SSL", 5, ContainerName = "EmailField")]
        public virtual bool UseSSL { get; set; }

        [EditableCheckBox("Use default credentials", 6, ContainerName = "EmailField")]
        public virtual bool UseDefaultCredentials { get; set; }
    }
}
