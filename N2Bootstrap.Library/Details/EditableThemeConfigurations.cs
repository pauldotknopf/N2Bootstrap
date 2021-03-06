﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Cassette;
using Cassette.Stylesheets;
using N2.Edit.Workflow;
using N2.Plugin;
using N2.Web;
using N2.Web.UI;
using N2.Web.UI.WebControls;
using N2Bootstrap.Library.Resources;

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
                var tab = tabContainer.AddTo(panel) as TabPanel;
                var editor = new ItemEditor { ID = themeName.ToLower() };
                editor.Init += OnChildEditorInit;
                tab.Controls.Add(editor);
                AddValidator(tab);
            }
            return panel;
        }

        private void AddValidator(TabPanel themeTabPanel)
        {
            var validator = new CustomValidator();
            validator.Display = ValidatorDisplay.None;
            validator.ServerValidate += (s, e) =>
            {
                var control = (s as CustomValidator);
                var themeEditor = control.Parent.Controls.Cast<Control>().Where(x => x is ItemEditor).Cast<ItemEditor>().First();
                var resourcePlugins = N2.Context.Current.Resolve<IPluginFinder>()
                                        .GetPlugins<BootstrapResourceAttribute>()
                                        .Where(x => x.ResourceType == BootstrapResourceAttribute.ResourceTypeEnum.CssOrLess)
                                        .ToList();
                themeEditor.UpdateObject(new CommandContext(themeEditor.Definition,
                            themeEditor.CurrentItem,
                            "theme-editor",
                            N2.Context.Current.RequestContext.User));
                var variables = Less.ThemedLessEngine.GetThemeVariables(themeEditor.CurrentItem as Models.BootstrapThemeConfiguration);
                foreach (var resource in resourcePlugins)
                {
                    var themedLocation = Less.ThemedLessEngine.GetThemedFile(Path.Combine(Url.ResolveTokens("{ThemesUrl}/Default/"), resource.Name), themeEditor.ID);
                    try
                    {
                        Less.ThemedLessEngine.CompileLess(themedLocation, null, themeEditor.ID, variables);
                    }
                    catch (Exception ex)
                    {
                        control.ErrorMessage = "Error with theme \"" + themeEditor.ID + "\":    " + ex.Message;
                        e.IsValid = false;
                        return;
                    }
                }
                e.IsValid = true;
            };
            themeTabPanel.Controls.Add(validator);
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
            var themeEditors = GetThemeEditors(editor);

            foreach (var themeEditor in themeEditors)
            {
                themeEditor.UpdateObject(new CommandContext(themeEditor.Definition,
                    themeEditor.CurrentItem,
                    "theme-editor",
                    N2.Context.Current.RequestContext.User));
            }

            global::Cassette.Aspnet.CassetteHttpModule.Host.RequestContainer.Resolve<IBundleCacheRebuilder>()
                  .RebuildCache();

            return true;
        }

        private IList<ItemEditor> GetThemeEditors(Control parentTabPanel)
        {
            return parentTabPanel.Controls.Cast<Control>()
                .Where(x => x is TabPanel)
                .SelectMany(x => x.Controls.Cast<Control>().Where(y => y is ItemEditor))
                .Cast<ItemEditor>()
                .ToList();
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

        private void ValidateLess()
        {
            
        } 

        protected virtual void OnChildEditorInit(object sender, EventArgs e)
        {
            var itemEditor = sender as ItemEditor;
            if (itemEditor != null)
                itemEditor.CurrentItem = Less.ThemedLessEngine.GetOrCreateThemeConfiguration(itemEditor.ID.ToLower());
        }
    }
}
