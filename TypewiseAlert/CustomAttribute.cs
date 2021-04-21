using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    [AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Struct,
                       AllowMultiple = true)]  // multiuse attribute 
    public class CustomAttribute : System.Attribute
    {
        public string Name { get; set; }
        public int value;
        public CustomAttribute(string name)
        {
            this.Name = name;
            value = 0;
        }
    }
}
