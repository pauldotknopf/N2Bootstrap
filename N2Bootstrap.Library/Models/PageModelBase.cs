using N2;
using N2.Details;
using N2.Persistence;
using N2.Web.UI;
using N2.Integrity;
using N2.Definitions;

namespace N2Bootstrap.Library.Models
{
    /// <summary>
    /// Base implementation for pages.
    /// </summary>
    [WithEditableTitle(ContainerName = Defaults.Containers.Content, SortOrder = 100)]
    [WithEditableName(ContainerName = Defaults.Containers.Metadata, SortOrder = 100)]
    [WithEditableVisibility(ContainerName = Defaults.Containers.Metadata, SortOrder = 101)]
    [SidebarContainer(Defaults.Containers.Layout, 200, HeadingText = "Layout")]
    [TabContainer(Defaults.Containers.Content, "Content", 1000)]
    [RestrictParents(typeof(IPage))]
    [NotVersionable]
    public abstract class PageModelBase : ModelBase, IPage
    {

    }
}