using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SorryLab.Spring {
    //public enum SpringOption { Hair /*0.075,3*/, Grass/*0.05,7.5*/, Rubber/*0.2,25*/ }
    /*
    zeta  - damping ratio     (input)
    omega - angular frequency (input)
    h     - time step         (input)
    */
    abstract public class Spring<T> {
        protected float zeta;
        protected float omega;
        protected T value;
        protected T velocity;
        protected T target;
        public void SetStringProperty(float zeta, float omega) {
            this.zeta = zeta;
            this.omega = omega;
        }
        public virtual void SetTargetAndClearVelocity(T target) {
            this.target = target;
            velocity = default;
        }
        public virtual T Update(T target, bool usingRealTime = false) {
            this.target = target;
            return Update(usingRealTime);
        }
        public T Update(bool usingRealTime = false) {
            DoSpring(ref value, ref velocity, target, zeta, omega, usingRealTime ? Time.unscaledDeltaTime : Time.deltaTime);
            return value;
        }
        public virtual float GetVelocity() { return 0f; }
        protected virtual void ClearVelocity() { velocity = default; }
        protected virtual void DoSpring(ref T value, ref T velocity, T target, float zeta, float omega, float h) { }
    }

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
    public class SpringVector2 : Spring<Vector2> {
        public SpringVector2(Vector2 value, Vector2 target, float zeta = 0.1f, float omega = 3.14159265359f) {
            this.value = value;
            this.target = target;
            this.zeta = zeta;
            this.omega = omega;
        }
        public override float GetVelocity() { return velocity.magnitude; }
        protected override void DoSpring(ref Vector2 value, ref Vector2 velocity, Vector2 target, float zeta, float omega, float h) {
            float f = 1.0f + 2.0f * h * zeta * omega;
            float oo = omega * omega;
            float hoo = h * oo;
            float hhoo = h * hoo;
            float detInv = 1.0f / (f + hhoo);
            Vector2 detX = (value * f) + (velocity * h) + (target * hhoo);
            Vector2 detV = velocity + hoo * (target - value);
            value = detX * detInv;
            velocity = detV * detInv;
        }
    }
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
    public class SpringVector4 : Spring<Vector4> {
        public SpringVector4(Vector4 value, Vector4 target, float zeta = 0.1f, float omega = 3.14159265359f) {
            this.value = value;
            this.target = target;
            this.zeta = zeta;
            this.omega = omega;
        }
        public override float GetVelocity() { return velocity.magnitude; }
        protected override void DoSpring(ref Vector4 value, ref Vector4 velocity, Vector4 target, float zeta, float omega, float h) {
            float f = 1.0f + 2.0f * h * zeta * omega;
            float oo = omega * omega;
            float hoo = h * oo;
            float hhoo = h * hoo;
            float detInv = 1.0f / (f + hhoo);
            Vector4 detX = (value * f) + (velocity * h) + (target * hhoo);
            Vector4 detV = velocity + hoo * (target - value);
            value = detX * detInv;
            velocity = detV * detInv;
        }
    }
}

