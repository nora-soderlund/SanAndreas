using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Data;
using Game.Data.Types;

namespace Game.World.Types {
    public class DrawDistance : MonoBehaviour
    {
        public float Distance;

        public void Start() {
            this.gameObject.SetActive(false);
        }
    }
}
