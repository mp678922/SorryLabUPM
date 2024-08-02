using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
namespace SorryLab.Editor {
    public class AssetBundleBuilderHandler {
        public virtual void Handle(GameObject gameObject) { }
        static public List<Type> GetDerivedTypes() {
            System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> derivedTypes = new List<Type>();
            foreach (System.Reflection.Assembly assembly in assemblies) {
                derivedTypes.AddRange(assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(AssetBundleBuilderHandler))));
            }
            return derivedTypes;
        }
    }
}