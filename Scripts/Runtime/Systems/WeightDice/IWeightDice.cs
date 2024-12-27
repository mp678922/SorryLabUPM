using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SorryLab {
    public interface IWeightDice<T> where T : class {
        public float GetWeight();
        public T GetValue();
    }
}
