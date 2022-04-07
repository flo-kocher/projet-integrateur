<<<<<<< HEAD
﻿using System;
using UnityEngine;
=======
﻿using UnityEngine;
>>>>>>> origin/alpha_merge

namespace Mirror.Experimental
{
    [DisallowMultipleComponent]
<<<<<<< HEAD
    // Deprecated 2022-01-18
    [Obsolete("Use the default NetworkTransform instead, it has proper snapshot interpolation.")]
    [AddComponentMenu("")]
=======
    [AddComponentMenu("Network/Experimental/NetworkTransformExperimental")]
    [HelpURL("https://mirror-networking.com/docs/Articles/Components/NetworkTransform.html")]
>>>>>>> origin/alpha_merge
    public class NetworkTransform : NetworkTransformBase
    {
        protected override Transform targetTransform => transform;
    }
}
