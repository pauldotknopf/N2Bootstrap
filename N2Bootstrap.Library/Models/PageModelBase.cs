using BootstrapBlog.Blog;
using N2;
using N2.Details;
using N2.Web.UI;
using N2.Integrity;
using N2.Definitions;

namespace N2Bootstrap.Library.Models
{
	/// <summary>
	/// Base implementation for pages.
	/// </summary>
	[WithEditableTitle]
	[WithEditableName(ContainerName = Defaults.Containers.Metadata)]
	[WithEditableVisibility(ContainerName = Defaults.Containers.Metadata)]
    [SidebarContainer(Defaults.Containers.Layout, 200, HeadingText="Layout")]
	[TabContainer(Defaults.Containers.Content, "Content", 1000)]
	[RestrictParents(typeof(IPage))]
	public abstract class PageModelBase : ModelBase, IPage
	{
        
	}
}