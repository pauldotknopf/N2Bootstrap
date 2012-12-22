using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using N2.Details;

namespace N2Bootstrap.Library.Details
{
    [AttributeUsage(AttributeTargets.Property, Inherited=true)]
    public class EditableLessVariableAttribute : EditableTextBoxAttribute
    {
        private string _lessVariableName;

        public EditableLessVariableAttribute()
        {
            
        }

        public EditableLessVariableAttribute(string title, int sortOrder)
            :base(title, sortOrder)
        {
            
        }

        public EditableLessVariableAttribute(string title, int sortOrder, int maxLength)
            :base(title, sortOrder, maxLength)
        {
            
        }
        protected override Label AddLabel(System.Web.UI.Control container)
        {
            var label = new Label();
            label.ID = "lbl" + Name;
            label.Text = LessVariableName + ":";
            label.CssClass = "editorLabel";
            label.Attributes["data-sortorder"] = SortOrder.ToString(CultureInfo.InvariantCulture);
            container.Controls.Add(label);
            return label;
        }
        public string LessVariableName
        {
            get { return string.IsNullOrEmpty(_lessVariableName) ? Name : _lessVariableName; }
            set { _lessVariableName = value; }
        }
    }
}
