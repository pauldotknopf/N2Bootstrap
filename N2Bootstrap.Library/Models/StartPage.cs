using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BootstrapBlog.Blog;
using N2;
using N2.Definitions;
using N2.Details;
using N2.Edit;
using N2.Engine.Globalization;
using N2.Installation;
using N2.Integrity;
using N2.Security;
using N2.Web;
using N2.Web.UI;
using N2Bootstrap.Library.Adapters;

namespace N2Bootstrap.Library.Models
{
	/// <summary>
	/// This is the start page on a site. Separate start pages can respond to 
	/// a domain name and/or form the root of translation.
	/// </summary>
    [PageDefinition("Start Page",
        Description = "The topmost node of a site. This can be placed below a language intersection to also represent a language",
        InstallerVisibility=InstallerHint.PreferredStartPage,
        IconUrl = "{IconsUrl}/page_world.png")]
    [RestrictParents(typeof(IRootPage), typeof(LanguageIntersection))]
    [RecursiveContainer("SiteContainer", 1000, RequiredPermission = Permission.Administer)]
    [FieldSetContainer(Defaults.Containers.Site, "Site", 1000, ContainerName = "SiteContainer")]
	public class StartPage : ContentPage, IStartPage, IStructuralPage, IThemeable, ILanguage, ISitesSource, IItemAdapter
	{
		#region IThemeable Members

        [EditableThemeSelection(EnablePreview = true, ContainerName = Defaults.Containers.Site)]
		public virtual string Theme { get; set; }

		#endregion

		#region ILanguage Members

		public string FlagUrl
		{
			get
			{
				if (string.IsNullOrEmpty(LanguageCode))
					return "";

				var parts = LanguageCode.Split('-');
				return N2.Web.Url.ResolveTokens(string.Format("~/N2/Resources/Img/Flags/{0}.png", parts[parts.Length - 1].ToLower()));
			}
		}

        [EditableLanguagesDropDown(Title="Languages",ContainerName=Defaults.Containers.Site)]
		public virtual string LanguageCode { get; set; }

		public string LanguageTitle
		{
			get
			{
			    return string.IsNullOrEmpty(LanguageCode) ? "" : new CultureInfo(LanguageCode).DisplayName;
			}
		}

		#endregion
		
        [EditableFreeTextArea(Title="Footer Text",ContainerName=Defaults.Containers.Site)]
        [DisplayableTokens]
		public virtual string FooterText { get; set; }

        [EditableImage(Title="Logo", ContainerName = Defaults.Containers.Site)]
		public virtual string Logotype { get; set; }

		#region ISitesSource Members

        [EditableText(Title = "Site collection host name (DNS)",
            ContainerName = Defaults.Containers.Site,
            HelpTitle = "Sets a shared host name for all languages on a site. The web server must be configured to accept this host name for this to work.")]
		public virtual string HostName { get; set; }

		public IEnumerable<Site> GetSites()
		{
			if (!string.IsNullOrEmpty(HostName))
				yield return new Site(Find.EnumerateParents(this, null, true).Last().ID, ID, HostName) { Wildcards = true };
		}

		#endregion

        public override string ViewTemplate
        {
            get { return "ContentPage"; }
        }

        public IDictionary<string, System.Web.UI.Control> AddDefinedEditors(ItemDefinition definition, System.Web.UI.Control container, System.Security.Principal.IPrincipal user, System.Type containerTypeFilter, IEnumerable<string> editableNameFilter)
        {
            if (containerTypeFilter != null && containerTypeFilter == typeof(RecursiveContainerAttribute))
            {
                // we are rendering the "Site" page
                // the language intersection and startpage both define "host name" depending on how the user structures the site. 
                // We want to ensure startpage only uses it when it is NOT defined under the language intersection.
                // Most root item (start/intersection) wins for both ISiteSource and the host name editor
                if (Parent is LanguageIntersection) 
                {
                    // clone the definition and remove "host name" from it
                    var clonedDefinition = definition.Clone();
                    var hostName = clonedDefinition.Editables.SingleOrDefault(x => x.Name == "HostName");
                    if (hostName != null)
                    {
                        clonedDefinition.Remove(hostName);
                    }
                    return new EditableAdapter().AddDefinedEditors(clonedDefinition, this, container, user, containerTypeFilter, editableNameFilter);
                }
            }
            return null;
        }
    }
}