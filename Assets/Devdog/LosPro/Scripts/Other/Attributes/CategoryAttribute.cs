using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.LosPro
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class CategoryAttribute : Attribute
    {
        public string category{ get; private set; }

        public CategoryAttribute(string category)
        {
            this.category = category;
        }

        public override string ToString()
        {
            return category;
        }
    }
}
