using Mono.CecilX;
using Mono.CecilX.Cil;

namespace Mirror.Weaver
{
<<<<<<< HEAD
    // Processes [Rpc] methods in NetworkBehaviour
    public static class RpcProcessor
    {
        public static MethodDefinition ProcessRpcInvoke(WeaverTypes weaverTypes, Writers writers, Readers readers, Logger Log, TypeDefinition td, MethodDefinition md, MethodDefinition rpcCallFunc, ref bool WeavingFailed)
        {
            string rpcName = Weaver.GenerateMethodName(Weaver.InvokeRpcPrefix, md);

            MethodDefinition rpc = new MethodDefinition(rpcName, MethodAttributes.Family | MethodAttributes.Static | MethodAttributes.HideBySig,
                                                        weaverTypes.Import(typeof(void)));
=======
    /// <summary>
    /// Processes [Rpc] methods in NetworkBehaviour
    /// </summary>
    public static class RpcProcessor
    {
        public static MethodDefinition ProcessRpcInvoke(TypeDefinition td, MethodDefinition md, MethodDefinition rpcCallFunc)
        {
            MethodDefinition rpc = new MethodDefinition(
                Weaver.InvokeRpcPrefix + md.Name,
                MethodAttributes.Family | MethodAttributes.Static | MethodAttributes.HideBySig,
                WeaverTypes.Import(typeof(void)));
>>>>>>> origin/alpha_merge

            ILProcessor worker = rpc.Body.GetILProcessor();
            Instruction label = worker.Create(OpCodes.Nop);

<<<<<<< HEAD
            NetworkBehaviourProcessor.WriteClientActiveCheck(worker, weaverTypes, md.Name, label, "RPC");
=======
            NetworkBehaviourProcessor.WriteClientActiveCheck(worker, md.Name, label, "RPC");
>>>>>>> origin/alpha_merge

            // setup for reader
            worker.Emit(OpCodes.Ldarg_0);
            worker.Emit(OpCodes.Castclass, td);

<<<<<<< HEAD
            if (!NetworkBehaviourProcessor.ReadArguments(md, readers, Log, worker, RemoteCallType.ClientRpc, ref WeavingFailed))
=======
            if (!NetworkBehaviourProcessor.ReadArguments(md, worker, RemoteCallType.ClientRpc))
>>>>>>> origin/alpha_merge
                return null;

            // invoke actual command function
            worker.Emit(OpCodes.Callvirt, rpcCallFunc);
            worker.Emit(OpCodes.Ret);

<<<<<<< HEAD
            NetworkBehaviourProcessor.AddInvokeParameters(weaverTypes, rpc.Parameters);
=======
            NetworkBehaviourProcessor.AddInvokeParameters(rpc.Parameters);
>>>>>>> origin/alpha_merge
            td.Methods.Add(rpc);
            return rpc;
        }

        /*
         * generates code like:

            public void RpcTest (int param)
            {
                NetworkWriter writer = new NetworkWriter ();
                writer.WritePackedUInt32((uint)param);
                base.SendRPCInternal(typeof(class),"RpcTest", writer, 0);
            }
            public void CallRpcTest (int param)
            {
                // whatever the user did before
            }

            Originally HLAPI put the send message code inside the Call function
            and then proceeded to replace every call to RpcTest with CallRpcTest

            This method moves all the user's code into the "CallRpc" method
            and replaces the body of the original method with the send message code.
            This way we do not need to modify the code anywhere else,  and this works
            correctly in dependent assemblies
        */
<<<<<<< HEAD
        public static MethodDefinition ProcessRpcCall(WeaverTypes weaverTypes, Writers writers, Logger Log, TypeDefinition td, MethodDefinition md, CustomAttribute clientRpcAttr, ref bool WeavingFailed)
        {
            MethodDefinition rpc = MethodProcessor.SubstituteMethod(Log, td, md, ref WeavingFailed);

            ILProcessor worker = md.Body.GetILProcessor();

            NetworkBehaviourProcessor.WriteSetupLocals(worker, weaverTypes);

            // add a log message if needed for debugging
            //worker.Emit(OpCodes.Ldstr, $"Call ClientRpc function {md.Name}");
            //worker.Emit(OpCodes.Call, WeaverTypes.logErrorReference);

            NetworkBehaviourProcessor.WriteCreateWriter(worker, weaverTypes);

            // write all the arguments that the user passed to the Rpc call
            if (!NetworkBehaviourProcessor.WriteArguments(worker, writers, Log, md, RemoteCallType.ClientRpc, ref WeavingFailed))
                return null;

=======
        public static MethodDefinition ProcessRpcCall(TypeDefinition td, MethodDefinition md, CustomAttribute clientRpcAttr)
        {
            MethodDefinition rpc = MethodProcessor.SubstituteMethod(td, md);

            ILProcessor worker = md.Body.GetILProcessor();

            NetworkBehaviourProcessor.WriteSetupLocals(worker);

            if (Weaver.GenerateLogErrors)
            {
                worker.Emit(OpCodes.Ldstr, "Call ClientRpc function " + md.Name);
                worker.Emit(OpCodes.Call, WeaverTypes.logErrorReference);
            }

            NetworkBehaviourProcessor.WriteCreateWriter(worker);

            // write all the arguments that the user passed to the Rpc call
            if (!NetworkBehaviourProcessor.WriteArguments(worker, md, RemoteCallType.ClientRpc))
                return null;

            string rpcName = md.Name;
>>>>>>> origin/alpha_merge
            int channel = clientRpcAttr.GetField("channel", 0);
            bool includeOwner = clientRpcAttr.GetField("includeOwner", true);

            // invoke SendInternal and return
            // this
            worker.Emit(OpCodes.Ldarg_0);
<<<<<<< HEAD
            // pass full function name to avoid ClassA.Func <-> ClassB.Func collisions
            worker.Emit(OpCodes.Ldstr, md.FullName);
=======
            worker.Emit(OpCodes.Ldtoken, td);
            // invokerClass
            worker.Emit(OpCodes.Call, WeaverTypes.getTypeFromHandleReference);
            worker.Emit(OpCodes.Ldstr, rpcName);
>>>>>>> origin/alpha_merge
            // writer
            worker.Emit(OpCodes.Ldloc_0);
            worker.Emit(OpCodes.Ldc_I4, channel);
            // includeOwner ? 1 : 0
            worker.Emit(includeOwner ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
<<<<<<< HEAD
            worker.Emit(OpCodes.Callvirt, weaverTypes.sendRpcInternal);

            NetworkBehaviourProcessor.WriteRecycleWriter(worker, weaverTypes);
=======
            worker.Emit(OpCodes.Callvirt, WeaverTypes.sendRpcInternal);

            NetworkBehaviourProcessor.WriteRecycleWriter(worker);
>>>>>>> origin/alpha_merge

            worker.Emit(OpCodes.Ret);

            return rpc;
        }
    }
}
