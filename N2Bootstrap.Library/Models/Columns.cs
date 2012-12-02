using System.Collections.Generic;
using N2;
using N2.Details;
using N2.Integrity;

namespace N2Bootstrap.Library.Models
{
    [PartDefinition("Columns", IconUrl="{IconsUrl}/text_columns.png")]
    [RestrictChildren(typeof(Column))]
    public class Columns : PartModelBase
    {
        public override string TemplateKey
        {
            get { return "Columns"; }
        }

        [EditableChildren("Columns", "Columns", 1)]
        public List<Columns.Column> ColumnsList { get; set; }

        [PartDefinition("Column")]
        [RestrictParents(typeof(Columns))]
        [AllowedZones("Columns")]
        public class Column : ContentItem
        {
            [EditableText(Title = "Number of columns", DefaultValue = 3, Required = true, Validate=true, ValidationExpression = "^[1-9][0-2]*", ValidationMessage="Please specify a valid number of columns (1-12).")]
            public virtual int NumberOfColumns { get; set; }
        }
    }
}