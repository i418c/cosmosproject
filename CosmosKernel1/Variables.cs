using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    class Variables
    {
        System.Collections.ArrayList variables;

        public Variables()
        {
            variables = new System.Collections.ArrayList();
        }

        public void setVar(string name, int value)
        {
            for (int i = 0; i < variables.Count; i++)
            {
                Variable variable = (Variable) variables[i];
                if (variable.getName() == name)
                {
                    variable.setValue(value);
                    return;
                }
            }
            variables.Add(new Variable(name, value));
        }

        public int getVar(string name)
        {
            for (int i = 0; i < variables.Count; i++)
            {
                Variable variable = (Variable) variables[i];
                if(variable.getName() == name)
                {
                    return variable.getValue();
                }
            }
            throw new Exception("No variable with name " + name);
        }
    }
}
