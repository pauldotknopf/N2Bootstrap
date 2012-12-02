using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using N2.Definitions;
using N2.Details;
using N2.Engine;
using N2.Plugin;
using N2Bootstrap.Library.Models;

namespace N2Bootstrap.Library.Services
{
    [Service]
    public class ContainerWrappableDefinitionAppender : IAutoStart
    {
        private readonly IDefinitionManager _definitionManager;

        public ContainerWrappableDefinitionAppender(IDefinitionManager definitionManager)
        {
            _definitionManager = definitionManager;
        }

        public void Start()
        {
            foreach (ItemDefinition definition in _definitionManager.GetDefinitions())
            {
                if (typeof(ModelBase).IsAssignableFrom(definition.ItemType) && !definition.IsPage)
                {
                    var ecb = new EditableCheckBoxAttribute
                    {
                        Title = "",
                        CheckBoxText = "Wrap with container",
                        Name = "UseContainer",
                        DefaultValue = true,
                        ContainerName = Defaults.Containers.Metadata
                    };
                    definition.Add(ecb);
                }
            }
        }

        public void Stop()
        {
        }
    }
}
