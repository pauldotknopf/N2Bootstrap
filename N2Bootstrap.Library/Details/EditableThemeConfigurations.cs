using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.UI;
using N2.Edit.Workflow;
using N2.Web;
using N2.Web.UI;
using N2.Web.UI.WebControls;

namespace N2Bootstrap.Library.Details
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class EditableThemeConfigurations : N2.Details.AbstractEditableAttribute
    {
        public EditableThemeConfigurations()
        {
            Name = "EditableThemeConfigurations";
        }

        protected override Control AddEditor(Control container)
        {
            var panel = AddPanel(container);
            foreach (var themeDirectory in GetThemeDirectories())
            {
                var themeName = Path.GetFileName(themeDirectory);
                if (string.IsNullOrEmpty(themeName)) continue;
                var tabContainer = new TabContainerAttribute(themeName + "-tab", themeName, 0);
                var tab = tabContainer.AddTo(panel);
                var editor = new ItemEditor { ID = themeName.ToLower() };
                editor.Init += OnChildEditorInit;
                tab.Controls.Add(editor);
            }
            return panel;
        }

        public override Control AddTo(Control container)
        {
            var editor = AddEditor(container);
            return editor;
        }

        public override void UpdateEditor(N2.ContentItem item, Control editor)
        {

        }

        public override bool UpdateItem(N2.ContentItem item, Control editor)
        {
            var themeEditors = editor.Controls.Cast<Control>()
                .Where(x => x is TabPanel)
                .SelectMany(x => x.Controls.Cast<Control>().Where(y => y is ItemEditor))
                .Cast<ItemEditor>()
                .ToList();

            foreach (var themeEditor in themeEditors)
            {
                themeEditor.UpdateObject(new CommandContext(themeEditor.Definition,
                    themeEditor.CurrentItem,
                    "theme-editor",
                    N2.Context.Current.RequestContext.User));
            }

            return true;
        }

        private IEnumerable<string> GetThemeDirectories()
        {
            string path = HostingEnvironment.MapPath(Url.ResolveTokens(Url.ThemesUrlToken));
            if (Directory.Exists(path))
            {
                foreach (var directoryPath in Directory.GetDirectories(path))
                {
                    string directoryName = Path.GetFileName(directoryPath);
                    if (directoryName != null && !directoryName.StartsWith("."))
                        yield return directoryPath;
                }
            }
        }

        protected virtual void OnChildEditorInit(object sender, EventArgs e)
        {
            var itemEditor = sender as ItemEditor;
            if (itemEditor != null)
                itemEditor.CurrentItem = Models.BootstrapThemeConfiguration.GetOrCreateThemeConfiguration(itemEditor.ID.ToLower());
        }
    }
}
