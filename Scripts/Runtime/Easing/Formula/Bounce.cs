using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorryLab.Easing {
    class Bounce : EaseBase {
        public static double LerpIn(float a, float b, float t) {
            return Ease.LerpBounceIn(a, b, t);
        }
        public static double LerpOut(float a, float b, float t) {
            return Ease.LerpBounceOut(a, b, t);
        }
        public static double LerpInOut(float a, float b, float t) {
            return Ease.LerpBounceInOut(a, b, t);
        }
        public static double Out(double t, double b, double c, double d) {
            if ((t /= d) < (1 / 2.75)) {
                return c * (7.5625 * t * t) + b;
            } else if (t < (2 / 2.75)) {
                return c * (7.5625 * (t -= (1.5 / 2.75)) * t + .75) + b;
            } else if (t < (2.5 / 2.75)) {
                return c * (7.5625 * (t -= (2.25 / 2.75)) * t + .9375) + b;
            } else {
                return c * (7.5625 * (t -= (2.625 / 2.75)) * t + .984375) + b;
            }
        }
        public static double In(double t, double b, double c, double d) {
            return c - Out(d - t, 0, c, d) + b;
        }
        public static double InOut(double t, double b, double c, double d) {
            if (t < d / 2) return In(t * 2, 0, c, d) * .5 + b;
            else return Out(t * 2 - d, 0, c, d) * .5 + c * .5 + b;
        }
    }
}