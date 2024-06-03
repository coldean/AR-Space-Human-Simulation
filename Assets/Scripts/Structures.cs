using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Structures
{
    [System.Serializable]
    public class PlaneData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Size;
    }

    [System.Serializable]
    public class LocationProbability
    {
        public Vector3 location; // 위치.
        public float probability; // 확률 (0~1).
        public int count; // 이 위치에서 생성할 'Person' 수.
    }
}