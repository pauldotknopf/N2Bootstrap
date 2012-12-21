using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public string LessVariableName
        {
            get { return string.IsNullOrEmpty(_lessVariableName) ? Name : _lessVariableName; }
            set { _lessVariableName = value; }
        }
    }
}
