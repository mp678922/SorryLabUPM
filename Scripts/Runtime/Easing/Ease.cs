using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SorryLab.Easing;

namespace SorryLab.Easing {

    public delegate double EaseDelegate(double t, double b, double c, double d);

    public enum EaseType {
        Linear,
        BackIn, BackOut, BackInOut,
        BounceIn, BounceOut, BounceInOut,
        CircIn, CircOut, CircInOut,
        CubicIn, CubicOut, CubicInOut,
        ElasticIn, ElasticOut, ElasticInOut,
        ExpoIn, ExpoOut, ExpoInOut,
        QuadIn, QuadOut, QuadInOut,
        QuartIn, QuartOut, QuartInOut,
        QuintIn, QuintOut, QuintInOut,
        StrongIn, StrongOut, StrongInOut,
        SineIn, SineOut, SineInOut
    }

    public class Ease {

        static public float Lerp(float a, float b, float t, EaseType easeType) {
            float val = 0f;
            switch (easeType) {
                case EaseType.Linear: val = LerpLinear(a, b, t); break;
                case EaseType.BackIn: val = LerpBackIn(a, b, t); break;
                case EaseType.BackOut: val = LerpBackOut(a, b, t); break;
                case EaseType.BackInOut: val = LerpBackInOut(a, b, t); break;
                case EaseType.BounceIn: val = LerpBounceIn(a, b, t); break;
                case EaseType.BounceOut: val = LerpBounceOut(a, b, t); break;
                case EaseType.BounceInOut: val = LerpBounceInOut(a, b, t); break;
                case EaseType.CircIn: val = LerpCircIn(a, b, t); break;
                case EaseType.CircOut: val = LerpCircOut(a, b, t); break;
                case EaseType.CircInOut: val = LerpCircInOut(a, b, t); break;
                case EaseType.CubicIn: val = LerpCubicIn(a, b, t); break;
                case EaseType.CubicOut: val = LerpCubicOut(a, b, t); break;
                case EaseType.CubicInOut: val = LerpCubicInOut(a, b, t); break;
                case EaseType.ElasticIn: val = LerpElasticIn(a, b, t); break;
                case EaseType.ElasticOut: val = LerpElasticOut(a, b, t); break;
                case EaseType.ElasticInOut: val = LerpElasticInOut(a, b, t); break;
                case EaseType.ExpoIn: val = LerpExpoIn(a, b, t); break;
                case EaseType.ExpoOut: val = LerpExpoOut(a, b, t); break;
                case EaseType.ExpoInOut: val = LerpExpoInOut(a, b, t); break;
                case EaseType.QuadIn: val = LerpQuadIn(a, b, t); break;
                case EaseType.QuadOut: val = LerpQuadOut(a, b, t); break;
                case EaseType.QuadInOut: val = LerpQuadInOut(a, b, t); break;
                case EaseType.QuartIn: val = LerpQuartIn(a, b, t); break;
                case EaseType.QuartOut: val = LerpQuartOut(a, b, t); break;
                case EaseType.QuartInOut: val = LerpQuartInOut(a, b, t); break;
                case EaseType.QuintIn: val = LerpQuintIn(a, b, t); break;
                case EaseType.QuintOut: val = LerpQuintOut(a, b, t); break;
                case EaseType.QuintInOut: val = LerpQuintInOut(a, b, t); break;
                case EaseType.StrongIn: val = LerpStrongIn(a, b, t); break;
                case EaseType.StrongOut: val = LerpStrongOut(a, b, t); break;
                case EaseType.StrongInOut: val = LerpStrongInOut(a, b, t); break;
                case EaseType.SineIn: val = LerpSineIn(a, b, t); break;
                case EaseType.SineOut: val = LerpSineOut(a, b, t); break;
                case EaseType.SineInOut: val = LerpSineInOut(a, b, t); break;
            }
            return val;
        }

        static public Vector2 Lerp(Vector2 a, Vector2 b, float t, EaseType easeType) {
            Vector2 val = Vector2.zero;
            switch (easeType) {
                case EaseType.Linear: val = LerpLinear(a, b, t); break;
                case EaseType.BackIn: val = LerpBackIn(a, b, t); break;
                case EaseType.BackOut: val = LerpBackOut(a, b, t); break;
                case EaseType.BackInOut: val = LerpBackInOut(a, b, t); break;
                case EaseType.BounceIn: val = LerpBounceIn(a, b, t); break;
                case EaseType.BounceOut: val = LerpBounceOut(a, b, t); break;
                case EaseType.BounceInOut: val = LerpBounceInOut(a, b, t); break;
                case EaseType.CircIn: val = LerpCircIn(a, b, t); break;
                case EaseType.CircOut: val = LerpCircOut(a, b, t); break;
                case EaseType.CircInOut: val = LerpCircInOut(a, b, t); break;
                case EaseType.CubicIn: val = LerpCubicIn(a, b, t); break;
                case EaseType.CubicOut: val = LerpCubicOut(a, b, t); break;
                case EaseType.CubicInOut: val = LerpCubicInOut(a, b, t); break;
                case EaseType.ElasticIn: val = LerpElasticIn(a, b, t); break;
                case EaseType.ElasticOut: val = LerpElasticOut(a, b, t); break;
                case EaseType.ElasticInOut: val = LerpElasticInOut(a, b, t); break;
                case EaseType.ExpoIn: val = LerpExpoIn(a, b, t); break;
                case EaseType.ExpoOut: val = LerpExpoOut(a, b, t); break;
                case EaseType.ExpoInOut: val = LerpExpoInOut(a, b, t); break;
                case EaseType.QuadIn: val = LerpQuadIn(a, b, t); break;
                case EaseType.QuadOut: val = LerpQuadOut(a, b, t); break;
                case EaseType.QuadInOut: val = LerpQuadInOut(a, b, t); break;
                case EaseType.QuartIn: val = LerpQuartIn(a, b, t); break;
                case EaseType.QuartOut: val = LerpQuartOut(a, b, t); break;
                case EaseType.QuartInOut: val = LerpQuartInOut(a, b, t); break;
                case EaseType.QuintIn: val = LerpQuintIn(a, b, t); break;
                case EaseType.QuintOut: val = LerpQuintOut(a, b, t); break;
                case EaseType.QuintInOut: val = LerpQuintInOut(a, b, t); break;
                case EaseType.StrongIn: val = LerpStrongIn(a, b, t); break;
                case EaseType.StrongOut: val = LerpStrongOut(a, b, t); break;
                case EaseType.StrongInOut: val = LerpStrongInOut(a, b, t); break;
                case EaseType.SineIn: val = LerpSineIn(a, b, t); break;
                case EaseType.SineOut: val = LerpSineOut(a, b, t); break;
                case EaseType.SineInOut: val = LerpSineInOut(a, b, t); break;
            }
            return val;
        }

        static public Vector3 Lerp(Vector3 a, Vector3 b, float t, EaseType easeType) {
            Vector3 val = Vector2.zero;
            switch (easeType) {
                case EaseType.Linear: val = LerpLinear(a, b, t); break;
                case EaseType.BackIn: val = LerpBackIn(a, b, t); break;
                case EaseType.BackOut: val = LerpBackOut(a, b, t); break;
                case EaseType.BackInOut: val = LerpBackInOut(a, b, t); break;
                case EaseType.BounceIn: val = LerpBounceIn(a, b, t); break;
                case EaseType.BounceOut: val = LerpBounceOut(a, b, t); break;
                case EaseType.BounceInOut: val = LerpBounceInOut(a, b, t); break;
                case EaseType.CircIn: val = LerpCircIn(a, b, t); break;
                case EaseType.CircOut: val = LerpCircOut(a, b, t); break;
                case EaseType.CircInOut: val = LerpCircInOut(a, b, t); break;
                case EaseType.CubicIn: val = LerpCubicIn(a, b, t); break;
                case EaseType.CubicOut: val = LerpCubicOut(a, b, t); break;
                case EaseType.CubicInOut: val = LerpCubicInOut(a, b, t); break;
                case EaseType.ElasticIn: val = LerpElasticIn(a, b, t); break;
                case EaseType.ElasticOut: val = LerpElasticOut(a, b, t); break;
                case EaseType.ElasticInOut: val = LerpElasticInOut(a, b, t); break;
                case EaseType.ExpoIn: val = LerpExpoIn(a, b, t); break;
                case EaseType.ExpoOut: val = LerpExpoOut(a, b, t); break;
                case EaseType.ExpoInOut: val = LerpExpoInOut(a, b, t); break;
                case EaseType.QuadIn: val = LerpQuadIn(a, b, t); break;
                case EaseType.QuadOut: val = LerpQuadOut(a, b, t); break;
                case EaseType.QuadInOut: val = LerpQuadInOut(a, b, t); break;
                case EaseType.QuartIn: val = LerpQuartIn(a, b, t); break;
                case EaseType.QuartOut: val = LerpQuartOut(a, b, t); break;
                case EaseType.QuartInOut: val = LerpQuartInOut(a, b, t); break;
                case EaseType.QuintIn: val = LerpQuintIn(a, b, t); break;
                case EaseType.QuintOut: val = LerpQuintOut(a, b, t); break;
                case EaseType.QuintInOut: val = LerpQuintInOut(a, b, t); break;
                case EaseType.StrongIn: val = LerpStrongIn(a, b, t); break;
                case EaseType.StrongOut: val = LerpStrongOut(a, b, t); break;
                case EaseType.StrongInOut: val = LerpStrongInOut(a, b, t); break;
                case EaseType.SineIn: val = LerpSineIn(a, b, t); break;
                case EaseType.SineOut: val = LerpSineOut(a, b, t); break;
                case EaseType.SineInOut: val = LerpSineInOut(a, b, t); break;
            }
            return val;
        }

        static public Color Lerp(Color a, Color b, float t, EaseType easeType) {
            Color val = Color.clear;
            switch (easeType) {
                case EaseType.Linear: val = LerpLinear(a, b, t); break;
                case EaseType.BackIn: val = LerpBackIn(a, b, t); break;
                case EaseType.BackOut: val = LerpBackOut(a, b, t); break;
                case EaseType.BackInOut: val = LerpBackInOut(a, b, t); break;
                case EaseType.BounceIn: val = LerpBounceIn(a, b, t); break;
                case EaseType.BounceOut: val = LerpBounceOut(a, b, t); break;
                case EaseType.BounceInOut: val = LerpBounceInOut(a, b, t); break;
                case EaseType.CircIn: val = LerpCircIn(a, b, t); break;
                case EaseType.CircOut: val = LerpCircOut(a, b, t); break;
                case EaseType.CircInOut: val = LerpCircInOut(a, b, t); break;
                case EaseType.CubicIn: val = LerpCubicIn(a, b, t); break;
                case EaseType.CubicOut: val = LerpCubicOut(a, b, t); break;
                case EaseType.CubicInOut: val = LerpCubicInOut(a, b, t); break;
                case EaseType.ElasticIn: val = LerpElasticIn(a, b, t); break;
                case EaseType.ElasticOut: val = LerpElasticOut(a, b, t); break;
                case EaseType.ElasticInOut: val = LerpElasticInOut(a, b, t); break;
                case EaseType.ExpoIn: val = LerpExpoIn(a, b, t); break;
                case EaseType.ExpoOut: val = LerpExpoOut(a, b, t); break;
                case EaseType.ExpoInOut: val = LerpExpoInOut(a, b, t); break;
                case EaseType.QuadIn: val = LerpQuadIn(a, b, t); break;
                case EaseType.QuadOut: val = LerpQuadOut(a, b, t); break;
                case EaseType.QuadInOut: val = LerpQuadInOut(a, b, t); break;
                case EaseType.QuartIn: val = LerpQuartIn(a, b, t); break;
                case EaseType.QuartOut: val = LerpQuartOut(a, b, t); break;
                case EaseType.QuartInOut: val = LerpQuartInOut(a, b, t); break;
                case EaseType.QuintIn: val = LerpQuintIn(a, b, t); break;
                case EaseType.QuintOut: val = LerpQuintOut(a, b, t); break;
                case EaseType.QuintInOut: val = LerpQuintInOut(a, b, t); break;
                case EaseType.StrongIn: val = LerpStrongIn(a, b, t); break;
                case EaseType.StrongOut: val = LerpStrongOut(a, b, t); break;
                case EaseType.StrongInOut: val = LerpStrongInOut(a, b, t); break;
                case EaseType.SineIn: val = LerpSineIn(a, b, t); break;
                case EaseType.SineOut: val = LerpSineOut(a, b, t); break;
                case EaseType.SineInOut: val = LerpSineInOut(a, b, t); break;
            }
            return val;
        }

        static public Vector4 Lerp(Vector4 a, Vector4 b, float t, EaseType easeType) {
            Vector4 val = Vector4.zero;
            switch (easeType) {
                case EaseType.Linear: val = LerpLinear(a, b, t); break;
                case EaseType.BackIn: val = LerpBackIn(a, b, t); break;
                case EaseType.BackOut: val = LerpBackOut(a, b, t); break;
                case EaseType.BackInOut: val = LerpBackInOut(a, b, t); break;
                case EaseType.BounceIn: val = LerpBounceIn(a, b, t); break;
                case EaseType.BounceOut: val = LerpBounceOut(a, b, t); break;
                case EaseType.BounceInOut: val = LerpBounceInOut(a, b, t); break;
                case EaseType.CircIn: val = LerpCircIn(a, b, t); break;
                case EaseType.CircOut: val = LerpCircOut(a, b, t); break;
                case EaseType.CircInOut: val = LerpCircInOut(a, b, t); break;
                case EaseType.CubicIn: val = LerpCubicIn(a, b, t); break;
                case EaseType.CubicOut: val = LerpCubicOut(a, b, t); break;
                case EaseType.CubicInOut: val = LerpCubicInOut(a, b, t); break;
                case EaseType.ElasticIn: val = LerpElasticIn(a, b, t); break;
                case EaseType.ElasticOut: val = LerpElasticOut(a, b, t); break;
                case EaseType.ElasticInOut: val = LerpElasticInOut(a, b, t); break;
                case EaseType.ExpoIn: val = LerpExpoIn(a, b, t); break;
                case EaseType.ExpoOut: val = LerpExpoOut(a, b, t); break;
                case EaseType.ExpoInOut: val = LerpExpoInOut(a, b, t); break;
                case EaseType.QuadIn: val = LerpQuadIn(a, b, t); break;
                case EaseType.QuadOut: val = LerpQuadOut(a, b, t); break;
                case EaseType.QuadInOut: val = LerpQuadInOut(a, b, t); break;
                case EaseType.QuartIn: val = LerpQuartIn(a, b, t); break;
                case EaseType.QuartOut: val = LerpQuartOut(a, b, t); break;
                case EaseType.QuartInOut: val = LerpQuartInOut(a, b, t); break;
                case EaseType.QuintIn: val = LerpQuintIn(a, b, t); break;
                case EaseType.QuintOut: val = LerpQuintOut(a, b, t); break;
                case EaseType.QuintInOut: val = LerpQuintInOut(a, b, t); break;
                case EaseType.StrongIn: val = LerpStrongIn(a, b, t); break;
                case EaseType.StrongOut: val = LerpStrongOut(a, b, t); break;
                case EaseType.StrongInOut: val = LerpStrongInOut(a, b, t); break;
                case EaseType.SineIn: val = LerpSineIn(a, b, t); break;
                case EaseType.SineOut: val = LerpSineOut(a, b, t); break;
                case EaseType.SineInOut: val = LerpSineInOut(a, b, t); break;
            }
            return val;
        }

        static public Quaternion Lerp(Quaternion a, Quaternion b, float t, EaseType easeType) {
            Quaternion val = Quaternion.identity;
            switch (easeType) {
                case EaseType.Linear: val = LerpLinear(a, b, t); break;
                case EaseType.BackIn: val = LerpBackIn(a, b, t); break;
                case EaseType.BackOut: val = LerpBackOut(a, b, t); break;
                case EaseType.BackInOut: val = LerpBackInOut(a, b, t); break;
                case EaseType.BounceIn: val = LerpBounceIn(a, b, t); break;
                case EaseType.BounceOut: val = LerpBounceOut(a, b, t); break;
                case EaseType.BounceInOut: val = LerpBounceInOut(a, b, t); break;
                case EaseType.CircIn: val = LerpCircIn(a, b, t); break;
                case EaseType.CircOut: val = LerpCircOut(a, b, t); break;
                case EaseType.CircInOut: val = LerpCircInOut(a, b, t); break;
                case EaseType.CubicIn: val = LerpCubicIn(a, b, t); break;
                case EaseType.CubicOut: val = LerpCubicOut(a, b, t); break;
                case EaseType.CubicInOut: val = LerpCubicInOut(a, b, t); break;
                case EaseType.ElasticIn: val = LerpElasticIn(a, b, t); break;
                case EaseType.ElasticOut: val = LerpElasticOut(a, b, t); break;
                case EaseType.ElasticInOut: val = LerpElasticInOut(a, b, t); break;
                case EaseType.ExpoIn: val = LerpExpoIn(a, b, t); break;
                case EaseType.ExpoOut: val = LerpExpoOut(a, b, t); break;
                case EaseType.ExpoInOut: val = LerpExpoInOut(a, b, t); break;
                case EaseType.QuadIn: val = LerpQuadIn(a, b, t); break;
                case EaseType.QuadOut: val = LerpQuadOut(a, b, t); break;
                case EaseType.QuadInOut: val = LerpQuadInOut(a, b, t); break;
                case EaseType.QuartIn: val = LerpQuartIn(a, b, t); break;
                case EaseType.QuartOut: val = LerpQuartOut(a, b, t); break;
                case EaseType.QuartInOut: val = LerpQuartInOut(a, b, t); break;
                case EaseType.QuintIn: val = LerpQuintIn(a, b, t); break;
                case EaseType.QuintOut: val = LerpQuintOut(a, b, t); break;
                case EaseType.QuintInOut: val = LerpQuintInOut(a, b, t); break;
                case EaseType.StrongIn: val = LerpStrongIn(a, b, t); break;
                case EaseType.StrongOut: val = LerpStrongOut(a, b, t); break;
                case EaseType.StrongInOut: val = LerpStrongInOut(a, b, t); break;
                case EaseType.SineIn: val = LerpSineIn(a, b, t); break;
                case EaseType.SineOut: val = LerpSineOut(a, b, t); break;
                case EaseType.SineInOut: val = LerpSineInOut(a, b, t); break;
            }
            return val;
        }

        #region Back
        //Back

        static public float LerpBackIn(float a, float b, float t) {
            return (float)Back.In(t, a, b, 1f);
        }
        static public Vector2 LerpBackIn(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpBackIn(0f, 1f, t));
        }
        static public Vector3 LerpBackIn(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpBackIn(0f, 1f, t));
        }
        static public Color LerpBackIn(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpBackIn(0f, 1f, t));
        }
        static public Vector4 LerpBackIn(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpBackIn(0f, 1f, t));
        }
        static public Quaternion LerpBackIn(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpBackIn(0f, 1f, t));
        }

        static public float LerpBackOut(float a, float b, float t) {
            return (float)Back.Out(t, a, b, 1f);
        }
        static public Vector2 LerpBackOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpBackOut(0f, 1f, t));
        }
        static public Vector3 LerpBackOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpBackOut(0f, 1f, t));
        }
        static public Color LerpBackOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpBackOut(0f, 1f, t));
        }
        static public Vector4 LerpBackOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpBackOut(0f, 1f, t));
        }
        static public Quaternion LerpBackOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpBackOut(0f, 1f, t));
        }

        static public float LerpBackInOut(float a, float b, float t) {
            return (float)Back.InOut(t, a, b, 1f);
        }
        static public Vector2 LerpBackInOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpBackInOut(0f, 1f, t));
        }
        static public Vector3 LerpBackInOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpBackInOut(0f, 1f, t));
        }
        static public Color LerpBackInOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpBackInOut(0f, 1f, t));
        }
        static public Vector4 LerpBackInOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpBackInOut(0f, 1f, t));
        }
        static public Quaternion LerpBackInOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpBackInOut(0f, 1f, t));
        }

        #endregion

        #region Bounce
        //Bounce

        static public float LerpBounceIn(float a, float b, float t) {
            return (float)Bounce.In(t, a, b, 1f);
        }
        static public Vector2 LerpBounceIn(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpBounceIn(0f, 1f, t));
        }
        static public Vector3 LerpBounceIn(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpBounceIn(0f, 1f, t));
        }
        static public Color LerpBounceIn(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpBounceIn(0f, 1f, t));
        }
        static public Vector4 LerpBounceIn(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpBounceIn(0f, 1f, t));
        }
        static public Quaternion LerpBounceIn(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpBounceIn(0f, 1f, t));
        }

        static public float LerpBounceOut(float a, float b, float t) {
            return (float)Bounce.Out(t, a, b, 1f);
        }
        static public Vector2 LerpBounceOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpBounceOut(0f, 1f, t));
        }
        static public Vector3 LerpBounceOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpBounceOut(0f, 1f, t));
        }
        static public Color LerpBounceOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpBounceOut(0f, 1f, t));
        }
        static public Vector4 LerpBounceOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpBounceOut(0f, 1f, t));
        }
        static public Quaternion LerpBounceOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpBounceOut(0f, 1f, t));
        }

        static public float LerpBounceInOut(float a, float b, float t) {
            return (float)Bounce.InOut(t, a, b, 1f);
        }
        static public Vector2 LerpBounceInOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpBounceInOut(0f, 1f, t));
        }
        static public Vector3 LerpBounceInOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpBounceInOut(0f, 1f, t));
        }
        static public Color LerpBounceInOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpBounceInOut(0f, 1f, t));
        }
        static public Vector4 LerpBounceInOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpBounceInOut(0f, 1f, t));
        }
        static public Quaternion LerpBounceInOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpBounceInOut(0f, 1f, t));
        }

        #endregion

        #region Circ
        //Circ

        static public float LerpCircIn(float a, float b, float t) {
            return (float)Circ.In(t, a, b, 1f);
        }
        static public Vector2 LerpCircIn(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpCircIn(0f, 1f, t));
        }
        static public Vector3 LerpCircIn(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpCircIn(0f, 1f, t));
        }
        static public Color LerpCircIn(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpCircIn(0f, 1f, t));
        }
        static public Vector4 LerpCircIn(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpCircIn(0f, 1f, t));
        }
        static public Quaternion LerpCircIn(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpCircIn(0f, 1f, t));
        }

        static public float LerpCircOut(float a, float b, float t) {
            return (float)Circ.Out(t, a, b, 1f);
        }
        static public Vector2 LerpCircOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpCircOut(0f, 1f, t));
        }
        static public Vector3 LerpCircOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpCircOut(0f, 1f, t));
        }
        static public Color LerpCircOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpCircOut(0f, 1f, t));
        }
        static public Vector4 LerpCircOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpCircOut(0f, 1f, t));
        }
        static public Quaternion LerpCircOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpCircOut(0f, 1f, t));
        }

        static public float LerpCircInOut(float a, float b, float t) {
            return (float)Circ.InOut(t, a, b, 1f);
        }
        static public Vector2 LerpCircInOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpCircInOut(0f, 1f, t));
        }
        static public Vector3 LerpCircInOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpCircInOut(0f, 1f, t));
        }
        static public Color LerpCircInOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpCircInOut(0f, 1f, t));
        }
        static public Vector4 LerpCircInOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpCircInOut(0f, 1f, t));
        }
        static public Quaternion LerpCircInOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpCircInOut(0f, 1f, t));
        }

        #endregion

        #region Cubic
        //Cubic

        static public float LerpCubicIn(float a, float b, float t) {
            return (float)Cubic.In(t, a, b, 1f);
        }
        static public Vector2 LerpCubicIn(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpCubicIn(0f, 1f, t));
        }
        static public Vector3 LerpCubicIn(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpCubicIn(0f, 1f, t));
        }
        static public Color LerpCubicIn(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpCubicIn(0f, 1f, t));
        }
        static public Vector4 LerpCubicIn(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpCubicIn(0f, 1f, t));
        }
        static public Quaternion LerpCubicIn(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpCubicIn(0f, 1f, t));
        }

        static public float LerpCubicOut(float a, float b, float t) {
            return (float)Cubic.Out(t, a, b, 1f);
        }
        static public Vector2 LerpCubicOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpCubicOut(0f, 1f, t));
        }
        static public Vector3 LerpCubicOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpCubicOut(0f, 1f, t));
        }
        static public Color LerpCubicOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpCubicOut(0f, 1f, t));
        }
        static public Vector4 LerpCubicOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpCubicOut(0f, 1f, t));
        }
        static public Quaternion LerpCubicOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpCubicOut(0f, 1f, t));
        }

        static public float LerpCubicInOut(float a, float b, float t) {
            return (float)Cubic.InOut(t, a, b, 1f);
        }
        static public Vector2 LerpCubicInOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpCubicInOut(0f, 1f, t));
        }
        static public Vector3 LerpCubicInOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpCubicInOut(0f, 1f, t));
        }
        static public Color LerpCubicInOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpCubicInOut(0f, 1f, t));
        }
        static public Vector4 LerpCubicInOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpCubicInOut(0f, 1f, t));
        }
        static public Quaternion LerpCubicInOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpCubicInOut(0f, 1f, t));
        }

        #endregion

        #region Elastic
        //Elastic

        static public float LerpElasticIn(float a, float b, float t) {
            if (a == b && a == 0f) { return 0f; }
            return (float)Elastic.In(t, a, b, 1f);
        }
        static public Vector2 LerpElasticIn(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpElasticIn(0f, 1f, t));
        }
        static public Vector3 LerpElasticIn(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpElasticIn(0f, 1f, t));
        }
        static public Color LerpElasticIn(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpElasticIn(0f, 1f, t));
        }
        static public Vector4 LerpElasticIn(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpElasticIn(0f, 1f, t));
        }
        static public Quaternion LerpElasticIn(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpElasticIn(0f, 1f, t));
        }

        static public float LerpElasticOut(float a, float b, float t) {
            if (a == b && a == 0f) { return 0f; }
            return (float)Elastic.Out(t, a, b, 1f);
        }
        static public Vector2 LerpElasticOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpElasticOut(0f, 1f, t));
        }
        static public Vector3 LerpElasticOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpElasticOut(0f, 1f, t));
        }
        static public Color LerpElasticOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpElasticOut(0f, 1f, t));
        }
        static public Vector4 LerpElasticOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpElasticOut(0f, 1f, t));
        }
        static public Quaternion LerpElasticOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpElasticOut(0f, 1f, t));
        }

        static public float LerpElasticInOut(float a, float b, float t) {
            if (a == b && a == 0f) { return 0f; }
            return (float)Elastic.InOut(t, a, b, 1f);
        }
        static public Vector2 LerpElasticInOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpElasticInOut(0f, 1f, t));
        }
        static public Vector3 LerpElasticInOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpElasticInOut(0f, 1f, t));
        }
        static public Color LerpElasticInOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpElasticInOut(0f, 1f, t));
        }
        static public Vector4 LerpElasticInOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpElasticInOut(0f, 1f, t));
        }
        static public Quaternion LerpElasticInOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpElasticInOut(0f, 1f, t));
        }

        #endregion

        #region Expo
        //Expo

        static public float LerpExpoIn(float a, float b, float t) {
            return (float)Expo.In(t, a, b, 1f);
        }
        static public Vector2 LerpExpoIn(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpExpoIn(0f, 1f, t));
        }
        static public Vector3 LerpExpoIn(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpExpoIn(0f, 1f, t));
        }
        static public Color LerpExpoIn(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpExpoIn(0f, 1f, t));
        }
        static public Vector4 LerpExpoIn(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpExpoIn(0f, 1f, t));
        }
        static public Quaternion LerpExpoIn(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpExpoIn(0f, 1f, t));
        }

        static public float LerpExpoOut(float a, float b, float t) {
            return (float)Expo.Out(t, a, b, 1f);
        }
        static public Vector2 LerpExpoOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpExpoOut(0f, 1f, t));
        }
        static public Vector3 LerpExpoOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpExpoOut(0f, 1f, t));
        }
        static public Color LerpExpoOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpExpoOut(0f, 1f, t));
        }
        static public Vector4 LerpExpoOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpExpoOut(0f, 1f, t));
        }
        static public Quaternion LerpExpoOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpExpoOut(0f, 1f, t));
        }

        static public float LerpExpoInOut(float a, float b, float t) {
            return (float)Expo.InOut(t, a, b, 1f);
        }
        static public Vector2 LerpExpoInOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpExpoInOut(0f, 1f, t));
        }
        static public Vector3 LerpExpoInOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpExpoInOut(0f, 1f, t));
        }
        static public Color LerpExpoInOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpExpoInOut(0f, 1f, t));
        }
        static public Vector4 LerpExpoInOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpExpoInOut(0f, 1f, t));
        }
        static public Quaternion LerpExpoInOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpExpoInOut(0f, 1f, t));
        }

        #endregion

        #region Linear
        //Linear

        static public float LerpLinear(float a, float b, float t) {
            return (float)Linear.In(t, a, b, 1f);
        }
        static public Vector2 LerpLinear(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpLinear(0f, 1f, t));
        }
        static public Vector3 LerpLinear(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpLinear(0f, 1f, t));
        }
        static public Color LerpLinear(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpLinear(0f, 1f, t));
        }
        static public Vector4 LerpLinear(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpLinear(0f, 1f, t));
        }
        static public Quaternion LerpLinear(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpLinear(0f, 1f, t));
        }

        #endregion

        #region Quad
        //Quad

        static public float LerpQuadIn(float a, float b, float t) {
            return (float)Quad.In(t, a, b, 1f);
        }
        static public Vector2 LerpQuadIn(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpQuadIn(0f, 1f, t));
        }
        static public Vector3 LerpQuadIn(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpQuadIn(0f, 1f, t));
        }
        static public Color LerpQuadIn(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpQuadIn(0f, 1f, t));
        }
        static public Vector4 LerpQuadIn(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpQuadIn(0f, 1f, t));
        }
        static public Quaternion LerpQuadIn(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpQuadIn(0f, 1f, t));
        }

        static public float LerpQuadOut(float a, float b, float t) {
            return (float)Quad.Out(t, a, b, 1f);
        }
        static public Vector2 LerpQuadOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpQuadOut(0f, 1f, t));
        }
        static public Vector3 LerpQuadOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpQuadOut(0f, 1f, t));
        }
        static public Color LerpQuadOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpQuadOut(0f, 1f, t));
        }
        static public Vector4 LerpQuadOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpQuadOut(0f, 1f, t));
        }
        static public Quaternion LerpQuadOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpQuadOut(0f, 1f, t));
        }

        static public float LerpQuadInOut(float a, float b, float t) {
            return (float)Quad.InOut(t, a, b, 1f);
        }
        static public Vector2 LerpQuadInOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpQuadInOut(0f, 1f, t));
        }
        static public Vector3 LerpQuadInOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpQuadInOut(0f, 1f, t));
        }
        static public Color LerpQuadInOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpQuadInOut(0f, 1f, t));
        }
        static public Vector4 LerpQuadInOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpQuadInOut(0f, 1f, t));
        }
        static public Quaternion LerpQuadInOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpQuadInOut(0f, 1f, t));
        }

        #endregion

        #region Quart
        //Quart

        static public float LerpQuartIn(float a, float b, float t) {
            return (float)Quart.In(t, a, b, 1f);
        }
        static public Vector2 LerpQuartIn(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpQuartIn(0f, 1f, t));
        }
        static public Vector3 LerpQuartIn(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpQuartIn(0f, 1f, t));
        }
        static public Color LerpQuartIn(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpQuartIn(0f, 1f, t));
        }
        static public Vector4 LerpQuartIn(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpQuartIn(0f, 1f, t));
        }
        static public Quaternion LerpQuartIn(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpQuartIn(0f, 1f, t));
        }

        static public float LerpQuartOut(float a, float b, float t) {
            return (float)Quart.Out(t, a, b, 1f);
        }
        static public Vector2 LerpQuartOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpQuartOut(0f, 1f, t));
        }
        static public Vector3 LerpQuartOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpQuartOut(0f, 1f, t));
        }
        static public Color LerpQuartOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpQuartOut(0f, 1f, t));
        }
        static public Vector4 LerpQuartOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpQuartOut(0f, 1f, t));
        }
        static public Quaternion LerpQuartOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpQuartOut(0f, 1f, t));
        }

        static public float LerpQuartInOut(float a, float b, float t) {
            return (float)Quart.InOut(t, a, b, 1f);
        }
        static public Vector2 LerpQuartInOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpQuartInOut(0f, 1f, t));
        }
        static public Vector3 LerpQuartInOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpQuartInOut(0f, 1f, t));
        }
        static public Color LerpQuartInOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpQuartInOut(0f, 1f, t));
        }
        static public Vector4 LerpQuartInOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpQuartInOut(0f, 1f, t));
        }
        static public Quaternion LerpQuartInOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpQuartInOut(0f, 1f, t));
        }

        #endregion

        #region Quint
        //Quint

        static public float LerpQuintIn(float a, float b, float t) {
            return (float)Quint.In(t, a, b, 1f);
        }
        static public Vector2 LerpQuintIn(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpQuintIn(0f, 1f, t));
        }
        static public Vector3 LerpQuintIn(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpQuintIn(0f, 1f, t));
        }
        static public Color LerpQuintIn(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpQuintIn(0f, 1f, t));
        }
        static public Vector4 LerpQuintIn(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpQuintIn(0f, 1f, t));
        }
        static public Quaternion LerpQuintIn(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpQuintIn(0f, 1f, t));
        }

        static public float LerpQuintOut(float a, float b, float t) {
            return (float)Quint.Out(t, a, b, 1f);
        }
        static public Vector2 LerpQuintOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpQuintOut(0f, 1f, t));
        }
        static public Vector3 LerpQuintOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpQuintOut(0f, 1f, t));
        }
        static public Color LerpQuintOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpQuintOut(0f, 1f, t));
        }
        static public Vector4 LerpQuintOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpQuintOut(0f, 1f, t));
        }
        static public Quaternion LerpQuintOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpQuintOut(0f, 1f, t));
        }

        static public float LerpQuintInOut(float a, float b, float t) {
            return (float)Quint.InOut(t, a, b, 1f);
        }
        static public Vector2 LerpQuintInOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpQuintInOut(0f, 1f, t));
        }
        static public Vector3 LerpQuintInOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpQuintInOut(0f, 1f, t));
        }
        static public Color LerpQuintInOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpQuintInOut(0f, 1f, t));
        }
        static public Vector4 LerpQuintInOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpQuintInOut(0f, 1f, t));
        }
        static public Quaternion LerpQuintInOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpQuintInOut(0f, 1f, t));
        }

        #endregion

        #region Sine
        //Sine

        static public float LerpSineIn(float a, float b, float t) {
            return (float)Sine.In(t, a, b, 1f);
        }
        static public Vector2 LerpSineIn(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpSineIn(0f, 1f, t));
        }
        static public Vector3 LerpSineIn(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpSineIn(0f, 1f, t));
        }
        static public Color LerpSineIn(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpSineIn(0f, 1f, t));
        }
        static public Vector4 LerpSineIn(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpSineIn(0f, 1f, t));
        }
        static public Quaternion LerpSineIn(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpSineIn(0f, 1f, t));
        }


        static public float LerpSineOut(float a, float b, float t) {
            return (float)Sine.Out(t, a, b, 1f);
        }
        static public Vector2 LerpSineOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpSineOut(0f, 1f, t));
        }
        static public Vector3 LerpSineOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpSineOut(0f, 1f, t));
        }
        static public Color LerpSineOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpSineOut(0f, 1f, t));
        }
        static public Vector4 LerpSineOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpSineOut(0f, 1f, t));
        }
        static public Quaternion LerpSineOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpSineOut(0f, 1f, t));
        }


        static public float LerpSineInOut(float a, float b, float t) {
            return (float)Sine.InOut(t, a, b, 1f);
        }
        static public Vector2 LerpSineInOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpSineInOut(0f, 1f, t));
        }
        static public Vector3 LerpSineInOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpSineInOut(0f, 1f, t));
        }
        static public Color LerpSineInOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpSineInOut(0f, 1f, t));
        }
        static public Vector4 LerpSineInOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpSineInOut(0f, 1f, t));
        }
        static public Quaternion LerpSineInOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpSineInOut(0f, 1f, t));
        }


        #endregion

        #region Strong
        //Strong

        static public float LerpStrongIn(float a, float b, float t) {
            return (float)Strong.In(t, a, b, 1f);
        }
        static public Vector2 LerpStrongIn(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpStrongIn(0f, 1f, t));
        }
        static public Vector3 LerpStrongIn(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpStrongIn(0f, 1f, t));
        }
        static public Color LerpStrongIn(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpStrongIn(0f, 1f, t));
        }
        static public Vector4 LerpStrongIn(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpStrongIn(0f, 1f, t));
        }
        static public Quaternion LerpStrongIn(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpStrongIn(0f, 1f, t));
        }

        static public float LerpStrongOut(float a, float b, float t) {
            return (float)Strong.Out(t, a, b, 1f);
        }
        static public Vector2 LerpStrongOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpStrongOut(0f, 1f, t));
        }
        static public Vector3 LerpStrongOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpStrongOut(0f, 1f, t));
        }
        static public Color LerpStrongOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpStrongOut(0f, 1f, t));
        }
        static public Vector4 LerpStrongOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpStrongOut(0f, 1f, t));
        }
        static public Quaternion LerpStrongOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpStrongOut(0f, 1f, t));
        }

        static public float LerpStrongInOut(float a, float b, float t) {
            return (float)Strong.InOut(t, a, b, 1f);
        }
        static public Vector2 LerpStrongInOut(Vector2 a, Vector2 b, float t) {
            return Vector2.Lerp(a, b, LerpStrongInOut(0f, 1f, t));
        }
        static public Vector3 LerpStrongInOut(Vector3 a, Vector3 b, float t) {
            return Vector3.Lerp(a, b, LerpStrongInOut(0f, 1f, t));
        }
        static public Color LerpStrongInOut(Color a, Color b, float t) {
            return Color.Lerp(a, b, LerpStrongInOut(0f, 1f, t));
        }
        static public Vector4 LerpStrongInOut(Vector4 a, Vector4 b, float t) {
            return Vector4.Lerp(a, b, LerpStrongInOut(0f, 1f, t));
        }
        static public Quaternion LerpStrongInOut(Quaternion a, Quaternion b, float t) {
            return Quaternion.Lerp(a, b, LerpStrongInOut(0f, 1f, t));
        }

        #endregion
    }

    abstract class EaseBase {
        protected const double TWO_PI = Math.PI * 2;
        protected const double HALF_PI = Math.PI / 2;
    }

}
