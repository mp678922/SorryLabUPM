using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorryLab.Easing {
    class Expo : EaseBase {
        public static double LerpIn(float a, float b, float t) {
            return Ease.LerpExpoIn(a, b, t);
        }
        public static double LerpOut(float a, float b, float t) {
            return Ease.LerpExpoOut(a, b, t);
        }
        public static double LerpInOut(float a, float b, float t) {
            return Ease.LerpExpoInOut(a, b, t);
        }
        public static double In(double t, double b, double c, double d) {
            return (t == 0) ? b : c * Math.Pow(2, 10 * (t / d - 1)) + b - c * 0.001;
        }
        public static double Out(double t, double b, double c, double d) {
            return (t == d) ? b + c : c * (-Math.Pow(2, -10 * t / d) + 1) + b;
        }
        public static double InOut(double t, double b, double c, double d) {
            if (t == 0) return b;
            if (t == d) return b + c;
            if ((t /= d / 2) < 1) return c / 2 * Math.Pow(2, 10 * (t - 1)) + b;
            return c / 2 * (-Math.Pow(2, -10 * --t) + 2) + b;
        }
    }
}