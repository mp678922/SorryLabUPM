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
        public void SetSpringProperty(float zeta, float omega) {
            this.zeta = zeta;
            this.omega = omega;
        }
        public void SetTargetAndClearVelocity(T target) {
            this.target = target;
            velocity = default;
        }
        public T Update(T target, bool usingRealTime = false) {
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
}

