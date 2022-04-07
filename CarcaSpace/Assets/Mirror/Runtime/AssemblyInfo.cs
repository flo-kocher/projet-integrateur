using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Mirror.Tests.Common")]
[assembly: InternalsVisibleTo("Mirror.Tests")]
<<<<<<< HEAD
// need to use Unity.*.CodeGen assembly name to import Unity.CompilationPipeline
// for ILPostProcessor tests.
[assembly: InternalsVisibleTo("Unity.Mirror.Tests.CodeGen")]
=======
>>>>>>> origin/alpha_merge
[assembly: InternalsVisibleTo("Mirror.Tests.Generated")]
[assembly: InternalsVisibleTo("Mirror.Tests.Runtime")]
[assembly: InternalsVisibleTo("Mirror.Tests.Performance.Editor")]
[assembly: InternalsVisibleTo("Mirror.Tests.Performance.Runtime")]
[assembly: InternalsVisibleTo("Mirror.Editor")]
