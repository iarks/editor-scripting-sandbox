using System;
using System.Collections.Generic;
using UnityEngine;

class BasicGUILogger : MonoBehaviour
{
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    [SerializeField]
    private List<string> _onScreenLogKeys;

    private Dictionary<string, string> _onScreenLogs;

    [SerializeField]
    private float _topOffset = 0.0f;

    [SerializeField]
    private float _leftOffset = 10.0f;

    [SerializeField]
    private float _spaceBetweenDebugTexts = 25.0f;

    [SerializeField]
    private float _widthOfDebugTexts = 500.0f;

    [SerializeField]
    private float _heightOfDebugTexts = 20.0f;

    [SerializeField]
    private bool _logToConsole = false;

    [SerializeField]
    private bool _doNotDestroy = true;

#endif

    public static BasicGUILogger Log { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        _onScreenLogs = new Dictionary<string, string>();
        _onScreenLogKeys.Add("default");
        if (_onScreenLogKeys != null)
        {
            foreach(var value in _onScreenLogKeys)
            {
                _onScreenLogs.Add(value, $"on screen debug log {value}");
            }
        }
        if(GameObject.FindObjectsOfType<BasicGUILogger>().Length>1)
        {
            throw new Exception("Cannot have more than one loggers in scene");
        }
        if(_doNotDestroy)
        {
            DontDestroyOnLoad(this.gameObject);
        }
#endif
        Log = this;
    }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
    private void OnGUI()
    {

        float top = _topOffset;
        foreach(string key in _onScreenLogs.Keys)
        {
            GUI.Label(new Rect(_leftOffset, top, _widthOfDebugTexts, _heightOfDebugTexts), _onScreenLogs[key]);
            top += 25;
        }
    }
#endif


    public void LogToScreen(string message, string key="default")
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (_onScreenLogs.ContainsKey(key))
        {
            _onScreenLogs[key] = message;
        }
        LogToConsole(message, key);
#endif
    }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
    private void LogToConsole(string message, string key)
    {
        if(_logToConsole)
        {
            Debug.Log($"{key} : {message}");
        }
    }
#endif
}
