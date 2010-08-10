using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphingWithShapes
{
    public class NameValuePair
    {
        public NameValuePair() { }

        public NameValuePair(string newName, double newValue)
        {
            Name = newName;
            Value = newValue;
        }

        public string Name { get; set; }

        public double Value { get; set; }

        public object Tag { get; set; }
    }
}
