using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;
using System.Linq;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VinTools
{
    /// <summary>
    /// Useful math functions
    /// </summary>
    public static class Math
    {
        /// <summary>
        /// Converts a radian angle to a Vector with a magnitude of one
        /// </summary>
        /// <param name="radian">The angle in radians</param>
        /// <returns></returns>
        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;
        }
        /// <summary>
        /// Converts a radian angle to a Vector with a magnitude of magnitude
        /// </summary>
        /// <param name="radian">The angle in radians</param>
        /// <param name="magnitude">The lenght of the returned vector</param>
        /// <returns></returns>
        public static Vector2 RadianToVector2(float radian, float magnitude)
        {
            return RadianToVector2(radian) * magnitude;
        }
        /// <summary>
        /// Converts an angle to a Vector with a magnitude of one
        /// </summary>
        /// <param name="degree">The angle in degrees</param>
        /// <returns></returns>
        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }
        /// <summary>
        /// Converts an angle to a Vector with a magnitude of magnitude
        /// </summary>
        /// <param name="degree">The angle in degrees</param>
        /// <param name="magnitude">The lenght of the returned vector</param>
        /// <returns></returns>
        public static Vector2 DegreeToVector2(float degree, float magnitude)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad) * magnitude;
        }

        /// <summary>
        /// Converts a vector2 to a radian angle
        /// </summary>
        /// <param name="vector2"></param>
        /// <returns>Angle in radians</returns>
        public static float Vector2ToRadian(Vector2 vector2)
        {
            Vector2 lookDir = vector2 * 10;
            return Mathf.Atan2(lookDir.y, lookDir.x);
        }
        /// <summary>
        /// Converts a vector2 to an angle
        /// </summary>
        /// <param name="vector2"></param>
        /// <returns>Angle in degrees</returns>
        public static float Vector2ToDegree(Vector2 vector2)
        {
            return Vector2ToRadian(vector2) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Takes the Y and Z values of a vector3 and returns the rotation on the X axis
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns>Rotation on the X axis</returns>
        public static Quaternion Vector3ToRotationOnX(Vector3 vector3)
        {
            Vector2 lookDir = new Vector2(vector3.y, vector3.z) * 10;
            float angle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
            return Quaternion.Euler(angle, 0, 0);
        }
        /// <summary>
        /// Takes the X and Z values of a vector3 and returns the rotation on the Y axis
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns>Rotation on the Y axis</returns>
        public static Quaternion Vector3ToRotationOnY(Vector3 vector3)
        {
            Vector2 lookDir = new Vector2(vector3.x, vector3.z) * 10;
            float angle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
            return Quaternion.Euler(0, angle, 0);
        }
        /// <summary>
        /// Takes the X and Y values of a vector3 and returns the rotation on the Z axis
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static Quaternion Vector3ToRotationOnZ(Vector3 vector3)
        {
            Vector2 lookDir = new Vector2(vector3.x, vector3.y) * 10;
            float angle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
            return Quaternion.Euler(0, 0, angle);
        }
        /// <summary>
        /// Takes a vector3 and returns a rotatino on all of the angles
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static Quaternion Vector3ToRotation(Vector3 vector3)
        {
            Vector2 lookDirX = new Vector2(vector3.y, vector3.z) * 10;
            Vector2 lookDirY = new Vector2(vector3.x, vector3.z) * 10;
            Vector2 lookDirZ = new Vector2(vector3.x, vector3.y) * 10;

            float angleX = Mathf.Atan2(lookDirX.x, lookDirX.y) * Mathf.Rad2Deg;
            float angleY = Mathf.Atan2(lookDirY.x, lookDirY.y) * Mathf.Rad2Deg;
            float angleZ = Mathf.Atan2(lookDirZ.x, lookDirZ.y) * Mathf.Rad2Deg;

            return Quaternion.Euler(angleX, angleY, angleZ);
        }
    }

    /// <summary>
    /// Useful functions
    /// </summary>
    public static class Funcs
    {
        /// <summary>
        /// Takes in 2 vectors and returns the minimum and the maximum vectors
        /// </summary>
        /// <param name="p1">Vector</param>
        /// <param name="p2">Vector</param>
        /// <param name="min">Minimum vector</param>
        /// <param name="max">Maximum</param>
        public static void GetMinMax(Vector3Int p1, Vector3Int p2, out Vector3Int min, out Vector3Int max)
        {
            min = Vector3Int.Min(p1, p2);
            max = Vector3Int.Max(p1, p2);
        }
        /// <summary>
        /// Takes in 2 vectors and returns the minimum and the maximum vectors
        /// </summary>
        /// <param name="p1">Vector</param>
        /// <param name="p2">Vector</param>
        /// <param name="min">Minimum vector</param>
        /// <param name="max">Maximum</param>
        public static void GetMinMax(Vector3 p1, Vector3 p2, out Vector3 min, out Vector3 max)
        {
            min = Vector3.Min(p1, p2);
            max = Vector3.Max(p1, p2);
        }
        /// <summary>
        /// Takes in 2 vectors and returns the minimum and the maximum vectors
        /// </summary>
        /// <param name="p1">Vector</param>
        /// <param name="p2">Vector</param>
        /// <param name="min">Minimum vector</param>
        /// <param name="max">Maximum</param>
        public static void GetMinMax(Vector2Int p1, Vector2Int p2, out Vector2Int min, out Vector2Int max)
        {
            min = Vector2Int.Min(p1, p2);
            max = Vector2Int.Max(p1, p2);
        }
        /// <summary>
        /// Takes in 2 vectors and returns the minimum and the maximum vectors
        /// </summary>
        /// <param name="p1">Vector</param>
        /// <param name="p2">Vector</param>
        /// <param name="min">Minimum vector</param>
        /// <param name="max">Maximum</param>
        public static void GetMinMax(Vector2 p1, Vector2 p2, out Vector2 min, out Vector2 max)
        {
            min = Vector2.Min(p1, p2);
            max = Vector2.Max(p1, p2);
        }

        /// <summary>
        /// Adjusts the position to fit on a pixel grid
        /// </summary>
        /// <param name="p">Position to adjust</param>
        /// <param name="gridSize">Grid size in pixels per unit</param>
        /// <returns></returns>
        public static Vector3 AlignToGridPosition(Vector3 p, float gridSize) => AlignToGridPosition(p, gridSize, Vector3.zero);
        /// <summary>
        /// Adjusts the position to fit on a pixel grid
        /// </summary>
        /// <param name="p">Position to adjust</param>
        /// <param name="gridSize">Grid size in pixels per unit</param>
        /// <param name="offset">Offset</param>
        /// <returns></returns>
        public static Vector3 AlignToGridPosition(Vector3 p, float gridSize, Vector3 offset)
        {
            return new Vector3(ToGridPos(p.x) + GetOffset(offset.x), ToGridPos(p.y) + GetOffset(offset.y), ToGridPos(p.z) + GetOffset(offset.z));

            float ToGridPos(float pos)
            {
                return Mathf.Round(pos * gridSize) / gridSize;
            }

            float GetOffset(float offset)
            {
                return (1f / (float)gridSize * offset);
            }
        }
        /// <summary>
        /// Adjusts the position to fit on a pixel grid
        /// </summary>
        /// <param name="p">Position to adjust</param>
        /// <param name="gridSize">Grid size in pixels per unit</param>
        /// <returns></returns>
        public static Vector2 AlignToGridPosition(Vector2 p, float gridSize) => AlignToGridPosition(p, gridSize, Vector2.zero);
        /// <summary>
        /// Adjusts the position to fit on a pixel grid
        /// </summary>
        /// <param name="p">Position to adjust</param>
        /// <param name="gridSize">Grid size in pixels per unit</param>
        /// <param name="offset">Offset</param>
        /// <returns></returns>
        public static Vector2 AlignToGridPosition(Vector2 p, float gridSize, Vector2 offset)
        {
            return new Vector2(ToGridPos(p.x) + GetOffset(offset.x), ToGridPos(p.y) + GetOffset(offset.y));

            float ToGridPos(float pos)
            {
                return Mathf.Round(pos * gridSize) / gridSize;
            }

            float GetOffset(float offset)
            {
                return (1f / (float)gridSize * offset);
            }
        }

        /// <summary>
        /// Return a random element from a list
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="source">Source list</param>
        /// <returns>A random element from a list</returns>
        public static T GetRandom<T>(List<T> source)
        {
            int r = Random.Range(0, source.Count);
            return source[r];
        }

        /// <summary>
        /// Return a random element from an array
        /// </summary>
        /// <typeparam name="T">Type of array</typeparam>
        /// <param name="source">Source array</param>
        /// <returns>A random element from an array</returns>
        public static T GetRandom<T>(T[] source)
        {
            int r = Random.Range(0, source.Length);
            return source[r];
        }
    }

    public static class CameraFuncs
    {
        /*public static void AccurateMouseZoom(Camera cam, Vector2 mousePos, Vector2 scrollDelta, float minZoom, float maxZoom) => AccurateMouseZoom(cam, cam.transform, mousePos, scrollDelta, 1, minZoom, maxZoom);
        public static void AccurateMouseZoom(Camera cam, Vector2 mousePos, Vector2 scrollDelta) => AccurateMouseZoom(cam, cam.transform, mousePos, scrollDelta, 1, float.NegativeInfinity, float.PositiveInfinity);
        public static void AccurateMouseZoom(Camera cam, Vector2 mousePos, Vector2 scrollDelta, float zoomMultiplier) => AccurateMouseZoom(cam, cam.transform, mousePos, scrollDelta, zoomMultiplier, float.NegativeInfinity, float.PositiveInfinity);
        public static void AccurateMouseZoom(Camera cam, Vector2 mousePos, Vector2 scrollDelta, float zoomMultiplier, float minZoom, float maxZoom) => AccurateMouseZoom(cam, cam.transform, mousePos, scrollDelta, zoomMultiplier, minZoom, maxZoom);
        public static void AccurateMouseZoom(Camera cam, Transform cameraRig, Vector2 mousePos, Vector2 scrollDelta, float minZoom, float maxZoom) => AccurateMouseZoom(cam, cameraRig, mousePos, scrollDelta, 1, minZoom, maxZoom);
        public static void AccurateMouseZoom(Camera cam, Transform cameraRig, Vector2 mousePos, Vector2 scrollDelta) => AccurateMouseZoom(cam, cameraRig, mousePos, scrollDelta, 1, float.NegativeInfinity, float.PositiveInfinity);
        public static void AccurateMouseZoom(Camera cam, Transform cameraRig, Vector2 mousePos, Vector2 scrollDelta, float zoomMultiplier) => AccurateMouseZoom(cam, cameraRig, mousePos, scrollDelta, zoomMultiplier, float.NegativeInfinity, float.PositiveInfinity);
        public static void AccurateMouseZoom(Camera cam, Transform cameraRig, Vector2 mousePos, Vector2 scrollDelta, float zoomMultiplier, float minZoom, float maxZoom)
        {
            //zoom
            if (scrollDelta.y != 0)
            {
                Vector2 mousepos = Vector2.zero;

                float width = cam.OrthographicWidth();
                float height = cam.orthographicSize;

                mousepos = cam.ScreenToWorldPoint(mousePos) - cameraRig.position;
                mousepos.x /= width;
                mousepos.y /= height;

                cam.orthographicSize += scrollDelta.y * -zoomMultiplier * Time.deltaTime;
                ClampOrthographicSize(cam, minZoom, maxZoom);

                cameraRig.position = cam.ScreenToWorldPoint(mousePos) - new Vector3(mousepos.x * cam.OrthographicWidth(), mousepos.y * cam.orthographicSize, 0);
            }
        }*/

        public static Vector2 ScreenToCanvasPosition(Vector2 screenPos, RectTransform fullScreenRect)
        {
            Vector2 scPos = new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);
            Vector2 cvRes = new Vector2(fullScreenRect.rect.width, fullScreenRect.rect.height);
            Vector2 cvPos = cvRes * scPos;

            return cvPos;
        }

        public static void ClampOrthographicSize(Camera cam, float min, float max) => cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, min, max);

        public static float GetOrthographicWidth(Camera cam) => cam.orthographicSize / Screen.height * Screen.width;
        public static void SetOrthographicWidth(Camera cam, float width) => cam.orthographicSize = width / Screen.width * Screen.height;
    }

    public static class Extensions
    {
        /// <summary>
        /// Return a random element from a list
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="source">Source list</param>
        /// <returns>A random element from a list</returns>
        public static T GetRandom<T>(this List<T> source) => Funcs.GetRandom(source);
        
        /// <summary>
        /// Return a random element from an array
        /// </summary>
        /// <typeparam name="T">Type of array</typeparam>
        /// <param name="source">Source array</param>
        /// <returns>A random element from an array</returns>
        public static T GetRandom<T>(this T[] source) => Funcs.GetRandom(source);
    }

    static class Tiles
    {
#if UNITY_EDITOR
        /// <summary>
        /// Create a default tile asset
        /// </summary>
        [MenuItem("Assets/Create/VinTools/Custom Tiles/Default Tile")]
        static void CreateDefaultTile()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Default Tile", "New Default Tile", "Asset", "Save Default Tile", "Assets");
            if (path == "") return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<UnityEngine.Tilemaps.Tile>(), path);
        }
#endif
    }

    /// <summary>
    /// Easily set up settings menus
    /// </summary>
    public static class Settings
    {
#region Volume Slider
        /// <summary>
        /// Set up sliders to automatically adjust the volume on an audio mixer, display the volume percentage and save the settings without any additional code required
        /// </summary>
        /// <param name="_group">A list of volume slider variable groups</param>
        public static void SetUpVolumeSlider(List<VolumeSliderGroup> _group) => SetUpVolumeSlider(_group.ToArray());
        /// <summary>
        /// Set up sliders to automatically adjust the volume on an audio mixer, display the volume percentage and save the settings without any additional code required
        /// </summary>
        /// <param name="_group">A list of volume slider variable groups</param>
        public static void SetUpVolumeSlider(List<TMPVolumeSliderGroup> _group) => SetUpVolumeSlider(_group.ToArray());
        /// <summary>
        /// Set up sliders to automatically adjust the volume on an audio mixer, display the volume percentage and save the settings without any additional code required
        /// </summary>
        /// <param name="_group">An array of volume slider variable groups</param>
        public static void SetUpVolumeSlider(VolumeSliderGroup[] _group)
        {
            foreach (var item in _group)
            {
                SetUpVolumeSlider(item);
            }
        }
        /// <summary>
        /// Set up sliders to automatically adjust the volume on an audio mixer, display the volume percentage and save the settings without any additional code required
        /// </summary>
        /// <param name="_group">An array of volume slider variable groups</param>
        public static void SetUpVolumeSlider(TMPVolumeSliderGroup[] _group)
        {
            foreach (var item in _group)
            {
                SetUpVolumeSlider(item);
            }
        }
        /// <summary>
        /// Set up sliders to automatically adjust the volume on an audio mixer, display the volume percentage and save the settings without any additional code required
        /// </summary>
        /// <param name="_group">Group of volume slider variables</param>
        public static void SetUpVolumeSlider(VolumeSliderGroup _group) => SetUpVolumeSlider(_group._slider, _group._mixer, _group._percentage, _group._volumeKey);
        /// <summary>
        /// Set up sliders to automatically adjust the volume on an audio mixer, display the volume percentage and save the settings without any additional code required
        /// </summary>
        /// <param name="_group">Group of volume slider variables</param>
        public static void SetUpVolumeSlider(TMPVolumeSliderGroup _group) => SetUpVolumeSlider(_group._slider, _group._mixer, _group._percentage, _group._volumeKey);

        /// <summary>
        /// Set up sliders to automatically adjust the volume on an audio mixer, display the volume percentage and save the settings without any additional code required
        /// </summary>
        /// <param name="_slider">Slider to set up</param>
        /// <param name="_mixer">Audio mixer to link with the slider</param>
        /// <param name="_percentage">Text to display the percentage, this can be left as null</param>
        /// <param name="_volumeKey">The key of the exposed parameter of the audio mixer</param>
        public static void SetUpVolumeSlider(Slider _slider, AudioMixerGroup _mixer, Text _percentage, string _volumeKey) => SetUpVolumeSlider(_slider, _mixer, _percentage, null, _volumeKey);
        /// <summary>
        /// Set up sliders to automatically adjust the volume on an audio mixer, display the volume percentage and save the settings without any additional code required
        /// </summary>
        /// <param name="_slider">Slider to set up</param>
        /// <param name="_mixer">Audio mixer to link with the slider</param>
        /// <param name="_percentage">Text to display the percentage, this can be left as null</param>
        /// <param name="_volumeKey">The key of the exposed parameter of the audio mixer</param>
        public static void SetUpVolumeSlider(Slider _slider, AudioMixerGroup _mixer, TextMeshProUGUI _percentage, string _volumeKey) => SetUpVolumeSlider(_slider, _mixer, null, _percentage, _volumeKey);
        /// <summary>
        /// Set up sliders to automatically adjust the volume on an audio mixer, display the volume percentage and save the settings without any additional code required
        /// </summary>
        /// <param name="_slider">Slider to set up</param>
        /// <param name="_mixer">Audio mixer to link with the slider</param>
        /// <param name="_volumeKey">The key of the exposed parameter of the audio mixer</param>
        public static void SetUpVolumeSlider(Slider _slider, AudioMixerGroup _mixer, string _volumeKey) => SetUpVolumeSlider(_slider, _mixer, null, null, _volumeKey);

        /// <summary>
        /// Set up sliders to automatically adjust the volume on an audio mixer, display the volume percentage and save the settings without any additional code required
        /// </summary>
        /// <param name="_slider">Slider to set up</param>
        /// <param name="_mixer">Audio mixer to link with the slider</param>
        /// <param name="_percentage">>Text to display the percentage, this can be left as null</param>
        /// <param name="_tmppercentage">>Textmeshpro text to display the percentage, this can be left as null</param>
        /// <param name="_volumeKey">The key of the exposed parameter of the audio mixer</param>
        static void SetUpVolumeSlider(Slider _slider, AudioMixerGroup _mixer, Text _percentage, TextMeshProUGUI _tmppercentage, string _volumeKey)
        {
            //Debug.Log("Set up volume slider: " + _mixer.name);

            //set up values
            _slider.minValue = -80;
            _slider.maxValue = 20;

            //get saved setting and apply to slider
            _slider.value = PlayerPrefs.GetFloat("VinToolsSettingsVolumeSliderValue_" + _mixer.name, 0);

            //set mixer volume
            _mixer.audioMixer.SetFloat(_volumeKey, PlayerPrefs.GetInt("VinToolsSettingsVolumeToggleValue_" + _mixer.name, 1) == 1 ? _slider.value : -80);

            //set percentage
            if (_percentage != null) _percentage.text = (Mathf.InverseLerp(-80, 20, _slider.value) * 100).ToString("0") + "%";
            if (_tmppercentage != null) _tmppercentage.text = (Mathf.InverseLerp(-80, 20, _slider.value) * 100).ToString("0") + "%";

            //set up slider
            _slider.onValueChanged.AddListener((float volume) => VolumeSliderChange(volume, _mixer, _percentage, _tmppercentage, _volumeKey));
        }

        /// <summary>
        /// Set values when changing the slider value
        /// </summary>
        /// <param name="_value">Value of the slider</param>
        /// <param name="_slider">The slider which value was changed</param>
        /// <param name="_mixer">The audio mixer group to apply the changes</param>
        /// <param name="_percentage">Text to display the volume percentage</param>
        /// <param name="_volumeKey">The key of the exposed parameter of the audio mixer</param>
        static void VolumeSliderChange(float _value, AudioMixerGroup _mixer, Text _percentage, TextMeshProUGUI _tmppercentage, string _volumeKey)
        {
            //Debug.Log("Updated settings for volume slider: " + _mixer.name);

            //set setting
            PlayerPrefs.SetFloat("VinToolsSettingsVolumeSliderValue_" + _mixer.name, _value);

            //set mixer volume
            _mixer.audioMixer.SetFloat(_volumeKey, PlayerPrefs.GetInt("VinToolsSettingsVolumeToggleValue_" + _mixer.name, 1) == 1 ? _value : -80);

            //set percentage
            if (_percentage != null) _percentage.text = (Mathf.InverseLerp(-80, 20, _value) * 100).ToString("0") + "%";
            if (_tmppercentage != null) _tmppercentage.text = (Mathf.InverseLerp(-80, 20, _value) * 100).ToString("0") + "%";
        }

        /// <summary>
        /// A class to make setting up volume sliders in the inspector easier and more organised
        /// </summary>
        [System.Serializable]
        public class VolumeSliderGroup
        {
            [Tooltip("The volume slider")]
            public Slider _slider;
            [Tooltip("The volume mixer which the slider will adjust")]
            public AudioMixerGroup _mixer;
            [Tooltip("The exposed volume variable key of the mixer")]
            public string _volumeKey;

            [Header("Optional")]
            [Tooltip("The percentage of the slider, this can be null")]
            public Text _percentage;
        }
        /// <summary>
        /// A class to make setting up volume sliders in the inspector easier and more organised
        /// </summary>
        [System.Serializable]
        public class TMPVolumeSliderGroup
        {
            [Tooltip("The volume slider")]
            public Slider _slider;
            [Tooltip("The volume mixer which the slider will adjust")]
            public AudioMixerGroup _mixer;
            [Tooltip("The exposed volume variable key of the mixer")]
            public string _volumeKey;

            [Header("Optional")]
            [Tooltip("The percentage of the slider, this can be null")]
            public TextMeshProUGUI _percentage;
        }
#endregion

#region Volume Toggle
        /// <summary>
        /// Set up toggles to automatically turn on and off the volume on an audio mixer and save the settings without any additional code required
        /// </summary>
        /// <param name="_group">A list of volume toggle variable groups</param>
        public static void SetUpVolumeToggle(List<VolumeToggleGroup> _group) => SetUpVolumeToggle(_group.ToArray());
        /// <summary>
        /// Set up toggles to automatically turn on and off the volume on an audio mixer and save the settings without any additional code required
        /// </summary>
        /// <param name="_group">A list of volume toggle variable groups</param>
        public static void SetUpVolumeToggle(List<TMPVolumeToggleGroup> _group) => SetUpVolumeToggle(_group.ToArray());
        /// <summary>
        /// Set up toggles to automatically turn on and off the volume on an audio mixer and save the settings without any additional code required
        /// </summary>
        /// <param name="_group">An array of volume toggle variable groups</param>
        public static void SetUpVolumeToggle(VolumeToggleGroup[] _group)
        {
            foreach (var item in _group)
            {
                SetUpVolumeToggle(item);
            }
        }
        /// <summary>
        /// Set up toggles to automatically turn on and off the volume on an audio mixer and save the settings without any additional code required
        /// </summary>
        /// <param name="_group">An array of volume toggle variable groups</param>
        public static void SetUpVolumeToggle(TMPVolumeToggleGroup[] _group)
        {
            foreach (var item in _group)
            {
                SetUpVolumeToggle(item);
            }
        }
        /// <summary>
        /// Set up toggles to automatically turn on and off the volume on an audio mixer and save the settings without any additional code required
        /// </summary>
        /// <param name="_group">A group of volume toggle variables</param>
        public static void SetUpVolumeToggle(VolumeToggleGroup _group) => SetUpVolumeToggle(_group._toggle, _group._mixer, _group._text, null, _group._volumeKey, _group._enabledText, _group._disabledText);
        /// <summary>
        /// Set up toggles to automatically turn on and off the volume on an audio mixer and save the settings without any additional code required
        /// </summary>
        /// <param name="_group">A group of volume toggle variables</param>
        public static void SetUpVolumeToggle(TMPVolumeToggleGroup _group) => SetUpVolumeToggle(_group._toggle, _group._mixer, null, _group._text, _group._volumeKey, _group._enabledText, _group._disabledText);

        /// <summary>
        /// Set up toggles to automatically turn on and off the volume on an audio mixer and save the settings without any additional code required
        /// </summary>
        /// <param name="_toggle">The toggle to set up</param>
        /// <param name="_mixer">The volume mixer which the toggle will adjust</param>
        /// <param name="_text">Text which will change based on the state of the toggle</param>
        /// <param name="_volumeKey">The exposed volume variable key of the audio mixer</param>
        /// <param name="_enabledText">The text which will be displayed when the toggle is enabled</param>
        /// <param name="_disabledText">The text which will be displayed when the toggle is disabled</param>
        public static void SetUpVolumeToggle(Toggle _toggle, AudioMixerGroup _mixer, Text _text, string _volumeKey, string _enabledText, string _disabledText) => SetUpVolumeToggle(_toggle, _mixer, _text, null, _volumeKey, _enabledText, _disabledText);
        /// <summary>
        /// Set up toggles to automatically turn on and off the volume on an audio mixer and save the settings without any additional code required
        /// </summary>
        /// <param name="_toggle">The toggle to set up</param>
        /// <param name="_mixer">The volume mixer which the toggle will adjust</param>
        /// <param name="_text">Text which will change based on the state of the toggle</param>
        /// <param name="_volumeKey">The exposed volume variable key of the audio mixer</param>
        /// <param name="_enabledText">The text which will be displayed when the toggle is enabled</param>
        /// <param name="_disabledText">The text which will be displayed when the toggle is disabled</param>
        public static void SetUpVolumeToggle(Toggle _toggle, AudioMixerGroup _mixer, TextMeshProUGUI _text, string _volumeKey, string _enabledText, string _disabledText) => SetUpVolumeToggle(_toggle, _mixer, null, _text, _volumeKey, _enabledText, _disabledText);
        /// <summary>
        /// Set up toggles to automatically turn on and off the volume on an audio mixer and save the settings without any additional code required
        /// </summary>
        /// <param name="_toggle">The toggle to set up</param>
        /// <param name="_mixer">The volume mixer which the toggle will adjust</param>
        /// <param name="_volumeKey">The exposed volume variable key of the audio mixer</param>
        public static void SetUpVolumeToggle(Toggle _toggle, AudioMixerGroup _mixer, string _volumeKey) => SetUpVolumeToggle(_toggle, _mixer, null, null, _volumeKey, "", "");

        /// <summary>
        /// Set up toggles to automatically turn on and off the volume on an audio mixer and save the settings without any additional code required
        /// </summary>
        /// <param name="_toggle">The toggle to set up</param>
        /// <param name="_mixer">The volume mixer which the toggle will adjust</param>
        /// <param name="_text">Text which will change based on the state of the toggle</param>
        /// <param name="_tmptext">Text which will change based on the state of the toggle</param>
        /// <param name="_volumeKey">The exposed volume variable key of the audio mixer</param>
        /// <param name="_enabledText">The text which will be displayed when the toggle is enabled</param>
        /// <param name="_disabledText">The text which will be displayed when the toggle is disabled</param>
        static void SetUpVolumeToggle(Toggle _toggle, AudioMixerGroup _mixer, Text _text, TextMeshProUGUI _tmptext, string _volumeKey, string _enabledText, string _disabledText)
        {
            //set toggle state
            _toggle.isOn = PlayerPrefs.GetInt("VinToolsSettingsVolumeToggleValue_" + _mixer.name, 1) == 1 ? true : false;

            //set mixer volume
            _mixer.audioMixer.SetFloat(_volumeKey, PlayerPrefs.GetInt("VinToolsSettingsVolumeToggleValue_" + _mixer.name, 1) == 1 ? PlayerPrefs.GetFloat("VinToolsSettingsVolumeSliderValue_" + _mixer.name, 0) : -80);

            //set text
            if (_text != null) _text.text = _toggle.isOn ? _enabledText : _disabledText;
            if (_tmptext != null) _tmptext.text = _toggle.isOn ? _enabledText : _disabledText;

            //set up toggle
            _toggle.onValueChanged.AddListener((bool value) => VolumeToggleChange(value, _mixer, _text, _tmptext, _volumeKey, _enabledText, _disabledText));
        }

        /// <summary>
        /// Set values when changing a volume toggle
        /// </summary>
        /// <param name="_value">The state of the button</param>
        /// <param name="_mixer">The volume mixer which the toggle will adjust</param>
        /// <param name="_text">Text which will change based on the state of the toggle</param>
        /// <param name="_tmptext">Text which will change based on the state of the toggle</param>
        /// <param name="_volumeKey">The exposed volume variable key of the audio mixer</param>
        /// <param name="_enabledText">The text which will be displayed when the toggle is enabled</param>
        /// <param name="_disabledText">The text which will be displayed when the toggle is disabled</param>
        static void VolumeToggleChange(bool _value, AudioMixerGroup _mixer, Text _text, TextMeshProUGUI _tmptext, string _volumeKey, string _enabledText, string _disabledText)
        {
            //set playerprefs
            PlayerPrefs.SetInt("VinToolsSettingsVolumeToggleValue_" + _mixer.name, _value ? 1 : 0);

            //set mixer volume
            _mixer.audioMixer.SetFloat(_volumeKey, _value ? PlayerPrefs.GetFloat("VinToolsSettingsVolumeSliderValue_" + _mixer.name, 0) : -80);

            //set text
            if (_text != null) _text.text = _value ? _enabledText : _disabledText;
            if (_tmptext != null) _tmptext.text = _value ? _enabledText : _disabledText;
        }

        /// <summary>
        /// A class to make setting up volume toggles in the inspector easier and more organised
        /// </summary>
        [System.Serializable]
        public class VolumeToggleGroup
        {
            [Tooltip("The volume toggle")]
            public Toggle _toggle;
            [Tooltip("The volume mixer which the toggle will adjust")]
            public AudioMixerGroup _mixer;
            [Tooltip("The exposed volume variable key of the mixer")]
            public string _volumeKey;

            [Header("Optional")]
            [Tooltip("Text which will change based on the state of the toggle")]
            public Text _text;
            [Tooltip("The text which will be displayed when the toggle is enabled")]
            public string _enabledText;
            [Tooltip("The text which will be displayed when the toggle is disabled")]
            public string _disabledText;
        }
        /// <summary>
        /// A class to make setting up volume toggles in the inspector easier and more organised
        /// </summary>
        [System.Serializable]
        public class TMPVolumeToggleGroup
        {
            [Tooltip("The volume toggle")]
            public Toggle _toggle;
            [Tooltip("The volume mixer which the toggle will adjust")]
            public AudioMixerGroup _mixer;
            [Tooltip("The exposed volume variable key of the mixer")]
            public string _volumeKey;

            [Header("Optional")]
            [Tooltip("Text which will change based on the state of the toggle")]
            public TextMeshProUGUI _text;
            [Tooltip("The text which will be displayed when the toggle is enabled")]
            public string _enabledText;
            [Tooltip("The text which will be displayed when the toggle is disabled")]
            public string _disabledText;
        }
#endregion

#region Resolution dropdown
        /// <summary>
        /// Set up a dropdown to function as a resolution settings dropdown, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_dropdown"></param>
        public static void SetUpResolutionDropdown(Dropdown _dropdown)
        {
            if (PlayerPrefs.HasKey("VinToolsSettingsResolutionValue_X"))
            {
                //set resolution based on saved settings
                Screen.SetResolution(PlayerPrefs.GetInt("VinToolsSettingsResolutionValue_X"), PlayerPrefs.GetInt("VinToolsSettingsResolutionValue_Y"), Screen.fullScreen);
            }
            else
            {
                //if keys haven't been set up yet
                Debug.Log("Set up resolution settings for the first time");

                PlayerPrefs.SetInt("VinToolsSettingsResolutionValue_X", Screen.width);
                PlayerPrefs.SetInt("VinToolsSettingsResolutionValue_Y", Screen.height);
            }

            //clear dropdown values
            _dropdown.ClearOptions();

            //set up dropdown values
            Resolution[] resolutions = Screen.resolutions;

            //filter out duplicates
            List<Vector2Int> res = new List<Vector2Int>();
            foreach (var item in resolutions)
            {
                if (!res.Contains(new Vector2Int(item.width, item.height))) res.Add(new Vector2Int(item.width, item.height));
            }

            //create list of options
            List<string> options = new List<string>();
            foreach (var item in res)
            {
                options.Add(item.x + " X " + item.y);
            }

            //add options to dropdown
            _dropdown.AddOptions(options);

            //set dropdown value based on current resolution
            _dropdown.value = res.IndexOf(new Vector2Int(Screen.width, Screen.height));

            //set onvaluechanged
            _dropdown.onValueChanged.AddListener((int value) => ResolutionDropdownChange(value, res));
        }
        /// <summary>
        /// Set up a dropdown to function as a resolution settings dropdown, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_dropdown"></param>
        public static void SetUpResolutionDropdown(TMP_Dropdown _dropdown)
        {
            if (PlayerPrefs.HasKey("VinToolsSettingsResolutionValue_X"))
            {
                //set resolution based on saved settings
                Screen.SetResolution(PlayerPrefs.GetInt("VinToolsSettingsResolutionValue_X"), PlayerPrefs.GetInt("VinToolsSettingsResolutionValue_Y"), Screen.fullScreen);
            }
            else
            {
                //if keys haven't been set up yet
                Debug.Log("Set up resolution settings for the first time");

                PlayerPrefs.SetInt("VinToolsSettingsResolutionValue_X", Screen.width);
                PlayerPrefs.SetInt("VinToolsSettingsResolutionValue_Y", Screen.height);
            }

            //clear dropdown values
            _dropdown.ClearOptions();

            //set up dropdown values
            Resolution[] resolutions = Screen.resolutions;

            //filter out duplicates
            List<Vector2Int> res = new List<Vector2Int>();
            foreach (var item in resolutions)
            {
                if (!res.Contains(new Vector2Int(item.width, item.height))) res.Add(new Vector2Int(item.width, item.height));
            }

            //create list of options
            List<string> options = new List<string>();
            foreach (var item in res)
            {
                options.Add(item.x + " X " + item.y);
            }

            //add options to dropdown
            _dropdown.AddOptions(options);

            //set dropdown value based on current resolution
            _dropdown.value = res.IndexOf(new Vector2Int(Screen.width, Screen.height));

            //set onvaluechanged
            _dropdown.onValueChanged.AddListener((int value) => ResolutionDropdownChange(value, res));
        }

        /// <summary>
        /// Set value when changing the resolution dropdown
        /// </summary>
        /// <param name="_value"></param>
        /// <param name="_resolutions"></param>
        static void ResolutionDropdownChange(int _value, List<Vector2Int> _resolutions)
        {
            Screen.SetResolution(_resolutions[_value].x, _resolutions[_value].y, Screen.fullScreen);

            //save settings
            PlayerPrefs.SetInt("VinToolsSettingsResolutionValue_X", _resolutions[_value].x);
            PlayerPrefs.SetInt("VinToolsSettingsResolutionValue_Y", _resolutions[_value].y);
        }
#endregion

#region Quality dropdown
        /// <summary>
        /// Set up a dropdown to function as a quality settings dropdown, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_dropdown">Dropdown to set up</param>
        public static void SetUpQualityDropdown(Dropdown _dropdown)
        {
            //get the current quality setting
            if (PlayerPrefs.HasKey("VinToolsSettingsQualityValue"))
            {
                //if pref already exists
                QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("VinToolsSettingsQualityValue"));
            }
            else
            {
                //if set for the first time
                PlayerPrefs.SetInt("VinToolsSettingsQualityValue", QualitySettings.GetQualityLevel());
                Debug.Log("Set up quality settings for the first time");
            }

            //Debug.Log("Loaded quality " + QualitySettings.names[QualitySettings.GetQualityLevel()]);

            //set up dhe dropdown options
            _dropdown.ClearOptions();
            string[] qualityOptions = QualitySettings.names;
            _dropdown.AddOptions(qualityOptions.ToList());

            //set up the dropdown value
            _dropdown.value = QualitySettings.GetQualityLevel();

            //set up the dropdown method
            _dropdown.onValueChanged.AddListener(QualityDropdownChange);
        }
        /// <summary>
        /// Set up a dropdown to function as a quality settings dropdown, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_dropdown">Dropdown to set up</param>
        public static void SetUpQualityDropdown(TMP_Dropdown _dropdown)
        {
            //get the current quality setting
            if (PlayerPrefs.HasKey("VinToolsSettingsQualityValue"))
            {
                //if pref already exists
                QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("VinToolsSettingsQualityValue"));
            }
            else
            {
                //if set for the first time
                PlayerPrefs.SetInt("VinToolsSettingsQualityValue", QualitySettings.GetQualityLevel());
                Debug.Log("Set up quality settings for the first time");
            }

            Debug.Log("Loaded quality " + QualitySettings.names[QualitySettings.GetQualityLevel()]);

            //set up dhe dropdown options
            _dropdown.ClearOptions();
            string[] qualityOptions = QualitySettings.names;
            _dropdown.AddOptions(qualityOptions.ToList());

            //set up the dropdown value
            _dropdown.value = QualitySettings.GetQualityLevel();

            //set up the dropdown method
            _dropdown.onValueChanged.AddListener(QualityDropdownChange);
        }

        /// <summary>
        /// Set quality settings when changing the quality dropdown
        /// </summary>
        /// <param name="_value"></param>
        static void QualityDropdownChange(int _value)
        {
            Debug.Log("Set the quality to " + QualitySettings.names[_value]);

            //set quality
            QualitySettings.SetQualityLevel(_value);

            //set pref
            PlayerPrefs.SetInt("VinToolsSettingsQualityValue", _value);
        }
#endregion

#region Fullscreen toggle
        /// <summary>
        /// Set up a toggle to function as a fullscreen setting toggle, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_group">Group of fullscreen toggle variables</param>
        public static void SetUpFullscreenToggle(FullscreenToggleGroup _group) => SetUpFullscreenToggle(_group._toggle, _group._text, _group._enabledText, _group._disabledText);
        /// <summary>
        /// Set up a toggle to function as a fullscreen setting toggle, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_group">Group of fullscreen toggle variables</param>
        public static void SetUpFullscreenToggle(TMPFullscreenToggleGroup _group) => SetUpFullscreenToggle(_group._toggle, _group._text, _group._enabledText, _group._disabledText);

        /// <summary>
        /// Set up a toggle to function as a fullscreen setting toggle, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_toggle">The fullscreen toggle</param>
        /// <param name="_text">Text which will change based on the state of the toggle</param>
        /// <param name="_enabledText">The text which will be displayed when the toggle is enabled</param>
        /// <param name="_disabledText">The text which will be displayed when the toggle is disabled</param>
        public static void SetUpFullscreenToggle(Toggle _toggle, Text _text, string _enabledText, string _disabledText) => SetUpFullscreenToggle(_toggle, _text, null, _enabledText, _disabledText);
        /// <summary>
        /// Set up a toggle to function as a fullscreen setting toggle, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_toggle">The fullscreen toggle</param>
        /// <param name="_text">Text which will change based on the state of the toggle</param>
        /// <param name="_enabledText">The text which will be displayed when the toggle is enabled</param>
        /// <param name="_disabledText">The text which will be displayed when the toggle is disabled</param>
        public static void SetUpFullscreenToggle(Toggle _toggle, TextMeshProUGUI _text, string _enabledText, string _disabledText) => SetUpFullscreenToggle(_toggle, null, _text, _enabledText, _disabledText);
        /// <summary>
        /// Set up a toggle to function as a fullscreen setting toggle, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_toggle">The fullscreen toggle</param>
        public static void SetUpFullscreenToggle(Toggle _toggle) => SetUpFullscreenToggle(_toggle, null, null, "", "");

        /// <summary>
        /// Set up a toggle to function as a fullscreen setting toggle, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_toggle">The fullscreen toggle</param>
        /// <param name="_text">Text which will change based on the state of the toggle</param>
        /// <param name="_tmptext">Text which will change based on the state of the toggle</param>
        /// <param name="_enabledText">The text which will be displayed when the toggle is enabled</param>
        /// <param name="_disabledText">The text which will be displayed when the toggle is disabled</param>
        static void SetUpFullscreenToggle(Toggle _toggle, Text _text, TextMeshProUGUI _tmptext, string _enabledText, string _disabledText)
        {
            //get the current quality setting
            if (PlayerPrefs.HasKey("VinToolsSettingsFullscreenValue"))
            {
                //if pref already exists
                Screen.fullScreen = PlayerPrefs.GetInt("VinToolsSettingsFullscreenValue") == 1 ? true : false;
            }
            else
            {
                //if set for the first time
                PlayerPrefs.SetInt("VinToolsSettingsFullscreenValue", Screen.fullScreen ? 1 : 0);
                Debug.Log("Set up fullscreen setting for the first time");
            }

            //set toggle value
            _toggle.isOn = Screen.fullScreen;

            //set text
            if (_text != null) _text.text = _toggle.isOn ? _enabledText : _disabledText;
            if (_tmptext != null) _tmptext.text = _toggle.isOn ? _enabledText : _disabledText;

            //add onvaluechanged method
            _toggle.onValueChanged.AddListener((bool value) => FullscreenToggleChange(value, _text, _tmptext, _enabledText, _disabledText));
        }

        /// <summary>
        /// Set fullscreen setting when changing the fullscreen toggle
        /// </summary>
        /// <param name="_value">State of the toggle</param>
        /// <param name="_text">Text which will change based on the state of the toggle</param>
        /// <param name="_tmptext">Text which will change based on the state of the toggle</param>
        /// <param name="_enabledText">The text which will be displayed when the toggle is enabled</param>
        /// <param name="_disabledText">The text which will be displayed when the toggle is disabled</param>
        static void FullscreenToggleChange(bool _value, Text _text, TextMeshProUGUI _tmptext, string _enabledText, string _disabledText)
        {
            //set quality
            Screen.fullScreen = _value;

            //set pref
            PlayerPrefs.SetInt("VinToolsSettingsFullscreenValue", _value ? 1 : 0);

            //set text
            if (_text != null) _text.text = _value ? _enabledText : _disabledText;
            if (_tmptext != null) _tmptext.text = _value ? _enabledText : _disabledText;
        }

        /// <summary>
        /// A class to make setting up a fullscreen toggle in the inspector easier and more organised
        /// </summary>
        [System.Serializable]
        public class FullscreenToggleGroup
        {
            [Tooltip("The fullscreen toggle")]
            public Toggle _toggle;

            [Header("Optional")]
            [Tooltip("Text which will change based on the state of the toggle")]
            public Text _text;
            [Tooltip("The text which will be displayed when the toggle is enabled")]
            public string _enabledText;
            [Tooltip("The text which will be displayed when the toggle is disabled")]
            public string _disabledText;
        }
        /// <summary>
        /// A class to make setting up a fullscreen toggle in the inspector easier and more organised
        /// </summary>
        [System.Serializable]
        public class TMPFullscreenToggleGroup
        {
            [Tooltip("The fullscreen toggle")]
            public Toggle _toggle;

            [Header("Optional")]
            [Tooltip("Text which will change based on the state of the toggle")]
            public TextMeshProUGUI _text;
            [Tooltip("The text which will be displayed when the toggle is enabled")]
            public string _enabledText;
            [Tooltip("The text which will be displayed when the toggle is disabled")]
            public string _disabledText;
        }
#endregion

#region Fps slider
        /// <summary>
        /// Set up a slider to function as an fps slider, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_group">Group of fps slider variables</param>
        public static void SetUpFpsSlider(FpsSliderGroup _group) => SetUpFpsSlider(_group._slider, _group._text, _group._textAfterNumber, _group._unlimitedText, _group._maxFrameRate, _group._increment);
        /// <summary>
        /// Set up a slider to function as an fps slider, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_group">Group of fps slider variables</param>
        public static void SetUpFpsSlider(TMPFpsSliderGroup _group) => SetUpFpsSlider(_group._slider, _group._text, _group._textAfterNumber, _group._unlimitedText, _group._maxFrameRate, _group._increment);

        /// <summary>
        /// Set up a slider to function as an fps slider, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_slider">The fps slider</param>
        /// <param name="_valueDisplay">Text to show fps value</param>
        /// <param name="_textAfterNumber">Text to display after the fps value, ex. " Fps"</param>
        /// <param name="_unlimitedText">Text displayed when the slider is set to -1</param>
        /// <param name="_maxFrameRate">The maximum value of the slider</param>
        /// <param name="_increment">Increment which by the fps can be increased</param>
        public static void SetUpFpsSlider(Slider _slider, Text _valueDisplay, string _textAfterNumber, string _unlimitedText, int _maxFrameRate, int _increment) => SetUpFpsSlider(_slider, _valueDisplay, null, _textAfterNumber, _unlimitedText, _maxFrameRate, _increment);
        /// <summary>
        /// Set up a slider to function as an fps slider, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_slider">The fps slider</param>
        /// <param name="_valueDisplay">Text to show fps value</param>
        /// <param name="_textAfterNumber">Text to display after the fps value, ex. " Fps"</param>
        /// <param name="_unlimitedText">Text displayed when the slider is set to -1</param>
        /// <param name="_maxFrameRate">The maximum value of the slider</param>
        /// <param name="_increment">Increment which by the fps can be increased</param>
        public static void SetUpFpsSlider(Slider _slider, TextMeshProUGUI _valueDisplay, string _textAfterNumber, string _unlimitedText, int _maxFrameRate, int _increment) => SetUpFpsSlider(_slider, null, _valueDisplay, _textAfterNumber, _unlimitedText, _maxFrameRate, _increment);

        /// <summary>
        /// Set up a slider to function as an fps slider, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_slider">The fps slider</param>
        /// <param name="_valueDisplay">Text to show fps value</param>
        /// <param name="_tmpvalueDisplay">Text to show fps value</param>
        /// <param name="_textAfterNumber">Text to display after the fps value, ex. " Fps"</param>
        /// <param name="_unlimitedText">Text displayed when the slider is set to -1</param>
        /// <param name="_maxFrameRate">The maximum value of the slider</param>
        /// <param name="_increment">Increment which by the fps can be increased</param>
        static void SetUpFpsSlider(Slider _slider, Text _valueDisplay, TextMeshProUGUI _tmpvalueDisplay, string _textAfterNumber, string _unlimitedText, int _maxFrameRate, int _increment)
        {
            //set value
            Application.targetFrameRate = PlayerPrefs.GetInt("VinToolsSettingsFpsValue", -1);

            //make sure that increment is greater than 0
            if (_increment < 1) _increment = 1;

            //set slider values
            _slider.minValue = 0;
            _slider.maxValue = _maxFrameRate;

            _slider.value = Application.targetFrameRate;

            //add method to onvaluechanged
            _slider.onValueChanged.AddListener((float value) => FpsSliderChange(value, _valueDisplay, _tmpvalueDisplay, _textAfterNumber, _unlimitedText, _maxFrameRate, _increment));

            //refresh the values
            FpsSliderChange(_slider.value, _valueDisplay, _tmpvalueDisplay, _textAfterNumber, _unlimitedText, _maxFrameRate, _increment);
        }

        /// <summary>
        /// Set fps setting when changing the slider value
        /// </summary>
        /// <param name="_value">Fps to set</param>
        /// <param name="_valueDisplay">Text to show fps value</param>
        /// <param name="_tmpvalueDisplay">Text to show fps value</param>
        /// <param name="_textAfterNumber">Text to display after the fps value, ex. " Fps"</param>
        /// <param name="_unlimitedText">Text displayed when the slider is set to -1</param>
        /// <param name="_maxFrameRate">The maximum value of the slider</param>
        /// <param name="_increment">Increment which by the fps can be increased</param>
        static void FpsSliderChange(float _value, Text _valueDisplay, TextMeshProUGUI _tmpvalueDisplay, string _textAfterNumber, string _unlimitedText, int _maxFrameRate, int _increment)
        {
            string textToDisplay = "";

            if (_value == 0)
            {
                Application.targetFrameRate = -1;
                PlayerPrefs.SetInt("VinToolsSettingsFpsValue", -1);

                textToDisplay = _unlimitedText;
            }
            else
            {
                int val = Mathf.Clamp((int)(_value / _increment) * _increment, _increment, _maxFrameRate);

                Application.targetFrameRate = val;
                PlayerPrefs.SetInt("VinToolsSettingsFpsValue", val);

                textToDisplay = val + _textAfterNumber;
            }

            if (_valueDisplay != null) _valueDisplay.text = textToDisplay;
            if (_tmpvalueDisplay != null) _tmpvalueDisplay.text = textToDisplay;
        }

        /// <summary>
        /// A class to make setting up an fps slider in the inspector easier and more organised
        /// </summary>
        [System.Serializable]
        public class FpsSliderGroup
        {
            [Tooltip("The fps slider")]
            public Slider _slider;
            [Tooltip("Text where to show fps value")]
            public Text _text;
            [Tooltip("Text to display after the fps value, ex. \" Fps\"")]
            public string _textAfterNumber;
            [Tooltip("Text displayed when the slider is set to -1")]
            public string _unlimitedText;
            [Tooltip("The maximum value of the slider")]
            public int _maxFrameRate;
            [Tooltip("Increment which by the fps can be increased")]
            [Range(1, 10)]
            public int _increment = 1;
        }
        /// <summary>
        /// A class to make setting up an fps slider in the inspector easier and more organised
        /// </summary>
        [System.Serializable]
        public class TMPFpsSliderGroup
        {
            [Tooltip("The fps slider")]
            public Slider _slider;
            [Tooltip("Text where to show fps value")]
            public TextMeshProUGUI _text;
            [Tooltip("Text to display after the fps value, ex. \" Fps\"")]
            public string _textAfterNumber;
            [Tooltip("Text displayed when the slider is set to -1")]
            public string _unlimitedText;
            [Tooltip("The maximum value of the slider")]
            public int _maxFrameRate;
            [Tooltip("Increment which by the fps can be increased")]
            [Range(1, 10)]
            public int _increment = 1;
        }
#endregion

#region Fps dropdown
        /// <summary>
        /// Set up a dropdown to function as an fps dropdown, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_group">Group of fps dropdown variables</param>
        public static void SetUpFpsDropdown(TMPFpsDropdownGroup _group) => SetUpFpsDropdown(_group._dropdown, _group._textAfterNumber, _group._unlimitedText);
        /// <summary>
        /// Set up a dropdown to function as an fps dropdown, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_group">Group of fps dropdown variables</param>
        public static void SetUpFpsDropdown(FpsDropdownGroup _group) => SetUpFpsDropdown(_group._dropdown, _group._textAfterNumber, _group._unlimitedText);
        /// <summary>
        /// Set up a dropdown to function as an fps dropdown, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_dropdown">The dropdown to set up</param>
        /// <param name="_textAfterNumber">Text to display after the fps value, ex. " Fps"</param>
        /// <param name="_unlimitedText">Text displayed when the slider is set to -1</param>
        public static void SetUpFpsDropdown(TMP_Dropdown _dropdown, string _textAfterNumber, string _unlimitedText)
        {
            //set up framerate
            Application.targetFrameRate = PlayerPrefs.GetInt("VinToolsSettingsFpsValue", -1);

            //assign lists
            Resolution[] resolutions = Screen.resolutions;
            List<int> framerates = new List<int>() { -1 };

            //add values to list
            foreach (var item in resolutions)
            {
                if (!framerates.Contains(item.refreshRate)) framerates.Add(item.refreshRate);
            }

            //sort list
            framerates.Sort();

            //make list of strings
            List<string> names = new List<string>();
            foreach (var item in framerates)
            {
                if (item == -1) names.Add(_unlimitedText);
                else names.Add(item + _textAfterNumber);
            }

            //set up dropdown
            _dropdown.ClearOptions();
            _dropdown.AddOptions(names);
            _dropdown.value = framerates.IndexOf(Application.targetFrameRate);

            _dropdown.onValueChanged.AddListener((int value) => FpsDropdownChange(value, framerates));
        }
        /// <summary>
        /// Set up a dropdown to function as an fps dropdown, values are automatically assigned and settings are automatically saved
        /// </summary>
        /// <param name="_dropdown">The dropdown to set up</param>
        /// <param name="_textAfterNumber">Text to display after the fps value, ex. " Fps"</param>
        /// <param name="_unlimitedText">Text displayed when the slider is set to -1</param>
        public static void SetUpFpsDropdown(Dropdown _dropdown, string _textAfterNumber, string _unlimitedText)
        {
            //set up framerate
            Application.targetFrameRate = PlayerPrefs.GetInt("VinToolsSettingsFpsValue", -1);

            //assign lists
            Resolution[] resolutions = Screen.resolutions;
            List<int> framerates = new List<int>() { -1 };

            //add values to list
            foreach (var item in resolutions)
            {
                if (!framerates.Contains(item.refreshRate)) framerates.Add(item.refreshRate);
            }

            //sort list
            framerates.Sort();

            //make list of strings
            List<string> names = new List<string>();
            foreach (var item in framerates)
            {
                if (item == -1) names.Add(_unlimitedText);
                else names.Add(item + _textAfterNumber);
            }

            //set up dropdown
            _dropdown.ClearOptions();
            _dropdown.AddOptions(names);
            _dropdown.value = framerates.IndexOf(Application.targetFrameRate);

            _dropdown.onValueChanged.AddListener((int value) => FpsDropdownChange(value, framerates));
        }

        /// <summary>
        /// Set fps setting when changing the dropdown value
        /// </summary>
        /// <param name="_value">The value of the dropdown</param>
        /// <param name="_framerates">List of options to choose from</param>
        static void FpsDropdownChange(int _value, List<int> _framerates)
        {
            //set value
            Application.targetFrameRate = _framerates[_value];

            //save setting
            PlayerPrefs.SetInt("VinToolsSettingsFpsValue", _framerates[_value]);
        }

        /// <summary>
        /// A class to make setting up an fps dropdown in the inspector easier and more organised
        /// </summary>
        [System.Serializable]
        public class FpsDropdownGroup
        {
            [Tooltip("The fps dropdown")]
            public Dropdown _dropdown;
            [Tooltip("Text to display after the fps value, ex. \" Fps\"")]
            public string _textAfterNumber;
            [Tooltip("Text displayed when the slider is set to -1")]
            public string _unlimitedText;
        }
        /// <summary>
        /// A class to make setting up an fps dropdown in the inspector easier and more organised
        /// </summary>
        [System.Serializable]
        public class TMPFpsDropdownGroup
        {
            [Tooltip("The fps dropdown")]
            public TMP_Dropdown _dropdown;
            [Tooltip("Text to display after the fps value, ex. \" Fps\"")]
            public string _textAfterNumber;
            [Tooltip("Text displayed when the slider is set to -1")]
            public string _unlimitedText;
        }
#endregion

#region Generic Toggle
        /// <summary>
        /// Set up a toggle to automatically execute an action on it's value changed and change a text based on it's state
        /// </summary>
        /// <param name="_group">A group of toggle variables</param>
        public static void SetUpGenericToggle(GenericToggleGroup _group) => SetUpGenericToggle(_group._toggle, _group._onButtonChange, _group._defaultState, _group._text, _group._enabledText, _group._disabledText);
        /// <summary>
        /// Set up a toggle to automatically execute an action on it's value changed and change a text based on it's state
        /// </summary>
        /// <param name="_group">A group of toggle variables</param>
        public static void SetUpGenericToggle(TMPGenericToggleGroup _group) => SetUpGenericToggle(_group._toggle, _group._onButtonChange, _group._defaultState, _group._text, _group._enabledText, _group._disabledText);
        /// <summary>
        /// Set up a toggle to automatically execute an action on it's value changed and change a text based on it's state
        /// </summary>
        /// <param name="_group">A group of toggle variables</param>
        /// <param name="_onButtonChange">The action to execute when the toggle's value changes</param>
        public static void SetUpGenericToggle(GenericToggleGroup _group, UnityAction<bool> _onButtonChange) => SetUpGenericToggle(_group._toggle, _onButtonChange, _group._defaultState, _group._text, _group._enabledText, _group._disabledText);
        /// <summary>
        /// Set up a toggle to automatically execute an action on it's value changed and change a text based on it's state
        /// </summary>
        /// <param name="_group">A group of toggle variables</param>
        /// <param name="_onButtonChange">The action to execute when the toggle's value changes</param>
        public static void SetUpGenericToggle(TMPGenericToggleGroup _group, UnityAction<bool> _onButtonChange) => SetUpGenericToggle(_group._toggle, _onButtonChange, _group._defaultState, _group._text, _group._enabledText, _group._disabledText);

        /// <summary>
        /// Set up a toggle to automatically execute an action on it's value changed and change a text based on it's state and automatically save it's state
        /// </summary>
        /// <param name="_toggle">The toggle to set up</param>
        /// <param name="_onButtonChange">The action to execute when the toggle's value changes</param>
        public static void SetUpGenericToggle(Toggle _toggle, UnityAction<bool> _onButtonChange) => SetUpGenericToggle(_toggle, _onButtonChange, _toggle.isOn, null, null, "", "");
        /// <summary>
        /// Set up a toggle to automatically execute an action on it's value changed and change a text based on it's state and automatically save it's state
        /// </summary>
        /// <param name="_toggle">The toggle to set up</param>
        /// <param name="_onButtonChange">The action to execute when the toggle's value changes</param>
        /// <param name="_defaultState">The state of the toggle at the start of the scene</param>
        public static void SetUpGenericToggle(Toggle _toggle, UnityAction<bool> _onButtonChange, bool _defaultState) => SetUpGenericToggle(_toggle, _onButtonChange, _defaultState, null, null, "", "");
        /// <summary>
        /// Set up a toggle to automatically execute an action on it's value changed and change a text based on it's state and automatically save it's state
        /// </summary>
        /// <param name="_toggle">The toggle to set up</param>
        /// <param name="_onButtonChange">The action to execute when the toggle's value changes</param>
        /// <param name="_text">Text which will change based on the state of the toggle</param>
        /// <param name="_enabledText">The text which will be displayed when the toggle is enabled</param>
        /// <param name="_disabledText">The text which will be displayed when the toggle is disabled</param>
        public static void SetUpGenericToggle(Toggle _toggle, UnityAction<bool> _onButtonChange, Text _text, string _enabledText, string _disabledText) => SetUpGenericToggle(_toggle, _onButtonChange, _toggle.isOn, _text, null, _enabledText, _disabledText);
        /// <summary>
        /// Set up a toggle to automatically execute an action on it's value changed and change a text based on it's state and automatically save it's state
        /// </summary>
        /// <param name="_toggle">The toggle to set up</param>
        /// <param name="_onButtonChange">The action to execute when the toggle's value changes</param>
        /// <param name="_text">Text which will change based on the state of the toggle</param>
        /// <param name="_enabledText">The text which will be displayed when the toggle is enabled</param>
        /// <param name="_disabledText">The text which will be displayed when the toggle is disabled</param>
        public static void SetUpGenericToggle(Toggle _toggle, UnityAction<bool> _onButtonChange, TextMeshProUGUI _text, string _enabledText, string _disabledText) => SetUpGenericToggle(_toggle, _onButtonChange, _toggle.isOn, null, _text, _enabledText, _disabledText);
        /// <summary>
        /// Set up a toggle to automatically execute an action on it's value changed and change a text based on it's state and automatically save it's state
        /// </summary>
        /// <param name="_toggle">The toggle to set up</param>
        /// <param name="_onButtonChange">The action to execute when the toggle's value changes</param>
        /// <param name="_defaultState">The state of the toggle at the start of the scene</param>
        /// <param name="_text">Text which will change based on the state of the toggle</param>
        /// <param name="_enabledText">The text which will be displayed when the toggle is enabled</param>
        /// <param name="_disabledText">The text which will be displayed when the toggle is disabled</param>
        public static void SetUpGenericToggle(Toggle _toggle, UnityAction<bool> _onButtonChange, bool _defaultState, Text _text, string _enabledText, string _disabledText) => SetUpGenericToggle(_toggle, _onButtonChange, _defaultState, _text, null, _enabledText, _disabledText);
        /// <summary>
        /// Set up a toggle to automatically execute an action on it's value changed and change a text based on it's state and automatically save it's state
        /// </summary>
        /// <param name="_toggle">The toggle to set up</param>
        /// <param name="_onButtonChange">The action to execute when the toggle's value changes</param>
        /// <param name="_defaultState">The state of the toggle at the start of the scene</param>
        /// <param name="_text">Text which will change based on the state of the toggle</param>
        /// <param name="_enabledText">The text which will be displayed when the toggle is enabled</param>
        /// <param name="_disabledText">The text which will be displayed when the toggle is disabled</param>
        public static void SetUpGenericToggle(Toggle _toggle, UnityAction<bool> _onButtonChange, bool _defaultState, TextMeshProUGUI _text, string _enabledText, string _disabledText) => SetUpGenericToggle(_toggle, _onButtonChange, _defaultState, null, _text, _enabledText, _disabledText);
        /// <summary>
        /// Set up a toggle to automatically execute an action on it's value changed and change a text based on it's state and automatically save it's state
        /// </summary>
        /// <param name="_toggle">The toggle to set up</param>
        /// <param name="_onButtonChange">The action to execute when the toggle's value changes</param>
        /// <param name="_defaultState">The state of the toggle at the start of the scene</param>
        /// <param name="_text">Text which will change based on the state of the toggle</param>
        /// <param name="_tmptext">Text which will change based on the state of the toggle</param>
        /// <param name="_enabledText">The text which will be displayed when the toggle is enabled</param>
        /// <param name="_disabledText">The text which will be displayed when the toggle is disabled</param>
        static void SetUpGenericToggle(Toggle _toggle, UnityAction<bool> _onButtonChange, bool _defaultState, Text _text, TextMeshProUGUI _tmptext, string _enabledText, string _disabledText)
        {
            //set default state
            _toggle.isOn = PlayerPrefs.GetInt("VinToolsUIToggleValue_" + _toggle.name, _defaultState ? 1 : 0) == 1;

            //set text
            if (_text != null) _text.text = _toggle.isOn ? _enabledText : _disabledText;
            if (_tmptext != null) _tmptext.text = _toggle.isOn ? _enabledText : _disabledText;

            //
            _toggle.onValueChanged.AddListener(_onButtonChange);
            _toggle.onValueChanged.AddListener((bool value) => GenericToggleChange(value, _toggle.name, _text, _tmptext, _enabledText, _disabledText));
            _toggle.onValueChanged.Invoke(_toggle.isOn);
        }

        /// <summary>
        /// change the text when the toggle state was changed
        /// </summary>
        /// <param name="_value">The state of the toggle</param>
        /// <param name="_text">Text which will change based on the state of the toggle</param>
        /// <param name="_tmptext">Text which will change based on the state of the toggle</param>
        /// <param name="_enabledText">The text which will be displayed when the toggle is enabled</param>
        /// <param name="_disabledText">The text which will be displayed when the toggle is disabled</param>
        static void GenericToggleChange(bool _value, string _name, Text _text, TextMeshProUGUI _tmptext, string _enabledText, string _disabledText)
        {
            if (_text != null) _text.text = _value ? _enabledText : _disabledText;
            if (_tmptext != null) _tmptext.text = _value ? _enabledText : _disabledText;

            //set pref
            PlayerPrefs.SetInt("VinToolsUIToggleValue_" + _name, _value ? 1 : 0);
        }

        /// <summary>
        /// A class to make setting up toggles easier and more organised in the inspector
        /// </summary>
        [System.Serializable]
        public class GenericToggleGroup
        {
            [Tooltip("The toggle to set up")]
            public Toggle _toggle;
            [Tooltip(">The action to execute when the toggle's value changes, this can be overwritten in script")]
            public UnityAction<bool> _onButtonChange;
            [Tooltip("The state of the toggle at the start of the scene")]
            public bool _defaultState;

            [Header("Optional")]
            [Tooltip("Text which will change based on the state of the toggle")]
            public Text _text;
            [Tooltip("The text which will be displayed when the toggle is enabled")]
            public string _enabledText;
            [Tooltip("The text which will be displayed when the toggle is disabled")]
            public string _disabledText;
        }
        /// <summary>
        /// A class to make setting up toggles easier and more organised in the inspector
        /// </summary>
        [System.Serializable]
        public class TMPGenericToggleGroup
        {
            [Tooltip("The toggle to set up")]
            public Toggle _toggle;
            [Tooltip(">The action to execute when the toggle's value changes, this can be overwritten in script")]
            public UnityAction<bool> _onButtonChange;
            [Tooltip("The state of the toggle at the start of the scene")]
            public bool _defaultState;

            [Header("Optional")]
            [Tooltip("Text which will change based on the state of the toggle")]
            public TextMeshProUGUI _text;
            [Tooltip("The text which will be displayed when the toggle is enabled")]
            public string _enabledText;
            [Tooltip("The text which will be displayed when the toggle is disabled")]
            public string _disabledText;
        }
#endregion
    }

    public static class Platform
    {
        public enum ApplicationPlatform { Desktop, Mobile, Web, Console }

        public static ApplicationPlatform currentPlatform
        {
            get
            {
                bool c = Application.isConsolePlatform;
                bool m = Application.isMobilePlatform;
                bool w = Application.platform == RuntimePlatform.WebGLPlayer;

                if (c) return ApplicationPlatform.Console;
                else if (m) return ApplicationPlatform.Mobile;
                else if (w) return ApplicationPlatform.Web;
                else return ApplicationPlatform.Desktop;
            }
        }
    }

    static class Test
    {
        static void PlannedFeatures()
        {
            //disable and enable settings automatically based on the platform

            //methods to automatically display application data on text elements
            string ver = Application.version;

            //localization tools:
            //automatically set text language if it's enabled
            //localise the text based on key, or it's english text value
            //tool in the unity editor to automatically find all tex components, and add them to the list of texts to translate
            SystemLanguage lang = Application.systemLanguage;

            //audio manage
            //audio subtitle system

            //dialogue system with branches: subtitle and audio

            //
            //QualitySettings.
        }
    }
}
 

namespace VinTools.Debugging
{

}