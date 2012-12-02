using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using N2;
using N2.Details;
using System.Linq;

namespace BootstrapBlog.Blog.Details
{
    public class EditableMultipleItemProviderSelectionAttribute : N2.Details.EditableMultipleItemSelectionAttribute
    {
        private readonly Type _provider;
        private ContentItem _currentItem = null;

        public EditableMultipleItemProviderSelectionAttribute(Type provider)
            :base()
        {
            if (provider == null)
                throw new ArgumentNullException("You must specify a provider that implements IItemProvider");

            if (!typeof (IItemProvider).IsAssignableFrom(provider))
                throw new InvalidOperationException("The provider must implement IItemProvider");

            _provider = provider;
        }

        public IItemProvider ItemProvider
        {
            get { return Activator.CreateInstance(_provider) as IItemProvider; }
        }

        protected override void Configure(N2.Web.UI.WebControls.MultiSelect ddl)
        {
            ddl.Items.Clear();
            ddl.Items.AddRange(GetContentItems());
            base.Configure(ddl);
        }

        protected override HashSet<int> GetStoredSelection(ContentItem item)
        {
            _currentItem = item;
            return base.GetStoredSelection(item);
        }

        protected virtual ListItem[] GetContentItems()
        {
            return ItemProvider.GetContentItems(_currentItem, LinkedType, ExcludedType, SearchTreshold, Include)
                               .Select(x => new ListItem(x.Title, x.ID.ToString()))
                               .ToArray();
        }

        public interface IItemProvider
        {
            List<ContentItem> GetContentItems(ContentItem curent, Type linkedType, Type excludedType, int searchThreshold, EditableItemSelectionFilter filtler);
        }
    }
}