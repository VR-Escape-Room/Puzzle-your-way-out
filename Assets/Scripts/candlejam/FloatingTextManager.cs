using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public static class GameObjectTextExtension
{
    public static FloatingText FloatText(this GameObject o, string s, Vector3 offset)
    {
        return FloatingTextManager.Instance().CreateFloatingText(o, s, offset, null);
    }

    public static FloatingText FloatText(this GameObject o, string s, Vector3 offset, GameObject prefabOverride)
    {
        return FloatingTextManager.Instance().CreateFloatingText(o, s, offset, prefabOverride);
    }
}

public class FloatingTextManager : MonoBehaviour {

    public GameObject Prefab = null;
    public float DistanceFromCamera = 5.0f;

    public FloatingText CreateFloatingTextInWorld(GameObject obj, string s)
    {
        GameObject ftObj = null;
        if (obj != null)
        {
            // Offset the text towards the camera
            Vector3 fromCamera = obj.transform.position - Camera.main.transform.position;
            Vector3 spawnPoint = Camera.main.transform.position + fromCamera.normalized * DistanceFromCamera;

            if (Camera.main.orthographic)
            {
                spawnPoint = obj.transform.position - Camera.main.transform.forward;
            }

            ftObj = GameObject.Instantiate(Prefab, spawnPoint, Camera.main.transform.rotation) as GameObject;

            ftObj.transform.SetParent(transform, false);

            ftObj.GetComponentInChildren<TextMeshProUGUI>().text = s;
        }

        FloatingText result = ftObj.GetComponent<FloatingText>();

        return result;

    }

    // This function creates floating text on our own canvas
    public FloatingText CreateFloatingText(GameObject obj, string s, Vector3 worldOffset, GameObject prefabToUse = null)
    {
        GameObject ftObj = null;
        if (obj != null)
        {
            if (prefabToUse == null)
                prefabToUse = Prefab;

            ftObj = GameObject.Instantiate(prefabToUse, transform) as GameObject;
            ftObj.GetComponentInChildren<TextMeshProUGUI>().text = s;


            PositionFloatingTextOnCanvas(ftObj, obj.transform.position + worldOffset);
            //Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, obj.transform.position + worldOffset);
            //ftObj.GetComponent<RectTransform>().anchoredPosition = screenPoint - _instance.GetComponent<RectTransform>().sizeDelta / 2f;
        }

        FloatingText result = ftObj.GetComponent<FloatingText>();

        return result;

    }

    private void PositionFloatingTextOnCanvas(GameObject ftObj, Vector3 worldTransform)
    {
        //first you need the RectTransform component of your canvas
        RectTransform CanvasRect = GetComponent<RectTransform>();

        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(worldTransform);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        //now you can set the position of the ui element
        ftObj.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
    }


    private static FloatingTextManager _instance;
    public static FloatingTextManager Instance()
    {
        if (_instance == null)
        {
            // GameObject newObj = new GameObject("FloatingTextManager");
            GameObject newObj = GameObject.Instantiate(Resources.Load("FloatingTextCanvas") as GameObject);
            _instance = newObj.AddComponent<FloatingTextManager>();
            _instance.Prefab = Resources.Load("FloatingText") as GameObject;
        }

        return _instance;
    }

    public FloatingTextManager()
    {
        if (_instance == null)
        {
            _instance = this;
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
