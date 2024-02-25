using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using VinTools;
using VinTools.Tweening;
using VinTools.Tweening.Internal;

namespace VinTools.Tweening
{
    public static class VinTween
    {
		/// <summary>
		/// Creates the instance of the VinTween manager. This function can be called to set up the VinTween manager if some animations don't play at the start of the game
		/// </summary>
		public static void Initialize() 
		{
			var vam = VinTweenManager.instance;
		}

        //value tweening
        public static VinTweenElement TweenValue(UnityAction<float> onValueChanged, float from, float to, float time) { return new VinTweenElement(onValueChanged, from, to, time); }
        public static VinTweenElement TweenValue(UnityAction<Vector2> onValueChanged, Vector2 from, Vector2 to, float time) { return new VinTweenElement(onValueChanged, from, to, time); }
        public static VinTweenElement TweenValue(UnityAction<Vector3> onValueChanged, Vector3 from, Vector3 to, float time) { return new VinTweenElement(onValueChanged, from, to, time); }
        public static VinTweenElement TweenValue(UnityAction<Color> onValueChanged, Color from, Color to, float time) { return new VinTweenElement(onValueChanged, from, to, time); }
        public static VinTweenElement TweenValue(UnityAction<Quaternion> onValueChanged, Quaternion from, Quaternion to, float time) { return new VinTweenElement(onValueChanged, from, to, time); }

        //object tweening
        public static VinTweenElement TweenAlpha(SpriteRenderer obj, float to, float time) { return new VinTweenElement(TweenType.ColorSprite, obj, new Color(obj.color.r, obj.color.g, obj.color.b, to), time); }
        public static VinTweenElement TweenAlpha(Image obj, float to, float time) { return new VinTweenElement(TweenType.ColorImage, obj, new Color(obj.color.r, obj.color.g, obj.color.b, to), time); }
        public static VinTweenElement TweenAlpha(Text obj, float to, float time) { return new VinTweenElement(TweenType.ColorText, obj, new Color(obj.color.r, obj.color.g, obj.color.b, to), time); }
        public static VinTweenElement TweenAlpha(TextMeshProUGUI obj, float to, float time) { return new VinTweenElement(TweenType.ColorTMP, obj, new Color(obj.color.r, obj.color.g, obj.color.b, to), time); }
        public static VinTweenElement TweenAlpha(CanvasGroup obj, float to, float time) { return new VinTweenElement(TweenType.AlphaCanvas, obj, to, time); }

        public static VinTweenElement TweenColor(SpriteRenderer obj, Color to, float time) { return new VinTweenElement(TweenType.ColorSprite, obj, to, time); }
        public static VinTweenElement TweenColor(Image obj, Color to, float time) { return new VinTweenElement(TweenType.ColorImage, obj, to, time); }
        public static VinTweenElement TweenColor(Text obj, Color to, float time) { return new VinTweenElement(TweenType.ColorText, obj, to, time); }
        public static VinTweenElement TweenColor(TextMeshProUGUI obj, Color to, float time) { return new VinTweenElement(TweenType.ColorTMP, obj, to, time); }

        public static VinTweenElement TweenMove(Transform obj, Vector3 to, float time) { return new VinTweenElement(TweenType.Move, obj, to, time); }
        public static VinTweenElement TweenMove(RectTransform obj, Vector3 to, float time) { return new VinTweenElement(TweenType.Move, obj.transform, to, time); }
        public static VinTweenElement TweenMove(GameObject obj, Vector3 to, float time) { return new VinTweenElement(TweenType.Move, obj.transform, to, time); }
        
        public static VinTweenElement TweenMoveX(Transform obj, float to, float time) { return new VinTweenElement(TweenType.MoveX, obj, to, time); }
        public static VinTweenElement TweenMoveX(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.MoveX, obj.transform, to, time); }
        public static VinTweenElement TweenMoveX(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.MoveX, obj.transform, to, time); }
        
        public static VinTweenElement TweenMoveY(Transform obj, float to, float time) { return new VinTweenElement(TweenType.MoveY, obj, to, time); }
        public static VinTweenElement TweenMoveY(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.MoveY, obj.transform, to, time); }
        public static VinTweenElement TweenMoveY(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.MoveY, obj.transform, to, time); }
        
        public static VinTweenElement TweenMoveZ(Transform obj, float to, float time) { return new VinTweenElement(TweenType.MoveZ, obj, to, time); }
        public static VinTweenElement TweenMoveZ(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.MoveZ, obj.transform, to, time); }
        public static VinTweenElement TweenMoveZ(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.MoveZ, obj.transform, to, time); }

        public static VinTweenElement TweenMoveLocal(Transform obj, Vector3 to, float time) { return new VinTweenElement(TweenType.MoveLocal, obj, to, time); }
        public static VinTweenElement TweenMoveLocal(RectTransform obj, Vector3 to, float time) { return new VinTweenElement(TweenType.MoveLocal, obj.transform, to, time); }
        public static VinTweenElement TweenMoveLocal(GameObject obj, Vector3 to, float time) { return new VinTweenElement(TweenType.MoveLocal, obj.transform, to, time); }
        
        public static VinTweenElement TweenMoveLocalX(Transform obj, float to, float time) { return new VinTweenElement(TweenType.MoveLocalX, obj, to, time); }
        public static VinTweenElement TweenMoveLocalX(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.MoveLocalX, obj.transform, to, time); }
        public static VinTweenElement TweenMoveLocalX(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.MoveLocalX, obj.transform, to, time); }
        
        public static VinTweenElement TweenMoveLocalY(Transform obj, float to, float time) { return new VinTweenElement(TweenType.MoveLocalY, obj, to, time); }
        public static VinTweenElement TweenMoveLocalY(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.MoveLocalY, obj.transform, to, time); }
        public static VinTweenElement TweenMoveLocalY(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.MoveLocalY, obj.transform, to, time); }
        
        public static VinTweenElement TweenMoveLocalZ(Transform obj, float to, float time) { return new VinTweenElement(TweenType.MoveLocalZ, obj, to, time); }
        public static VinTweenElement TweenMoveLocalZ(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.MoveLocalZ, obj.transform, to, time); }
        public static VinTweenElement TweenMoveLocalZ(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.MoveLocalZ, obj.transform, to, time); }

        public static VinTweenElement TweenScaleLocal(Transform obj, Vector3 to, float time) { return new VinTweenElement(TweenType.ScaleLocal, obj, to, time); }
        public static VinTweenElement TweenScaleLocal(RectTransform obj, Vector3 to, float time) { return new VinTweenElement(TweenType.ScaleLocal, obj.transform, to, time); }
        public static VinTweenElement TweenScaleLocal(GameObject obj, Vector3 to, float time) { return new VinTweenElement(TweenType.ScaleLocal, obj.transform, to, time); }
        
        public static VinTweenElement TweenScaleLocalX(Transform obj, float to, float time) { return new VinTweenElement(TweenType.ScaleLocalX, obj, to, time); }
        public static VinTweenElement TweenScaleLocalX(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.ScaleLocalX, obj.transform, to, time); }
        public static VinTweenElement TweenScaleLocalX(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.ScaleLocalX, obj.transform, to, time); }
        
        public static VinTweenElement TweenScaleLocalY(Transform obj, float to, float time) { return new VinTweenElement(TweenType.ScaleLocalY, obj, to, time); }
        public static VinTweenElement TweenScaleLocalY(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.ScaleLocalY, obj.transform, to, time); }
        public static VinTweenElement TweenScaleLocalY(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.ScaleLocalY, obj.transform, to, time); }
        
        public static VinTweenElement TweenScaleLocalZ(Transform obj, float to, float time) { return new VinTweenElement(TweenType.ScaleLocalZ, obj, to, time); }
        public static VinTweenElement TweenScaleLocalZ(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.ScaleLocalZ, obj.transform, to, time); }
        public static VinTweenElement TweenScaleLocalZ(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.ScaleLocalZ, obj.transform, to, time); }

        public static VinTweenElement TweenRotate(Transform obj, Quaternion to, float time) { return new VinTweenElement(TweenType.Rotate, obj, to, time); }
        public static VinTweenElement TweenRotate(RectTransform obj, Quaternion to, float time) { return new VinTweenElement(TweenType.Rotate, obj.transform, to, time); }
        public static VinTweenElement TweenRotate(GameObject obj, Quaternion to, float time) { return new VinTweenElement(TweenType.Rotate, obj.transform, to, time); }
        
        public static VinTweenElement TweenRotate(Transform obj, Vector3 to, float time) { return new VinTweenElement(TweenType.Rotate, obj, Quaternion.Euler(to), time); }
        public static VinTweenElement TweenRotate(RectTransform obj, Vector3 to, float time) { return new VinTweenElement(TweenType.Rotate, obj.transform, Quaternion.Euler(to), time); }
        public static VinTweenElement TweenRotate(GameObject obj, Vector3 to, float time) { return new VinTweenElement(TweenType.Rotate, obj.transform, Quaternion.Euler(to), time); }

        public static VinTweenElement TweenRotateX(Transform obj, float to, float time) { return new VinTweenElement(TweenType.RotateX, obj, to, time); }
        public static VinTweenElement TweenRotateX(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.RotateX, obj.transform, to, time); }
        public static VinTweenElement TweenRotateX(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.RotateX, obj.transform, to, time); }

        public static VinTweenElement TweenRotateY(Transform obj, float to, float time) { return new VinTweenElement(TweenType.RotateY, obj, to, time); }
        public static VinTweenElement TweenRotateY(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.RotateY, obj.transform, to, time); }
        public static VinTweenElement TweenRotateY(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.RotateY, obj.transform, to, time); }

        public static VinTweenElement TweenRotateZ(Transform obj, float to, float time) { return new VinTweenElement(TweenType.RotateZ, obj, to, time); }
        public static VinTweenElement TweenRotateZ(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.RotateZ, obj.transform, to, time); }
        public static VinTweenElement TweenRotateZ(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.RotateZ, obj.transform, to, time); }

        public static VinTweenElement TweenRotateLocal(Transform obj, Quaternion to, float time) { return new VinTweenElement(TweenType.RotateLocal, obj, to, time); }
        public static VinTweenElement TweenRotateLocal(RectTransform obj, Quaternion to, float time) { return new VinTweenElement(TweenType.RotateLocal, obj.transform, to, time); }
        public static VinTweenElement TweenRotateLocal(GameObject obj, Quaternion to, float time) { return new VinTweenElement(TweenType.RotateLocal, obj.transform, to, time); }
        
        public static VinTweenElement TweenRotateLocal(Transform obj, Vector3 to, float time) { return new VinTweenElement(TweenType.RotateLocal, obj, Quaternion.Euler(to), time); }
        public static VinTweenElement TweenRotateLocal(RectTransform obj, Vector3 to, float time) { return new VinTweenElement(TweenType.RotateLocal, obj.transform, Quaternion.Euler(to), time); }
        public static VinTweenElement TweenRotateLocal(GameObject obj, Vector3 to, float time) { return new VinTweenElement(TweenType.RotateLocal, obj.transform, Quaternion.Euler(to), time); }

        public static VinTweenElement TweenRotateLocalX(Transform obj, float to, float time) { return new VinTweenElement(TweenType.RotateLocalX, obj, to, time); }
        public static VinTweenElement TweenRotateLocalX(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.RotateLocalX, obj.transform, to, time); }
        public static VinTweenElement TweenRotateLocalX(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.RotateLocalX, obj.transform, to, time); }

        public static VinTweenElement TweenRotateLocalY(Transform obj, float to, float time) { return new VinTweenElement(TweenType.RotateLocalY, obj, to, time); }
        public static VinTweenElement TweenRotateLocalY(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.RotateLocalY, obj.transform, to, time); }
        public static VinTweenElement TweenRotateLocalY(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.RotateLocalY, obj.transform, to, time); }

        public static VinTweenElement TweenRotateLocalZ(Transform obj, float to, float time) { return new VinTweenElement(TweenType.RotateLocalZ, obj, to, time); }
        public static VinTweenElement TweenRotateLocalZ(RectTransform obj, float to, float time) { return new VinTweenElement(TweenType.RotateLocalZ, obj.transform, to, time); }
        public static VinTweenElement TweenRotateLocalZ(GameObject obj, float to, float time) { return new VinTweenElement(TweenType.RotateLocalZ, obj.transform, to, time); }

        public static void CancelTween(CanvasGroup obj) => VinTweenManager.instance.CancelTween(obj);
        public static void CancelTween(Image obj) => VinTweenManager.instance.CancelTween(obj);
        public static void CancelTween(SpriteRenderer obj) => VinTweenManager.instance.CancelTween(obj);
        public static void CancelTween(TextMeshProUGUI obj) => VinTweenManager.instance.CancelTween(obj);
        public static void CancelTween(Text obj) => VinTweenManager.instance.CancelTween(obj);
        public static void CancelTween(Transform obj) => VinTweenManager.instance.CancelTween(obj);

        //unsupported by canceltween
        public static VinTweenElement TweenSize(RectTransform obj, Vector2 to, float time) { return TweenValue((Vector2 val) => { obj.sizeDelta = val; }, obj.sizeDelta, to, time); }
        public static VinTweenElement TweenAnchoredPos(RectTransform obj, Vector2 to, float time) { return TweenValue((Vector2 val) => { obj.anchoredPosition = val; }, obj.anchoredPosition, to, time); }
    }

    public static class VinTweenExtensions
    {
        //object tweening
        public static VinTweenElement TweenAlpha(this SpriteRenderer obj, float to, float time) { return VinTween.TweenAlpha(obj, to, time); }
        public static VinTweenElement TweenAlpha(this Image obj, float to, float time) { return VinTween.TweenAlpha(obj, to, time); }
        public static VinTweenElement TweenAlpha(this Text obj, float to, float time) { return VinTween.TweenAlpha(obj, to, time); }
        public static VinTweenElement TweenAlpha(this TextMeshProUGUI obj, float to, float time) { return VinTween.TweenAlpha(obj, to, time); }
        public static VinTweenElement TweenAlpha(this CanvasGroup obj, float to, float time) { return VinTween.TweenAlpha(obj, to, time); }

        public static VinTweenElement TweenColor(this SpriteRenderer obj, Color to, float time) { return VinTween.TweenColor(obj, to, time); }
        public static VinTweenElement TweenColor(this Image obj, Color to, float time) { return VinTween.TweenColor(obj, to, time); }
        public static VinTweenElement TweenColor(this Text obj, Color to, float time) { return VinTween.TweenColor(obj, to, time); }
        public static VinTweenElement TweenColor(this TextMeshProUGUI obj, Color to, float time) { return VinTween.TweenColor(obj, to, time); }

        public static VinTweenElement TweenMove(this Transform obj, Vector3 to, float time) { return VinTween.TweenMove(obj, to, time); }
        public static VinTweenElement TweenMove(this RectTransform obj, Vector3 to, float time) { return VinTween.TweenMove(obj, to, time); }
        public static VinTweenElement TweenMove(this GameObject obj, Vector3 to, float time) { return VinTween.TweenMove(obj, to, time); }
        
        public static VinTweenElement TweenMoveX(this Transform obj, float to, float time) { return VinTween.TweenMoveX(obj, to, time); }
        public static VinTweenElement TweenMoveX(this RectTransform obj, float to, float time) { return VinTween.TweenMoveX(obj, to, time); }
        public static VinTweenElement TweenMoveX(this GameObject obj, float to, float time) { return VinTween.TweenMoveX(obj, to, time); }
       
        public static VinTweenElement TweenMoveY(this Transform obj, float to, float time) { return VinTween.TweenMoveY(obj, to, time); }
        public static VinTweenElement TweenMoveY(this RectTransform obj, float to, float time) { return VinTween.TweenMoveY(obj, to, time); }
        public static VinTweenElement TweenMoveY(this GameObject obj, float to, float time) { return VinTween.TweenMoveY(obj, to, time); }
        
        public static VinTweenElement TweenMoveZ(this Transform obj, float to, float time) { return VinTween.TweenMoveZ(obj, to, time); }
        public static VinTweenElement TweenMoveZ(this RectTransform obj, float to, float time) { return VinTween.TweenMoveZ(obj, to, time); }
        public static VinTweenElement TweenMoveZ(this GameObject obj, float to, float time) { return VinTween.TweenMoveZ(obj, to, time); }

        public static VinTweenElement TweenMoveLocal(this Transform obj, Vector3 to, float time) { return VinTween.TweenMoveLocal(obj, to, time); }
        public static VinTweenElement TweenMoveLocal(this RectTransform obj, Vector3 to, float time) { return VinTween.TweenMoveLocal(obj, to, time); }
        public static VinTweenElement TweenMoveLocal(this GameObject obj, Vector3 to, float time) { return VinTween.TweenMoveLocal(obj, to, time); }
       
        public static VinTweenElement TweenMoveLocalX(this Transform obj, float to, float time) { return VinTween.TweenMoveLocalX(obj, to, time); }
        public static VinTweenElement TweenMoveLocalX(this RectTransform obj, float to, float time) { return VinTween.TweenMoveLocalX(obj, to, time); }
        public static VinTweenElement TweenMoveLocalX(this GameObject obj, float to, float time) { return VinTween.TweenMoveLocalX(obj, to, time); }
        
        public static VinTweenElement TweenMoveLocalY(this Transform obj, float to, float time) { return VinTween.TweenMoveLocalY(obj, to, time); }
        public static VinTweenElement TweenMoveLocalY(this RectTransform obj, float to, float time) { return VinTween.TweenMoveLocalY(obj, to, time); }
        public static VinTweenElement TweenMoveLocalY(this GameObject obj, float to, float time) { return VinTween.TweenMoveLocalY(obj, to, time); }
        
        public static VinTweenElement TweenMoveLocalZ(this Transform obj, float to, float time) { return VinTween.TweenMoveLocalZ(obj, to, time); }
        public static VinTweenElement TweenMoveLocalZ(this RectTransform obj, float to, float time) { return VinTween.TweenMoveLocalZ(obj, to, time); }
        public static VinTweenElement TweenMoveLocalZ(this GameObject obj, float to, float time) { return VinTween.TweenMoveLocalZ(obj, to, time); }

        public static VinTweenElement TweenScaleLocal(this Transform obj, Vector3 to, float time) { return VinTween.TweenScaleLocal(obj, to, time); }
        public static VinTweenElement TweenScaleLocal(this RectTransform obj, Vector3 to, float time) { return VinTween.TweenScaleLocal(obj, to, time); }
        public static VinTweenElement TweenScaleLocal(this GameObject obj, Vector3 to, float time) { return VinTween.TweenScaleLocal(obj, to, time); }
       
        public static VinTweenElement TweenScaleLocalX(this Transform obj, float to, float time) { return VinTween.TweenScaleLocalX(obj, to, time); }
        public static VinTweenElement TweenScaleLocalX(this RectTransform obj, float to, float time) { return VinTween.TweenScaleLocalX(obj, to, time); }
        public static VinTweenElement TweenScaleLocalX(this GameObject obj, float to, float time) { return VinTween.TweenScaleLocalX(obj, to, time); }
        
        public static VinTweenElement TweenScaleLocalY(this Transform obj, float to, float time) { return VinTween.TweenScaleLocalY(obj, to, time); }
        public static VinTweenElement TweenScaleLocalY(this RectTransform obj, float to, float time) { return VinTween.TweenScaleLocalY(obj, to, time); }
        public static VinTweenElement TweenScaleLocalY(this GameObject obj, float to, float time) { return VinTween.TweenScaleLocalY(obj, to, time); }
        
        public static VinTweenElement TweenScaleLocalZ(this Transform obj, float to, float time) { return VinTween.TweenScaleLocalZ(obj, to, time); }
        public static VinTweenElement TweenScaleLocalZ(this RectTransform obj, float to, float time) { return VinTween.TweenScaleLocalZ(obj, to, time); }
        public static VinTweenElement TweenScaleLocalZ(this GameObject obj, float to, float time) { return VinTween.TweenScaleLocalZ(obj, to, time); }

        public static VinTweenElement TweenRotate(this Transform obj, Quaternion to, float time) { return VinTween.TweenRotate(obj, to, time); }
        public static VinTweenElement TweenRotate(this RectTransform obj, Quaternion to, float time) { return VinTween.TweenRotate(obj, to, time); }
        public static VinTweenElement TweenRotate(this GameObject obj, Quaternion to, float time) { return VinTween.TweenRotate(obj, to, time); }
        
        public static VinTweenElement TweenRotate(this Transform obj, Vector3 to, float time) { return VinTween.TweenRotate(obj, to, time); }
        public static VinTweenElement TweenRotate(this RectTransform obj, Vector3 to, float time) { return VinTween.TweenRotate(obj, to, time); }
        public static VinTweenElement TweenRotate(this GameObject obj, Vector3 to, float time) { return VinTween.TweenRotate(obj, to, time); }

        public static VinTweenElement TweenRotateX(this Transform obj, float to, float time) { return VinTween.TweenRotateX(obj, to, time); }
        public static VinTweenElement TweenRotateX(this RectTransform obj, float to, float time) { return VinTween.TweenRotateX(obj, to, time); }
        public static VinTweenElement TweenRotateX(this GameObject obj, float to, float time) { return VinTween.TweenRotateX(obj, to, time); }

        public static VinTweenElement TweenRotateY(this Transform obj, float to, float time) { return VinTween.TweenRotateY(obj, to, time); }
        public static VinTweenElement TweenRotateY(this RectTransform obj, float to, float time) { return VinTween.TweenRotateY(obj, to, time); }
        public static VinTweenElement TweenRotateY(this GameObject obj, float to, float time) { return VinTween.TweenRotateY(obj, to, time); }

        public static VinTweenElement TweenRotateZ(this Transform obj, float to, float time) { return VinTween.TweenRotateZ(obj, to, time); }
        public static VinTweenElement TweenRotateZ(this RectTransform obj, float to, float time) { return VinTween.TweenRotateZ(obj, to, time); }
        public static VinTweenElement TweenRotateZ(this GameObject obj, float to, float time) { return VinTween.TweenRotateZ(obj, to, time); }

        public static VinTweenElement TweenRotateLocal(this Transform obj, Quaternion to, float time) { return VinTween.TweenRotateLocal(obj, to, time); }
        public static VinTweenElement TweenRotateLocal(this RectTransform obj, Quaternion to, float time) { return VinTween.TweenRotateLocal(obj, to, time); }
        public static VinTweenElement TweenRotateLocal(this GameObject obj, Quaternion to, float time) { return VinTween.TweenRotateLocal(obj, to, time); }
        
        public static VinTweenElement TweenRotateLocal(this Transform obj, Vector3 to, float time) { return VinTween.TweenRotateLocal(obj, to, time); }
        public static VinTweenElement TweenRotateLocal(this RectTransform obj, Vector3 to, float time) { return VinTween.TweenRotateLocal(obj, to, time); }
        public static VinTweenElement TweenRotateLocal(this GameObject obj, Vector3 to, float time) { return VinTween.TweenRotateLocal(obj, to, time); }

        public static VinTweenElement TweenRotateLocalX(this Transform obj, float to, float time) { return VinTween.TweenRotateLocalX(obj, to, time); }
        public static VinTweenElement TweenRotateLocalX(this RectTransform obj, float to, float time) { return VinTween.TweenRotateLocalX(obj, to, time); }
        public static VinTweenElement TweenRotateLocalX(this GameObject obj, float to, float time) { return VinTween.TweenRotateLocalX(obj, to, time); }

        public static VinTweenElement TweenRotateLocalY(this Transform obj, float to, float time) { return VinTween.TweenRotateLocalY(obj, to, time); }
        public static VinTweenElement TweenRotateLocalY(this RectTransform obj, float to, float time) { return VinTween.TweenRotateLocalY(obj, to, time); }
        public static VinTweenElement TweenRotateLocalY(this GameObject obj, float to, float time) { return VinTween.TweenRotateLocalY(obj, to, time); }

        public static VinTweenElement TweenRotateLocalZ(this Transform obj, float to, float time) { return VinTween.TweenRotateLocalZ(obj, to, time); }
        public static VinTweenElement TweenRotateLocalZ(this RectTransform obj, float to, float time) { return VinTween.TweenRotateLocalZ(obj, to, time); }
        public static VinTweenElement TweenRotateLocalZ(this GameObject obj, float to, float time) { return VinTween.TweenRotateLocalZ(obj, to, time); }

        public static void CancelTween(this CanvasGroup obj) => VinTweenManager.instance.CancelTween(obj);
        public static void CancelTween(this Image obj) => VinTweenManager.instance.CancelTween(obj);
        public static void CancelTween(this SpriteRenderer obj) => VinTweenManager.instance.CancelTween(obj);
        public static void CancelTween(this TextMeshProUGUI obj) => VinTweenManager.instance.CancelTween(obj);
        public static void CancelTween(this Text obj) => VinTweenManager.instance.CancelTween(obj);
        public static void CancelTween(this Transform obj) => VinTweenManager.instance.CancelTween(obj);

        //unsupported by canceltween
        public static VinTweenElement TweenSize(this RectTransform obj, Vector2 to, float time) { return VinTween.TweenSize(obj, to, time); }
        public static VinTweenElement TweenAnchoredPos(this RectTransform obj, Vector2 to, float time) { return VinTween.TweenAnchoredPos(obj, to, time); }
    }
}

namespace VinTools.Tweening.Internal
{
    /// <summary>
    /// Class that contains every variable for a tween 
    /// </summary>
    [System.Serializable]
    public class VinTweenElement
    {
        private TweenType tweenType;

        //vin tween target
        public Transform VTT_Transform;
        public Image VTT_Image;
        public SpriteRenderer VTT_SpriteRenderer;
        public Text VTT_Text;
        public TextMeshProUGUI VTT_TMP;
        public CanvasGroup VTT_CanvasGroup;

        private UnityAction<float> VTV_Float;
        private UnityAction<Vector2> VTV_Vector2;
        private UnityAction<Vector3> VTV_Vector3;
        private UnityAction<Color> VTV_Color;
        private UnityAction<Quaternion> VTV_Quaternion;

        //tween target
        private Quaternion TweenStart_Quaternion;
        private Quaternion TweenTarget_Quaternion;
        private Vector3 TweenStart_Vector3;
        private Vector3 TweenTarget_Vector3;
        private Vector2 TweenStart_Vector2;
        private Vector2 TweenTarget_Vector2;
        private float TweenStart_Float;
        private float TweenTarget_Float;
        private Color TweenStart_Color;
        private Color TweenTarget_Color;

        //animation
        private AnimationCurve Anim_Curve;

        //Optional
        private UnityEvent OT_OnStartAction = new UnityEvent();
        private UnityEvent OT_OnCompleteAction = new UnityEvent();

        //private variables
        AnimationCurve animationCurve
        {
            get
            {
                if (Anim_Curve != null) return Anim_Curve;
                else return AnimationCurve.Linear(0, 0, 1, 1);
            }
        }
        float tweenStartTime;
        float tweenEndTime;
        float startDelay;
        float endDelay;
        bool started = false;
        bool cancelled = false;

        #region setups
        //value tweening
        public VinTweenElement(UnityAction<float> onValueChanged, float from, float to, float time)
        {
            this.VTV_Float = onValueChanged;
            this.TweenTarget_Float = to;
            this.TweenStart_Float = from;

            EndSetup(TweenType.ValueFloat, time);
        }
        public VinTweenElement(UnityAction<Vector2> onValueChanged, Vector2 from, Vector2 to, float time)
        {
            this.VTV_Vector2 = onValueChanged;
            this.TweenTarget_Vector2 = to;
            this.TweenStart_Vector2 = from;

            EndSetup(TweenType.ValueVector2, time);
        }
        public VinTweenElement(UnityAction<Vector3> onValueChanged, Vector3 from, Vector3 to, float time)
        {
            this.VTV_Vector3 = onValueChanged;
            this.TweenTarget_Vector3 = to;
            this.TweenStart_Vector3 = from;

            EndSetup(TweenType.ValueVector3, time);
        }
        public VinTweenElement(UnityAction<Color> onValueChanged, Color from, Color to, float time)
        {
            this.VTV_Color = onValueChanged;
            this.TweenTarget_Color = to;
            this.TweenStart_Color = from;

            EndSetup(TweenType.ValueColor, time);
        }
        public VinTweenElement(UnityAction<Quaternion> onValueChanged, Quaternion from, Quaternion to, float time)
        {
            this.VTV_Quaternion = onValueChanged;
            this.TweenTarget_Quaternion = to;
            this.TweenStart_Quaternion = from;

            EndSetup(TweenType.ValueQuaternion, time);
        }

        //object tweening
        public VinTweenElement(TweenType type, Transform obj, Vector3 to, float time)
        {
            this.VTT_Transform = obj;
            this.TweenTarget_Vector3 = to;

            switch (type)
            {
                case TweenType.Move:
                    this.TweenStart_Vector3 = obj.position;
                    break;
                case TweenType.MoveLocal:
                    this.TweenStart_Vector3 = obj.localPosition;
                    break;
                case TweenType.ScaleLocal:
                    this.TweenStart_Vector3 = obj.localScale;
                    break;
                default:
                    break;
            }

            EndSetup(type, time);
        }
        public VinTweenElement(TweenType type, Transform obj, Quaternion to, float time)
        {
            this.VTT_Transform = obj;
            this.TweenTarget_Quaternion = to;

            switch (type)
            {
                case TweenType.Rotate:
                    this.TweenStart_Quaternion = obj.rotation;
                    break;
                case TweenType.RotateLocal:
                    this.TweenStart_Quaternion = obj.localRotation;
                    break;
                default:
                    break;
            }

            EndSetup(type, time);
        }
        public VinTweenElement(TweenType type, Transform obj, float to, float time)
        {
            this.VTT_Transform = obj;
            this.TweenTarget_Float = to;

            switch (type)
            {
                case TweenType.MoveX:
                    this.TweenStart_Float = obj.position.x;
                    break;
                case TweenType.MoveY:
                    this.TweenStart_Float = obj.position.y;
                    break;
                case TweenType.MoveZ:
                    this.TweenStart_Float = obj.position.z;
                    break;
                case TweenType.MoveLocalX:
                    this.TweenStart_Float = obj.localPosition.x;
                    break;
                case TweenType.MoveLocalY:
                    this.TweenStart_Float = obj.localPosition.y;
                    break;
                case TweenType.MoveLocalZ:
                    this.TweenStart_Float = obj.localPosition.z;
                    break;
                case TweenType.ScaleLocalX:
                    this.TweenStart_Float = obj.localScale.x;
                    break;
                case TweenType.ScaleLocalY:
                    this.TweenStart_Float = obj.localScale.y;
                    break;
                case TweenType.ScaleLocalZ:
                    this.TweenStart_Float = obj.localScale.z;
                    break;
                case TweenType.RotateX:
                    this.TweenStart_Float = obj.rotation.eulerAngles.x;
                    break;
                case TweenType.RotateY:
                    this.TweenStart_Float = obj.rotation.eulerAngles.y;
                    break;
                case TweenType.RotateZ:
                    this.TweenStart_Float = obj.rotation.eulerAngles.z;
                    break;
                case TweenType.RotateLocalX:
                    this.TweenStart_Float = obj.localRotation.eulerAngles.x;
                    break;
                case TweenType.RotateLocalY:
                    this.TweenStart_Float = obj.localRotation.eulerAngles.y;
                    break;
                case TweenType.RotateLocalZ:
                    this.TweenStart_Float = obj.localRotation.eulerAngles.z;
                    break;
                default:
                    break;
            }

            EndSetup(type, time);
        }
        public VinTweenElement(TweenType type, SpriteRenderer obj, Color to, float time)
        {
            this.VTT_SpriteRenderer = obj;
            this.TweenTarget_Color = to;

            switch (type)
            {
                case TweenType.ColorSprite:
                    this.TweenStart_Color = obj.color;
                    break;
            }

            EndSetup(type, time);
        }
        public VinTweenElement(TweenType type, Image obj, Color to, float time)
        {
            this.VTT_Image = obj;
            this.TweenTarget_Color = to;

            switch (type)
            {
                case TweenType.ColorImage:
                    this.TweenStart_Color = obj.color;
                    break;
            }

            EndSetup(type, time);
        }
        public VinTweenElement(TweenType type, Text obj, Color to, float time)
        {
            this.VTT_Text = obj;
            this.TweenTarget_Color = to;

            switch (type)
            {
                case TweenType.ColorText:
                    this.TweenStart_Color = obj.color;
                    break;
            }

            EndSetup(type, time);
        }
        public VinTweenElement(TweenType type, TextMeshProUGUI obj, Color to, float time)
        {
            this.VTT_TMP = obj;
            this.TweenTarget_Color = to;

            switch (type)
            {
                case TweenType.ColorTMP:
                    this.TweenStart_Color = obj.color;
                    break;
            }

            EndSetup(type, time);
        }
        public VinTweenElement(TweenType type, CanvasGroup obj, float to, float time)
        {
            this.VTT_CanvasGroup = obj;
            this.TweenTarget_Float = to;

            switch (type)
            {
                case TweenType.AlphaCanvas:
                    this.TweenStart_Float = obj.alpha;
                    break;
            }

            EndSetup(type, time);
        }

        void EndSetup(TweenType type, float time)
        {
            tweenType = type;

            tweenStartTime = Time.time;
            tweenEndTime = Time.time + time;

            VinTweenManager.instance.AddTween(this);
        }
        #endregion
        
        #region Tweening
        public void UpdateTween()
        {
            float time = tweenEndTime <= tweenStartTime ? 1 : Mathf.InverseLerp(tweenStartTime + startDelay, tweenEndTime + startDelay, Time.time);
            
            if (!started && tweenStartTime + startDelay <= Time.time)
            {
                ExecuteOnStart();
                started = true;
            }

            switch (tweenType)
            {
                case TweenType.ValueFloat:
                    UpdateTween_ValueFloat(time);
                    break;
                case TweenType.ValueVector2:
                    UpdateTween_ValueVector2(time);
                    break;
                case TweenType.ValueVector3:
                    UpdateTween_ValueVector3(time);
                    break;
                case TweenType.ValueColor:
                    UpdateTween_ValueColor(time);
                    break;
                case TweenType.ValueQuaternion:
                    UpdateTween_ValueQuaternion(time);
                    break;
                case TweenType.Move:
                    UpdateTween_Move(time);
                    break;
                case TweenType.MoveX:
                    UpdateTween_MoveX(time);
                    break;
                case TweenType.MoveY:
                    UpdateTween_MoveY(time);
                    break;
                case TweenType.MoveZ:
                    UpdateTween_MoveZ(time);
                    break;
                case TweenType.MoveLocal:
                    UpdateTween_MoveLocal(time);
                    break;
                case TweenType.MoveLocalX:
                    UpdateTween_MoveLocalX(time);
                    break;
                case TweenType.MoveLocalY:
                    UpdateTween_MoveLocalY(time);
                    break;
                case TweenType.MoveLocalZ:
                    UpdateTween_MoveLocalZ(time);
                    break;
                case TweenType.AlphaCanvas:
                    UpdateTween_Alpha(time);
                    break;
                case TweenType.ColorImage:
                    UpdateTween_ColorImage(time);
                    break;
                case TweenType.ColorSprite:
                    UpdateTween_ColorSprite(time);
                    break;
                case TweenType.ColorText:
                    UpdateTween_ColorText(time);
                    break;
                case TweenType.ColorTMP:
                    UpdateTween_ColorTMP(time);
                    break;
                case TweenType.ScaleLocal:
                    UpdateTween_ScaleLocal(time);
                    break;
                case TweenType.ScaleLocalX:
                    UpdateTween_ScaleLocalX(time);
                    break;
                case TweenType.ScaleLocalY:
                    UpdateTween_ScaleLocalY(time);
                    break;
                case TweenType.ScaleLocalZ:
                    UpdateTween_ScaleLocalZ(time);
                    break;
                case TweenType.Rotate:
                    UpdateTween_Rotate(time);
                    break;
                case TweenType.RotateX:
                    UpdateTween_RotateX(time);
                    break;
                case TweenType.RotateY:
                    UpdateTween_RotateY(time);
                    break;
                case TweenType.RotateZ:
                    UpdateTween_RotateZ(time);
                    break;
                case TweenType.RotateLocal:
                    UpdateTween_RotateLocal(time);
                    break;
                case TweenType.RotateLocalX:
                    UpdateTween_RotateLocalX(time);
                    break;
                case TweenType.RotateLocalY:
                    UpdateTween_RotateLocalY(time);
                    break;
                case TweenType.RotateLocalZ:
                    UpdateTween_RotateLocalZ(time);
                    break;
                default:
                    break;
            }

            //Debug.Log($"Updating tween: {(int)(time * 100)}%");
        }

        void UpdateTween_ValueFloat(float time)
        {
            if (cancelled) return;

            if (time < 1)
            {
                VTV_Float.Invoke(Evaluate(TweenStart_Float, TweenTarget_Float, time));
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTV_Float.Invoke(TweenTarget_Float);
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_ValueVector2(float time)
        {
            if (cancelled) return;

            if (time < 1)
            {
                VTV_Vector2.Invoke(Evaluate(TweenStart_Vector2, TweenTarget_Vector2, time));
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTV_Vector2.Invoke(TweenTarget_Vector2);
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_ValueVector3(float time)
        {
            if (cancelled) return;

            if (time < 1)
            {
                VTV_Vector3.Invoke(Evaluate(TweenStart_Vector3, TweenTarget_Vector3, time));
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTV_Vector3.Invoke(TweenTarget_Vector3);
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_ValueColor(float time)
        {
            if (cancelled) return;

            if (time < 1)
            {
                VTV_Color.Invoke(Evaluate(TweenStart_Color, TweenTarget_Color, time));
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTV_Color.Invoke(TweenTarget_Color);
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_ValueQuaternion(float time)
        {
            if (cancelled) return;

            if (time < 1)
            {
                VTV_Quaternion.Invoke(Evaluate(TweenStart_Quaternion, TweenTarget_Quaternion, time));
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTV_Quaternion.Invoke(TweenTarget_Quaternion);
                ExecuteOnComplete();
                return;
            }
        }

        void UpdateTween_Move(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.position = Evaluate(TweenStart_Vector3, TweenTarget_Vector3, time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.position = TweenTarget_Vector3;
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_MoveX(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.position = new Vector3(Evaluate(TweenStart_Float, TweenTarget_Float, time), VTT_Transform.position.y, VTT_Transform.position.z);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.position = new Vector3(TweenTarget_Float, VTT_Transform.position.y, VTT_Transform.position.z);
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_MoveY(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.position = new Vector3(VTT_Transform.position.x, Evaluate(TweenStart_Float, TweenTarget_Float, time), VTT_Transform.position.z);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.position = new Vector3(VTT_Transform.position.x, TweenTarget_Float, VTT_Transform.position.z);
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_MoveZ(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.position = new Vector3(VTT_Transform.position.x, VTT_Transform.position.y, Evaluate(TweenStart_Float, TweenTarget_Float, time));
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.position = new Vector3(VTT_Transform.position.x, VTT_Transform.position.y, TweenTarget_Float);
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_MoveLocal(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localPosition = Evaluate(TweenStart_Vector3, TweenTarget_Vector3, time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.localPosition = TweenTarget_Vector3;
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_MoveLocalX(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localPosition = new Vector3(Evaluate(TweenStart_Float, TweenTarget_Float, time), VTT_Transform.localPosition.y, VTT_Transform.localPosition.z);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.localPosition = new Vector3(TweenTarget_Float, VTT_Transform.localPosition.y, VTT_Transform.localPosition.z);
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_MoveLocalY(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localPosition = new Vector3(VTT_Transform.localPosition.x, Evaluate(TweenStart_Float, TweenTarget_Float, time), VTT_Transform.localPosition.z);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.localPosition = new Vector3(VTT_Transform.localPosition.x, TweenTarget_Float, VTT_Transform.localPosition.z);
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_MoveLocalZ(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localPosition = new Vector3(VTT_Transform.localPosition.x, VTT_Transform.localPosition.y, Evaluate(TweenStart_Float, TweenTarget_Float, time));
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.localPosition = new Vector3(VTT_Transform.localPosition.x, VTT_Transform.localPosition.y, TweenTarget_Float);
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_Alpha(float time)
        {
            if (VTT_CanvasGroup == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_CanvasGroup.alpha = Evaluate(TweenStart_Float, TweenTarget_Float, time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_CanvasGroup.alpha = TweenTarget_Float;
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_ColorImage(float time)
        {
            if (VTT_Image == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Image.color = Evaluate(TweenStart_Color, TweenTarget_Color, time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Image.color = TweenTarget_Color;
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_ColorSprite(float time)
        {
            if (VTT_SpriteRenderer == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_SpriteRenderer.color = Evaluate(TweenStart_Color, TweenTarget_Color, time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_SpriteRenderer.color = TweenTarget_Color;
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_ColorText(float time)
        {
            if (VTT_Text == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Text.color = Evaluate(TweenStart_Color, TweenTarget_Color, time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Text.color = TweenTarget_Color;
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_ColorTMP(float time)
        {
            if (VTT_TMP == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_TMP.color = Evaluate(TweenStart_Color, TweenTarget_Color, time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_TMP.color = TweenTarget_Color;
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_ScaleLocal(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localScale = Evaluate(TweenStart_Vector3, TweenTarget_Vector3, time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.localScale = TweenTarget_Vector3;
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_ScaleLocalX(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localScale = new Vector3(Evaluate(TweenStart_Float, TweenTarget_Float, time), VTT_Transform.localPosition.y, VTT_Transform.localPosition.z);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.localScale = new Vector3(TweenTarget_Float, VTT_Transform.localPosition.y, VTT_Transform.localPosition.z);
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_ScaleLocalY(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localScale = new Vector3(VTT_Transform.localPosition.x, Evaluate(TweenStart_Float, TweenTarget_Float, time), VTT_Transform.localPosition.z);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.localScale = new Vector3(VTT_Transform.localPosition.x, TweenTarget_Float, VTT_Transform.localPosition.z);
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_ScaleLocalZ(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localScale = new Vector3(VTT_Transform.localPosition.x, VTT_Transform.localPosition.y, Evaluate(TweenStart_Float, TweenTarget_Float, time));
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.localScale = new Vector3(VTT_Transform.localPosition.x, VTT_Transform.localPosition.y, TweenTarget_Float);
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_Rotate(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.rotation = Evaluate(TweenStart_Quaternion, TweenTarget_Quaternion, time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.rotation = TweenTarget_Quaternion;
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_RotateX(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localRotation = Evaluate(
                    Quaternion.Euler(TweenStart_Float, VTT_Transform.rotation.eulerAngles.y, VTT_Transform.rotation.eulerAngles.z),
                    Quaternion.Euler(TweenTarget_Float, VTT_Transform.rotation.eulerAngles.y, VTT_Transform.rotation.eulerAngles.z),
                    time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.rotation = Quaternion.Euler(new Vector3(TweenTarget_Float, VTT_Transform.rotation.eulerAngles.y, VTT_Transform.rotation.eulerAngles.z));
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_RotateY(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localRotation = Evaluate(
                    Quaternion.Euler(VTT_Transform.rotation.eulerAngles.x, TweenStart_Float, VTT_Transform.rotation.eulerAngles.z),
                    Quaternion.Euler(VTT_Transform.rotation.eulerAngles.x, TweenTarget_Float, VTT_Transform.rotation.eulerAngles.z),
                    time); 
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.rotation = Quaternion.Euler(new Vector3(VTT_Transform.rotation.eulerAngles.x, TweenTarget_Float, VTT_Transform.rotation.eulerAngles.z));
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_RotateZ(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localRotation = Evaluate(
                    Quaternion.Euler(VTT_Transform.rotation.eulerAngles.x, VTT_Transform.rotation.eulerAngles.y, TweenStart_Float),
                    Quaternion.Euler(VTT_Transform.rotation.eulerAngles.x, VTT_Transform.rotation.eulerAngles.y, TweenTarget_Float),
                    time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.rotation = Quaternion.Euler(new Vector3(VTT_Transform.rotation.eulerAngles.x, VTT_Transform.rotation.eulerAngles.y, TweenTarget_Float));
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_RotateLocal(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localRotation = Evaluate(TweenStart_Quaternion, TweenTarget_Quaternion, time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.localRotation = TweenTarget_Quaternion;
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_RotateLocalX(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localRotation = Evaluate(
                    Quaternion.Euler(TweenStart_Float, VTT_Transform.localRotation.eulerAngles.y, VTT_Transform.localRotation.eulerAngles.z),
                    Quaternion.Euler(TweenTarget_Float, VTT_Transform.localRotation.eulerAngles.y, VTT_Transform.localRotation.eulerAngles.z),
                    time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.localRotation = Quaternion.Euler(new Vector3(TweenTarget_Float, VTT_Transform.localRotation.eulerAngles.y, VTT_Transform.localRotation.eulerAngles.z));
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_RotateLocalY(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localRotation = Evaluate(
                    Quaternion.Euler(VTT_Transform.localRotation.eulerAngles.x, TweenStart_Float, VTT_Transform.localRotation.eulerAngles.z),
                    Quaternion.Euler(VTT_Transform.localRotation.eulerAngles.x, TweenTarget_Float, VTT_Transform.localRotation.eulerAngles.z),
                    time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.localRotation = Quaternion.Euler(new Vector3(VTT_Transform.localRotation.eulerAngles.x, TweenTarget_Float, VTT_Transform.localRotation.eulerAngles.z));
                ExecuteOnComplete();
                return;
            }
        }
        void UpdateTween_RotateLocalZ(float time)
        {
            if (VTT_Transform == null) CancelTween();
            if (cancelled) return;

            if (time < 1)
            {
                VTT_Transform.localRotation = Evaluate(
                    Quaternion.Euler(VTT_Transform.localRotation.eulerAngles.x, VTT_Transform.localRotation.eulerAngles.y, TweenStart_Float), 
                    Quaternion.Euler(VTT_Transform.localRotation.eulerAngles.x, VTT_Transform.localRotation.eulerAngles.y, TweenTarget_Float), 
                    time);
                return;
            }

            if (time >= 1 && Time.time >= tweenEndTime + startDelay + endDelay)
            {
                VTT_Transform.localRotation = Quaternion.Euler(new Vector3(VTT_Transform.localRotation.eulerAngles.x, VTT_Transform.localRotation.eulerAngles.y, TweenTarget_Float));
                ExecuteOnComplete();
                return;
            }
        }
        #endregion

        #region Functions
        Vector3 Evaluate(Vector3 start, Vector3 end, float time)
        {
            float step = animationCurve.Evaluate(time);

            /*return  new Vector3(
                Mathf.Lerp(start.x, end.x, step),
                Mathf.Lerp(start.y, end.y, step),
                Mathf.Lerp(start.z, end.z, step)
                );*/

            //return start + (end - start) * step;

            return Vector3.LerpUnclamped(start, end, step);
        }
        float Evaluate(float start, float end, float time)
        {
            float step = animationCurve.Evaluate(time);
            //return start + (end - start) * step;
            return Mathf.LerpUnclamped(start, end, step);
        }
        Color Evaluate(Color start, Color end, float time)
        {
            float step = animationCurve.Evaluate(time);
            /*return new Color(
                Mathf.Clamp01(start.r + (end.r - start.r) * step),
                Mathf.Clamp01(start.g + (end.g - start.g) * step),
                Mathf.Clamp01(start.b + (end.b - start.b) * step),
                Mathf.Clamp01(start.a + (end.a - start.a) * step)
                );*/

            return new Color(
                Mathf.Lerp(start.r, end.r, step),
                Mathf.Lerp(start.g, end.g, step),
                Mathf.Lerp(start.b, end.b, step),
                Mathf.Lerp(start.a, end.a, step)
                );
        }
        Quaternion Evaluate(Quaternion start, Quaternion end, float time)
        {
            float step = animationCurve.Evaluate(time);

            /*return  new Vector3(
                Mathf.Lerp(start.x, end.x, step),
                Mathf.Lerp(start.y, end.y, step),
                Mathf.Lerp(start.z, end.z, step)
                );*/

            return Quaternion.LerpUnclamped(start, end, step);
        }

        void ExecuteOnStart()
        {
            //if (OT_OnStartAction != null) OT_OnStartAction.Invoke();
            OT_OnStartAction.Invoke();
        }
        void ExecuteOnComplete()
        {
            //if (OT_OnCompleteAction != null) OT_OnCompleteAction.Invoke();
            OT_OnCompleteAction.Invoke();

            //Debug.Log("Tween Done");

            VinTweenManager.instance.RemoveTween(this);
        }
        public void CancelTween()
        {
            if (!cancelled)
            {
                cancelled = true;
                VinTweenManager.instance.RemoveTween(this);
            }
        }
        #endregion

        #region Tween extensions
        /// <summary>
        /// Set an action which will execute when the animation starts
        /// </summary>
        /// <param name="action">Action to execute when the animation starts</param>
        /// <returns></returns>
        public VinTweenElement SetOnStart(UnityAction action)
        {
            this.OT_OnStartAction.AddListener(action);
            return this;
        }
        /// <summary>
        /// Set an action which will execute after the animation finished
        /// </summary>
        /// <param name="action">Action to execute upon completition</param>
        /// <returns></returns>
        public VinTweenElement SetOnComplete(UnityAction action)
        {
            this.OT_OnCompleteAction.AddListener(action);
            return this;
        }
        /// <summary>
        /// Set ease curve of the animation
        /// </summary>
        /// <param name="curve">An animation curve, you can use the "EaseType" class to choose from predefined ease types</param>
        /// <returns></returns>
        public VinTweenElement SetEase(AnimationCurve curve)
        {
            Anim_Curve = curve;
            return this;
        }
        /// <summary>
        /// Set ease curve of the animation
        /// </summary>
        /// <param name="curve">An EaseCurve enum to specifiy the animation curve</param>
        /// <returns></returns>
        public VinTweenElement SetEase(EaseCurve curve)
        {
            Anim_Curve = curve.ToAnimationCurve();
            return this;
        }
        /// <summary>
        /// Wait before starting the tween
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public VinTweenElement SetDelay(float delay)
        {
            startDelay = delay;
            return this;
        }
        /// <summary>
        /// Wait after completing the tween before calling the complete action
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public VinTweenElement SetDelayAfter(float delay)
        {
            endDelay = delay;
            return this;
        }
        
        public VinTweenElement SetEaseInBack() { Anim_Curve = EaseType.backIn; return this; }
        public VinTweenElement SetEaseInBounce() { Anim_Curve = EaseType.bounceIn; return this; }
        public VinTweenElement SetEaseInCirc() { Anim_Curve = EaseType.circIn; return this; }
        public VinTweenElement SetEaseInCubic() { Anim_Curve = EaseType.cubicIn; return this; }
        public VinTweenElement SetEaseInElastic() { Anim_Curve = EaseType.elasticIn; return this; }
        public VinTweenElement SetEaseInExpo() { Anim_Curve = EaseType.expoIn; return this; }
        public VinTweenElement SetEaseInQuint() { Anim_Curve = EaseType.quintIn; return this; }
        public VinTweenElement SetEaseInSine() { Anim_Curve = EaseType.sineIn; return this; }
        public VinTweenElement SetEaseInOutBack() { Anim_Curve = EaseType.backInOut; return this; }
        public VinTweenElement SetEaseInOutBounce() { Anim_Curve = EaseType.bounceInOut; return this; }
        public VinTweenElement SetEaseInOutCirc() { Anim_Curve = EaseType.circInOut; return this; }
        public VinTweenElement SetEaseInOutCubic() { Anim_Curve = EaseType.cubicInOut; return this; }
        public VinTweenElement SetEaseInOutElastic() { Anim_Curve = EaseType.elasticInOut; return this; }
        public VinTweenElement SetEaseInOutExpo() { Anim_Curve = EaseType.expoInOut; return this; }
        public VinTweenElement SetEaseInOutQuint() { Anim_Curve = EaseType.quintInOut; return this; }
        public VinTweenElement SetEaseInOutSine() { Anim_Curve = EaseType.sineInOut; return this; }
        public VinTweenElement SetEaseOutBack() { Anim_Curve = EaseType.backOut; return this; }
        public VinTweenElement SetEaseOutBounce() { Anim_Curve = EaseType.bounceOut; return this; }
        public VinTweenElement SetEaseOutCirc() { Anim_Curve = EaseType.circOut; return this; }
        public VinTweenElement SetEaseOutCubic() { Anim_Curve = EaseType.cubicOut; return this; }
        public VinTweenElement SetEaseOutElastic() { Anim_Curve = EaseType.elasticOut; return this; }
        public VinTweenElement SetEaseOutExpo() { Anim_Curve = EaseType.expoOut; return this; }
        public VinTweenElement SetEaseOutQuint() { Anim_Curve = EaseType.quintOut; return this; }
        public VinTweenElement SetEaseOutSine() { Anim_Curve = EaseType.sineOut; return this; }
        #endregion
    }
}
