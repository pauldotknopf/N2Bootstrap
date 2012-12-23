using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2;
using N2.Definitions;
using N2.Details;
using N2.Web.UI;

namespace N2Bootstrap.Library.Models
{
    [Definition]
    [FieldSetContainer("Scaffolding", "Scaffolding", 0)]
    [FieldSetContainer("Links", "Links", 0)]
    [Disable]
    public class BootstrapThemeConfiguration : ContentItem
    {
        #region Scaffolding

        [Details.EditableLessVariable(LessVariableName = "bodyBackground", Placeholder = "@white", ContainerName = "Scaffolding")]
        public virtual string BodyBackground { get; set; }

        [Details.EditableLessVariable(LessVariableName = "textColor", Placeholder = "@grayDark", ContainerName = "Scaffolding")]
        public virtual string TextColor { get; set; }

        #endregion

        #region Links

        [Details.EditableLessVariable(LessVariableName = "linkColor", Placeholder = "#08c", ContainerName = "Links")]
        public virtual string LinkColor { get; set; }

        [Details.EditableLessVariable(LessVariableName = "linkColorHover", Placeholder = "darken(@linkColor, 15%)", ContainerName = "Links")]
        public virtual string LinkColorHover { get; set; }

        #endregion
    }
}
