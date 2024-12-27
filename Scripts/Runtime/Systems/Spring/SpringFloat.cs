using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SorryLab.Spring {
    public class SpringFloat : Spring<float> {
        public SpringFloat(float value, float target, float zeta = 0.023f, float omega = 3.14159265359f) {
            this.value = value;
            this.target = target;
            this.zeta = zeta;
            this.omega = omega;
        }
        public override float GetVelocity() { return velocity; }
        protected override void DoSpring(ref float value, ref float velocity, float target, float zeta, float omega, float h) {
            float f = 1.0f + 2.0f * h * zeta * omega;
            float oo = omega * omega;
            float hoo = h * oo;
            float hhoo = h * hoo;
            float detInv = 1.0f / (f + hhoo);
            float detX = (value * f) + (velocity * h) + (target * hhoo);
            float detV = velocity + hoo * (target - value);
            value = detX * detInv;
            velocity = detV * detInv;
        }

    }
}
