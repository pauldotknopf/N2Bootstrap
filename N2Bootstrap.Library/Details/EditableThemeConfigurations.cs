using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web.UI;
using N2.Edit.Workflow;
using N2.Web;
using N2.Web.UI;
using N2.Web.UI.WebControls;

namespace N2Bootstrap.Library.Details
{
    public class EditableThemeConfigurations : N2.Details.AbstractEditableAttribute, N2.Definitions.IDefinitionRefiner
    {
        protected override System.Web.UI.Control AddEditor(System.Web.UI.Control container)
        {
            Control panel = AddPanel(container);
            foreach (var themeDirectory in GetThemeDirectories())
            {
                ItemEditor editor = new ItemEditor();
                editor.ID = Name;
                editor.Init += OnChildEditorInit;
                panel.Controls.Add(editor);
            }
            return panel;
        }

        public override void UpdateEditor(N2.ContentItem item, System.Web.UI.Control editor)
        {
        }

        public override bool UpdateItem(N2.ContentItem item, System.Web.UI.Control editor)
        {
            return true;
        }

        private IEnumerable<string> GetThemeDirectories()
        {
            string path = HostingEnvironment.MapPath(Url.ResolveTokens(Url.ThemesUrlToken));
            if (Directory.Exists(path))
            {
                foreach (string directoryPath in Directory.GetDirectories(path))
                {
                    string directoryName = Path.GetFileName(directoryPath);
                    if (!directoryName.StartsWith("."))
                        yield return directoryPath;
                }
            }
        }

        protected virtual void OnChildEditorInit(object sender, EventArgs e)
        {
            var itemEditor = sender as ItemEditor;
            var parentEditor = ItemUtility.FindInParents<IItemEditor>(itemEditor.Parent);
            itemEditor.CurrentItem = parentEditor.CurrentItem;
        }

        public void Refine(N2.Definitions.ItemDefinition currentDefinition, IList<N2.Definitions.ItemDefinition> allDefinitions)
        {
            currentDefinition.Editables.Add(this);
        }

        public int RefinementOrder
        {
            get { return int.MaxValue; }
        }

        public int CompareTo(N2.Definitions.ISortableRefiner other)
        {
            return RefinementOrder.CompareTo(other.RefinementOrder);
        }
    }
}
