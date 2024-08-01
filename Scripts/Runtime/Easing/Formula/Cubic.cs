using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorryLab.Easing {
    class Cubic : EaseBase {
        public static double LerpIn(float a, float b, float t) {
            return Ease.LerpCubicIn(a, b, t);
        }
        public static double LerpOut(float a, float b, float t) {
            return Ease.LerpCubicOut(a, b, t);
        }
        public static double LerpInOut(float a, float b, float t) {
            return Ease.LerpCubicInOut(a, b, t);
        }
        public static double In(double t, double b, double c, double d) {
            return c * (t /= d) * t * t + b;
        }
        public static double Out(double t, double b, double c, double d) {
            return c * ((t = t / d - 1) * t * t + 1) + b;
        }
        public static double InOut(double t, double b, double c, double d) {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t + 2) + b;
        }
    }
}