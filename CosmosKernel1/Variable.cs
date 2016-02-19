using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    class Variable
    {
        string name;
        int value;

        public Variable(string name, int value)
        {
            this.name = name;
            this.value = value;
        }

        public string getName()
        {
            return this.name;
        }
        
        public int getValue()
        {
            return this.value;
        }

        public void setValue(int value)
        {
            this.value = value;
        }
    }
}
