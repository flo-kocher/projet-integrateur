using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Diagnostics;
=======
using System.IO;
using System.Linq;
>>>>>>> origin/alpha_merge
using Mono.CecilX;

namespace Mirror.Weaver
{
<<<<<<< HEAD
    // not static, because ILPostProcessor is multithreaded
    internal class Weaver
    {
        public const string InvokeRpcPrefix = "InvokeUserCode_";
=======
    // This data is flushed each time - if we are run multiple times in the same process/domain
    class WeaverLists
    {
        // setter functions that replace [SyncVar] member variable references. dict<field, replacement>
        public Dictionary<FieldDefinition, MethodDefinition> replacementSetterProperties = new Dictionary<FieldDefinition, MethodDefinition>();
        // getter functions that replace [SyncVar] member variable references. dict<field, replacement>
        public Dictionary<FieldDefinition, MethodDefinition> replacementGetterProperties = new Dictionary<FieldDefinition, MethodDefinition>();

        // amount of SyncVars per class. dict<className, amount>
        public Dictionary<string, int> numSyncVars = new Dictionary<string, int>();

        public int GetSyncVarStart(string className)
        {
            return numSyncVars.ContainsKey(className)
                   ? numSyncVars[className]
                   : 0;
        }

        public void SetNumSyncVars(string className, int num)
        {
            numSyncVars[className] = num;
        }
    }

    internal static class Weaver
    {
        public static string InvokeRpcPrefix => "InvokeUserCode_";
>>>>>>> origin/alpha_merge

        // generated code class
        public const string GeneratedCodeNamespace = "Mirror";
        public const string GeneratedCodeClassName = "GeneratedNetworkCode";
<<<<<<< HEAD
        TypeDefinition GeneratedCodeClass;

        // for resolving Mirror.dll in ReaderWriterProcessor, we need to know
        // Mirror.dll name
        public const string MirrorAssemblyName = "Mirror";

        WeaverTypes weaverTypes;
        SyncVarAccessLists syncVarAccessLists;
        AssemblyDefinition CurrentAssembly;
        Writers writers;
        Readers readers;
        bool WeavingFailed;

        // logger functions can be set from the outside.
        // for example, Debug.Log or ILPostProcessor Diagnostics log for
        // multi threaded logging.
        public Logger Log;

        // remote actions now support overloads,
        // -> but IL2CPP doesnt like it when two generated methods
        // -> have the same signature,
        // -> so, append the signature to the generated method name,
        // -> to create a unique name
        // Example:
        // RpcTeleport(Vector3 position) -> InvokeUserCode_RpcTeleport__Vector3()
        // RpcTeleport(Vector3 position, Quaternion rotation) -> InvokeUserCode_RpcTeleport__Vector3Quaternion()
        // fixes https://github.com/vis2k/Mirror/issues/3060
        public static string GenerateMethodName(string initialPrefix, MethodDefinition md)
        {
            initialPrefix += md.Name;

            for (int i = 0; i < md.Parameters.Count; ++i)
            {
                // with __ so it's more obvious that this is the parameter suffix.
                // otherwise RpcTest(int) => RpcTestInt(int) which is not obvious.
                initialPrefix += $"__{md.Parameters[i].ParameterType.Name}";
            }

            return initialPrefix;
        }

        public Weaver(Logger Log)
        {
            this.Log = Log;
        }

        // returns 'true' if modified (=if we did anything)
        bool WeaveNetworkBehavior(TypeDefinition td)
=======
        public static TypeDefinition GeneratedCodeClass;

        public static WeaverLists WeaveLists { get; private set; }
        public static AssemblyDefinition CurrentAssembly { get; private set; }
        public static bool WeavingFailed { get; private set; }
        public static bool GenerateLogErrors;

        // private properties
        static readonly bool DebugLogEnabled = true;

        public static void DLog(TypeDefinition td, string fmt, params object[] args)
        {
            if (!DebugLogEnabled)
                return;

            Console.WriteLine("[" + td.Name + "] " + string.Format(fmt, args));
        }

        // display weaver error
        // and mark process as failed
        public static void Error(string message)
        {
            Log.Error(message);
            WeavingFailed = true;
        }

        public static void Error(string message, MemberReference mr)
        {
            Log.Error($"{message} (at {mr})");
            WeavingFailed = true;
        }

        public static void Warning(string message, MemberReference mr)
        {
            Log.Warning($"{message} (at {mr})");
        }


        static void CheckMonoBehaviour(TypeDefinition td)
        {
            if (td.IsDerivedFrom<UnityEngine.MonoBehaviour>())
            {
                MonoBehaviourProcessor.Process(td);
            }
        }

        static bool WeaveNetworkBehavior(TypeDefinition td)
>>>>>>> origin/alpha_merge
        {
            if (!td.IsClass)
                return false;

            if (!td.IsDerivedFrom<NetworkBehaviour>())
            {
<<<<<<< HEAD
                if (td.IsDerivedFrom<UnityEngine.MonoBehaviour>())
                    MonoBehaviourProcessor.Process(Log, td, ref WeavingFailed);
=======
                CheckMonoBehaviour(td);
>>>>>>> origin/alpha_merge
                return false;
            }

            // process this and base classes from parent to child order

            List<TypeDefinition> behaviourClasses = new List<TypeDefinition>();

            TypeDefinition parent = td;
            while (parent != null)
            {
                if (parent.Is<NetworkBehaviour>())
                {
                    break;
                }

                try
                {
                    behaviourClasses.Insert(0, parent);
                    parent = parent.BaseType.Resolve();
                }
                catch (AssemblyResolutionException)
                {
                    // this can happen for plugins.
                    //Console.WriteLine("AssemblyResolutionException: "+ ex.ToString());
                    break;
                }
            }

            bool modified = false;
            foreach (TypeDefinition behaviour in behaviourClasses)
            {
<<<<<<< HEAD
                modified |= new NetworkBehaviourProcessor(CurrentAssembly, weaverTypes, syncVarAccessLists, writers, readers, Log, behaviour).Process(ref WeavingFailed);
=======
                modified |= new NetworkBehaviourProcessor(behaviour).Process();
>>>>>>> origin/alpha_merge
            }
            return modified;
        }

<<<<<<< HEAD
        bool WeaveModule(ModuleDefinition moduleDefinition)
        {
            bool modified = false;

            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();

            foreach (TypeDefinition td in moduleDefinition.Types)
            {
                if (td.IsClass && td.BaseType.CanBeResolved())
                {
                    modified |= WeaveNetworkBehavior(td);
                    modified |= ServerClientAttributeProcessor.Process(weaverTypes, Log, td, ref WeavingFailed);
                }
            }

            watch.Stop();
            Console.WriteLine($"Weave behaviours and messages took {watch.ElapsedMilliseconds} milliseconds");

            return modified;
        }

        void CreateGeneratedCodeClass()
        {
            // create "Mirror.GeneratedNetworkCode" class which holds all
            // Readers<T> and Writers<T>
            GeneratedCodeClass = new TypeDefinition(GeneratedCodeNamespace, GeneratedCodeClassName,
                TypeAttributes.BeforeFieldInit | TypeAttributes.Class | TypeAttributes.AnsiClass | TypeAttributes.Public | TypeAttributes.AutoClass | TypeAttributes.Abstract | TypeAttributes.Sealed,
                weaverTypes.Import<object>());
        }

        // Weave takes an AssemblyDefinition to be compatible with both old and
        // new weavers:
        // * old takes a filepath, new takes a in-memory byte[]
        // * old uses DefaultAssemblyResolver with added dependencies paths,
        //   new uses ...?
        //
        // => assembly: the one we are currently weaving (MyGame.dll)
        // => resolver: useful in case we need to resolve any of the assembly's
        //              assembly.MainModule.AssemblyReferences.
        //              -> we can resolve ANY of them given that the resolver
        //                 works properly (need custom one for ILPostProcessor)
        //              -> IMPORTANT: .Resolve() takes an AssemblyNameReference.
        //                 those from assembly.MainModule.AssemblyReferences are
        //                 guaranteed to be resolve-able.
        //                 Parsing from a string for Library/.../Mirror.dll
        //                 would not be guaranteed to be resolve-able because
        //                 for ILPostProcessor we can't assume where Mirror.dll
        //                 is etc.
        public bool Weave(AssemblyDefinition assembly, IAssemblyResolver resolver, out bool modified)
        {
            WeavingFailed = false;
            modified = false;
            try
            {
                CurrentAssembly = assembly;
=======
        static bool WeaveModule(ModuleDefinition moduleDefinition)
        {
            try
            {
                bool modified = false;

                System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

                watch.Start();
                foreach (TypeDefinition td in moduleDefinition.Types)
                {
                    if (td.IsClass && td.BaseType.CanBeResolved())
                    {
                        modified |= WeaveNetworkBehavior(td);
                        modified |= ServerClientAttributeProcessor.Process(td);
                    }
                }
                watch.Stop();
                Console.WriteLine("Weave behaviours and messages took" + watch.ElapsedMilliseconds + " milliseconds");

                return modified;
            }
            catch (Exception ex)
            {
                Error(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }

        static void CreateGeneratedCodeClass()
        {
            // create "Mirror.GeneratedNetworkCode" class
            GeneratedCodeClass = new TypeDefinition(GeneratedCodeNamespace, GeneratedCodeClassName,
                TypeAttributes.BeforeFieldInit | TypeAttributes.Class | TypeAttributes.AnsiClass | TypeAttributes.Public | TypeAttributes.AutoClass | TypeAttributes.Abstract | TypeAttributes.Sealed,
                WeaverTypes.Import<object>());
        }

        static bool ContainsGeneratedCodeClass(ModuleDefinition module)
        {
            return module.GetTypes().Any(td => td.Namespace == GeneratedCodeNamespace &&
                                               td.Name == GeneratedCodeClassName);
        }

        static bool Weave(string assName, IEnumerable<string> dependencies)
        {
            using (DefaultAssemblyResolver asmResolver = new DefaultAssemblyResolver())
            using (CurrentAssembly = AssemblyDefinition.ReadAssembly(assName, new ReaderParameters { ReadWrite = true, ReadSymbols = true, AssemblyResolver = asmResolver }))
            {
                asmResolver.AddSearchDirectory(Path.GetDirectoryName(assName));
                asmResolver.AddSearchDirectory(Helpers.UnityEngineDllDirectoryName());
                if (dependencies != null)
                {
                    foreach (string path in dependencies)
                    {
                        asmResolver.AddSearchDirectory(path);
                    }
                }
>>>>>>> origin/alpha_merge

                // fix "No writer found for ..." error
                // https://github.com/vis2k/Mirror/issues/2579
                // -> when restarting Unity, weaver would try to weave a DLL
                //    again
                // -> resulting in two GeneratedNetworkCode classes (see ILSpy)
                // -> the second one wouldn't have all the writer types setup
<<<<<<< HEAD
                if (CurrentAssembly.MainModule.ContainsClass(GeneratedCodeNamespace, GeneratedCodeClassName))
=======
                if (ContainsGeneratedCodeClass(CurrentAssembly.MainModule))
>>>>>>> origin/alpha_merge
                {
                    //Log.Warning($"Weaver: skipping {CurrentAssembly.Name} because already weaved");
                    return true;
                }

<<<<<<< HEAD
                weaverTypes = new WeaverTypes(CurrentAssembly, Log, ref WeavingFailed);

                // weaverTypes are needed for CreateGeneratedCodeClass
                CreateGeneratedCodeClass();

                // WeaverList depends on WeaverTypes setup because it uses Import
                syncVarAccessLists = new SyncVarAccessLists();

                // initialize readers & writers with this assembly.
                // we need to do this in every Process() call.
                // otherwise we would get
                // "System.ArgumentException: Member ... is declared in another module and needs to be imported"
                // errors when still using the previous module's reader/writer funcs.
                writers = new Writers(CurrentAssembly, weaverTypes, GeneratedCodeClass, Log);
                readers = new Readers(CurrentAssembly, weaverTypes, GeneratedCodeClass, Log);

                Stopwatch rwstopwatch = Stopwatch.StartNew();
                // Need to track modified from ReaderWriterProcessor too because it could find custom read/write functions or create functions for NetworkMessages
                modified = ReaderWriterProcessor.Process(CurrentAssembly, resolver, Log, writers, readers, ref WeavingFailed);
=======
                WeaverTypes.SetupTargetTypes(CurrentAssembly);

                CreateGeneratedCodeClass();

                // WeaverList depends on WeaverTypes setup because it uses Import
                WeaveLists = new WeaverLists();

                System.Diagnostics.Stopwatch rwstopwatch = System.Diagnostics.Stopwatch.StartNew();
                // Need to track modified from ReaderWriterProcessor too because it could find custom read/write functions or create functions for NetworkMessages
                bool modified = ReaderWriterProcessor.Process(CurrentAssembly);
>>>>>>> origin/alpha_merge
                rwstopwatch.Stop();
                Console.WriteLine($"Find all reader and writers took {rwstopwatch.ElapsedMilliseconds} milliseconds");

                ModuleDefinition moduleDefinition = CurrentAssembly.MainModule;
                Console.WriteLine($"Script Module: {moduleDefinition.Name}");

                modified |= WeaveModule(moduleDefinition);

                if (WeavingFailed)
                {
                    return false;
                }

                if (modified)
                {
<<<<<<< HEAD
                    SyncVarAttributeAccessReplacer.Process(moduleDefinition, syncVarAccessLists);
=======
                    PropertySiteProcessor.Process(moduleDefinition);
>>>>>>> origin/alpha_merge

                    // add class that holds read/write functions
                    moduleDefinition.Types.Add(GeneratedCodeClass);

<<<<<<< HEAD
                    ReaderWriterProcessor.InitializeReaderAndWriters(CurrentAssembly, weaverTypes, writers, readers, GeneratedCodeClass);

                    // DO NOT WRITE here.
                    // CompilationFinishedHook writes to the file.
                    // ILPostProcessor writes to in-memory assembly.
                    // it depends on the caller.
                    //CurrentAssembly.Write(new WriterParameters{ WriteSymbols = true });
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Error($"Exception :{e}");
                WeavingFailed = true;
                return false;
            }
        }
=======
                    ReaderWriterProcessor.InitializeReaderAndWriters(CurrentAssembly);

                    // write to outputDir if specified, otherwise perform in-place write
                    WriterParameters writeParams = new WriterParameters { WriteSymbols = true };
                    CurrentAssembly.Write(writeParams);
                }
            }

            return true;
        }

        public static bool WeaveAssembly(string assembly, IEnumerable<string> dependencies)
        {
            WeavingFailed = false;

            try
            {
                return Weave(assembly, dependencies);
            }
            catch (Exception e)
            {
                Log.Error("Exception :" + e);
                return false;
            }
        }

>>>>>>> origin/alpha_merge
    }
}
