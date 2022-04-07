using System.Collections.Generic;
using Mono.CecilX;
using Mono.CecilX.Cil;

namespace Mirror.Weaver
{
    public enum RemoteCallType
    {
        Command,
        ClientRpc,
        TargetRpc
    }

<<<<<<< HEAD
    // processes SyncVars, Cmds, Rpcs, etc. of NetworkBehaviours
    class NetworkBehaviourProcessor
    {
        AssemblyDefinition assembly;
        WeaverTypes weaverTypes;
        SyncVarAccessLists syncVarAccessLists;
        SyncVarAttributeProcessor syncVarAttributeProcessor;
        Writers writers;
        Readers readers;
        Logger Log;

=======

    /// <summary>
    /// processes SyncVars, Cmds, Rpcs, etc. of NetworkBehaviours
    /// </summary>
    class NetworkBehaviourProcessor
    {
>>>>>>> origin/alpha_merge
        List<FieldDefinition> syncVars = new List<FieldDefinition>();
        List<FieldDefinition> syncObjects = new List<FieldDefinition>();
        // <SyncVarField,NetIdField>
        Dictionary<FieldDefinition, FieldDefinition> syncVarNetIds = new Dictionary<FieldDefinition, FieldDefinition>();
        readonly List<CmdResult> commands = new List<CmdResult>();
        readonly List<ClientRpcResult> clientRpcs = new List<ClientRpcResult>();
        readonly List<MethodDefinition> targetRpcs = new List<MethodDefinition>();
        readonly List<MethodDefinition> commandInvocationFuncs = new List<MethodDefinition>();
        readonly List<MethodDefinition> clientRpcInvocationFuncs = new List<MethodDefinition>();
        readonly List<MethodDefinition> targetRpcInvocationFuncs = new List<MethodDefinition>();

        readonly TypeDefinition netBehaviourSubclass;

        public struct CmdResult
        {
            public MethodDefinition method;
            public bool requiresAuthority;
        }

        public struct ClientRpcResult
        {
            public MethodDefinition method;
            public bool includeOwner;
        }

<<<<<<< HEAD
        public NetworkBehaviourProcessor(AssemblyDefinition assembly, WeaverTypes weaverTypes, SyncVarAccessLists syncVarAccessLists, Writers writers, Readers readers, Logger Log, TypeDefinition td)
        {
            this.assembly = assembly;
            this.weaverTypes = weaverTypes;
            this.syncVarAccessLists = syncVarAccessLists;
            this.writers = writers;
            this.readers = readers;
            this.Log = Log;
            syncVarAttributeProcessor = new SyncVarAttributeProcessor(assembly, weaverTypes, syncVarAccessLists, Log);
=======
        public NetworkBehaviourProcessor(TypeDefinition td)
        {
            Weaver.DLog(td, "NetworkBehaviourProcessor");
>>>>>>> origin/alpha_merge
            netBehaviourSubclass = td;
        }

        // return true if modified
<<<<<<< HEAD
        public bool Process(ref bool WeavingFailed)
=======
        public bool Process()
>>>>>>> origin/alpha_merge
        {
            // only process once
            if (WasProcessed(netBehaviourSubclass))
            {
                return false;
            }
<<<<<<< HEAD

            MarkAsProcessed(netBehaviourSubclass);

            // deconstruct tuple and set fields
            (syncVars, syncVarNetIds) = syncVarAttributeProcessor.ProcessSyncVars(netBehaviourSubclass, ref WeavingFailed);

            syncObjects = SyncObjectProcessor.FindSyncObjectsFields(writers, readers, Log, netBehaviourSubclass, ref WeavingFailed);

            ProcessMethods(ref WeavingFailed);
            if (WeavingFailed)
=======
            Weaver.DLog(netBehaviourSubclass, "Found NetworkBehaviour " + netBehaviourSubclass.FullName);

            if (netBehaviourSubclass.HasGenericParameters)
            {
                Weaver.Error($"{netBehaviourSubclass.Name} cannot have generic parameters", netBehaviourSubclass);
                // originally Process returned true in every case, except if already processed.
                // maybe return false here in the future.
                return true;
            }
            Weaver.DLog(netBehaviourSubclass, "Process Start");
            MarkAsProcessed(netBehaviourSubclass);

            // deconstruct tuple and set fields
            (syncVars, syncVarNetIds) = SyncVarProcessor.ProcessSyncVars(netBehaviourSubclass);

            syncObjects = SyncObjectProcessor.FindSyncObjectsFields(netBehaviourSubclass);

            ProcessMethods();
            if (Weaver.WeavingFailed)
            {
                // originally Process returned true in every case, except if already processed.
                // maybe return false here in the future.
                return true;
            }
            GenerateConstants();

            GenerateSerialization();
            if (Weaver.WeavingFailed)
>>>>>>> origin/alpha_merge
            {
                // originally Process returned true in every case, except if already processed.
                // maybe return false here in the future.
                return true;
            }

<<<<<<< HEAD
            // inject initializations into static & instance constructor
            InjectIntoStaticConstructor(ref WeavingFailed);
            InjectIntoInstanceConstructor(ref WeavingFailed);

            GenerateSerialization(ref WeavingFailed);
            if (WeavingFailed)
            {
                // originally Process returned true in every case, except if already processed.
                // maybe return false here in the future.
                return true;
            }

            GenerateDeSerialization(ref WeavingFailed);
=======
            GenerateDeSerialization();
            Weaver.DLog(netBehaviourSubclass, "Process Done");
>>>>>>> origin/alpha_merge
            return true;
        }

        /*
        generates code like:
            if (!NetworkClient.active)
              Debug.LogError((object) "Command function CmdRespawn called on server.");

            which is used in InvokeCmd, InvokeRpc, etc.
        */
<<<<<<< HEAD
        public static void WriteClientActiveCheck(ILProcessor worker, WeaverTypes weaverTypes, string mdName, Instruction label, string errString)
        {
            // client active check
            worker.Emit(OpCodes.Call, weaverTypes.NetworkClientGetActive);
            worker.Emit(OpCodes.Brtrue, label);

            worker.Emit(OpCodes.Ldstr, $"{errString} {mdName} called on server.");
            worker.Emit(OpCodes.Call, weaverTypes.logErrorReference);
=======
        public static void WriteClientActiveCheck(ILProcessor worker, string mdName, Instruction label, string errString)
        {
            // client active check
            worker.Emit(OpCodes.Call, WeaverTypes.NetworkClientGetActive);
            worker.Emit(OpCodes.Brtrue, label);

            worker.Emit(OpCodes.Ldstr, errString + " " + mdName + " called on server.");
            worker.Emit(OpCodes.Call, WeaverTypes.logErrorReference);
>>>>>>> origin/alpha_merge
            worker.Emit(OpCodes.Ret);
            worker.Append(label);
        }
        /*
        generates code like:
            if (!NetworkServer.active)
              Debug.LogError((object) "Command CmdMsgWhisper called on client.");
        */
<<<<<<< HEAD
        public static void WriteServerActiveCheck(ILProcessor worker, WeaverTypes weaverTypes, string mdName, Instruction label, string errString)
        {
            // server active check
            worker.Emit(OpCodes.Call, weaverTypes.NetworkServerGetActive);
            worker.Emit(OpCodes.Brtrue, label);

            worker.Emit(OpCodes.Ldstr, $"{errString} {mdName} called on client.");
            worker.Emit(OpCodes.Call, weaverTypes.logErrorReference);
=======
        public static void WriteServerActiveCheck(ILProcessor worker, string mdName, Instruction label, string errString)
        {
            // server active check
            worker.Emit(OpCodes.Call, WeaverTypes.NetworkServerGetActive);
            worker.Emit(OpCodes.Brtrue, label);

            worker.Emit(OpCodes.Ldstr, errString + " " + mdName + " called on client.");
            worker.Emit(OpCodes.Call, WeaverTypes.logErrorReference);
>>>>>>> origin/alpha_merge
            worker.Emit(OpCodes.Ret);
            worker.Append(label);
        }

<<<<<<< HEAD
        public static void WriteSetupLocals(ILProcessor worker, WeaverTypes weaverTypes)
        {
            worker.Body.InitLocals = true;
            worker.Body.Variables.Add(new VariableDefinition(weaverTypes.Import<PooledNetworkWriter>()));
        }

        public static void WriteCreateWriter(ILProcessor worker, WeaverTypes weaverTypes)
        {
            // create writer
            worker.Emit(OpCodes.Call, weaverTypes.GetPooledWriterReference);
            worker.Emit(OpCodes.Stloc_0);
        }

        public static void WriteRecycleWriter(ILProcessor worker, WeaverTypes weaverTypes)
        {
            // NetworkWriterPool.Recycle(writer);
            worker.Emit(OpCodes.Ldloc_0);
            worker.Emit(OpCodes.Call, weaverTypes.RecycleWriterReference);
        }

        public static bool WriteArguments(ILProcessor worker, Writers writers, Logger Log, MethodDefinition method, RemoteCallType callType, ref bool WeavingFailed)
=======
        public static void WriteSetupLocals(ILProcessor worker)
        {
            worker.Body.InitLocals = true;
            worker.Body.Variables.Add(new VariableDefinition(WeaverTypes.Import<PooledNetworkWriter>()));
        }

        public static void WriteCreateWriter(ILProcessor worker)
        {
            // create writer
            worker.Emit(OpCodes.Call, WeaverTypes.GetPooledWriterReference);
            worker.Emit(OpCodes.Stloc_0);
        }

        public static void WriteRecycleWriter(ILProcessor worker)
        {
            // NetworkWriterPool.Recycle(writer);
            worker.Emit(OpCodes.Ldloc_0);
            worker.Emit(OpCodes.Call, WeaverTypes.RecycleWriterReference);
        }

        public static bool WriteArguments(ILProcessor worker, MethodDefinition method, RemoteCallType callType)
>>>>>>> origin/alpha_merge
        {
            // write each argument
            // example result
            /*
            writer.WritePackedInt32(someNumber);
            writer.WriteNetworkIdentity(someTarget);
             */

            bool skipFirst = callType == RemoteCallType.TargetRpc
                && TargetRpcProcessor.HasNetworkConnectionParameter(method);

            // arg of calling  function, arg 0 is "this" so start counting at 1
            int argNum = 1;
            foreach (ParameterDefinition param in method.Parameters)
            {
                // NetworkConnection is not sent via the NetworkWriter so skip it here
                // skip first for NetworkConnection in TargetRpc
                if (argNum == 1 && skipFirst)
                {
                    argNum += 1;
                    continue;
                }
                // skip SenderConnection in Command
                if (IsSenderConnection(param, callType))
                {
                    argNum += 1;
                    continue;
                }

<<<<<<< HEAD
                MethodReference writeFunc = writers.GetWriteFunc(param.ParameterType, ref WeavingFailed);
                if (writeFunc == null)
                {
                    Log.Error($"{method.Name} has invalid parameter {param}", method);
                    WeavingFailed = true;
=======
                MethodReference writeFunc = Writers.GetWriteFunc(param.ParameterType);
                if (writeFunc == null)
                {
                    Weaver.Error($"{method.Name} has invalid parameter {param}", method);
>>>>>>> origin/alpha_merge
                    return false;
                }

                // use built-in writer func on writer object
                // NetworkWriter object
                worker.Emit(OpCodes.Ldloc_0);
                // add argument to call
                worker.Emit(OpCodes.Ldarg, argNum);
                // call writer extension method
                worker.Emit(OpCodes.Call, writeFunc);
                argNum += 1;
            }
            return true;
        }

        #region mark / check type as processed
        public const string ProcessedFunctionName = "MirrorProcessed";

        // by adding an empty MirrorProcessed() function
        public static bool WasProcessed(TypeDefinition td)
        {
            return td.GetMethod(ProcessedFunctionName) != null;
        }

<<<<<<< HEAD
        public void MarkAsProcessed(TypeDefinition td)
        {
            if (!WasProcessed(td))
            {
                MethodDefinition versionMethod = new MethodDefinition(ProcessedFunctionName, MethodAttributes.Private, weaverTypes.Import(typeof(void)));
=======
        public static void MarkAsProcessed(TypeDefinition td)
        {
            if (!WasProcessed(td))
            {
                MethodDefinition versionMethod = new MethodDefinition(ProcessedFunctionName, MethodAttributes.Private, WeaverTypes.Import(typeof(void)));
>>>>>>> origin/alpha_merge
                ILProcessor worker = versionMethod.Body.GetILProcessor();
                worker.Emit(OpCodes.Ret);
                td.Methods.Add(versionMethod);
            }
        }
        #endregion

<<<<<<< HEAD
        // helper function to remove 'Ret' from the end of the method if 'Ret'
        // is the last instruction.
        // returns false if there was an issue
        static bool RemoveFinalRetInstruction(MethodDefinition method)
        {
            // remove the return opcode from end of function. will add our own later.
            if (method.Body.Instructions.Count != 0)
            {
                Instruction retInstr = method.Body.Instructions[method.Body.Instructions.Count - 1];
                if (retInstr.OpCode == OpCodes.Ret)
                {
                    method.Body.Instructions.RemoveAt(method.Body.Instructions.Count - 1);
                    return true;
                }
                return false;
            }

            // we did nothing, but there was no error.
            return true;
        }

        // we need to inject several initializations into NetworkBehaviour cctor
        void InjectIntoStaticConstructor(ref bool WeavingFailed)
        {
            if (commands.Count == 0 && clientRpcs.Count == 0 && targetRpcs.Count == 0)
                return;

=======
        void GenerateConstants()
        {
            if (commands.Count == 0 && clientRpcs.Count == 0 && targetRpcs.Count == 0 && syncObjects.Count == 0)
                return;

            Weaver.DLog(netBehaviourSubclass, "  GenerateConstants ");

>>>>>>> origin/alpha_merge
            // find static constructor
            MethodDefinition cctor = netBehaviourSubclass.GetMethod(".cctor");
            bool cctorFound = cctor != null;
            if (cctor != null)
            {
                // remove the return opcode from end of function. will add our own later.
<<<<<<< HEAD
                if (!RemoveFinalRetInstruction(cctor))
                {
                    Log.Error($"{netBehaviourSubclass.Name} has invalid class constructor", cctor);
                    WeavingFailed = true;
                    return;
=======
                if (cctor.Body.Instructions.Count != 0)
                {
                    Instruction retInstr = cctor.Body.Instructions[cctor.Body.Instructions.Count - 1];
                    if (retInstr.OpCode == OpCodes.Ret)
                    {
                        cctor.Body.Instructions.RemoveAt(cctor.Body.Instructions.Count - 1);
                    }
                    else
                    {
                        Weaver.Error($"{netBehaviourSubclass.Name} has invalid class constructor", cctor);
                        return;
                    }
>>>>>>> origin/alpha_merge
                }
            }
            else
            {
                // make one!
                cctor = new MethodDefinition(".cctor", MethodAttributes.Private |
                        MethodAttributes.HideBySig |
                        MethodAttributes.SpecialName |
                        MethodAttributes.RTSpecialName |
                        MethodAttributes.Static,
<<<<<<< HEAD
                        weaverTypes.Import(typeof(void)));
            }

            ILProcessor cctorWorker = cctor.Body.GetILProcessor();

            // register all commands in cctor
            for (int i = 0; i < commands.Count; ++i)
            {
                CmdResult cmdResult = commands[i];
                GenerateRegisterCommandDelegate(cctorWorker, weaverTypes.registerCommandReference, commandInvocationFuncs[i], cmdResult);
            }

            // register all client rpcs in cctor
            for (int i = 0; i < clientRpcs.Count; ++i)
            {
                ClientRpcResult clientRpcResult = clientRpcs[i];
                GenerateRegisterRemoteDelegate(cctorWorker, weaverTypes.registerRpcReference, clientRpcInvocationFuncs[i], clientRpcResult.method.FullName);
            }

            // register all target rpcs in cctor
            for (int i = 0; i < targetRpcs.Count; ++i)
            {
                GenerateRegisterRemoteDelegate(cctorWorker, weaverTypes.registerRpcReference, targetRpcInvocationFuncs[i], targetRpcs[i].FullName);
            }

            // add final 'Ret' instruction to cctor
=======
                        WeaverTypes.Import(typeof(void)));
            }

            // find instance constructor
            MethodDefinition ctor = netBehaviourSubclass.GetMethod(".ctor");

            if (ctor == null)
            {
                Weaver.Error($"{netBehaviourSubclass.Name} has invalid constructor", netBehaviourSubclass);
                return;
            }

            Instruction ret = ctor.Body.Instructions[ctor.Body.Instructions.Count - 1];
            if (ret.OpCode == OpCodes.Ret)
            {
                ctor.Body.Instructions.RemoveAt(ctor.Body.Instructions.Count - 1);
            }
            else
            {
                Weaver.Error($"{netBehaviourSubclass.Name} has invalid constructor", ctor);
                return;
            }

            // TODO: find out if the order below matters. If it doesn't split code below into 2 functions
            ILProcessor ctorWorker = ctor.Body.GetILProcessor();
            ILProcessor cctorWorker = cctor.Body.GetILProcessor();

            for (int i = 0; i < commands.Count; ++i)
            {
                CmdResult cmdResult = commands[i];
                GenerateRegisterCommandDelegate(cctorWorker, WeaverTypes.registerCommandDelegateReference, commandInvocationFuncs[i], cmdResult);
            }

            for (int i = 0; i < clientRpcs.Count; ++i)
            {
                ClientRpcResult clientRpcResult = clientRpcs[i];
                GenerateRegisterRemoteDelegate(cctorWorker, WeaverTypes.registerRpcDelegateReference, clientRpcInvocationFuncs[i], clientRpcResult.method.Name);
            }

            for (int i = 0; i < targetRpcs.Count; ++i)
            {
                GenerateRegisterRemoteDelegate(cctorWorker, WeaverTypes.registerRpcDelegateReference, targetRpcInvocationFuncs[i], targetRpcs[i].Name);
            }

            foreach (FieldDefinition fd in syncObjects)
            {
                SyncObjectInitializer.GenerateSyncObjectInitializer(ctorWorker, fd);
            }

>>>>>>> origin/alpha_merge
            cctorWorker.Append(cctorWorker.Create(OpCodes.Ret));
            if (!cctorFound)
            {
                netBehaviourSubclass.Methods.Add(cctor);
            }

<<<<<<< HEAD
=======
            // finish ctor
            ctorWorker.Append(ctorWorker.Create(OpCodes.Ret));

>>>>>>> origin/alpha_merge
            // in case class had no cctor, it might have BeforeFieldInit, so injected cctor would be called too late
            netBehaviourSubclass.Attributes &= ~TypeAttributes.BeforeFieldInit;
        }

<<<<<<< HEAD
        // we need to inject several initializations into NetworkBehaviour ctor
        void InjectIntoInstanceConstructor(ref bool WeavingFailed)
        {
            if (syncObjects.Count == 0)
                return;

            // find instance constructor
            MethodDefinition ctor = netBehaviourSubclass.GetMethod(".ctor");
            if (ctor == null)
            {
                Log.Error($"{netBehaviourSubclass.Name} has invalid constructor", netBehaviourSubclass);
                WeavingFailed = true;
                return;
            }

            // remove the return opcode from end of function. will add our own later.
            if (!RemoveFinalRetInstruction(ctor))
            {
                Log.Error($"{netBehaviourSubclass.Name} has invalid constructor", ctor);
                WeavingFailed = true;
                return;
            }

            ILProcessor ctorWorker = ctor.Body.GetILProcessor();

            // initialize all sync objects in ctor
            foreach (FieldDefinition fd in syncObjects)
            {
                SyncObjectInitializer.GenerateSyncObjectInitializer(ctorWorker, weaverTypes, fd);
            }

            // add final 'Ret' instruction to ctor
            ctorWorker.Append(ctorWorker.Create(OpCodes.Ret));
        }

=======
>>>>>>> origin/alpha_merge
        /*
            // This generates code like:
            NetworkBehaviour.RegisterCommandDelegate(base.GetType(), "CmdThrust", new NetworkBehaviour.CmdDelegate(ShipControl.InvokeCmdCmdThrust));
        */
<<<<<<< HEAD

        // pass full function name to avoid ClassA.Func <-> ClassB.Func collisions
        void GenerateRegisterRemoteDelegate(ILProcessor worker, MethodReference registerMethod, MethodDefinition func, string functionFullName)
        {
            worker.Emit(OpCodes.Ldtoken, netBehaviourSubclass);
            worker.Emit(OpCodes.Call, weaverTypes.getTypeFromHandleReference);
            worker.Emit(OpCodes.Ldstr, functionFullName);
            worker.Emit(OpCodes.Ldnull);
            worker.Emit(OpCodes.Ldftn, func);

            worker.Emit(OpCodes.Newobj, weaverTypes.RemoteCallDelegateConstructor);
=======
        void GenerateRegisterRemoteDelegate(ILProcessor worker, MethodReference registerMethod, MethodDefinition func, string cmdName)
        {
            worker.Emit(OpCodes.Ldtoken, netBehaviourSubclass);
            worker.Emit(OpCodes.Call, WeaverTypes.getTypeFromHandleReference);
            worker.Emit(OpCodes.Ldstr, cmdName);
            worker.Emit(OpCodes.Ldnull);
            worker.Emit(OpCodes.Ldftn, func);

            worker.Emit(OpCodes.Newobj, WeaverTypes.CmdDelegateConstructor);
>>>>>>> origin/alpha_merge
            //
            worker.Emit(OpCodes.Call, registerMethod);
        }

        void GenerateRegisterCommandDelegate(ILProcessor worker, MethodReference registerMethod, MethodDefinition func, CmdResult cmdResult)
        {
<<<<<<< HEAD
            // pass full function name to avoid ClassA.Func <-> ClassB.Func collisions
            string cmdName = cmdResult.method.FullName;
            bool requiresAuthority = cmdResult.requiresAuthority;

            worker.Emit(OpCodes.Ldtoken, netBehaviourSubclass);
            worker.Emit(OpCodes.Call, weaverTypes.getTypeFromHandleReference);
=======
            string cmdName = cmdResult.method.Name;
            bool requiresAuthority = cmdResult.requiresAuthority;

            worker.Emit(OpCodes.Ldtoken, netBehaviourSubclass);
            worker.Emit(OpCodes.Call, WeaverTypes.getTypeFromHandleReference);
>>>>>>> origin/alpha_merge
            worker.Emit(OpCodes.Ldstr, cmdName);
            worker.Emit(OpCodes.Ldnull);
            worker.Emit(OpCodes.Ldftn, func);

<<<<<<< HEAD
            worker.Emit(OpCodes.Newobj, weaverTypes.RemoteCallDelegateConstructor);
=======
            worker.Emit(OpCodes.Newobj, WeaverTypes.CmdDelegateConstructor);
>>>>>>> origin/alpha_merge

            // requiresAuthority ? 1 : 0
            worker.Emit(requiresAuthority ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);

            worker.Emit(OpCodes.Call, registerMethod);
        }

<<<<<<< HEAD
        void GenerateSerialization(ref bool WeavingFailed)
        {
=======
        void GenerateSerialization()
        {
            Weaver.DLog(netBehaviourSubclass, "  GenerateSerialization");

>>>>>>> origin/alpha_merge
            const string SerializeMethodName = "SerializeSyncVars";
            if (netBehaviourSubclass.GetMethod(SerializeMethodName) != null)
                return;

            if (syncVars.Count == 0)
            {
                // no synvars,  no need for custom OnSerialize
                return;
            }

            MethodDefinition serialize = new MethodDefinition(SerializeMethodName,
                    MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig,
<<<<<<< HEAD
                    weaverTypes.Import<bool>());

            serialize.Parameters.Add(new ParameterDefinition("writer", ParameterAttributes.None, weaverTypes.Import<NetworkWriter>()));
            serialize.Parameters.Add(new ParameterDefinition("forceAll", ParameterAttributes.None, weaverTypes.Import<bool>()));
=======
                    WeaverTypes.Import<bool>());

            serialize.Parameters.Add(new ParameterDefinition("writer", ParameterAttributes.None, WeaverTypes.Import<NetworkWriter>()));
            serialize.Parameters.Add(new ParameterDefinition("forceAll", ParameterAttributes.None, WeaverTypes.Import<bool>()));
>>>>>>> origin/alpha_merge
            ILProcessor worker = serialize.Body.GetILProcessor();

            serialize.Body.InitLocals = true;

            // loc_0,  this local variable is to determine if any variable was dirty
<<<<<<< HEAD
            VariableDefinition dirtyLocal = new VariableDefinition(weaverTypes.Import<bool>());
            serialize.Body.Variables.Add(dirtyLocal);

            MethodReference baseSerialize = Resolvers.TryResolveMethodInParents(netBehaviourSubclass.BaseType, assembly, SerializeMethodName);
=======
            VariableDefinition dirtyLocal = new VariableDefinition(WeaverTypes.Import<bool>());
            serialize.Body.Variables.Add(dirtyLocal);

            MethodReference baseSerialize = Resolvers.TryResolveMethodInParents(netBehaviourSubclass.BaseType, Weaver.CurrentAssembly, SerializeMethodName);
>>>>>>> origin/alpha_merge
            if (baseSerialize != null)
            {
                // base
                worker.Emit(OpCodes.Ldarg_0);
                // writer
                worker.Emit(OpCodes.Ldarg_1);
                // forceAll
                worker.Emit(OpCodes.Ldarg_2);
                worker.Emit(OpCodes.Call, baseSerialize);
                // set dirtyLocal to result of base.OnSerialize()
                worker.Emit(OpCodes.Stloc_0);
            }

            // Generates: if (forceAll);
            Instruction initialStateLabel = worker.Create(OpCodes.Nop);
            // forceAll
            worker.Emit(OpCodes.Ldarg_2);
            worker.Emit(OpCodes.Brfalse, initialStateLabel);

<<<<<<< HEAD
            foreach (FieldDefinition syncVarDef in syncVars)
            {
                FieldReference syncVar = syncVarDef;
                if (netBehaviourSubclass.HasGenericParameters)
                {
                    syncVar = syncVarDef.MakeHostInstanceGeneric();
                }
=======
            foreach (FieldDefinition syncVar in syncVars)
            {
>>>>>>> origin/alpha_merge
                // Generates a writer call for each sync variable
                // writer
                worker.Emit(OpCodes.Ldarg_1);
                // this
                worker.Emit(OpCodes.Ldarg_0);
                worker.Emit(OpCodes.Ldfld, syncVar);
<<<<<<< HEAD
                MethodReference writeFunc = writers.GetWriteFunc(syncVar.FieldType, ref WeavingFailed);
=======
                MethodReference writeFunc = Writers.GetWriteFunc(syncVar.FieldType);
>>>>>>> origin/alpha_merge
                if (writeFunc != null)
                {
                    worker.Emit(OpCodes.Call, writeFunc);
                }
                else
                {
<<<<<<< HEAD
                    Log.Error($"{syncVar.Name} has unsupported type. Use a supported Mirror type instead", syncVar);
                    WeavingFailed = true;
=======
                    Weaver.Error($"{syncVar.Name} has unsupported type. Use a supported Mirror type instead", syncVar);
>>>>>>> origin/alpha_merge
                    return;
                }
            }

            // always return true if forceAll

            // Generates: return true
            worker.Emit(OpCodes.Ldc_I4_1);
            worker.Emit(OpCodes.Ret);

            // Generates: end if (forceAll);
            worker.Append(initialStateLabel);

            // write dirty bits before the data fields
            // Generates: writer.WritePackedUInt64 (base.get_syncVarDirtyBits ());
            // writer
            worker.Emit(OpCodes.Ldarg_1);
            // base
            worker.Emit(OpCodes.Ldarg_0);
<<<<<<< HEAD
            worker.Emit(OpCodes.Call, weaverTypes.NetworkBehaviourDirtyBitsReference);
            MethodReference writeUint64Func = writers.GetWriteFunc(weaverTypes.Import<ulong>(), ref WeavingFailed);
=======
            worker.Emit(OpCodes.Call, WeaverTypes.NetworkBehaviourDirtyBitsReference);
            MethodReference writeUint64Func = Writers.GetWriteFunc(WeaverTypes.Import<ulong>());
>>>>>>> origin/alpha_merge
            worker.Emit(OpCodes.Call, writeUint64Func);

            // generate a writer call for any dirty variable in this class

            // start at number of syncvars in parent
<<<<<<< HEAD
            int dirtyBit = syncVarAccessLists.GetSyncVarStart(netBehaviourSubclass.BaseType.FullName);
            foreach (FieldDefinition syncVarDef in syncVars)
            {

                FieldReference syncVar = syncVarDef;
                if (netBehaviourSubclass.HasGenericParameters)
                {
                    syncVar = syncVarDef.MakeHostInstanceGeneric();
                }
=======
            int dirtyBit = Weaver.WeaveLists.GetSyncVarStart(netBehaviourSubclass.BaseType.FullName);
            foreach (FieldDefinition syncVar in syncVars)
            {
>>>>>>> origin/alpha_merge
                Instruction varLabel = worker.Create(OpCodes.Nop);

                // Generates: if ((base.get_syncVarDirtyBits() & 1uL) != 0uL)
                // base
                worker.Emit(OpCodes.Ldarg_0);
<<<<<<< HEAD
                worker.Emit(OpCodes.Call, weaverTypes.NetworkBehaviourDirtyBitsReference);
=======
                worker.Emit(OpCodes.Call, WeaverTypes.NetworkBehaviourDirtyBitsReference);
>>>>>>> origin/alpha_merge
                // 8 bytes = long
                worker.Emit(OpCodes.Ldc_I8, 1L << dirtyBit);
                worker.Emit(OpCodes.And);
                worker.Emit(OpCodes.Brfalse, varLabel);

                // Generates a call to the writer for that field
                // writer
                worker.Emit(OpCodes.Ldarg_1);
                // base
                worker.Emit(OpCodes.Ldarg_0);
                worker.Emit(OpCodes.Ldfld, syncVar);

<<<<<<< HEAD
                MethodReference writeFunc = writers.GetWriteFunc(syncVar.FieldType, ref WeavingFailed);
=======
                MethodReference writeFunc = Writers.GetWriteFunc(syncVar.FieldType);
>>>>>>> origin/alpha_merge
                if (writeFunc != null)
                {
                    worker.Emit(OpCodes.Call, writeFunc);
                }
                else
                {
<<<<<<< HEAD
                    Log.Error($"{syncVar.Name} has unsupported type. Use a supported Mirror type instead", syncVar);
                    WeavingFailed = true;
=======
                    Weaver.Error($"{syncVar.Name} has unsupported type. Use a supported Mirror type instead", syncVar);
>>>>>>> origin/alpha_merge
                    return;
                }

                // something was dirty
                worker.Emit(OpCodes.Ldc_I4_1);
                // set dirtyLocal to true
                worker.Emit(OpCodes.Stloc_0);

                worker.Append(varLabel);
                dirtyBit += 1;
            }

<<<<<<< HEAD
            // add a log message if needed for debugging
            //worker.Emit(OpCodes.Ldstr, $"Injected Serialize {netBehaviourSubclass.Name}");
            //worker.Emit(OpCodes.Call, WeaverTypes.logErrorReference);
=======
            if (Weaver.GenerateLogErrors)
            {
                worker.Emit(OpCodes.Ldstr, "Injected Serialize " + netBehaviourSubclass.Name);
                worker.Emit(OpCodes.Call, WeaverTypes.logErrorReference);
            }
>>>>>>> origin/alpha_merge

            // generate: return dirtyLocal
            worker.Emit(OpCodes.Ldloc_0);
            worker.Emit(OpCodes.Ret);
            netBehaviourSubclass.Methods.Add(serialize);
        }

<<<<<<< HEAD
        void DeserializeField(FieldDefinition syncVar, ILProcessor worker, ref bool WeavingFailed)
        {
            // put 'this.' onto stack for 'this.syncvar' below
            worker.Append(worker.Create(OpCodes.Ldarg_0));

            // push 'ref T this.field'
            worker.Emit(OpCodes.Ldarg_0);
            // if the netbehaviour class is generic, we need to make the field reference generic as well for correct IL
            if (netBehaviourSubclass.HasGenericParameters)
            {
                worker.Emit(OpCodes.Ldflda, syncVar.MakeHostInstanceGeneric());
            }
            else
            {
                worker.Emit(OpCodes.Ldflda, syncVar);
            }

            // hook? then push 'new Action<T,T>(Hook)' onto stack
            MethodDefinition hookMethod = syncVarAttributeProcessor.GetHookMethod(netBehaviourSubclass, syncVar, ref WeavingFailed);
            if (hookMethod != null)
            {
                syncVarAttributeProcessor.GenerateNewActionFromHookMethod(syncVar, worker, hookMethod);
            }
            // otherwise push 'null' as hook
            else
            {
                worker.Emit(OpCodes.Ldnull);
            }

            // call GeneratedSyncVarDeserialize<T>.
            // special cases for GameObject/NetworkIdentity/NetworkBehaviour
            // passing netId too for persistence.
            if (syncVar.FieldType.Is<UnityEngine.GameObject>())
            {
                // reader
                worker.Emit(OpCodes.Ldarg_1);

                // GameObject setter needs one more parameter: netId field ref
                FieldDefinition netIdField = syncVarNetIds[syncVar];
                worker.Emit(OpCodes.Ldarg_0);
                worker.Emit(OpCodes.Ldflda, netIdField);
                worker.Emit(OpCodes.Call, weaverTypes.generatedSyncVarDeserialize_GameObject);
            }
            else if (syncVar.FieldType.Is<NetworkIdentity>())
            {
                // reader
                worker.Emit(OpCodes.Ldarg_1);

                // NetworkIdentity deserialize needs one more parameter: netId field ref
                FieldDefinition netIdField = syncVarNetIds[syncVar];
                worker.Emit(OpCodes.Ldarg_0);
                worker.Emit(OpCodes.Ldflda, netIdField);
                worker.Emit(OpCodes.Call, weaverTypes.generatedSyncVarDeserialize_NetworkIdentity);
            }
            // TODO this only uses the persistent netId for types DERIVED FROM NB.
            //      not if the type is just 'NetworkBehaviour'.
            //      this is what original implementation did too. fix it after.
            else if (syncVar.FieldType.IsDerivedFrom<NetworkBehaviour>())
            {
                // reader
                worker.Emit(OpCodes.Ldarg_1);

                // NetworkIdentity deserialize needs one more parameter: netId field ref
                // (actually its a NetworkBehaviourSyncVar type)
                FieldDefinition netIdField = syncVarNetIds[syncVar];
                worker.Emit(OpCodes.Ldarg_0);
                worker.Emit(OpCodes.Ldflda, netIdField);
                // make generic version of GeneratedSyncVarSetter_NetworkBehaviour<T>
                MethodReference getFunc = weaverTypes.generatedSyncVarDeserialize_NetworkBehaviour_T.MakeGeneric(assembly.MainModule, syncVar.FieldType);
                worker.Emit(OpCodes.Call, getFunc);
            }
            else
            {
                // T value = reader.ReadT();
                // this is still in IL because otherwise weaver generated
                // readers/writers don't seem to work in tests.
                // besides, this also avoids reader.Read<T> overhead.
                MethodReference readFunc = readers.GetReadFunc(syncVar.FieldType, ref WeavingFailed);
                if (readFunc == null)
                {
                    Log.Error($"{syncVar.Name} has unsupported type. Use a supported Mirror type instead", syncVar);
                    WeavingFailed = true;
                    return;
                }
                // reader. for 'reader.Read()' below
                worker.Emit(OpCodes.Ldarg_1);
                // reader.Read()
                worker.Emit(OpCodes.Call, readFunc);

                // make generic version of GeneratedSyncVarDeserialize<T>
                MethodReference generic = weaverTypes.generatedSyncVarDeserialize.MakeGeneric(assembly.MainModule, syncVar.FieldType);
                worker.Emit(OpCodes.Call, generic);
            }
        }

        void GenerateDeSerialization(ref bool WeavingFailed)
        {
=======
        void DeserializeField(FieldDefinition syncVar, ILProcessor worker, MethodDefinition deserialize)
        {
            // check for Hook function
            MethodDefinition hookMethod = SyncVarProcessor.GetHookMethod(netBehaviourSubclass, syncVar);

            if (syncVar.FieldType.IsDerivedFrom<NetworkBehaviour>())
            {
                DeserializeNetworkBehaviourField(syncVar, worker, deserialize, hookMethod);
            }
            else if (syncVar.FieldType.IsNetworkIdentityField())
            {
                DeserializeNetworkIdentityField(syncVar, worker, deserialize, hookMethod);
            }
            else
            {
                DeserializeNormalField(syncVar, worker, deserialize, hookMethod);
            }
        }

        /// <summary>
        /// [SyncVar] GameObject/NetworkIdentity?
        /// </summary>
        /// <param name="syncVar"></param>
        /// <param name="worker"></param>
        /// <param name="deserialize"></param>
        /// <param name="initialState"></param>
        /// <param name="hookResult"></param>
        void DeserializeNetworkIdentityField(FieldDefinition syncVar, ILProcessor worker, MethodDefinition deserialize, MethodDefinition hookMethod)
        {
            /*
            Generates code like:
               uint oldNetId = ___qNetId;
               // returns GetSyncVarGameObject(___qNetId)
               GameObject oldSyncVar = syncvar.getter;
               ___qNetId = reader.ReadPackedUInt32();
               if (!SyncVarEqual(oldNetId, ref ___goNetId))
               {
                   // getter returns GetSyncVarGameObject(___qNetId)
                   OnSetQ(oldSyncVar, syncvar.getter);
               }
            */

            // GameObject/NetworkIdentity SyncVar:
            //   OnSerialize sends writer.Write(go);
            //   OnDeserialize reads to __netId manually so we can use
            //     lookups in the getter (so it still works if objects
            //     move in and out of range repeatedly)
            FieldDefinition netIdField = syncVarNetIds[syncVar];

            // uint oldNetId = ___qNetId;
            VariableDefinition oldNetId = new VariableDefinition(WeaverTypes.Import<uint>());
            deserialize.Body.Variables.Add(oldNetId);
            worker.Emit(OpCodes.Ldarg_0);
            worker.Emit(OpCodes.Ldfld, netIdField);
            worker.Emit(OpCodes.Stloc, oldNetId);

            // GameObject/NetworkIdentity oldSyncVar = syncvar.getter;
            VariableDefinition oldSyncVar = new VariableDefinition(syncVar.FieldType);
            deserialize.Body.Variables.Add(oldSyncVar);
            worker.Emit(OpCodes.Ldarg_0);
            worker.Emit(OpCodes.Ldfld, syncVar);
            worker.Emit(OpCodes.Stloc, oldSyncVar);

            // read id and store in netId field BEFORE calling the hook
            // -> this makes way more sense. by definition, the hook is
            //    supposed to be called after it was changed. not before.
            // -> setting it BEFORE calling the hook fixes the following bug:
            //    https://github.com/vis2k/Mirror/issues/1151 in host mode
            //    where the value during the Hook call would call Cmds on
            //    the host server, and they would all happen and compare
            //    values BEFORE the hook even returned and hence BEFORE the
            //    actual value was even set.
            // put 'this.' onto stack for 'this.netId' below
            worker.Emit(OpCodes.Ldarg_0);
            // reader. for 'reader.Read()' below
            worker.Emit(OpCodes.Ldarg_1);
            // Read()
            worker.Emit(OpCodes.Call, Readers.GetReadFunc(WeaverTypes.Import<uint>()));
            // netId
            worker.Emit(OpCodes.Stfld, netIdField);

            if (hookMethod != null)
            {
                // call Hook(this.GetSyncVarGameObject/NetworkIdentity(reader.ReadPackedUInt32()))
                // because we send/receive the netID, not the GameObject/NetworkIdentity
                // but only if SyncVar changed. otherwise a client would
                // get hook calls for all initial values, even if they
                // didn't change from the default values on the client.
                // see also: https://github.com/vis2k/Mirror/issues/1278

                // IMPORTANT: for GameObjects/NetworkIdentities we usually
                //            use SyncVarGameObjectEqual to compare equality.
                //            in this case however, we can just use
                //            SyncVarEqual with the two uint netIds.
                //            => this is easier weaver code because we don't
                //               have to get the GameObject/NetworkIdentity
                //               from the uint netId
                //            => this is faster because we void one
                //               GetComponent call for GameObjects to get
                //               their NetworkIdentity when comparing.

                // Generates: if (!SyncVarEqual);
                Instruction syncVarEqualLabel = worker.Create(OpCodes.Nop);

                // 'this.' for 'this.SyncVarEqual'
                worker.Emit(OpCodes.Ldarg_0);
                // 'oldNetId'
                worker.Emit(OpCodes.Ldloc, oldNetId);
                // 'ref this.__netId'
                worker.Emit(OpCodes.Ldarg_0);
                worker.Emit(OpCodes.Ldflda, netIdField);
                // call the function
                GenericInstanceMethod syncVarEqualGm = new GenericInstanceMethod(WeaverTypes.syncVarEqualReference);
                syncVarEqualGm.GenericArguments.Add(netIdField.FieldType);
                worker.Emit(OpCodes.Call, syncVarEqualGm);
                worker.Emit(OpCodes.Brtrue, syncVarEqualLabel);

                // call the hook
                // Generates: OnValueChanged(oldValue, this.syncVar);
                SyncVarProcessor.WriteCallHookMethodUsingField(worker, hookMethod, oldSyncVar, syncVar);

                // Generates: end if (!SyncVarEqual);
                worker.Append(syncVarEqualLabel);
            }
        }

        /// <summary>
        /// [SyncVar] NetworkBehaviour
        /// </summary>
        /// <param name="syncVar"></param>
        /// <param name="worker"></param>
        /// <param name="deserialize"></param>
        /// <param name="initialState"></param>
        /// <param name="hookResult"></param>
        void DeserializeNetworkBehaviourField(FieldDefinition syncVar, ILProcessor worker, MethodDefinition deserialize, MethodDefinition hookMethod)
        {
            /*
            Generates code like:
               uint oldNetId = ___qNetId.netId;
               byte oldCompIndex = ___qNetId.componentIndex;
               T oldSyncVar = syncvar.getter;
               ___qNetId.netId = reader.ReadPackedUInt32();
               ___qNetId.componentIndex = reader.ReadByte();
               if (!SyncVarEqual(oldNetId, ref ___goNetId))
               {
                   // getter returns GetSyncVarGameObject(___qNetId)
                   OnSetQ(oldSyncVar, syncvar.getter);
               }
            */

            // GameObject/NetworkIdentity SyncVar:
            //   OnSerialize sends writer.Write(go);
            //   OnDeserialize reads to __netId manually so we can use
            //     lookups in the getter (so it still works if objects
            //     move in and out of range repeatedly)
            FieldDefinition netIdField = syncVarNetIds[syncVar];

            // uint oldNetId = ___qNetId;
            VariableDefinition oldNetId = new VariableDefinition(WeaverTypes.Import<NetworkBehaviour.NetworkBehaviourSyncVar>());
            deserialize.Body.Variables.Add(oldNetId);
            worker.Emit(OpCodes.Ldarg_0);
            worker.Emit(OpCodes.Ldfld, netIdField);
            worker.Emit(OpCodes.Stloc, oldNetId);

            // GameObject/NetworkIdentity oldSyncVar = syncvar.getter;
            VariableDefinition oldSyncVar = new VariableDefinition(syncVar.FieldType);
            deserialize.Body.Variables.Add(oldSyncVar);
            worker.Emit(OpCodes.Ldarg_0);
            worker.Emit(OpCodes.Ldfld, syncVar);
            worker.Emit(OpCodes.Stloc, oldSyncVar);

            // read id and store in netId field BEFORE calling the hook
            // -> this makes way more sense. by definition, the hook is
            //    supposed to be called after it was changed. not before.
            // -> setting it BEFORE calling the hook fixes the following bug:
            //    https://github.com/vis2k/Mirror/issues/1151 in host mode
            //    where the value during the Hook call would call Cmds on
            //    the host server, and they would all happen and compare
            //    values BEFORE the hook even returned and hence BEFORE the
            //    actual value was even set.
            // put 'this.' onto stack for 'this.netId' below
            worker.Emit(OpCodes.Ldarg_0);
            // reader. for 'reader.Read()' below
            worker.Emit(OpCodes.Ldarg_1);
            // Read()
            worker.Emit(OpCodes.Call, Readers.GetReadFunc(WeaverTypes.Import<NetworkBehaviour.NetworkBehaviourSyncVar>()));
            // netId
            worker.Emit(OpCodes.Stfld, netIdField);

            if (hookMethod != null)
            {
                // call Hook(this.GetSyncVarGameObject/NetworkIdentity(reader.ReadPackedUInt32()))
                // because we send/receive the netID, not the GameObject/NetworkIdentity
                // but only if SyncVar changed. otherwise a client would
                // get hook calls for all initial values, even if they
                // didn't change from the default values on the client.
                // see also: https://github.com/vis2k/Mirror/issues/1278

                // IMPORTANT: for GameObjects/NetworkIdentities we usually
                //            use SyncVarGameObjectEqual to compare equality.
                //            in this case however, we can just use
                //            SyncVarEqual with the two uint netIds.
                //            => this is easier weaver code because we don't
                //               have to get the GameObject/NetworkIdentity
                //               from the uint netId
                //            => this is faster because we void one
                //               GetComponent call for GameObjects to get
                //               their NetworkIdentity when comparing.

                // Generates: if (!SyncVarEqual);
                Instruction syncVarEqualLabel = worker.Create(OpCodes.Nop);

                // 'this.' for 'this.SyncVarEqual'
                worker.Emit(OpCodes.Ldarg_0);
                // 'oldNetId'
                worker.Emit(OpCodes.Ldloc, oldNetId);
                // 'ref this.__netId'
                worker.Emit(OpCodes.Ldarg_0);
                worker.Emit(OpCodes.Ldflda, netIdField);
                // call the function
                GenericInstanceMethod syncVarEqualGm = new GenericInstanceMethod(WeaverTypes.syncVarEqualReference);
                syncVarEqualGm.GenericArguments.Add(netIdField.FieldType);
                worker.Emit(OpCodes.Call, syncVarEqualGm);
                worker.Emit(OpCodes.Brtrue, syncVarEqualLabel);

                // call the hook
                // Generates: OnValueChanged(oldValue, this.syncVar);
                SyncVarProcessor.WriteCallHookMethodUsingField(worker, hookMethod, oldSyncVar, syncVar);

                // Generates: end if (!SyncVarEqual);
                worker.Append(syncVarEqualLabel);
            }
        }


        /// <summary>
        /// [SyncVar] int/float/struct/etc.?
        /// </summary>
        /// <param name="syncVar"></param>
        /// <param name="serWorker"></param>
        /// <param name="deserialize"></param>
        /// <param name="initialState"></param>
        /// <param name="hookResult"></param>
        void DeserializeNormalField(FieldDefinition syncVar, ILProcessor serWorker, MethodDefinition deserialize, MethodDefinition hookMethod)
        {
            /*
             Generates code like:
                // for hook
                int oldValue = a;
                Networka = reader.ReadPackedInt32();
                if (!SyncVarEqual(oldValue, ref a))
                {
                    OnSetA(oldValue, Networka);
                }
             */

            MethodReference readFunc = Readers.GetReadFunc(syncVar.FieldType);
            if (readFunc == null)
            {
                Weaver.Error($"{syncVar.Name} has unsupported type. Use a supported Mirror type instead", syncVar);
                return;
            }

            // T oldValue = value;
            VariableDefinition oldValue = new VariableDefinition(syncVar.FieldType);
            deserialize.Body.Variables.Add(oldValue);
            serWorker.Append(serWorker.Create(OpCodes.Ldarg_0));
            serWorker.Append(serWorker.Create(OpCodes.Ldfld, syncVar));
            serWorker.Append(serWorker.Create(OpCodes.Stloc, oldValue));

            // read value and store in syncvar BEFORE calling the hook
            // -> this makes way more sense. by definition, the hook is
            //    supposed to be called after it was changed. not before.
            // -> setting it BEFORE calling the hook fixes the following bug:
            //    https://github.com/vis2k/Mirror/issues/1151 in host mode
            //    where the value during the Hook call would call Cmds on
            //    the host server, and they would all happen and compare
            //    values BEFORE the hook even returned and hence BEFORE the
            //    actual value was even set.
            // put 'this.' onto stack for 'this.syncvar' below
            serWorker.Append(serWorker.Create(OpCodes.Ldarg_0));
            // reader. for 'reader.Read()' below
            serWorker.Append(serWorker.Create(OpCodes.Ldarg_1));
            // reader.Read()
            serWorker.Append(serWorker.Create(OpCodes.Call, readFunc));
            // syncvar
            serWorker.Append(serWorker.Create(OpCodes.Stfld, syncVar));

            if (hookMethod != null)
            {
                // call hook
                // but only if SyncVar changed. otherwise a client would
                // get hook calls for all initial values, even if they
                // didn't change from the default values on the client.
                // see also: https://github.com/vis2k/Mirror/issues/1278

                // Generates: if (!SyncVarEqual);
                Instruction syncVarEqualLabel = serWorker.Create(OpCodes.Nop);

                // 'this.' for 'this.SyncVarEqual'
                serWorker.Append(serWorker.Create(OpCodes.Ldarg_0));
                // 'oldValue'
                serWorker.Append(serWorker.Create(OpCodes.Ldloc, oldValue));
                // 'ref this.syncVar'
                serWorker.Append(serWorker.Create(OpCodes.Ldarg_0));
                serWorker.Append(serWorker.Create(OpCodes.Ldflda, syncVar));
                // call the function
                GenericInstanceMethod syncVarEqualGm = new GenericInstanceMethod(WeaverTypes.syncVarEqualReference);
                syncVarEqualGm.GenericArguments.Add(syncVar.FieldType);
                serWorker.Append(serWorker.Create(OpCodes.Call, syncVarEqualGm));
                serWorker.Append(serWorker.Create(OpCodes.Brtrue, syncVarEqualLabel));

                // call the hook
                // Generates: OnValueChanged(oldValue, this.syncVar);
                SyncVarProcessor.WriteCallHookMethodUsingField(serWorker, hookMethod, oldValue, syncVar);

                // Generates: end if (!SyncVarEqual);
                serWorker.Append(syncVarEqualLabel);
            }
        }

        void GenerateDeSerialization()
        {
            Weaver.DLog(netBehaviourSubclass, "  GenerateDeSerialization");

>>>>>>> origin/alpha_merge
            const string DeserializeMethodName = "DeserializeSyncVars";
            if (netBehaviourSubclass.GetMethod(DeserializeMethodName) != null)
                return;

            if (syncVars.Count == 0)
            {
                // no synvars,  no need for custom OnDeserialize
                return;
            }

            MethodDefinition serialize = new MethodDefinition(DeserializeMethodName,
                    MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig,
<<<<<<< HEAD
                    weaverTypes.Import(typeof(void)));

            serialize.Parameters.Add(new ParameterDefinition("reader", ParameterAttributes.None, weaverTypes.Import<NetworkReader>()));
            serialize.Parameters.Add(new ParameterDefinition("initialState", ParameterAttributes.None, weaverTypes.Import<bool>()));
            ILProcessor serWorker = serialize.Body.GetILProcessor();
            // setup local for dirty bits
            serialize.Body.InitLocals = true;
            VariableDefinition dirtyBitsLocal = new VariableDefinition(weaverTypes.Import<long>());
            serialize.Body.Variables.Add(dirtyBitsLocal);

            MethodReference baseDeserialize = Resolvers.TryResolveMethodInParents(netBehaviourSubclass.BaseType, assembly, DeserializeMethodName);
=======
                    WeaverTypes.Import(typeof(void)));

            serialize.Parameters.Add(new ParameterDefinition("reader", ParameterAttributes.None, WeaverTypes.Import<NetworkReader>()));
            serialize.Parameters.Add(new ParameterDefinition("initialState", ParameterAttributes.None, WeaverTypes.Import<bool>()));
            ILProcessor serWorker = serialize.Body.GetILProcessor();
            // setup local for dirty bits
            serialize.Body.InitLocals = true;
            VariableDefinition dirtyBitsLocal = new VariableDefinition(WeaverTypes.Import<long>());
            serialize.Body.Variables.Add(dirtyBitsLocal);

            MethodReference baseDeserialize = Resolvers.TryResolveMethodInParents(netBehaviourSubclass.BaseType, Weaver.CurrentAssembly, DeserializeMethodName);
>>>>>>> origin/alpha_merge
            if (baseDeserialize != null)
            {
                // base
                serWorker.Append(serWorker.Create(OpCodes.Ldarg_0));
                // reader
                serWorker.Append(serWorker.Create(OpCodes.Ldarg_1));
                // initialState
                serWorker.Append(serWorker.Create(OpCodes.Ldarg_2));
                serWorker.Append(serWorker.Create(OpCodes.Call, baseDeserialize));
            }

            // Generates: if (initialState);
            Instruction initialStateLabel = serWorker.Create(OpCodes.Nop);

            serWorker.Append(serWorker.Create(OpCodes.Ldarg_2));
            serWorker.Append(serWorker.Create(OpCodes.Brfalse, initialStateLabel));

            foreach (FieldDefinition syncVar in syncVars)
            {
<<<<<<< HEAD
                DeserializeField(syncVar, serWorker, ref WeavingFailed);
=======
                DeserializeField(syncVar, serWorker, serialize);
>>>>>>> origin/alpha_merge
            }

            serWorker.Append(serWorker.Create(OpCodes.Ret));

            // Generates: end if (initialState);
            serWorker.Append(initialStateLabel);

            // get dirty bits
            serWorker.Append(serWorker.Create(OpCodes.Ldarg_1));
<<<<<<< HEAD
            serWorker.Append(serWorker.Create(OpCodes.Call, readers.GetReadFunc(weaverTypes.Import<ulong>(), ref WeavingFailed)));
=======
            serWorker.Append(serWorker.Create(OpCodes.Call, Readers.GetReadFunc(WeaverTypes.Import<ulong>())));
>>>>>>> origin/alpha_merge
            serWorker.Append(serWorker.Create(OpCodes.Stloc_0));

            // conditionally read each syncvar
            // start at number of syncvars in parent
<<<<<<< HEAD
            int dirtyBit = syncVarAccessLists.GetSyncVarStart(netBehaviourSubclass.BaseType.FullName);
=======
            int dirtyBit = Weaver.WeaveLists.GetSyncVarStart(netBehaviourSubclass.BaseType.FullName);
>>>>>>> origin/alpha_merge
            foreach (FieldDefinition syncVar in syncVars)
            {
                Instruction varLabel = serWorker.Create(OpCodes.Nop);

                // check if dirty bit is set
                serWorker.Append(serWorker.Create(OpCodes.Ldloc_0));
                serWorker.Append(serWorker.Create(OpCodes.Ldc_I8, 1L << dirtyBit));
                serWorker.Append(serWorker.Create(OpCodes.And));
                serWorker.Append(serWorker.Create(OpCodes.Brfalse, varLabel));

<<<<<<< HEAD
                DeserializeField(syncVar, serWorker, ref WeavingFailed);
=======
                DeserializeField(syncVar, serWorker, serialize);
>>>>>>> origin/alpha_merge

                serWorker.Append(varLabel);
                dirtyBit += 1;
            }

<<<<<<< HEAD
            // add a log message if needed for debugging
            //serWorker.Append(serWorker.Create(OpCodes.Ldstr, $"Injected Deserialize {netBehaviourSubclass.Name}"));
            //serWorker.Append(serWorker.Create(OpCodes.Call, WeaverTypes.logErrorReference));
=======
            if (Weaver.GenerateLogErrors)
            {
                serWorker.Append(serWorker.Create(OpCodes.Ldstr, "Injected Deserialize " + netBehaviourSubclass.Name));
                serWorker.Append(serWorker.Create(OpCodes.Call, WeaverTypes.logErrorReference));
            }
>>>>>>> origin/alpha_merge

            serWorker.Append(serWorker.Create(OpCodes.Ret));
            netBehaviourSubclass.Methods.Add(serialize);
        }

<<<<<<< HEAD
        public static bool ReadArguments(MethodDefinition method, Readers readers, Logger Log, ILProcessor worker, RemoteCallType callType, ref bool WeavingFailed)
=======
        public static bool ReadArguments(MethodDefinition method, ILProcessor worker, RemoteCallType callType)
>>>>>>> origin/alpha_merge
        {
            // read each argument
            // example result
            /*
            CallCmdDoSomething(reader.ReadPackedInt32(), reader.ReadNetworkIdentity());
             */

            bool skipFirst = callType == RemoteCallType.TargetRpc
                && TargetRpcProcessor.HasNetworkConnectionParameter(method);

            // arg of calling  function, arg 0 is "this" so start counting at 1
            int argNum = 1;
            foreach (ParameterDefinition param in method.Parameters)
            {
                // NetworkConnection is not sent via the NetworkWriter so skip it here
                // skip first for NetworkConnection in TargetRpc
                if (argNum == 1 && skipFirst)
                {
                    argNum += 1;
                    continue;
                }
                // skip SenderConnection in Command
                if (IsSenderConnection(param, callType))
                {
                    argNum += 1;
                    continue;
                }


<<<<<<< HEAD
                MethodReference readFunc = readers.GetReadFunc(param.ParameterType, ref WeavingFailed);

                if (readFunc == null)
                {
                    Log.Error($"{method.Name} has invalid parameter {param}.  Unsupported type {param.ParameterType},  use a supported Mirror type instead", method);
                    WeavingFailed = true;
=======
                MethodReference readFunc = Readers.GetReadFunc(param.ParameterType);

                if (readFunc == null)
                {
                    Weaver.Error($"{method.Name} has invalid parameter {param}.  Unsupported type {param.ParameterType},  use a supported Mirror type instead", method);
>>>>>>> origin/alpha_merge
                    return false;
                }

                worker.Emit(OpCodes.Ldarg_1);
                worker.Emit(OpCodes.Call, readFunc);

                // conversion.. is this needed?
                if (param.ParameterType.Is<float>())
                {
                    worker.Emit(OpCodes.Conv_R4);
                }
                else if (param.ParameterType.Is<double>())
                {
                    worker.Emit(OpCodes.Conv_R8);
                }
            }
            return true;
        }

<<<<<<< HEAD
        public static void AddInvokeParameters(WeaverTypes weaverTypes, ICollection<ParameterDefinition> collection)
        {
            collection.Add(new ParameterDefinition("obj", ParameterAttributes.None, weaverTypes.Import<NetworkBehaviour>()));
            collection.Add(new ParameterDefinition("reader", ParameterAttributes.None, weaverTypes.Import<NetworkReader>()));
            // senderConnection is only used for commands but NetworkBehaviour.CmdDelegate is used for all remote calls
            collection.Add(new ParameterDefinition("senderConnection", ParameterAttributes.None, weaverTypes.Import<NetworkConnectionToClient>()));
        }

        // check if a Command/TargetRpc/Rpc function & parameters are valid for weaving
        public bool ValidateRemoteCallAndParameters(MethodDefinition method, RemoteCallType callType, ref bool WeavingFailed)
        {
            if (method.IsStatic)
            {
                Log.Error($"{method.Name} must not be static", method);
                WeavingFailed = true;
                return false;
            }

            return ValidateFunction(method, ref WeavingFailed) &&
                   ValidateParameters(method, callType, ref WeavingFailed);
        }

        // check if a Command/TargetRpc/Rpc function is valid for weaving
        bool ValidateFunction(MethodReference md, ref bool WeavingFailed)
        {
            if (md.ReturnType.Is<System.Collections.IEnumerator>())
            {
                Log.Error($"{md.Name} cannot be a coroutine", md);
                WeavingFailed = true;
=======
        public static void AddInvokeParameters(ICollection<ParameterDefinition> collection)
        {
            collection.Add(new ParameterDefinition("obj", ParameterAttributes.None, WeaverTypes.Import<NetworkBehaviour>()));
            collection.Add(new ParameterDefinition("reader", ParameterAttributes.None, WeaverTypes.Import<NetworkReader>()));
            // senderConnection is only used for commands but NetworkBehaviour.CmdDelegate is used for all remote calls
            collection.Add(new ParameterDefinition("senderConnection", ParameterAttributes.None, WeaverTypes.Import<NetworkConnectionToClient>()));
        }

        // check if a Command/TargetRpc/Rpc function & parameters are valid for weaving
        public static bool ValidateRemoteCallAndParameters(MethodDefinition method, RemoteCallType callType)
        {
            if (method.IsStatic)
            {
                Weaver.Error($"{method.Name} must not be static", method);
                return false;
            }

            return ValidateFunction(method) &&
                   ValidateParameters(method, callType);
        }

        // check if a Command/TargetRpc/Rpc function is valid for weaving
        static bool ValidateFunction(MethodReference md)
        {
            if (md.ReturnType.Is<System.Collections.IEnumerator>())
            {
                Weaver.Error($"{md.Name} cannot be a coroutine", md);
>>>>>>> origin/alpha_merge
                return false;
            }
            if (!md.ReturnType.Is(typeof(void)))
            {
<<<<<<< HEAD
                Log.Error($"{md.Name} cannot return a value.  Make it void instead", md);
                WeavingFailed = true;
=======
                Weaver.Error($"{md.Name} cannot return a value.  Make it void instead", md);
>>>>>>> origin/alpha_merge
                return false;
            }
            if (md.HasGenericParameters)
            {
<<<<<<< HEAD
                Log.Error($"{md.Name} cannot have generic parameters", md);
                WeavingFailed = true;
=======
                Weaver.Error($"{md.Name} cannot have generic parameters", md);
>>>>>>> origin/alpha_merge
                return false;
            }
            return true;
        }

        // check if all Command/TargetRpc/Rpc function's parameters are valid for weaving
<<<<<<< HEAD
        bool ValidateParameters(MethodReference method, RemoteCallType callType, ref bool WeavingFailed)
=======
        static bool ValidateParameters(MethodReference method, RemoteCallType callType)
>>>>>>> origin/alpha_merge
        {
            for (int i = 0; i < method.Parameters.Count; ++i)
            {
                ParameterDefinition param = method.Parameters[i];
<<<<<<< HEAD
                if (!ValidateParameter(method, param, callType, i == 0, ref WeavingFailed))
=======
                if (!ValidateParameter(method, param, callType, i == 0))
>>>>>>> origin/alpha_merge
                {
                    return false;
                }
            }
            return true;
        }

        // validate parameters for a remote function call like Rpc/Cmd
<<<<<<< HEAD
        bool ValidateParameter(MethodReference method, ParameterDefinition param, RemoteCallType callType, bool firstParam, ref bool WeavingFailed)
        {
            // need to check this before any type lookups since those will fail since generic types don't resolve
            if (param.ParameterType.IsGenericParameter)
            {
                Log.Error($"{method.Name} cannot have generic parameters", method);
                WeavingFailed = true;
                return false;
            }

=======
        static bool ValidateParameter(MethodReference method, ParameterDefinition param, RemoteCallType callType, bool firstParam)
        {
>>>>>>> origin/alpha_merge
            bool isNetworkConnection = param.ParameterType.Is<NetworkConnection>();
            bool isSenderConnection = IsSenderConnection(param, callType);

            if (param.IsOut)
            {
<<<<<<< HEAD
                Log.Error($"{method.Name} cannot have out parameters", method);
                WeavingFailed = true;
                return false;
            }

=======
                Weaver.Error($"{method.Name} cannot have out parameters", method);
                return false;
            }


>>>>>>> origin/alpha_merge
            // if not SenderConnection And not TargetRpc NetworkConnection first param
            if (!isSenderConnection && isNetworkConnection && !(callType == RemoteCallType.TargetRpc && firstParam))
            {
                if (callType == RemoteCallType.Command)
                {
<<<<<<< HEAD
                    Log.Error($"{method.Name} has invalid parameter {param}, Cannot pass NetworkConnections. Instead use 'NetworkConnectionToClient conn = null' to get the sender's connection on the server", method);
                }
                else
                {
                    Log.Error($"{method.Name} has invalid parameter {param}. Cannot pass NetworkConnections", method);
                }
                WeavingFailed = true;
=======
                    Weaver.Error($"{method.Name} has invalid parameter {param}, Cannot pass NetworkConnections. Instead use 'NetworkConnectionToClient conn = null' to get the sender's connection on the server", method);
                }
                else
                {
                    Weaver.Error($"{method.Name} has invalid parameter {param}. Cannot pass NetworkConnections", method);
                }
>>>>>>> origin/alpha_merge
                return false;
            }

            // sender connection can be optional
            if (param.IsOptional && !isSenderConnection)
            {
<<<<<<< HEAD
                Log.Error($"{method.Name} cannot have optional parameters", method);
                WeavingFailed = true;
=======
                Weaver.Error($"{method.Name} cannot have optional parameters", method);
>>>>>>> origin/alpha_merge
                return false;
            }

            return true;
        }

        public static bool IsSenderConnection(ParameterDefinition param, RemoteCallType callType)
        {
            if (callType != RemoteCallType.Command)
            {
                return false;
            }

            TypeReference type = param.ParameterType;

            return type.Is<NetworkConnectionToClient>()
                || type.Resolve().IsDerivedFrom<NetworkConnectionToClient>();
        }

<<<<<<< HEAD
        void ProcessMethods(ref bool WeavingFailed)
=======
        void ProcessMethods()
>>>>>>> origin/alpha_merge
        {
            HashSet<string> names = new HashSet<string>();

            // copy the list of methods because we will be adding methods in the loop
            List<MethodDefinition> methods = new List<MethodDefinition>(netBehaviourSubclass.Methods);
            // find command and RPC functions
            foreach (MethodDefinition md in methods)
            {
                foreach (CustomAttribute ca in md.CustomAttributes)
                {
                    if (ca.AttributeType.Is<CommandAttribute>())
                    {
<<<<<<< HEAD
                        ProcessCommand(names, md, ca, ref WeavingFailed);
=======
                        ProcessCommand(names, md, ca);
>>>>>>> origin/alpha_merge
                        break;
                    }

                    if (ca.AttributeType.Is<TargetRpcAttribute>())
                    {
<<<<<<< HEAD
                        ProcessTargetRpc(names, md, ca, ref WeavingFailed);
=======
                        ProcessTargetRpc(names, md, ca);
>>>>>>> origin/alpha_merge
                        break;
                    }

                    if (ca.AttributeType.Is<ClientRpcAttribute>())
                    {
<<<<<<< HEAD
                        ProcessClientRpc(names, md, ca, ref WeavingFailed);
=======
                        ProcessClientRpc(names, md, ca);
>>>>>>> origin/alpha_merge
                        break;
                    }
                }
            }
        }

<<<<<<< HEAD
        void ProcessClientRpc(HashSet<string> names, MethodDefinition md, CustomAttribute clientRpcAttr, ref bool WeavingFailed)
        {
            if (md.IsAbstract)
            {
                Log.Error("Abstract ClientRpc are currently not supported, use virtual method instead", md);
                WeavingFailed = true;
                return;
            }

            if (!ValidateRemoteCallAndParameters(md, RemoteCallType.ClientRpc, ref WeavingFailed))
=======
        void ProcessClientRpc(HashSet<string> names, MethodDefinition md, CustomAttribute clientRpcAttr)
        {
            if (md.IsAbstract)
            {
                Weaver.Error("Abstract ClientRpc are currently not supported, use virtual method instead", md);
                return;
            }

            if (!ValidateRemoteCallAndParameters(md, RemoteCallType.ClientRpc))
>>>>>>> origin/alpha_merge
            {
                return;
            }

<<<<<<< HEAD
=======
            if (names.Contains(md.Name))
            {
                Weaver.Error($"Duplicate ClientRpc name {md.Name}", md);
                return;
            }

>>>>>>> origin/alpha_merge
            bool includeOwner = clientRpcAttr.GetField("includeOwner", true);

            names.Add(md.Name);
            clientRpcs.Add(new ClientRpcResult
            {
                method = md,
                includeOwner = includeOwner
            });

<<<<<<< HEAD
            MethodDefinition rpcCallFunc = RpcProcessor.ProcessRpcCall(weaverTypes, writers, Log, netBehaviourSubclass, md, clientRpcAttr, ref WeavingFailed);
            // need null check here because ProcessRpcCall returns null if it can't write all the args
            if (rpcCallFunc == null) { return; }

            MethodDefinition rpcFunc = RpcProcessor.ProcessRpcInvoke(weaverTypes, writers, readers, Log, netBehaviourSubclass, md, rpcCallFunc, ref WeavingFailed);
=======
            MethodDefinition rpcCallFunc = RpcProcessor.ProcessRpcCall(netBehaviourSubclass, md, clientRpcAttr);
            // need null check here because ProcessRpcCall returns null if it can't write all the args
            if (rpcCallFunc == null) { return; }

            MethodDefinition rpcFunc = RpcProcessor.ProcessRpcInvoke(netBehaviourSubclass, md, rpcCallFunc);
>>>>>>> origin/alpha_merge
            if (rpcFunc != null)
            {
                clientRpcInvocationFuncs.Add(rpcFunc);
            }
        }

<<<<<<< HEAD
        void ProcessTargetRpc(HashSet<string> names, MethodDefinition md, CustomAttribute targetRpcAttr, ref bool WeavingFailed)
        {
            if (md.IsAbstract)
            {
                Log.Error("Abstract TargetRpc are currently not supported, use virtual method instead", md);
                WeavingFailed = true;
                return;
            }

            if (!ValidateRemoteCallAndParameters(md, RemoteCallType.TargetRpc, ref WeavingFailed))
                return;

            names.Add(md.Name);
            targetRpcs.Add(md);

            MethodDefinition rpcCallFunc = TargetRpcProcessor.ProcessTargetRpcCall(weaverTypes, writers, Log, netBehaviourSubclass, md, targetRpcAttr, ref WeavingFailed);

            MethodDefinition rpcFunc = TargetRpcProcessor.ProcessTargetRpcInvoke(weaverTypes, readers, Log, netBehaviourSubclass, md, rpcCallFunc, ref WeavingFailed);
=======
        void ProcessTargetRpc(HashSet<string> names, MethodDefinition md, CustomAttribute targetRpcAttr)
        {
            if (md.IsAbstract)
            {
                Weaver.Error("Abstract TargetRpc are currently not supported, use virtual method instead", md);
                return;
            }

            if (!ValidateRemoteCallAndParameters(md, RemoteCallType.TargetRpc))
                return;

            if (names.Contains(md.Name))
            {
                Weaver.Error($"Duplicate Target Rpc name {md.Name}", md);
                return;
            }
            names.Add(md.Name);
            targetRpcs.Add(md);

            MethodDefinition rpcCallFunc = TargetRpcProcessor.ProcessTargetRpcCall(netBehaviourSubclass, md, targetRpcAttr);

            MethodDefinition rpcFunc = TargetRpcProcessor.ProcessTargetRpcInvoke(netBehaviourSubclass, md, rpcCallFunc);
>>>>>>> origin/alpha_merge
            if (rpcFunc != null)
            {
                targetRpcInvocationFuncs.Add(rpcFunc);
            }
        }

<<<<<<< HEAD
        void ProcessCommand(HashSet<string> names, MethodDefinition md, CustomAttribute commandAttr, ref bool WeavingFailed)
        {
            if (md.IsAbstract)
            {
                Log.Error("Abstract Commands are currently not supported, use virtual method instead", md);
                WeavingFailed = true;
                return;
            }

            if (!ValidateRemoteCallAndParameters(md, RemoteCallType.Command, ref WeavingFailed))
                return;

=======
        void ProcessCommand(HashSet<string> names, MethodDefinition md, CustomAttribute commandAttr)
        {
            if (md.IsAbstract)
            {
                Weaver.Error("Abstract Commands are currently not supported, use virtual method instead", md);
                return;
            }

            if (!ValidateRemoteCallAndParameters(md, RemoteCallType.Command))
                return;

            if (names.Contains(md.Name))
            {
                Weaver.Error($"Duplicate Command name {md.Name}", md);
                return;
            }

>>>>>>> origin/alpha_merge
            bool requiresAuthority = commandAttr.GetField("requiresAuthority", true);

            names.Add(md.Name);
            commands.Add(new CmdResult
            {
                method = md,
                requiresAuthority = requiresAuthority
            });

<<<<<<< HEAD
            MethodDefinition cmdCallFunc = CommandProcessor.ProcessCommandCall(weaverTypes, writers, Log, netBehaviourSubclass, md, commandAttr, ref WeavingFailed);

            MethodDefinition cmdFunc = CommandProcessor.ProcessCommandInvoke(weaverTypes, readers, Log, netBehaviourSubclass, md, cmdCallFunc, ref WeavingFailed);
=======
            MethodDefinition cmdCallFunc = CommandProcessor.ProcessCommandCall(netBehaviourSubclass, md, commandAttr);

            MethodDefinition cmdFunc = CommandProcessor.ProcessCommandInvoke(netBehaviourSubclass, md, cmdCallFunc);
>>>>>>> origin/alpha_merge
            if (cmdFunc != null)
            {
                commandInvocationFuncs.Add(cmdFunc);
            }
        }
    }
}
