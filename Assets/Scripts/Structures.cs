using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Structures
{
    [System.Serializable]
    public class PlaneData
    {
        public List<PlaneData> List;

        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Size;
    }
}