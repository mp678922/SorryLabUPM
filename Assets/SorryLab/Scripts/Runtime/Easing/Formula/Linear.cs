using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorryLab.Easing {
    class Linear : EaseBase {
        public static double EaseNone(double t, double b, double c, double d) {
            return c * t / d + b;
        }
        public static double In(double t, double b, double c, double d) {
            return c * t / d + b;
        }
        public static double Out(double t, double b, double c, double d) {
            return c * t / d + b;
        }
        public static double InOut(double t, double b, double c, double d) {
            return c * t / d + b;
        }
    }
}