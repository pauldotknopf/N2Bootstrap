using System.Web.UI.WebControls;
using BootstrapBlog.Blog;
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
	public class ContentPage : PageModelBase, IContentPage, IStructuralPage
	{
		/// <summary>
		/// Summary text displayed in listings.
		/// </summary>
		[EditableText(TextMode = TextBoxMode.MultiLine, Columns = 80, Rows = 2, ValidationExpression = ".*{0,1000", ValidationMessage = "Max 100 characters")]
		[Persistable(Length = 1024)] // to minimize select+1
		public virtual string Summary { get; set; }

		/// <summary>
		/// Main content of this content item.
		/// </summary>
		[EditableFreeTextArea]
		[DisplayableTokens]
		public virtual string Text { get; set; }

        /// <summary>
        /// Title that replaces the regular title when not empty.
        /// </summary>
        [EditableText(Title = "SEO Title", ContainerName = Defaults.Containers.Metadata)]
        public virtual string SeoTitle { get; set; }

        /// <summary>
        /// The meta tag "author"
        /// </summary>
        [EditableMetaTag(Title = "Author", ContainerName = Defaults.Containers.Metadata)]
        public virtual string MetaAuthor { get; set; }

        /// <summary>
        /// The meta tag "keywords"
        /// </summary>
        [EditableMetaTag(Title = "Keywords", ContainerName = Defaults.Containers.Metadata)]
        public virtual string MetaKeywords { get; set; }

        /// <summary>
        /// The meta tag "description"
        /// </summary>
        [EditableMetaTag(Title = "Description", ContainerName = Defaults.Containers.Metadata)]
        public virtual string MetaDescription { get; set; }

        /// <summary>
        /// The column layout
        /// </summary>
        [EditableEnum(Title="Columns", EnumType=typeof(Defaults.Columns), ContainerName=Defaults.Containers.Layout)]
        public virtual Defaults.Columns Columns { get; set; }

        /// <summary>
        /// Show the breadcrumbs?
        /// </summary>
        [EditableCheckBox(Title="Show breadcrumb", ContainerName=Defaults.Containers.Layout, DefaultValue=true)]
        public virtual bool ShowBreadcrumb { get; set; }
	}
}