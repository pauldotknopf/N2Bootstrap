using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2;
using N2.Details;

namespace N2Bootstrap.Library.Models
{
    [Definition]
    public class BootstrapThemeConfiguration : ContentItem
    {
        [Details.EditableLessVariable(LessVariableName = "bodyBackground", DefaultValue = "@white")]
        public virtual string BodyBackgroundColor { get; set; }

        [Details.EditableLessVariable(LessVariableName = "gridColumnWidth", DefaultValue = "60px")]
        public virtual string GridColumnWidth { get; set; }

        #region Helpers

        public static BootstrapThemeConfiguration GetOrCreateThemeConfiguration(string theme = null)
        {
            if (string.IsNullOrEmpty(theme))
                theme = "Default";

            var themeItem = N2.Find.Items
                .Where
                        .Type.Eq(typeof(BootstrapThemeConfiguration))
                    .And
                        .Name.Eq(theme.ToLower())
                .Select<BootstrapThemeConfiguration>()
                .FirstOrDefault();

            if (themeItem == null)
            {
                themeItem = new BootstrapThemeConfiguration();
                themeItem.Name = theme.ToLower();
                Context.Persister.Save(themeItem);
            }

            return themeItem;
        }

        #endregion
    }
}
