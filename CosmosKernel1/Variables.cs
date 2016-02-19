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
            foreach (Variable variable in variables)
            {
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
            foreach(Variable variable in variables)
            {
                if(variable.getName() == name)
                {
                    return variable.getValue();
                }
            }
            throw new Exception("No variable with name " + name);
        }
    }
}
