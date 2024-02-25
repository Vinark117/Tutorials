using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VinTools.UI
{
    public static class UISummoner
    {
        public static Canvas SummonCanvas()
        {
            GameObject newCanvas = new GameObject("Canvas");
            Canvas c = newCanvas.AddComponent<Canvas>();
            c.renderMode = RenderMode.ScreenSpaceOverlay;
            c.sortingOrder = 32767;

            CanvasScaler s = newCanvas.AddComponent<CanvasScaler>();
            s.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            s.referenceResolution = new Vector2(1920, 1080);

            newCanvas.AddComponent<GraphicRaycaster>();

            return c;
        }

        public static Image SummonPanel(Transform parent)
        {
            GameObject panel = new GameObject("Panel");
            panel.transform.parent = parent;

            panel.AddComponent<CanvasRenderer>();

            Image i = panel.AddComponent<Image>();

            RectTransform rt = panel.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.anchoredPosition = Vector2.zero;
            rt.sizeDelta = Vector2.zero;

            return i;
        }

        public static CanvasGroup SummonCanvasGroupPanel()
        {
            CanvasGroup g = SummonCanvas().gameObject.AddComponent<CanvasGroup>();
            g.interactable = false;
            g.blocksRaycasts = false;

            SummonPanel(g.transform);

            return g;
        }
    }
}
