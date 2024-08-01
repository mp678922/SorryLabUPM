using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SorryLab.Spring {
    //public enum SpringOption { Hair /*0.075,3*/, Grass/*0.05,7.5*/, Rubber/*0.2,25*/ }
    abstract public class Spring {
        protected float zeta;
        protected float omega;
        public void SetStringProperty(float zeta, float omega) {
            this.zeta = zeta;
            this.omega = omega;
        }
    }

    public class SpringFloat : Spring {

        float value;
        float velocity = 0f;
        float target;

        public SpringFloat(float value, float target, float zeta = 0.023f, float omega = 3.14159265359f) {
            this.value = value;
            this.target = target;
            this.zeta = zeta;
            this.omega = omega;
        }

        public float Update(float target, bool usingRealTime = false) {
            this.target = target;
            return Update(usingRealTime);
        }

        public float Update(bool usingRealTime = false) {
            DoSpring(ref value, ref velocity, target, zeta, omega, usingRealTime ? Time.unscaledDeltaTime : Time.deltaTime);
            return value;
        }

        /*
            zeta  - damping ratio     (input)
            omega - angular frequency (input)
            h     - time step         (input)
        */

        static void DoSpring(ref float value, ref float velocity, float target, float zeta, float omega, float h) {
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

    public class SpringVector2 : Spring {

        Vector2 value;
        Vector2 velocity = Vector2.zero;
        Vector2 target;

        public SpringVector2(Vector2 value, Vector2 target, float zeta = 0.1f, float omega = 3.14159265359f) {
            this.value = value;
            this.target = target;
            this.zeta = zeta;
            this.omega = omega;
        }

        public Vector2 FixedUpdate(Vector2 target, bool usingRealTime = false) {
            this.target = target;
            return Update(usingRealTime);
        }

        public Vector2 FixedUpdate(bool usingRealTime = false) {
            DoSpring(ref value, ref velocity, target, zeta, omega, usingRealTime ? Time.fixedUnscaledDeltaTime : Time.fixedDeltaTime);
            return value;
        }

        public Vector2 Update(Vector2 target, bool usingRealTime = false) {
            this.target = target;
            return Update(usingRealTime);
        }

        public Vector2 Update(bool usingRealTime = false) {
            DoSpring(ref value, ref velocity, target, zeta, omega, usingRealTime ? Time.unscaledDeltaTime : Time.deltaTime);
            return value;
        }

        static void DoSpring(ref Vector2 value, ref Vector2 velocity, Vector2 target, float zeta, float omega, float h) {
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

    public class SpringVector3 : Spring {

        Vector3 value;
        Vector3 velocity = Vector3.zero;
        Vector3 target;

        public SpringVector3(Vector3 value, Vector3 target, float zeta = 0.1f, float omega = 3.14159265359f) {
            this.value = value;
            this.target = target;
            this.zeta = zeta;
            this.omega = omega;
        }

        public Vector3 Update(Vector3 target, bool usingRealTime = false) {
            this.target = target;
            return Update(usingRealTime);
        }

        public Vector3 Update(bool usingRealTime = false) {
            DoSpring(ref value, ref velocity, target, zeta, omega, usingRealTime ? Time.unscaledDeltaTime : Time.deltaTime);
            return value;
        }

        static void DoSpring(ref Vector3 value, ref Vector3 velocity, Vector3 target, float zeta, float omega, float h) {
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

    public class SpringVector4 {

        Vector4 value;
        Vector4 velocity = Vector4.zero;
        Vector4 target;
        float zeta;
        float omega;

        public SpringVector4(Vector4 value, Vector4 target, float zeta = 0.1f, float omega = 3.14159265359f) {
            this.value = value;
            this.target = target;
            this.zeta = zeta;
            this.omega = omega;
        }

        public Vector4 Update(Vector4 target, bool usingRealTime = false) {
            this.target = target;
            return Update(usingRealTime);
        }

        public Vector4 Update(bool usingRealTime = false) {
            DoSpring(ref value, ref velocity, target, zeta, omega, usingRealTime ? Time.unscaledDeltaTime : Time.deltaTime);
            return value;
        }

        static void DoSpring(ref Vector4 value, ref Vector4 velocity, Vector4 target, float zeta, float omega, float h) {
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

