using System;
using System.Collections.Generic;
using Mono.CecilX;
using Mono.CecilX.Cil;
<<<<<<< HEAD
// to use Mono.CecilX.Rocks here, we need to 'override references' in the
// Unity.Mirror.CodeGen assembly definition file in the Editor, and add CecilX.Rocks.
// otherwise we get an unknown import exception.
=======
>>>>>>> origin/alpha_merge
using Mono.CecilX.Rocks;

namespace Mirror.Weaver
{
<<<<<<< HEAD
    // not static, because ILPostProcessor is multithreaded
    public class Readers
    {
        // Readers are only for this assembly.
        // can't be used from another assembly, otherwise we will get:
        // "System.ArgumentException: Member ... is declared in another module and needs to be imported"
        AssemblyDefinition assembly;
        WeaverTypes weaverTypes;
        TypeDefinition GeneratedCodeClass;
        Logger Log;

        Dictionary<TypeReference, MethodReference> readFuncs =
            new Dictionary<TypeReference, MethodReference>(new TypeReferenceComparer());

        public Readers(AssemblyDefinition assembly, WeaverTypes weaverTypes, TypeDefinition GeneratedCodeClass, Logger Log)
        {
            this.assembly = assembly;
            this.weaverTypes = weaverTypes;
            this.GeneratedCodeClass = GeneratedCodeClass;
            this.Log = Log;
        }

        internal void Register(TypeReference dataType, MethodReference methodReference)
        {
            if (readFuncs.ContainsKey(dataType))
            {
                // TODO enable this again later.
                // Reader has some obsolete functions that were renamed.
                // Don't want weaver warnings for all of them.
                //Log.Warning($"Registering a Read method for {dataType.FullName} when one already exists", methodReference);
            }

            // we need to import type when we Initialize Readers so import here in case it is used anywhere else
            TypeReference imported = assembly.MainModule.ImportReference(dataType);
            readFuncs[imported] = methodReference;
        }

        void RegisterReadFunc(TypeReference typeReference, MethodDefinition newReaderFunc)
        {
            Register(typeReference, newReaderFunc);
            GeneratedCodeClass.Methods.Add(newReaderFunc);
        }

        // Finds existing reader for type, if non exists trys to create one
        public MethodReference GetReadFunc(TypeReference variable, ref bool WeavingFailed)
        {
            if (readFuncs.TryGetValue(variable, out MethodReference foundFunc))
                return foundFunc;

            TypeReference importedVariable = assembly.MainModule.ImportReference(variable);
            return GenerateReader(importedVariable, ref WeavingFailed);
        }

        MethodReference GenerateReader(TypeReference variableReference, ref bool WeavingFailed)
=======
    public static class Readers
    {
        static Dictionary<TypeReference, MethodReference> readFuncs;

        public static void Init()
        {
            readFuncs = new Dictionary<TypeReference, MethodReference>(new TypeReferenceComparer());
        }

        internal static void Register(TypeReference dataType, MethodReference methodReference)
        {
            if (readFuncs.ContainsKey(dataType))
            {
                Weaver.Warning($"Registering a Read method for {dataType.FullName} when one already exists", methodReference);
            }

            // we need to import type when we Initialize Readers so import here in case it is used anywhere else
            TypeReference imported = Weaver.CurrentAssembly.MainModule.ImportReference(dataType);
            readFuncs[imported] = methodReference;
        }

        static void RegisterReadFunc(TypeReference typeReference, MethodDefinition newReaderFunc)
        {
            Register(typeReference, newReaderFunc);

            Weaver.GeneratedCodeClass.Methods.Add(newReaderFunc);
        }

        /// <summary>
        /// Finds existing reader for type, if non exists trys to create one
        /// <para>This method is recursive</para>
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>Returns <see cref="MethodReference"/> or null</returns>
        public static MethodReference GetReadFunc(TypeReference variable)
        {
            if (readFuncs.TryGetValue(variable, out MethodReference foundFunc))
            {
                return foundFunc;
            }
            else
            {
                TypeReference importedVariable = Weaver.CurrentAssembly.MainModule.ImportReference(variable);
                return GenerateReader(importedVariable);
            }
        }

        static MethodReference GenerateReader(TypeReference variableReference)
>>>>>>> origin/alpha_merge
        {
            // Arrays are special,  if we resolve them, we get the element type,
            // so the following ifs might choke on it for scriptable objects
            // or other objects that require a custom serializer
            // thus check if it is an array and skip all the checks.
            if (variableReference.IsArray)
            {
                if (variableReference.IsMultidimensionalArray())
                {
<<<<<<< HEAD
                    Log.Error($"{variableReference.Name} is an unsupported type. Multidimensional arrays are not supported", variableReference);
                    WeavingFailed = true;
                    return null;
                }

                return GenerateReadCollection(variableReference, variableReference.GetElementType(), nameof(NetworkReaderExtensions.ReadArray), ref WeavingFailed);
            }

            TypeDefinition variableDefinition = variableReference.Resolve();

            // check if the type is completely invalid
            if (variableDefinition == null)
            {
                Log.Error($"{variableReference.Name} is not a supported type", variableReference);
                WeavingFailed = true;
                return null;
            }
            else if (variableReference.IsByReference)
            {
                // error??
                Log.Error($"Cannot pass type {variableReference.Name} by reference", variableReference);
                WeavingFailed = true;
                return null;
            }

            // use existing func for known types
            if (variableDefinition.IsEnum)
            {
                return GenerateEnumReadFunc(variableReference, ref WeavingFailed);
            }
            else if (variableDefinition.Is(typeof(ArraySegment<>)))
            {
                return GenerateArraySegmentReadFunc(variableReference, ref WeavingFailed);
=======
                    Weaver.Error($"{variableReference.Name} is an unsupported type. Multidimensional arrays are not supported", variableReference);
                    return null;
                }

                return GenerateReadCollection(variableReference, variableReference.GetElementType(), nameof(NetworkReaderExtensions.ReadArray));
            }

            TypeDefinition variableDefinition = variableReference.Resolve();
            if (variableDefinition == null)
            {
                Weaver.Error($"{variableReference.Name} is not a supported type", variableReference);
                return null;
            }
            if (variableDefinition.IsDerivedFrom<UnityEngine.Component>() &&
                !variableReference.IsDerivedFrom<NetworkBehaviour>())
            {
                Weaver.Error($"Cannot generate reader for component type {variableReference.Name}. Use a supported type or provide a custom reader", variableReference);
                return null;
            }
            if (variableReference.Is<UnityEngine.Object>())
            {
                Weaver.Error($"Cannot generate reader for {variableReference.Name}. Use a supported type or provide a custom reader", variableReference);
                return null;
            }
            if (variableReference.Is<UnityEngine.ScriptableObject>())
            {
                Weaver.Error($"Cannot generate reader for {variableReference.Name}. Use a supported type or provide a custom reader", variableReference);
                return null;
            }
            if (variableReference.IsByReference)
            {
                // error??
                Weaver.Error($"Cannot pass type {variableReference.Name} by reference", variableReference);
                return null;
            }
            if (variableDefinition.HasGenericParameters && !variableDefinition.Is(typeof(ArraySegment<>)) && !variableDefinition.Is(typeof(List<>)))
            {
                Weaver.Error($"Cannot generate reader for generic variable {variableReference.Name}. Use a supported type or provide a custom reader", variableReference);
                return null;
            }
            if (variableDefinition.IsInterface)
            {
                Weaver.Error($"Cannot generate reader for interface {variableReference.Name}. Use a supported type or provide a custom reader", variableReference);
                return null;
            }
            if (variableDefinition.IsAbstract)
            {
                Weaver.Error($"Cannot generate reader for abstract class {variableReference.Name}. Use a supported type or provide a custom reader", variableReference);
                return null;
            }

            if (variableDefinition.IsEnum)
            {
                return GenerateEnumReadFunc(variableReference);
            }
            else if (variableDefinition.Is(typeof(ArraySegment<>)))
            {
                return GenerateArraySegmentReadFunc(variableReference);
>>>>>>> origin/alpha_merge
            }
            else if (variableDefinition.Is(typeof(List<>)))
            {
                GenericInstanceType genericInstance = (GenericInstanceType)variableReference;
                TypeReference elementType = genericInstance.GenericArguments[0];

<<<<<<< HEAD
                return GenerateReadCollection(variableReference, elementType, nameof(NetworkReaderExtensions.ReadList), ref WeavingFailed);
=======
                return GenerateReadCollection(variableReference, elementType, nameof(NetworkReaderExtensions.ReadList));
>>>>>>> origin/alpha_merge
            }
            else if (variableReference.IsDerivedFrom<NetworkBehaviour>())
            {
                return GetNetworkBehaviourReader(variableReference);
            }

<<<<<<< HEAD
            // check if reader generation is applicable on this type
            if (variableDefinition.IsDerivedFrom<UnityEngine.Component>())
            {
                Log.Error($"Cannot generate reader for component type {variableReference.Name}. Use a supported type or provide a custom reader", variableReference);
                WeavingFailed = true;
                return null;
            }
            if (variableReference.Is<UnityEngine.Object>())
            {
                Log.Error($"Cannot generate reader for {variableReference.Name}. Use a supported type or provide a custom reader", variableReference);
                WeavingFailed = true;
                return null;
            }
            if (variableReference.Is<UnityEngine.ScriptableObject>())
            {
                Log.Error($"Cannot generate reader for {variableReference.Name}. Use a supported type or provide a custom reader", variableReference);
                WeavingFailed = true;
                return null;
            }
            if (variableDefinition.HasGenericParameters)
            {
                Log.Error($"Cannot generate reader for generic variable {variableReference.Name}. Use a supported type or provide a custom reader", variableReference);
                WeavingFailed = true;
                return null;
            }
            if (variableDefinition.IsInterface)
            {
                Log.Error($"Cannot generate reader for interface {variableReference.Name}. Use a supported type or provide a custom reader", variableReference);
                WeavingFailed = true;
                return null;
            }
            if (variableDefinition.IsAbstract)
            {
                Log.Error($"Cannot generate reader for abstract class {variableReference.Name}. Use a supported type or provide a custom reader", variableReference);
                WeavingFailed = true;
                return null;
            }

            return GenerateClassOrStructReadFunction(variableReference, ref WeavingFailed);
        }

        MethodReference GetNetworkBehaviourReader(TypeReference variableReference)
        {
            // uses generic ReadNetworkBehaviour rather than having weaver create one for each NB
            MethodReference generic = weaverTypes.readNetworkBehaviourGeneric;

            MethodReference readFunc = generic.MakeGeneric(assembly.MainModule, variableReference);
=======
            return GenerateClassOrStructReadFunction(variableReference);
        }

        static MethodReference GetNetworkBehaviourReader(TypeReference variableReference)
        {
            // uses generic ReadNetworkBehaviour rather than having weaver create one for each NB
            MethodReference generic = WeaverTypes.readNetworkBehaviourGeneric;

            MethodReference readFunc = generic.MakeGeneric(variableReference);
>>>>>>> origin/alpha_merge

            // register function so it is added to Reader<T>
            // use Register instead of RegisterWriteFunc because this is not a generated function
            Register(variableReference, readFunc);

            return readFunc;
        }

<<<<<<< HEAD
        MethodDefinition GenerateEnumReadFunc(TypeReference variable, ref bool WeavingFailed)
=======
        static MethodDefinition GenerateEnumReadFunc(TypeReference variable)
>>>>>>> origin/alpha_merge
        {
            MethodDefinition readerFunc = GenerateReaderFunction(variable);

            ILProcessor worker = readerFunc.Body.GetILProcessor();

            worker.Emit(OpCodes.Ldarg_0);

            TypeReference underlyingType = variable.Resolve().GetEnumUnderlyingType();
<<<<<<< HEAD
            MethodReference underlyingFunc = GetReadFunc(underlyingType, ref WeavingFailed);
=======
            MethodReference underlyingFunc = GetReadFunc(underlyingType);
>>>>>>> origin/alpha_merge

            worker.Emit(OpCodes.Call, underlyingFunc);
            worker.Emit(OpCodes.Ret);
            return readerFunc;
        }

<<<<<<< HEAD
        MethodDefinition GenerateArraySegmentReadFunc(TypeReference variable, ref bool WeavingFailed)
=======
        static MethodDefinition GenerateArraySegmentReadFunc(TypeReference variable)
>>>>>>> origin/alpha_merge
        {
            GenericInstanceType genericInstance = (GenericInstanceType)variable;
            TypeReference elementType = genericInstance.GenericArguments[0];

            MethodDefinition readerFunc = GenerateReaderFunction(variable);

            ILProcessor worker = readerFunc.Body.GetILProcessor();

            // $array = reader.Read<[T]>()
            ArrayType arrayType = elementType.MakeArrayType();
            worker.Emit(OpCodes.Ldarg_0);
<<<<<<< HEAD
            worker.Emit(OpCodes.Call, GetReadFunc(arrayType, ref WeavingFailed));

            // return new ArraySegment<T>($array);
            worker.Emit(OpCodes.Newobj, weaverTypes.ArraySegmentConstructorReference.MakeHostInstanceGeneric(assembly.MainModule, genericInstance));
=======
            worker.Emit(OpCodes.Call, GetReadFunc(arrayType));

            // return new ArraySegment<T>($array);
            worker.Emit(OpCodes.Newobj, WeaverTypes.ArraySegmentConstructorReference.MakeHostInstanceGeneric(genericInstance));
>>>>>>> origin/alpha_merge
            worker.Emit(OpCodes.Ret);
            return readerFunc;
        }

<<<<<<< HEAD
        MethodDefinition GenerateReaderFunction(TypeReference variable)
        {
            string functionName = $"_Read_{variable.FullName}";
=======
        static MethodDefinition GenerateReaderFunction(TypeReference variable)
        {
            string functionName = "_Read_" + variable.FullName;
>>>>>>> origin/alpha_merge

            // create new reader for this type
            MethodDefinition readerFunc = new MethodDefinition(functionName,
                    MethodAttributes.Public |
                    MethodAttributes.Static |
                    MethodAttributes.HideBySig,
                    variable);

<<<<<<< HEAD
            readerFunc.Parameters.Add(new ParameterDefinition("reader", ParameterAttributes.None, weaverTypes.Import<NetworkReader>()));
=======
            readerFunc.Parameters.Add(new ParameterDefinition("reader", ParameterAttributes.None, WeaverTypes.Import<NetworkReader>()));
>>>>>>> origin/alpha_merge
            readerFunc.Body.InitLocals = true;
            RegisterReadFunc(variable, readerFunc);

            return readerFunc;
        }

<<<<<<< HEAD
        MethodDefinition GenerateReadCollection(TypeReference variable, TypeReference elementType, string readerFunction, ref bool WeavingFailed)
        {
            MethodDefinition readerFunc = GenerateReaderFunction(variable);
            // generate readers for the element
            GetReadFunc(elementType, ref WeavingFailed);

            ModuleDefinition module = assembly.MainModule;
            TypeReference readerExtensions = module.ImportReference(typeof(NetworkReaderExtensions));
            MethodReference listReader = Resolvers.ResolveMethod(readerExtensions, assembly, Log, readerFunction, ref WeavingFailed);
=======
        static MethodDefinition GenerateReadCollection(TypeReference variable, TypeReference elementType, string readerFunction)
        {
            MethodDefinition readerFunc = GenerateReaderFunction(variable);
            // generate readers for the element
            GetReadFunc(elementType);

            ModuleDefinition module = Weaver.CurrentAssembly.MainModule;
            TypeReference readerExtensions = module.ImportReference(typeof(NetworkReaderExtensions));
            MethodReference listReader = Resolvers.ResolveMethod(readerExtensions, Weaver.CurrentAssembly, readerFunction);
>>>>>>> origin/alpha_merge

            GenericInstanceMethod methodRef = new GenericInstanceMethod(listReader);
            methodRef.GenericArguments.Add(elementType);

            // generates
            // return reader.ReadList<T>();

            ILProcessor worker = readerFunc.Body.GetILProcessor();
            worker.Emit(OpCodes.Ldarg_0); // reader
            worker.Emit(OpCodes.Call, methodRef); // Read

            worker.Emit(OpCodes.Ret);

            return readerFunc;
        }

<<<<<<< HEAD
        MethodDefinition GenerateClassOrStructReadFunction(TypeReference variable, ref bool WeavingFailed)
=======
        static MethodDefinition GenerateClassOrStructReadFunction(TypeReference variable)
>>>>>>> origin/alpha_merge
        {
            MethodDefinition readerFunc = GenerateReaderFunction(variable);

            // create local for return value
            readerFunc.Body.Variables.Add(new VariableDefinition(variable));

            ILProcessor worker = readerFunc.Body.GetILProcessor();

            TypeDefinition td = variable.Resolve();

            if (!td.IsValueType)
<<<<<<< HEAD
                GenerateNullCheck(worker, ref WeavingFailed);

            CreateNew(variable, worker, td, ref WeavingFailed);
            ReadAllFields(variable, worker, ref WeavingFailed);
=======
                GenerateNullCheck(worker);

            CreateNew(variable, worker, td);
            ReadAllFields(variable, worker);
>>>>>>> origin/alpha_merge

            worker.Emit(OpCodes.Ldloc_0);
            worker.Emit(OpCodes.Ret);
            return readerFunc;
        }

<<<<<<< HEAD
        void GenerateNullCheck(ILProcessor worker, ref bool WeavingFailed)
=======
        static void GenerateNullCheck(ILProcessor worker)
>>>>>>> origin/alpha_merge
        {
            // if (!reader.ReadBoolean()) {
            //   return null;
            // }
            worker.Emit(OpCodes.Ldarg_0);
<<<<<<< HEAD
            worker.Emit(OpCodes.Call, GetReadFunc(weaverTypes.Import<bool>(), ref WeavingFailed));
=======
            worker.Emit(OpCodes.Call, GetReadFunc(WeaverTypes.Import<bool>()));
>>>>>>> origin/alpha_merge

            Instruction labelEmptyArray = worker.Create(OpCodes.Nop);
            worker.Emit(OpCodes.Brtrue, labelEmptyArray);
            // return null
            worker.Emit(OpCodes.Ldnull);
            worker.Emit(OpCodes.Ret);
            worker.Append(labelEmptyArray);
        }

        // Initialize the local variable with a new instance
<<<<<<< HEAD
        void CreateNew(TypeReference variable, ILProcessor worker, TypeDefinition td, ref bool WeavingFailed)
=======
        static void CreateNew(TypeReference variable, ILProcessor worker, TypeDefinition td)
>>>>>>> origin/alpha_merge
        {
            if (variable.IsValueType)
            {
                // structs are created with Initobj
                worker.Emit(OpCodes.Ldloca, 0);
                worker.Emit(OpCodes.Initobj, variable);
            }
            else if (td.IsDerivedFrom<UnityEngine.ScriptableObject>())
            {
<<<<<<< HEAD
                GenericInstanceMethod genericInstanceMethod = new GenericInstanceMethod(weaverTypes.ScriptableObjectCreateInstanceMethod);
=======
                GenericInstanceMethod genericInstanceMethod = new GenericInstanceMethod(WeaverTypes.ScriptableObjectCreateInstanceMethod);
>>>>>>> origin/alpha_merge
                genericInstanceMethod.GenericArguments.Add(variable);
                worker.Emit(OpCodes.Call, genericInstanceMethod);
                worker.Emit(OpCodes.Stloc_0);
            }
            else
            {
                // classes are created with their constructor
                MethodDefinition ctor = Resolvers.ResolveDefaultPublicCtor(variable);
                if (ctor == null)
                {
<<<<<<< HEAD
                    Log.Error($"{variable.Name} can't be deserialized because it has no default constructor. Don't use {variable.Name} in [SyncVar]s, Rpcs, Cmds, etc.", variable);
                    WeavingFailed = true;
                    return;
                }

                MethodReference ctorRef = assembly.MainModule.ImportReference(ctor);
=======
                    Weaver.Error($"{variable.Name} can't be deserialized because it has no default constructor", variable);
                    return;
                }

                MethodReference ctorRef = Weaver.CurrentAssembly.MainModule.ImportReference(ctor);
>>>>>>> origin/alpha_merge

                worker.Emit(OpCodes.Newobj, ctorRef);
                worker.Emit(OpCodes.Stloc_0);
            }
        }

<<<<<<< HEAD
        void ReadAllFields(TypeReference variable, ILProcessor worker, ref bool WeavingFailed)
=======
        static void ReadAllFields(TypeReference variable, ILProcessor worker)
>>>>>>> origin/alpha_merge
        {
            foreach (FieldDefinition field in variable.FindAllPublicFields())
            {
                // mismatched ldloca/ldloc for struct/class combinations is invalid IL, which causes crash at runtime
                OpCode opcode = variable.IsValueType ? OpCodes.Ldloca : OpCodes.Ldloc;
                worker.Emit(opcode, 0);
<<<<<<< HEAD
                MethodReference readFunc = GetReadFunc(field.FieldType, ref WeavingFailed);
=======
                MethodReference readFunc = GetReadFunc(field.FieldType);
>>>>>>> origin/alpha_merge
                if (readFunc != null)
                {
                    worker.Emit(OpCodes.Ldarg_0);
                    worker.Emit(OpCodes.Call, readFunc);
                }
                else
                {
<<<<<<< HEAD
                    Log.Error($"{field.Name} has an unsupported type", field);
                    WeavingFailed = true;
                }
                FieldReference fieldRef = assembly.MainModule.ImportReference(field);
=======
                    Weaver.Error($"{field.Name} has an unsupported type", field);
                }
                FieldReference fieldRef = Weaver.CurrentAssembly.MainModule.ImportReference(field);
>>>>>>> origin/alpha_merge

                worker.Emit(OpCodes.Stfld, fieldRef);
            }
        }

<<<<<<< HEAD
        // Save a delegate for each one of the readers into Reader<T>.read
        internal void InitializeReaders(ILProcessor worker)
        {
            ModuleDefinition module = assembly.MainModule;
=======
        /// <summary>
        /// Save a delegate for each one of the readers into <see cref="Reader{T}.read"/>
        /// </summary>
        /// <param name="worker"></param>
        internal static void InitializeReaders(ILProcessor worker)
        {
            ModuleDefinition module = Weaver.CurrentAssembly.MainModule;
>>>>>>> origin/alpha_merge

            TypeReference genericReaderClassRef = module.ImportReference(typeof(Reader<>));

            System.Reflection.FieldInfo fieldInfo = typeof(Reader<>).GetField(nameof(Reader<object>.read));
            FieldReference fieldRef = module.ImportReference(fieldInfo);
            TypeReference networkReaderRef = module.ImportReference(typeof(NetworkReader));
            TypeReference funcRef = module.ImportReference(typeof(Func<,>));
            MethodReference funcConstructorRef = module.ImportReference(typeof(Func<,>).GetConstructors()[0]);

            foreach (KeyValuePair<TypeReference, MethodReference> kvp in readFuncs)
            {
                TypeReference targetType = kvp.Key;
                MethodReference readFunc = kvp.Value;

                // create a Func<NetworkReader, T> delegate
                worker.Emit(OpCodes.Ldnull);
                worker.Emit(OpCodes.Ldftn, readFunc);
                GenericInstanceType funcGenericInstance = funcRef.MakeGenericInstanceType(networkReaderRef, targetType);
<<<<<<< HEAD
                MethodReference funcConstructorInstance = funcConstructorRef.MakeHostInstanceGeneric(assembly.MainModule, funcGenericInstance);
=======
                MethodReference funcConstructorInstance = funcConstructorRef.MakeHostInstanceGeneric(funcGenericInstance);
>>>>>>> origin/alpha_merge
                worker.Emit(OpCodes.Newobj, funcConstructorInstance);

                // save it in Reader<T>.read
                GenericInstanceType genericInstance = genericReaderClassRef.MakeGenericInstanceType(targetType);
<<<<<<< HEAD
                FieldReference specializedField = fieldRef.SpecializeField(assembly.MainModule, genericInstance);
                worker.Emit(OpCodes.Stsfld, specializedField);
            }
=======
                FieldReference specializedField = fieldRef.SpecializeField(genericInstance);
                worker.Emit(OpCodes.Stsfld, specializedField);
            }

>>>>>>> origin/alpha_merge
        }
    }
}
