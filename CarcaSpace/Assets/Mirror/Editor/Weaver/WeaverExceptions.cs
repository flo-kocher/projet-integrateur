using System;
<<<<<<< HEAD
using System.Runtime.Serialization;
=======
>>>>>>> origin/alpha_merge
using Mono.CecilX;

namespace Mirror.Weaver
{
    [Serializable]
    public abstract class WeaverException : Exception
    {
        public MemberReference MemberReference { get; }

        protected WeaverException(string message, MemberReference member) : base(message)
        {
            MemberReference = member;
        }

<<<<<<< HEAD
        protected WeaverException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) {}
=======
        protected WeaverException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext) : base(serializationInfo, streamingContext) {}
>>>>>>> origin/alpha_merge
    }

    [Serializable]
    public class GenerateWriterException : WeaverException
    {
        public GenerateWriterException(string message, MemberReference member) : base(message, member) {}
<<<<<<< HEAD
        protected GenerateWriterException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) {}
=======
        protected GenerateWriterException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext) : base(serializationInfo, streamingContext) {}
>>>>>>> origin/alpha_merge
    }
}
