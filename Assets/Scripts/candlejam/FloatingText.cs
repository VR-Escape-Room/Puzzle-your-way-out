using UnityEngine;
using TMPro;


public class FloatingText : MonoBehaviour {

    private float DestroyTime = 0.0f;

    private float _fadetime = 2.0f;
    public float FadeTime
    {
        get { return _fadetime;  }
        set
        {
            _fadetime = value;
            DestroyTime = Time.timeSinceLevelLoad + _fadetime;
        }
    }
    public Vector3 Drift = new Vector3(0.0f, 0.5f, 0.0f);

    // TODO:
    // Set a floating path style - probably done as a parametric function / lambda / callback?

    TextMeshPro childTextMember = null;

    public Color TextColor
    {
        get
        {
            return childTextMember.color;
        }
        set
        {
            childTextMember.color = value;
        }
    }

    public float TextSize
    {
        get
        {
            return childTextMember.fontSize;
        }
        set
        {
            childTextMember.fontSize = value;
        }
    }

    void Awake()
    {
        childTextMember = gameObject.GetComponentInChildren<TextMeshPro>();
    }

    // Use this for initialization
    void Start () {
        DestroyTime = Time.timeSinceLevelLoad + FadeTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad > DestroyTime)
        {
            Destroy(gameObject);
            return;
        }

        gameObject.transform.position += Drift * Time.deltaTime;
	}
}
