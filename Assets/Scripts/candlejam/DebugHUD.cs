using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DebugHUD : MonoBehaviour {

    public static void TrackData(string key, string value)
    {
        if (Debug.isDebugBuild)
            DebugHUD.Instance()._AddData(key, value);
    }

    public static void TrackData(string key, int value)
    {
        if (Debug.isDebugBuild)
            DebugHUD.Instance()._AddData(key, value.ToString());
    }

    public static void TrackData(string key, float value)
    {
        if (Debug.isDebugBuild)
            DebugHUD.Instance()._AddData(key, value.ToString());
    }

    private static DebugHUD _instance;
    public static DebugHUD Instance()
    {
        if (_instance == null)
        {
            GameObject newObj = new GameObject("DebugHUD");
            _instance = newObj.AddComponent<DebugHUD>();

            GameObject prefab = Resources.Load("DebugHUDCanvas") as GameObject;
            _instance.uiCanvas = GameObject.Instantiate(prefab) as GameObject;
            _instance.uiCanvas.transform.SetParent(newObj.transform);
        }

        return _instance;
    }

    private Dictionary<string, string> dataValues;

    private GameObject uiCanvas = null;
    private void _AddData(string key, string value)
    {
        if (dataValues.ContainsKey(key))
        {
            dataValues.Remove(key);
        }
        dataValues.Add(key, value);

        if (uiCanvas != null)
        {
            GameObject goTextChild = uiCanvas.transform.Find("DebugHUDText").gameObject;
            Text txt = goTextChild.GetComponent<Text>();

            txt.text = "";
            foreach(KeyValuePair<string, string> pair in dataValues)
            {
                txt.text += pair.Key + ": " + pair.Value + "\n";

            }
        }
    }

    public DebugHUD()
    {
        if (_instance == null)
        {
            _instance = this;
            dataValues = new Dictionary<string, string>();
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
