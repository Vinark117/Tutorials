using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VinTools.Tweening;
using VinTools.Tweening.Internal;

namespace VinTools.Tweening.Internal
{
    public class VinTweenManager : MonoBehaviour
    {
        //set up instance
        static VinTweenManager inst;
        public static VinTweenManager instance
        {
            get
            {
                if (inst != null) return inst;
                else
                {
                    GameObject tweenObj = new GameObject("~VinTween");
                    var tweenManager = tweenObj.AddComponent<VinTweenManager>();
                    DontDestroyOnLoad(tweenManager);
                    inst = tweenManager;
                    return inst;
                }
            }
        }


        private List<VinTweenElement> tweens = new List<VinTweenElement>();
        private List<VinTweenElement> finishedTweens = new List<VinTweenElement>();

        private void Update()
        {
            //bool empty = false;

            try
            {
                foreach (var item in tweens)
                {
                    if (item != null) item.UpdateTween();
                    //else empty = true;
                }
            }
            catch { }

            //if (empty) tweens.RemoveAll(null);

            while (finishedTweens.Count > 0)
            {
                tweens.Remove(finishedTweens[0]);
                finishedTweens.RemoveAt(0);
            }
        }

        public void AddTween(VinTweenElement tween) => tweens.Add(tween);
        public void RemoveTween(VinTweenElement tween) => finishedTweens.Add(tween);
        public void CancelTween(CanvasGroup obj) { foreach (var item in tweens) { if (item.VTT_CanvasGroup == obj) item.CancelTween(); }}
        public void CancelTween(Image obj) { foreach (var item in tweens) { if (item.VTT_Image == obj) item.CancelTween(); }}
        public void CancelTween(SpriteRenderer obj) { foreach (var item in tweens) { if (item.VTT_SpriteRenderer == obj) item.CancelTween(); }}
        public void CancelTween(Text obj) { foreach (var item in tweens) { if (item.VTT_Text == obj) item.CancelTween(); }}
        public void CancelTween(TextMeshProUGUI obj) { foreach (var item in tweens) { if (item.VTT_TMP == obj) item.CancelTween(); }}
        public void CancelTween(Transform obj) { foreach (var item in tweens) { if (item.VTT_Transform == obj) item.CancelTween(); }}
    }
}