using Mono.CecilX;

namespace Mirror.Weaver
{
<<<<<<< HEAD
    // only shows warnings in case we use SyncVars etc. for MonoBehaviour.
    static class MonoBehaviourProcessor
    {
        public static void Process(Logger Log, TypeDefinition td, ref bool WeavingFailed)
        {
            ProcessSyncVars(Log, td, ref WeavingFailed);
            ProcessMethods(Log, td, ref WeavingFailed);
        }

        static void ProcessSyncVars(Logger Log, TypeDefinition td, ref bool WeavingFailed)
=======
    /// <summary>
    /// only shows warnings in case we use SyncVars etc. for MonoBehaviour.
    /// </summary>
    static class MonoBehaviourProcessor
    {
        public static void Process(TypeDefinition td)
        {
            ProcessSyncVars(td);
            ProcessMethods(td);
        }

        static void ProcessSyncVars(TypeDefinition td)
>>>>>>> origin/alpha_merge
        {
            // find syncvars
            foreach (FieldDefinition fd in td.Fields)
            {
                if (fd.HasCustomAttribute<SyncVarAttribute>())
<<<<<<< HEAD
                {
                    Log.Error($"SyncVar {fd.Name} must be inside a NetworkBehaviour.  {td.Name} is not a NetworkBehaviour", fd);
                    WeavingFailed = true;
                }

                if (SyncObjectInitializer.ImplementsSyncObject(fd.FieldType))
                {
                    Log.Error($"{fd.Name} is a SyncObject and must be inside a NetworkBehaviour.  {td.Name} is not a NetworkBehaviour", fd);
                    WeavingFailed = true;
=======
                    Weaver.Error($"SyncVar {fd.Name} must be inside a NetworkBehaviour.  {td.Name} is not a NetworkBehaviour", fd);

                if (SyncObjectInitializer.ImplementsSyncObject(fd.FieldType))
                {
                    Weaver.Error($"{fd.Name} is a SyncObject and must be inside a NetworkBehaviour.  {td.Name} is not a NetworkBehaviour", fd);
>>>>>>> origin/alpha_merge
                }
            }
        }

<<<<<<< HEAD
        static void ProcessMethods(Logger Log, TypeDefinition td, ref bool WeavingFailed)
=======
        static void ProcessMethods(TypeDefinition td)
>>>>>>> origin/alpha_merge
        {
            // find command and RPC functions
            foreach (MethodDefinition md in td.Methods)
            {
                if (md.HasCustomAttribute<CommandAttribute>())
<<<<<<< HEAD
                {
                    Log.Error($"Command {md.Name} must be declared inside a NetworkBehaviour", md);
                    WeavingFailed = true;
                }
                if (md.HasCustomAttribute<ClientRpcAttribute>())
                {
                    Log.Error($"ClientRpc {md.Name} must be declared inside a NetworkBehaviour", md);
                    WeavingFailed = true;
                }
                if (md.HasCustomAttribute<TargetRpcAttribute>())
                {
                    Log.Error($"TargetRpc {md.Name} must be declared inside a NetworkBehaviour", md);
                    WeavingFailed = true;
                }
=======
                    Weaver.Error($"Command {md.Name} must be declared inside a NetworkBehaviour", md);
                if (md.HasCustomAttribute<ClientRpcAttribute>())
                    Weaver.Error($"ClientRpc {md.Name} must be declared inside a NetworkBehaviour", md);
                if (md.HasCustomAttribute<TargetRpcAttribute>())
                    Weaver.Error($"TargetRpc {md.Name} must be declared inside a NetworkBehaviour", md);
>>>>>>> origin/alpha_merge
            }
        }
    }
}
