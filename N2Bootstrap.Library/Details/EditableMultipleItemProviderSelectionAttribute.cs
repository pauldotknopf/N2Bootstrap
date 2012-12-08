using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using N2;
using N2.Details;
using System.Linq;
using N2.Web.UI.WebControls;

namespace N2Bootstrap.Library.Details
{
    public class EditableMultipleItemProviderSelectionAttribute : N2.Details.EditableMultipleItemSelectionAttribute
    {
        private readonly Type _provider;
        private ContentItem _currentItem = null;

        public EditableMultipleItemProviderSelectionAttribute(Type provider)
            : base()
        {
            if (provider == null)
                throw new ArgumentNullException("You must specify a provider that implements IItemProvider");

            if (!typeof(IItemProvider).IsAssignableFrom(provider))
                throw new InvalidOperationException("The provider must implement IItemProvider");

            _provider = provider;
        }

        public IItemProvider ItemProvider
        {
            get { return Activator.CreateInstance(_provider) as IItemProvider; }
        }

        protected virtual ListItem[] GetContentItems(ContentItem current)
        {
            return ItemProvider.GetContentItems(current, LinkedType, ExcludedType, SearchTreshold, Include)
                               .Select(x => new ListItem(x.Title, x.ID.ToString()))
                               .ToArray();
        }

        public override void UpdateEditor(ContentItem item, System.Web.UI.Control editor)
        {
            var multiSelect = editor as MultiSelect;
            multiSelect.Items.Clear();
            multiSelect.Items.AddRange(GetContentItems(item));
            base.Configure(multiSelect);
            base.UpdateEditor(item, editor);
        }

        public interface IItemProvider
        {
            List<ContentItem> GetContentItems(ContentItem curent, Type linkedType, Type excludedType, int searchThreshold, EditableItemSelectionFilter filtler);
        }

        // static helpers

        public static List<T> GetStoredItems<T>(string detailName, ContentItem item) where T : ContentItem
        {
            DetailCollection detailCollection = item.GetDetailCollection(detailName, false);
            if (detailCollection == null)
            {
                return new List<T>();
            }
            return new List<T>(from d in detailCollection.Details
                               where d.LinkedItem != null
                               select d.LinkedItem as T);
        }

        public static void ReplaceStoredValue<T>(string detailName, ContentItem item, IEnumerable<T> linksToReplace)
        {
            item.GetDetailCollection(detailName, true).Replace(linksToReplace);
        }
    }
}