using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotless.Core.Plugins;

namespace N2Bootstrap.Library.Less
{
    public class VariableOverridePluginConfigurator : IPluginConfigurator
    {
        private readonly Dictionary<string, string> _variables;

        public VariableOverridePluginConfigurator(Dictionary<string, string> variables)
        {
            if (variables == null)
                variables = new Dictionary<string, string>();
            _variables = variables;
        }

        public Type Configurates
        {
            get { return typeof(VariableOverridePlugin); }
        }

        public IPlugin CreatePlugin()
        {
            return new VariableOverridePlugin(_variables);
        }

        public string Description
        {
            get { return "Overrides variable values."; }
        }

        public IEnumerable<IPluginParameter> GetParameters()
        {
            return Enumerable.Empty<IPluginParameter>();
        }

        public string Name
        {
            get { return "VariableOverridePlugin"; }
        }

        public void SetParameterValues(IEnumerable<IPluginParameter> parameters)
        {
        }
    }
}
