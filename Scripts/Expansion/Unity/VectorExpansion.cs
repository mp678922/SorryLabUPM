using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SorryLab.Expansion {
    static class VectorExpansion {

        /* Vector3 */
        static public Vector3 GetRound(this Vector3 target, int decimals) {
            Vector3 v3 = target;
            v3.x = (float)System.Math.Round(v3.x, decimals);
            v3.y = (float)System.Math.Round(v3.y, decimals);
            v3.z = (float)System.Math.Round(v3.z, decimals);
            return target;
        }

        /* Vector2 */
        static public Vector2 GetRound(this Vector2 target, int decimals) {
            Vector2 v2 = target;
            v2.x = (float)System.Math.Round(v2.x, decimals);
            v2.y = (float)System.Math.Round(v2.y, decimals);
            return target;
        }

        static public Vector3 ToVector3(this Vector2 v2) {
            Vector3 v3 = new Vector3(v2.x, v2.y, 0f);
            return v3;
        }

    }
}

