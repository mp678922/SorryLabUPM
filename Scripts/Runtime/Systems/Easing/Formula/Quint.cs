using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorryLab.Easing {
    class Quint : EaseBase {
        public static double LerpIn(float a, float b, float t) {
            return Ease.LerpQuintIn(a, b, t);
        }
        public static double LerpOut(float a, float b, float t) {
            return Ease.LerpQuintOut(a, b, t);
        }
        public static double LerpInOut(float a, float b, float t) {
            return Ease.LerpQuintInOut(a, b, t);
        }
        public static double In(double t, double b, double c, double d) {
            return c * (t /= d) * t * t * t * t + b;
        }
        public static double Out(double t, double b, double c, double d) {
            return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
        }
        public static double InOut(double t, double b, double c, double d) {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
        }
    }
}