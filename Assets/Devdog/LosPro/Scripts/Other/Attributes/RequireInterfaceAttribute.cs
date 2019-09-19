using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.LosPro
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class RequireInterfaceAttribute : Attribute
    {
        public Type type { get; private set; }

        public RequireInterfaceAttribute(Type type)
        {
            this.type = type;
            Assert.IsTrue(this.type.IsInterface, "Given type is not an interface (" + type.FullName + ")");
        }
    }
}
