using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine;

#if UNITY_IOS || UNITY_TVOS
public class NativeAPI {
    [DllImport("__Internal")]
    public static extern void sendDebugCmdToApp(string reason, string cmd, string parameters);

    [DllImport("__Internal")]
    public static extern void showHostMainWindow(string lastStringColor);

    [DllImport("__Internal")]
    public static extern void changeUnityWindowSize(string reason, int x, int y, int w, int h);

    [DllImport("__Internal")]
    public static extern void setViewFocus(string reason, string view, bool focus);
}
#endif

public class Cube : MonoBehaviour
{
    public Text text;
    private string m_inputText = "Unity Input";
    
    void appendToText(string line) { text.text += line + "\n"; }

    void Update()
    {
        transform.Rotate(0, Time.deltaTime*10, 0);
		
		if (Application.platform == RuntimePlatform.Android)
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    string lastStringColor = "";
    void ChangeColor(string newColor)
    {
        appendToText( "Chancing Color to " + newColor );

        lastStringColor = newColor;
    
        if (newColor == "red") GetComponent<Renderer>().material.color = Color.red;
        else if (newColor == "blue") GetComponent<Renderer>().material.color = Color.blue;
        else if (newColor == "yellow") GetComponent<Renderer>().material.color = Color.yellow;
        else GetComponent<Renderer>().material.color = Color.black;
    }


    void sendDebugCmdToApp(string reason, string cmd, string parameters)
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.company.product.OverrideUnityActivity");
            AndroidJavaObject overrideActivity = jc.GetStatic<AndroidJavaObject>("instance");
            overrideActivity.Call("sendDebugCmdToApp", cmd, parameters);
        } catch(Exception e)
        {
            appendToText("Exception during sendDebugCmdToApp");
            appendToText(e.Message);
        }
#elif UNITY_IOS || UNITY_TVOS
        NativeAPI.sendDebugCmdToApp(reason, cmd, parameters);
#endif
    }

    void showHostMainWindow()
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.company.product.OverrideUnityActivity");
            AndroidJavaObject overrideActivity = jc.GetStatic<AndroidJavaObject>("instance");
            overrideActivity.Call("showMainActivity", lastStringColor);
        } catch(Exception e)
        {
            appendToText("Exception during showHostMainWindow");
            appendToText(e.Message);
        }
#elif UNITY_IOS || UNITY_TVOS
        NativeAPI.showHostMainWindow(lastStringColor);
#endif
    }

    void changeUnityWindowSize(string reason, int x, int y, int w, int h)
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.company.product.OverrideUnityActivity");
            AndroidJavaObject overrideActivity = jc.GetStatic<AndroidJavaObject>("instance");
            overrideActivity.Call("changeUnityWindowSize", x, y, w, h);
        } catch(Exception e)
        {
            appendToText("Exception during changeUnityWindowSize");
            appendToText(e.Message);
        }
#elif UNITY_IOS || UNITY_TVOS
        NativeAPI.changeUnityWindowSize(reason, x, y, w, h);
#endif
    }

    void setViewFocus(string reason, string view, bool focus)
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.company.product.OverrideUnityActivity");
            AndroidJavaObject overrideActivity = jc.GetStatic<AndroidJavaObject>("instance");
            overrideActivity.Call("setViewFocus", view, focus);
        } catch(Exception e)
        {
            appendToText("Exception during setViewFocus");
            appendToText(e.Message);
        }
#elif UNITY_IOS || UNITY_TVOS
        NativeAPI.setViewFocus(reason, view, focus);
#endif
    }

    void OnGUI()
    {
        int maxLine = 23;
        GUIStyle customStyle = new GUIStyle("button");
        customStyle.fontSize = (int)(Screen.height / maxLine);
        GUI.skin.button = customStyle;
        GUI.skin.textField = customStyle;
        
        GUILayout.Label("Test in Unity");
        GUILayout.BeginVertical("box");
        {
            if (GUILayout.Button("Red")) ChangeColor("red");
            if (GUILayout.Button("Blue")) ChangeColor("blue");
        }
        GUILayout.EndVertical();

        GUILayout.Label("Unity VM");
        GUILayout.BeginVertical("box");
        {
            if (GUILayout.Button("Show Main With Color")) showHostMainWindow();

            if (GUILayout.Button("Unload")) Application.Unload();
            if (GUILayout.Button("Quit")) Application.Quit();
        }
        GUILayout.EndVertical();

        GUILayout.Label("Interact");
        GUILayout.BeginVertical("box");
        {
            if (GUILayout.Button("Debug")) sendDebugCmdToApp("Test", "setFocus", "50,250,200,200");
            if (GUILayout.Button("ChgUnityWindowSize")) changeUnityWindowSize("From unity", 50, 250, 400, 400);
        }
        GUILayout.EndVertical();

        GUILayout.Label("Focus");
        GUILayout.BeginVertical("box");
        {
            m_inputText = GUILayout.TextField(m_inputText);

            if (GUILayout.Button("ClearFocus")) setViewFocus("From unity", "", true);
            if (GUILayout.Button("FocusText1")) setViewFocus("From unity", "TextField1", true);
            if (GUILayout.Button("FocusText2")) setViewFocus("From unity", "TextField2", true);
        }
        GUILayout.EndVertical();
    }
}

