using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotless.Core.Parser;
using dotless.Core.Parser.Tree;
using dotless.Core.Plugins;

namespace N2Bootstrap.Library.Less
{
    public class VariableOverridePlugin : VisitorPlugin
    {
        private readonly Dictionary<string, string> _variables;

        public VariableOverridePlugin(Dictionary<string, string> variables)
        {
            _variables = variables;
        }

        public override VisitorPluginType AppliesTo
        {
            get { return VisitorPluginType.BeforeEvaluation; }
        }

        public override dotless.Core.Parser.Infrastructure.Nodes.Node Execute(dotless.Core.Parser.Infrastructure.Nodes.Node node, out bool visitDeeper)
        {
            visitDeeper = true;

            if (node is Rule)
            {
                var rule = node as Rule;
                if (rule.Variable)
                {
                    if (_variables.ContainsKey(rule.Name.TrimStart(Convert.ToChar("@"))))
                    {
                        var overrideValue = _variables[rule.Name.TrimStart(Convert.ToChar("@"))];
                        if (!string.IsNullOrEmpty(overrideValue))
                        {
                            var parse = new Parser();
                            var ruleset = parse.Parse(rule.Name + ":    " + overrideValue + ";", "variableoverrideplugin.less");
                            return ruleset.Rules[0] as Rule;
                        }
                    }
                }
            }

            return node;
        }
    }
}
