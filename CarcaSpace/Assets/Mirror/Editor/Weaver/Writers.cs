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
    public class Writers
    {
        // Writers are only for this assembly.
        // can't be used from another assembly, otherwise we will get:
        // "System.ArgumentException: Member ... is declared in another module and needs to be imported"
        AssemblyDefinition assembly;
        WeaverTypes weaverTypes;
        TypeDefinition GeneratedCodeClass;
        Logger Log;

        Dictionary<TypeReference, MethodReference> writeFuncs =
            new Dictionary<TypeReference, MethodReference>(new TypeReferenceComparer());

        public Writers(AssemblyDefinition assembly, WeaverTypes weaverTypes, TypeDefinition GeneratedCodeClass, Logger Log)
        {
            this.assembly = assembly;
            this.weaverTypes = weaverTypes;
            this.GeneratedCodeClass = GeneratedCodeClass;
            this.Log = Log;
        }

        public void Register(TypeReference dataType, MethodReference methodReference)
        {
            if (writeFuncs.ContainsKey(dataType))
            {
                // TODO enable this again later.
                // Writer has some obsolete functions that were renamed.
                // Don't want weaver warnings for all of them.
                //Log.Warning($"Registering a Write method for {dataType.FullName} when one already exists", methodReference);
            }

            // we need to import type when we Initialize Writers so import here in case it is used anywhere else
            TypeReference imported = assembly.MainModule.ImportReference(dataType);
            writeFuncs[imported] = methodReference;
        }

        void RegisterWriteFunc(TypeReference typeReference, MethodDefinition newWriterFunc)
        {
            Register(typeReference, newWriterFunc);
            GeneratedCodeClass.Methods.Add(newWriterFunc);
        }

        // Finds existing writer for type, if non exists trys to create one
        public MethodReference GetWriteFunc(TypeReference variable, ref bool WeavingFailed)
        {
            if (writeFuncs.TryGetValue(variable, out MethodReference foundFunc))
                return foundFunc;

            // this try/catch will be removed in future PR and make `GetWriteFunc` throw instead
            try
            {
                TypeReference importedVariable = assembly.MainModule.ImportReference(variable);
                return GenerateWriter(importedVariable, ref WeavingFailed);
            }
            catch (GenerateWriterException e)
            {
                Log.Error(e.Message, e.MemberReference);
                WeavingFailed = true;
                return null;
            }
        }

        //Throws GenerateWriterException when writer could not be generated for type
        MethodReference GenerateWriter(TypeReference variableReference, ref bool WeavingFailed)
=======
    public static class Writers
    {
        static Dictionary<TypeReference, MethodReference> writeFuncs;

        public static void Init()
        {
            writeFuncs = new Dictionary<TypeReference, MethodReference>(new TypeReferenceComparer());
        }

        public static void Register(TypeReference dataType, MethodReference methodReference)
        {
            if (writeFuncs.ContainsKey(dataType))
            {
                Weaver.Warning($"Registering a Write method for {dataType.FullName} when one already exists", methodReference);
            }

            // we need to import type when we Initialize Writers so import here in case it is used anywhere else
            TypeReference imported = Weaver.CurrentAssembly.MainModule.ImportReference(dataType);
            writeFuncs[imported] = methodReference;
        }

        static void RegisterWriteFunc(TypeReference typeReference, MethodDefinition newWriterFunc)
        {
            Register(typeReference, newWriterFunc);

            Weaver.GeneratedCodeClass.Methods.Add(newWriterFunc);
        }

        /// <summary>
        /// Finds existing writer for type, if non exists trys to create one
        /// <para>This method is recursive</para>
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>Returns <see cref="MethodReference"/> or null</returns>
        public static MethodReference GetWriteFunc(TypeReference variable)
        {
            if (writeFuncs.TryGetValue(variable, out MethodReference foundFunc))
            {
                return foundFunc;
            }
            else
            {
                // this try/catch will be removed in future PR and make `GetWriteFunc` throw instead
                try
                {
                    TypeReference importedVariable = Weaver.CurrentAssembly.MainModule.ImportReference(variable);
                    return GenerateWriter(importedVariable);
                }
                catch (GenerateWriterException e)
                {
                    Weaver.Error(e.Message, e.MemberReference);
                    return null;
                }
            }
        }

        /// <exception cref="GenerateWriterException">Throws when writer could not be generated for type</exception>
        static MethodReference GenerateWriter(TypeReference variableReference)
>>>>>>> origin/alpha_merge
        {
            if (variableReference.IsByReference)
            {
                throw new GenerateWriterException($"Cannot pass {variableReference.Name} by reference", variableReference);
            }

            // Arrays are special, if we resolve them, we get the element type,
            // e.g. int[] resolves to int
            // therefore process this before checks below
            if (variableReference.IsArray)
            {
                if (variableReference.IsMultidimensionalArray())
                {
                    throw new GenerateWriterException($"{variableReference.Name} is an unsupported type. Multidimensional arrays are not supported", variableReference);
                }
                TypeReference elementType = variableReference.GetElementType();
<<<<<<< HEAD
                return GenerateCollectionWriter(variableReference, elementType, nameof(NetworkWriterExtensions.WriteArray), ref WeavingFailed);
=======
                return GenerateCollectionWriter(variableReference, elementType, nameof(NetworkWriterExtensions.WriteArray));
>>>>>>> origin/alpha_merge
            }

            if (variableReference.Resolve()?.IsEnum ?? false)
            {
                // serialize enum as their base type
<<<<<<< HEAD
                return GenerateEnumWriteFunc(variableReference, ref WeavingFailed);
=======
                return GenerateEnumWriteFunc(variableReference);
>>>>>>> origin/alpha_merge
            }

            // check for collections
            if (variableReference.Is(typeof(ArraySegment<>)))
            {
                GenericInstanceType genericInstance = (GenericInstanceType)variableReference;
                TypeReference elementType = genericInstance.GenericArguments[0];

<<<<<<< HEAD
                return GenerateCollectionWriter(variableReference, elementType, nameof(NetworkWriterExtensions.WriteArraySegment), ref WeavingFailed);
=======
                return GenerateCollectionWriter(variableReference, elementType, nameof(NetworkWriterExtensions.WriteArraySegment));
>>>>>>> origin/alpha_merge
            }
            if (variableReference.Is(typeof(List<>)))
            {
                GenericInstanceType genericInstance = (GenericInstanceType)variableReference;
                TypeReference elementType = genericInstance.GenericArguments[0];

<<<<<<< HEAD
                return GenerateCollectionWriter(variableReference, elementType, nameof(NetworkWriterExtensions.WriteList), ref WeavingFailed);
=======
                return GenerateCollectionWriter(variableReference, elementType, nameof(NetworkWriterExtensions.WriteList));
>>>>>>> origin/alpha_merge
            }

            if (variableReference.IsDerivedFrom<NetworkBehaviour>())
            {
                return GetNetworkBehaviourWriter(variableReference);
            }

            // check for invalid types
            TypeDefinition variableDefinition = variableReference.Resolve();
            if (variableDefinition == null)
            {
                throw new GenerateWriterException($"{variableReference.Name} is not a supported type. Use a supported type or provide a custom writer", variableReference);
            }
            if (variableDefinition.IsDerivedFrom<UnityEngine.Component>())
            {
                throw new GenerateWriterException($"Cannot generate writer for component type {variableReference.Name}. Use a supported type or provide a custom writer", variableReference);
            }
            if (variableReference.Is<UnityEngine.Object>())
            {
                throw new GenerateWriterException($"Cannot generate writer for {variableReference.Name}. Use a supported type or provide a custom writer", variableReference);
            }
            if (variableReference.Is<UnityEngine.ScriptableObject>())
            {
                throw new GenerateWriterException($"Cannot generate writer for {variableReference.Name}. Use a supported type or provide a custom writer", variableReference);
            }
            if (variableDefinition.HasGenericParameters)
            {
                throw new GenerateWriterException($"Cannot generate writer for generic type {variableReference.Name}. Use a supported type or provide a custom writer", variableReference);
            }
            if (variableDefinition.IsInterface)
            {
                throw new GenerateWriterException($"Cannot generate writer for interface {variableReference.Name}. Use a supported type or provide a custom writer", variableReference);
            }
            if (variableDefinition.IsAbstract)
            {
                throw new GenerateWriterException($"Cannot generate writer for abstract class {variableReference.Name}. Use a supported type or provide a custom writer", variableReference);
            }

            // generate writer for class/struct
<<<<<<< HEAD
            return GenerateClassOrStructWriterFunction(variableReference, ref WeavingFailed);
        }

        MethodReference GetNetworkBehaviourWriter(TypeReference variableReference)
        {
            // all NetworkBehaviours can use the same write function
            if (writeFuncs.TryGetValue(weaverTypes.Import<NetworkBehaviour>(), out MethodReference func))
=======
            return GenerateClassOrStructWriterFunction(variableReference);
        }

        static MethodReference GetNetworkBehaviourWriter(TypeReference variableReference)
        {
            // all NetworkBehaviours can use the same write function
            if (writeFuncs.TryGetValue(WeaverTypes.Import<NetworkBehaviour>(), out MethodReference func))
>>>>>>> origin/alpha_merge
            {
                // register function so it is added to writer<T>
                // use Register instead of RegisterWriteFunc because this is not a generated function
                Register(variableReference, func);

                return func;
            }
            else
            {
                // this exception only happens if mirror is missing the WriteNetworkBehaviour method
                throw new MissingMethodException($"Could not find writer for NetworkBehaviour");
            }
        }

<<<<<<< HEAD
        MethodDefinition GenerateEnumWriteFunc(TypeReference variable, ref bool WeavingFailed)
=======
        static MethodDefinition GenerateEnumWriteFunc(TypeReference variable)
>>>>>>> origin/alpha_merge
        {
            MethodDefinition writerFunc = GenerateWriterFunc(variable);

            ILProcessor worker = writerFunc.Body.GetILProcessor();

<<<<<<< HEAD
            MethodReference underlyingWriter = GetWriteFunc(variable.Resolve().GetEnumUnderlyingType(), ref WeavingFailed);
=======
            MethodReference underlyingWriter = GetWriteFunc(variable.Resolve().GetEnumUnderlyingType());
>>>>>>> origin/alpha_merge

            worker.Emit(OpCodes.Ldarg_0);
            worker.Emit(OpCodes.Ldarg_1);
            worker.Emit(OpCodes.Call, underlyingWriter);

            worker.Emit(OpCodes.Ret);
            return writerFunc;
        }

<<<<<<< HEAD
        MethodDefinition GenerateWriterFunc(TypeReference variable)
        {
            string functionName = $"_Write_{variable.FullName}";
=======
        static MethodDefinition GenerateWriterFunc(TypeReference variable)
        {
            string functionName = "_Write_" + variable.FullName;
>>>>>>> origin/alpha_merge
            // create new writer for this type
            MethodDefinition writerFunc = new MethodDefinition(functionName,
                    MethodAttributes.Public |
                    MethodAttributes.Static |
                    MethodAttributes.HideBySig,
<<<<<<< HEAD
                    weaverTypes.Import(typeof(void)));

            writerFunc.Parameters.Add(new ParameterDefinition("writer", ParameterAttributes.None, weaverTypes.Import<NetworkWriter>()));
=======
                    WeaverTypes.Import(typeof(void)));

            writerFunc.Parameters.Add(new ParameterDefinition("writer", ParameterAttributes.None, WeaverTypes.Import<NetworkWriter>()));
>>>>>>> origin/alpha_merge
            writerFunc.Parameters.Add(new ParameterDefinition("value", ParameterAttributes.None, variable));
            writerFunc.Body.InitLocals = true;

            RegisterWriteFunc(variable, writerFunc);
            return writerFunc;
        }

<<<<<<< HEAD
        MethodDefinition GenerateClassOrStructWriterFunction(TypeReference variable, ref bool WeavingFailed)
=======
        static MethodDefinition GenerateClassOrStructWriterFunction(TypeReference variable)
>>>>>>> origin/alpha_merge
        {
            MethodDefinition writerFunc = GenerateWriterFunc(variable);

            ILProcessor worker = writerFunc.Body.GetILProcessor();

            if (!variable.Resolve().IsValueType)
<<<<<<< HEAD
                WriteNullCheck(worker, ref WeavingFailed);

            if (!WriteAllFields(variable, worker, ref WeavingFailed))
=======
                WriteNullCheck(worker);

            if (!WriteAllFields(variable, worker))
>>>>>>> origin/alpha_merge
                return null;

            worker.Emit(OpCodes.Ret);
            return writerFunc;
        }

<<<<<<< HEAD
        void WriteNullCheck(ILProcessor worker, ref bool WeavingFailed)
=======
        static void WriteNullCheck(ILProcessor worker)
>>>>>>> origin/alpha_merge
        {
            // if (value == null)
            // {
            //     writer.WriteBoolean(false);
            //     return;
            // }
            //

            Instruction labelNotNull = worker.Create(OpCodes.Nop);
            worker.Emit(OpCodes.Ldarg_1);
            worker.Emit(OpCodes.Brtrue, labelNotNull);
            worker.Emit(OpCodes.Ldarg_0);
            worker.Emit(OpCodes.Ldc_I4_0);
<<<<<<< HEAD
            worker.Emit(OpCodes.Call, GetWriteFunc(weaverTypes.Import<bool>(), ref WeavingFailed));
=======
            worker.Emit(OpCodes.Call, GetWriteFunc(WeaverTypes.Import<bool>()));
>>>>>>> origin/alpha_merge
            worker.Emit(OpCodes.Ret);
            worker.Append(labelNotNull);

            // write.WriteBoolean(true);
            worker.Emit(OpCodes.Ldarg_0);
            worker.Emit(OpCodes.Ldc_I4_1);
<<<<<<< HEAD
            worker.Emit(OpCodes.Call, GetWriteFunc(weaverTypes.Import<bool>(), ref WeavingFailed));
        }

        // Find all fields in type and write them
        bool WriteAllFields(TypeReference variable, ILProcessor worker, ref bool WeavingFailed)
        {
            foreach (FieldDefinition field in variable.FindAllPublicFields())
            {
                MethodReference writeFunc = GetWriteFunc(field.FieldType, ref WeavingFailed);
                // need this null check till later PR when GetWriteFunc throws exception instead
                if (writeFunc == null) { return false; }

                FieldReference fieldRef = assembly.MainModule.ImportReference(field);

=======
            worker.Emit(OpCodes.Call, GetWriteFunc(WeaverTypes.Import<bool>()));
        }

        /// <summary>
        /// Find all fields in type and write them
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="worker"></param>
        /// <returns>false if fail</returns>
        static bool WriteAllFields(TypeReference variable, ILProcessor worker)
        {
            uint fields = 0;
            foreach (FieldDefinition field in variable.FindAllPublicFields())
            {
                MethodReference writeFunc = GetWriteFunc(field.FieldType);
                // need this null check till later PR when GetWriteFunc throws exception instead
                if (writeFunc == null) { return false; }

                FieldReference fieldRef = Weaver.CurrentAssembly.MainModule.ImportReference(field);

                fields++;
>>>>>>> origin/alpha_merge
                worker.Emit(OpCodes.Ldarg_0);
                worker.Emit(OpCodes.Ldarg_1);
                worker.Emit(OpCodes.Ldfld, fieldRef);
                worker.Emit(OpCodes.Call, writeFunc);
            }

            return true;
        }

<<<<<<< HEAD
        MethodDefinition GenerateCollectionWriter(TypeReference variable, TypeReference elementType, string writerFunction, ref bool WeavingFailed)
=======
        static MethodDefinition GenerateCollectionWriter(TypeReference variable, TypeReference elementType, string writerFunction)
>>>>>>> origin/alpha_merge
        {

            MethodDefinition writerFunc = GenerateWriterFunc(variable);

<<<<<<< HEAD
            MethodReference elementWriteFunc = GetWriteFunc(elementType, ref WeavingFailed);
            MethodReference intWriterFunc = GetWriteFunc(weaverTypes.Import<int>(), ref WeavingFailed);
=======
            MethodReference elementWriteFunc = GetWriteFunc(elementType);
            MethodReference intWriterFunc = GetWriteFunc(WeaverTypes.Import<int>());
>>>>>>> origin/alpha_merge

            // need this null check till later PR when GetWriteFunc throws exception instead
            if (elementWriteFunc == null)
            {
<<<<<<< HEAD
                Log.Error($"Cannot generate writer for {variable}. Use a supported type or provide a custom writer", variable);
                WeavingFailed = true;
                return writerFunc;
            }

            ModuleDefinition module = assembly.MainModule;
            TypeReference readerExtensions = module.ImportReference(typeof(NetworkWriterExtensions));
            MethodReference collectionWriter = Resolvers.ResolveMethod(readerExtensions, assembly, Log, writerFunction, ref WeavingFailed);
=======
                Weaver.Error($"Cannot generate writer for {variable}. Use a supported type or provide a custom writer", variable);
                return writerFunc;
            }

            ModuleDefinition module = Weaver.CurrentAssembly.MainModule;
            TypeReference readerExtensions = module.ImportReference(typeof(NetworkWriterExtensions));
            MethodReference collectionWriter = Resolvers.ResolveMethod(readerExtensions, Weaver.CurrentAssembly, writerFunction);
>>>>>>> origin/alpha_merge

            GenericInstanceMethod methodRef = new GenericInstanceMethod(collectionWriter);
            methodRef.GenericArguments.Add(elementType);

            // generates
            // reader.WriteArray<T>(array);

            ILProcessor worker = writerFunc.Body.GetILProcessor();
            worker.Emit(OpCodes.Ldarg_0); // writer
            worker.Emit(OpCodes.Ldarg_1); // collection

            worker.Emit(OpCodes.Call, methodRef); // WriteArray

            worker.Emit(OpCodes.Ret);

            return writerFunc;
        }

<<<<<<< HEAD
        // Save a delegate for each one of the writers into Writer{T}.write
        internal void InitializeWriters(ILProcessor worker)
        {
            ModuleDefinition module = assembly.MainModule;
=======
        /// <summary>
        /// Save a delegate for each one of the writers into <see cref="Writer{T}.write"/>
        /// </summary>
        /// <param name="worker"></param>
        internal static void InitializeWriters(ILProcessor worker)
        {
            ModuleDefinition module = Weaver.CurrentAssembly.MainModule;
>>>>>>> origin/alpha_merge

            TypeReference genericWriterClassRef = module.ImportReference(typeof(Writer<>));

            System.Reflection.FieldInfo fieldInfo = typeof(Writer<>).GetField(nameof(Writer<object>.write));
            FieldReference fieldRef = module.ImportReference(fieldInfo);
            TypeReference networkWriterRef = module.ImportReference(typeof(NetworkWriter));
            TypeReference actionRef = module.ImportReference(typeof(Action<,>));
            MethodReference actionConstructorRef = module.ImportReference(typeof(Action<,>).GetConstructors()[0]);

            foreach (KeyValuePair<TypeReference, MethodReference> kvp in writeFuncs)
            {
                TypeReference targetType = kvp.Key;
                MethodReference writeFunc = kvp.Value;

                // create a Action<NetworkWriter, T> delegate
                worker.Emit(OpCodes.Ldnull);
                worker.Emit(OpCodes.Ldftn, writeFunc);
                GenericInstanceType actionGenericInstance = actionRef.MakeGenericInstanceType(networkWriterRef, targetType);
<<<<<<< HEAD
                MethodReference actionRefInstance = actionConstructorRef.MakeHostInstanceGeneric(assembly.MainModule, actionGenericInstance);
=======
                MethodReference actionRefInstance = actionConstructorRef.MakeHostInstanceGeneric(actionGenericInstance);
>>>>>>> origin/alpha_merge
                worker.Emit(OpCodes.Newobj, actionRefInstance);

                // save it in Writer<T>.write
                GenericInstanceType genericInstance = genericWriterClassRef.MakeGenericInstanceType(targetType);
<<<<<<< HEAD
                FieldReference specializedField = fieldRef.SpecializeField(assembly.MainModule, genericInstance);
                worker.Emit(OpCodes.Stsfld, specializedField);
            }
        }
=======
                FieldReference specializedField = fieldRef.SpecializeField(genericInstance);
                worker.Emit(OpCodes.Stsfld, specializedField);
            }
        }

>>>>>>> origin/alpha_merge
    }
}
