using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorryLab.Easing {
    class Circ : EaseBase {
        public static double LerpIn(float a, float b, float t) {
            return Ease.LerpCircIn(a, b, t);
        }
        public static double LerpOut(float a, float b, float t) {
            return Ease.LerpCircOut(a, b, t);
        }
        public static double LerpInOut(float a, float b, float t) {
            return Ease.LerpCircInOut(a, b, t);
        }
        public static double In(double t, double b, double c, double d) {
            return -c * (Math.Sqrt(1 - (t /= d) * t) - 1) + b;
        }
        public static double Out(double t, double b, double c, double d) {
            return c * Math.Sqrt(1 - (t = t / d - 1) * t) + b;
        }
        public static double InOut(double t, double b, double c, double d) {
            if ((t /= d / 2) < 1) return -c / 2 * (Math.Sqrt(1 - t * t) - 1) + b;
            return c / 2 * (Math.Sqrt(1 - (t -= 2) * t) + 1) + b;
        }
    }
}