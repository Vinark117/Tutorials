using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VinTools.Tweening.Internal
{
    public enum TweenType
    {
        AlphaCanvas,
        ColorSprite,
        ColorImage,
        ColorText,
        ColorTMP,
        Move,
        MoveX,
        MoveY,
        MoveZ,
        MoveLocal,
        MoveLocalX,
        MoveLocalY,
        MoveLocalZ,
        ScaleLocal,
        ScaleLocalX,
        ScaleLocalY,
        ScaleLocalZ,
        Rotate,
        RotateX,
        RotateY,
        RotateZ,
        RotateLocal,
        RotateLocalX,
        RotateLocalY,
        RotateLocalZ,
        ValueFloat,
        ValueVector2,
        ValueVector3,
        ValueColor,
        ValueQuaternion,
    }
}

namespace VinTools.Tweening
{
    public enum EaseCurve
    {
        linear,
        sineIn,
        sineOut,
        sineInOut,
        cubicIn,
        cubicOut,
        cubicInOut,
        quintIn,
        quintOut,
        quintInOut,
        expoIn,
        expoOut,
        expoInOut,
        backIn,
        backOut,
        backInOut,
        circIn,
        circOut,
        circInOut,
        elasticIn,
        elasticOut,
        elasticInOut,
        bounceIn,
        bounceOut,
        bounceInOut
    }

    public static class EaseType
    {
        public static AnimationCurve ToAnimationCurve(this EaseCurve _enum)
        {
            switch (_enum)
            {
                case EaseCurve.linear:
                    return EaseType.linear;
                case EaseCurve.sineIn:
                    return EaseType.sineIn;
                case EaseCurve.sineOut:
                    return EaseType.sineOut;
                case EaseCurve.sineInOut:
                    return EaseType.sineInOut;
                case EaseCurve.cubicIn:
                    return EaseType.cubicIn;
                case EaseCurve.cubicOut:
                    return EaseType.cubicOut;
                case EaseCurve.cubicInOut:
                    return EaseType.cubicInOut;
                case EaseCurve.quintIn:
                    return EaseType.quintIn;
                case EaseCurve.quintOut:
                    return EaseType.quintOut;
                case EaseCurve.quintInOut:
                    return EaseType.quintInOut;
                case EaseCurve.expoIn:
                    return EaseType.expoIn;
                case EaseCurve.expoOut:
                    return EaseType.expoOut;
                case EaseCurve.expoInOut:
                    return EaseType.expoInOut;
                case EaseCurve.backIn:
                    return EaseType.backIn;
                case EaseCurve.backOut:
                    return EaseType.backOut;
                case EaseCurve.backInOut:
                    return EaseType.backInOut;
                case EaseCurve.circIn:
                    return EaseType.circIn;
                case EaseCurve.circOut:
                    return EaseType.circOut;
                case EaseCurve.circInOut:
                    return EaseType.circInOut;
                case EaseCurve.elasticIn:
                    return EaseType.elasticIn;
                case EaseCurve.elasticOut:
                    return EaseType.elasticOut;
                case EaseCurve.elasticInOut:
                    return EaseType.elasticInOut;
                case EaseCurve.bounceIn:
                    return EaseType.bounceIn;
                case EaseCurve.bounceOut:
                    return EaseType.bounceOut;
                case EaseCurve.bounceInOut:
                    return EaseType.bounceInOut;
                default:
                    return EaseType.linear;
            }
        }

        public static AnimationCurve linear
        {
            get
            {
                return AnimationCurve.Linear(0, 0, 1, 1);
            }
        }
        public static AnimationCurve sineIn
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 0, 0));
                curve.AddKey(new Keyframe(1, 1, 2.7f, 2.7f));

                return curve;
            }
        }
        public static AnimationCurve sineOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 2.7f, 2.7f));
                curve.AddKey(new Keyframe(1, 1, 0, 0));

                return curve;
            }
        }
        public static AnimationCurve sineInOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 0, 0));
                curve.AddKey(new Keyframe(.5f, .5f, 2.7f, 2.7f));
                curve.AddKey(new Keyframe(1, 1, 0, 0));

                return curve;
            }
        }
        public static AnimationCurve cubicIn
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(0, 0);
                curve.AddKey(.2f, .0f);
                curve.AddKey(.6f, .16f);
                curve.AddKey(1, 1);

                curve.SmoothTangents(1, 1f);
                
                return curve;
            }
        }
        public static AnimationCurve cubicOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(0, 0);
                curve.AddKey(1 - .6f, 1 - .16f);
                curve.AddKey(.8f, 1f);
                curve.AddKey(1, 1);

                curve.SmoothTangents(2, -1f);

                return curve;
            }
        }
        public static AnimationCurve cubicInOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(0, 0);
                //curve.AddKey(.1f, 0.005f);
                curve.AddKey(.2f, .02f);
                curve.AddKey(.3f, .1f);
                curve.AddKey(.7f, .9f);
                curve.AddKey(.8f, .98f);
                //curve.AddKey(.9f, .995f);
                curve.AddKey(1, 1);

                return curve;
            }
        }
        public static AnimationCurve quintIn
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 0, 0));
                curve.AddKey(new Keyframe(.4f, 0, 0, 0));
                curve.AddKey(new Keyframe(1, 1, 5, 5));

                return curve;
            }
        }
        public static AnimationCurve quintOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 5, 5));
                curve.AddKey(new Keyframe(.6f, 1, 0, 0));
                curve.AddKey(new Keyframe(1, 1, 0, 0));

                return curve;
            }
        }
        public static AnimationCurve quintInOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 0, 0));
                curve.AddKey(new Keyframe(.2f, 0, 0, 0));
                curve.AddKey(new Keyframe(0.5f, 0.5f, 5, 5));
                curve.AddKey(new Keyframe(.8f, 1, 0, 0));
                curve.AddKey(new Keyframe(1, 1, 0, 0));

                return curve;
            }
        }
        public static AnimationCurve expoIn
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 0, 0));
                curve.AddKey(new Keyframe(.7f, .2f, 1, 1));
                curve.AddKey(new Keyframe(1, 1, 5, 5));

                return curve;
            }
        }
        public static AnimationCurve expoOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 5, 5));
                curve.AddKey(new Keyframe(.3f, .8f, 1, 1));
                curve.AddKey(new Keyframe(1, 1, 0, 0));

                return curve;
            }
        }
        public static AnimationCurve expoInOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 0, 0));
                curve.AddKey(new Keyframe(.35f, .1f, 1, 1));

                curve.AddKey(new Keyframe(.65f, .9f, 1, 1));
                curve.AddKey(new Keyframe(1, 1, 0, 0));

                return curve;
            }
        }
        public static AnimationCurve backIn
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 0, 0));
                curve.AddKey(new Keyframe(1, 1, 5, 5));

                return curve;
            }
        }
        public static AnimationCurve backOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 5, 5));
                curve.AddKey(new Keyframe(1, 1, 0, 0));

                return curve;
            }
        }
        public static AnimationCurve backInOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 0, 0));
                curve.AddKey(new Keyframe(.5f, .5f, 5, 5));
                curve.AddKey(new Keyframe(1, 1, 0, 0));

                return curve;
            }
        }
        public static AnimationCurve circIn
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 0, 0));
                curve.AddKey(new Keyframe(0.8f, 0.3f, 1f, 1f));
                curve.AddKey(new Keyframe(1, 1, 7, 7));

                return curve;
            }
        }
        public static AnimationCurve circOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 7, 7));
                curve.AddKey(new Keyframe(0.2f, 0.7f, 1f, 1f));
                curve.AddKey(new Keyframe(1, 1, 0, 0));

                return curve;
            }
        }
        public static AnimationCurve circInOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 0, 0));
                curve.AddKey(new Keyframe(0.4f, 0.15f, 1f, 1f));

                curve.AddKey(new Keyframe(.5f, .5f, 7, 7));

                curve.AddKey(new Keyframe(0.6f, 0.85f, 1f, 1f));
                curve.AddKey(new Keyframe(1, 1, 0, 0));

                return curve;
            }
        }
        public static AnimationCurve elasticIn
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 0, 0));
                curve.AddKey(new Keyframe(.2f, 0, 0, 0));
                curve.AddKey(new Keyframe(.3f, 0, .5f, .5f));
                curve.AddKey(new Keyframe(.4f, 0, -1, -1));
                curve.AddKey(new Keyframe(.5f, 0, 1.5f, 1.5f));
                curve.AddKey(new Keyframe(.6f, 0, -2, -2));
                curve.AddKey(new Keyframe(.7f, 0, 4, 4));
                curve.AddKey(new Keyframe(.8f, 0, -5, -5));
                curve.AddKey(new Keyframe(1, 1, 10, 10));

                return curve;
            }
        }
        public static AnimationCurve elasticOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 10, 10));
                curve.AddKey(new Keyframe(.2f, 1, -5, -5));
                curve.AddKey(new Keyframe(.3f, 1, 4, 4));
                curve.AddKey(new Keyframe(.4f, 1, -2, -2));
                curve.AddKey(new Keyframe(.5f, 1, 1.5f, 1.5f));
                curve.AddKey(new Keyframe(.6f, 1, -1, -1));
                curve.AddKey(new Keyframe(.7f, 1, .5f, .5f));
                curve.AddKey(new Keyframe(.8f, 1, 0, 0));
                curve.AddKey(new Keyframe(1, 1, 0, 0));

                return curve;
            }
        }
        public static AnimationCurve elasticInOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, 0, 0));
                curve.AddKey(new Keyframe(.1f, 0, 0, 0));
                curve.AddKey(new Keyframe(.15f, 0, .5f, .5f));
                curve.AddKey(new Keyframe(.2f, 0, -1, -1));
                curve.AddKey(new Keyframe(.25f, 0, 1.5f, 1.5f));
                curve.AddKey(new Keyframe(.3f, 0, -2, -2));
                curve.AddKey(new Keyframe(.35f, 0, 4, 4));
                curve.AddKey(new Keyframe(.4f, 0, -5, -5));

                curve.AddKey(new Keyframe(.5f, .5f, 10, 10));

                curve.AddKey(new Keyframe(.6f, 1, -5, -5));
                curve.AddKey(new Keyframe(.65f, 1, 4, 4));
                curve.AddKey(new Keyframe(.7f, 1, -2, -2));
                curve.AddKey(new Keyframe(.75f, 1, 1.5f, 1.5f));
                curve.AddKey(new Keyframe(.8f, 1, -1, -1));
                curve.AddKey(new Keyframe(.85f, 1, .5f, .5f));
                curve.AddKey(new Keyframe(.9f, 1, 0, 0));
                curve.AddKey(new Keyframe(1, 1, 0, 0));

                return curve;
            }
        }
        public static AnimationCurve bounceIn
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, .5f, .5f));
                curve.AddKey(new Keyframe(.1f, 0, -1, 1));
                curve.AddKey(new Keyframe(.25f, 0, -1.5f, 1.5f));
                curve.AddKey(new Keyframe(.45f, 0, -2, 2));
                curve.AddKey(new Keyframe(.7f, 0, -2.5f, 2.5f));
                curve.AddKey(new Keyframe(1, 1, .65f, .65f));

                return curve;
            }
        }
        public static AnimationCurve bounceOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, .65f, .65f));
                curve.AddKey(new Keyframe(.3f, 1, 2.5f, -2.5f));
                curve.AddKey(new Keyframe(.55f, 1, 2, -2));
                curve.AddKey(new Keyframe(.75f, 1, 1.5f, -1.5f));
                curve.AddKey(new Keyframe(.9f, 1, 1, -1));
                curve.AddKey(new Keyframe(1, 1, .5f, .5f));

                return curve;
            }
        }
        public static AnimationCurve bounceInOut
        {
            get
            {
                AnimationCurve curve = new AnimationCurve();

                curve.AddKey(new Keyframe(0, 0, .5f, .5f));
                curve.AddKey(new Keyframe(.1f / 2, 0, -1, 1));
                curve.AddKey(new Keyframe(.25f / 2, 0, -1.5f, 1.5f));
                curve.AddKey(new Keyframe(.45f / 2, 0, -2, 2));
                curve.AddKey(new Keyframe(.7f / 2, 0, -2.5f, 2.5f));

                curve.AddKey(new Keyframe(0.5f, 0.5f, .65f, .65f));

                curve.AddKey(new Keyframe(.3f / 2 + .5f, 1, 2.5f, -2.5f));
                curve.AddKey(new Keyframe(.55f / 2 + .5f, 1, 2, -2));
                curve.AddKey(new Keyframe(.75f / 2 + .5f, 1, 1.5f, -1.5f));
                curve.AddKey(new Keyframe(.9f / 2 + .5f, 1, 1, -1));
                curve.AddKey(new Keyframe(1, 1, .5f, .5f));

                return curve;
            }
        }
    }
}
