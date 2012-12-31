using System.Web.UI.WebControls;
using N2;
using N2.Details;
using N2.Integrity;
using N2.Persistence;
using N2.Definitions;

namespace N2Bootstrap.Library.Models
{
    /// <summary>
    /// Content page model used in several places:
    ///  * It serves as base class for start page
    ///  * It's the base for "template first" definitions located in /dinamico/default/views/contentpages/
    /// </summary>
    [PageDefinition("Content Page")]
    [RestrictParents(typeof(IStructuralPage))]
    [NotVersionable]
    public class ContentPage : PageModelBase, IContentPage, IStructuralPage
    {
        #region Content

        /// <summary>
        /// Summary text displayed in listings.
        /// </summary>
        [EditableSummary(Title = "Summary", SortOrder = 200, Source = "Text", ContainerName = Defaults.Containers.Content)]
        [Persistable(Length = 1024)] // to minimize select+1
        public virtual string Summary { get; set; }

        /// <summary>
        /// Main content of this content item.
        /// </summary>
        [EditableFreeTextArea(SortOrder = 201, ContainerName = Defaults.Containers.Content)]
        [DisplayableTokens]
        public virtual string Text { get; set; }

        /// <summary>
        /// Image used on the page and on listings.
        /// </summary>
        [EditableMediaUpload(PreferredSize = "wide", SortOrder = 202, ContainerName = Defaults.Containers.Content)]
        [Persistable(Length = 256)] // to minimize select+1
        public virtual string Image { get; set; }

        #endregion

        #region Metadata

        /// <summary>
        /// Title that replaces the regular title when not empty.
        /// </summary>
        [EditableText(Title = "SEO Title", SortOrder = 200, ContainerName = Defaults.Containers.Metadata)]
        public virtual string SeoTitle { get; set; }

        /// <summary>
        /// The meta tag "author"
        /// </summary>
        [EditableMetaTag(Title = "Author", SortOrder = 201, ContainerName = Defaults.Containers.Metadata)]
        public virtual string MetaAuthor { get; set; }

        /// <summary>
        /// The meta tag "keywords"
        /// </summary>
        [EditableMetaTag(Title = "Keywords", SortOrder = 202, ContainerName = Defaults.Containers.Metadata)]
        public virtual string MetaKeywords { get; set; }

        /// <summary>
        /// The meta tag "description"
        /// </summary>
        [EditableMetaTag(Title = "Description", SortOrder = 203, ContainerName = Defaults.Containers.Metadata)]
        public virtual string MetaDescription { get; set; }

        #endregion

        #region Layout

        /// <summary>
        /// The column layout
        /// </summary>
        [EditableEnum(Title = "Columns", EnumType = typeof(Defaults.Columns), SortOrder = 200, DefaultValue = Defaults.Columns.TwoColumn, ContainerName = Defaults.Containers.Layout)]
        public virtual Defaults.Columns Columns { get; set; }

        /// <summary>
        /// Show the breadcrumbs?
        /// </summary>
        [EditableCheckBox(CheckBoxText = "Show breadcrumb", Title = "", SortOrder = 201, ContainerName = Defaults.Containers.Layout, DefaultValue = true)]
        public virtual bool ShowBreadcrumb { get; set; }

        /// <summary>
        /// Show the page title?
        /// </summary>
        [EditableCheckBox(CheckBoxText = "Show page title", Title = "", SortOrder = 202, ContainerName = Defaults.Containers.Layout, DefaultValue = true)]
        public virtual bool ShowPageTitle { get; set; }

        #endregion
    }
}