using UnityEngine;
namespace SorryLab.Spring {
    public class SpringVector3 : Spring<Vector3> {
        public SpringVector3(Vector3 value, Vector3 target, float zeta = 0.1f, float omega = 3.14159265359f) {
            this.value = value;
            this.target = target;
            this.zeta = zeta;
            this.omega = omega;
        }
        public override float GetVelocity() { return velocity.magnitude; }
        protected override void DoSpring(ref Vector3 value, ref Vector3 velocity, Vector3 target, float zeta, float omega, float h) {
            float f = 1.0f + 2.0f * h * zeta * omega;
            float oo = omega * omega;
            float hoo = h * oo;
            float hhoo = h * hoo;
            float detInv = 1.0f / (f + hhoo);
            Vector3 detX = (value * f) + (velocity * h) + (target * hhoo);
            Vector3 detV = velocity + hoo * (target - value);
            value = detX * detInv;
            velocity = detV * detInv;
        }
    }
}
