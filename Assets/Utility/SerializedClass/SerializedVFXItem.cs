using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerializedClass.VFXClass {
    [Serializable]
    public class SerializedVFXItem {
        public string actionName;
        public ParticleSystem VFX;
    }
}