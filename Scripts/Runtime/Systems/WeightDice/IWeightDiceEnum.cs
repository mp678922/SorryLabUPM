using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SorryLab {
    public interface IWeightDiceEnum<T> where T : System.Enum {
        public float GetWeight();
        public T GetValue();
    }
}
