using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorryLab.Easing {
    class Sine : EaseBase {
        public static double LerpIn(float a, float b, float t) {
            return Ease.LerpSineIn(a, b, t);
        }
        public static double LerpOut(float a, float b, float t) {
            return Ease.LerpSineOut(a, b, t);
        }
        public static double LerpInOut(float a, float b, float t) {
            return Ease.LerpSineInOut(a, b, t);
        }
        public static double In(double t, double b, double c, double d) {
            return -c * Math.Cos(t / d * HALF_PI) + c + b;
        }
        public static double Out(double t, double b, double c, double d) {
            return c * Math.Sin(t / d * HALF_PI) + b;
        }
        public static double InOut(double t, double b, double c, double d) {
            return -c / 2 * (Math.Cos(Math.PI * t / d) - 1) + b;
        }
    }
}