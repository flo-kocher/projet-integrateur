// all the resolve functions for the weaver
// NOTE: these functions should be made extensions, but right now they still
//       make heavy use of Weaver.fail and we'd have to check each one's return
//       value for null otherwise.
//       (original FieldType.Resolve returns null if not found too, so
//        exceptions would be a bit inconsistent here)
using Mono.CecilX;

namespace Mirror.Weaver
{
    public static class Resolvers
    {
<<<<<<< HEAD
        public static MethodReference ResolveMethod(TypeReference tr, AssemblyDefinition assembly, Logger Log, string name, ref bool WeavingFailed)
        {
            if (tr == null)
            {
                Log.Error($"Cannot resolve method {name} without a class");
                WeavingFailed = true;
                return null;
            }
            MethodReference method = ResolveMethod(tr, assembly, Log, m => m.Name == name, ref WeavingFailed);
            if (method == null)
            {
                Log.Error($"Method not found with name {name} in type {tr.Name}", tr);
                WeavingFailed = true;
=======
        public static MethodReference ResolveMethod(TypeReference tr, AssemblyDefinition scriptDef, string name)
        {
            if (tr == null)
            {
                Weaver.Error($"Cannot resolve method {name} without a class");
                return null;
            }
            MethodReference method = ResolveMethod(tr, scriptDef, m => m.Name == name);
            if (method == null)
            {
                Weaver.Error($"Method not found with name {name} in type {tr.Name}", tr);
>>>>>>> origin/alpha_merge
            }
            return method;
        }

<<<<<<< HEAD
        public static MethodReference ResolveMethod(TypeReference t, AssemblyDefinition assembly, Logger Log, System.Func<MethodDefinition, bool> predicate, ref bool WeavingFailed)
=======
        public static MethodReference ResolveMethod(TypeReference t, AssemblyDefinition scriptDef, System.Func<MethodDefinition, bool> predicate)
>>>>>>> origin/alpha_merge
        {
            foreach (MethodDefinition methodRef in t.Resolve().Methods)
            {
                if (predicate(methodRef))
                {
<<<<<<< HEAD
                    return assembly.MainModule.ImportReference(methodRef);
                }
            }

            Log.Error($"Method not found in type {t.Name}", t);
            WeavingFailed = true;
            return null;
        }

        public static MethodReference TryResolveMethodInParents(TypeReference tr, AssemblyDefinition assembly, string name)
=======
                    return scriptDef.MainModule.ImportReference(methodRef);
                }
            }

            Weaver.Error($"Method not found in type {t.Name}", t);
            return null;
        }

        public static MethodReference TryResolveMethodInParents(TypeReference tr, AssemblyDefinition scriptDef, string name)
>>>>>>> origin/alpha_merge
        {
            if (tr == null)
            {
                return null;
            }
<<<<<<< HEAD
            foreach (MethodDefinition methodDef in tr.Resolve().Methods)
            {
                if (methodDef.Name == name)
                {
                    MethodReference methodRef = methodDef;
                    if (tr.IsGenericInstance)
                    {
                        methodRef = methodRef.MakeHostInstanceGeneric(tr.Module, (GenericInstanceType)tr);
                    }
                    return assembly.MainModule.ImportReference(methodRef);
=======
            foreach (MethodDefinition methodRef in tr.Resolve().Methods)
            {
                if (methodRef.Name == name)
                {
                    return scriptDef.MainModule.ImportReference(methodRef);
>>>>>>> origin/alpha_merge
                }
            }

            // Could not find the method in this class,  try the parent
<<<<<<< HEAD
            return TryResolveMethodInParents(tr.Resolve().BaseType.ApplyGenericParameters(tr), assembly, name);
=======
            return TryResolveMethodInParents(tr.Resolve().BaseType, scriptDef, name);
>>>>>>> origin/alpha_merge
        }

        public static MethodDefinition ResolveDefaultPublicCtor(TypeReference variable)
        {
            foreach (MethodDefinition methodRef in variable.Resolve().Methods)
            {
                if (methodRef.Name == ".ctor" &&
                    methodRef.Resolve().IsPublic &&
                    methodRef.Parameters.Count == 0)
                {
                    return methodRef;
                }
            }
            return null;
        }

<<<<<<< HEAD
        public static MethodReference ResolveProperty(TypeReference tr, AssemblyDefinition assembly, string name)
=======
        public static MethodReference ResolveProperty(TypeReference tr, AssemblyDefinition scriptDef, string name)
>>>>>>> origin/alpha_merge
        {
            foreach (PropertyDefinition pd in tr.Resolve().Properties)
            {
                if (pd.Name == name)
                {
<<<<<<< HEAD
                    return assembly.MainModule.ImportReference(pd.GetMethod);
=======
                    return scriptDef.MainModule.ImportReference(pd.GetMethod);
>>>>>>> origin/alpha_merge
                }
            }
            return null;
        }
    }
}
