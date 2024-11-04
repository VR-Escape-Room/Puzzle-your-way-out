using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announcer : MonoBehaviour {

    private struct AnnouncerClipEntry
    {
        public AnnouncerClipEntry(AudioClip c, int p)
        {
            clip = c;
            Priority = p;
        }
        public AudioClip clip;
        public int Priority;
    }

    private List<AnnouncerClipEntry> ClipsToPlay = new List<AnnouncerClipEntry>();

    private static Announcer _instance = null;
    public static Announcer Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Announcer>();

            return _instance;
        }
    }

    public void QueueAnnouncerClip(AudioClip clip, int Priority = 0)
    {
        if (clip == null)
            return;

        int insertionPoint = 0;
        for ( ; insertionPoint < ClipsToPlay.Count; insertionPoint++)
        {
            if (ClipsToPlay[insertionPoint].Priority < Priority)
                break;
        }

        ClipsToPlay.Insert(insertionPoint, new AnnouncerClipEntry(clip, Priority));
    }

    public AudioSource Source
    {
        get
        {
            return srcPlayFrom;
        }
        set
        {
            srcPlayFrom = value;
        }
    }

    private AudioSource srcPlayFrom = null;

	// Use this for initialization
	void Start () {
        if (srcPlayFrom == null)
            srcPlayFrom = gameObject.GetComponent<AudioSource>();
	}

	
	// Update is called once per frame
	void Update () {
        if (srcPlayFrom == null)
            return;

        if (ClipsToPlay.Count > 0 && !srcPlayFrom.isPlaying)
        {
            AudioClip nextClip = ClipsToPlay[0].clip;
            ClipsToPlay.RemoveAt(0);
            srcPlayFrom.clip = nextClip;
            srcPlayFrom.Play();
        }

    }
}
