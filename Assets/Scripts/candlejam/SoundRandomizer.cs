using UnityEngine;
using System.Collections;

public class SoundRandomizer : MonoBehaviour {

    public AudioClip[] clips = null;

    public bool PlayOnAwake = true;

    public void Play()
    {
        AudioSource attachedSource = gameObject.GetComponent<AudioSource>();
        attachedSource.clip = clips[Random.Range(0, clips.Length)];
        attachedSource.Play();
    }

	// Use this for initialization
	void Start () {
    }

    private void Awake()
    {
        if (PlayOnAwake)
        {
            Play();
        }
    }

    // Update is called once per frame
    void Update () {
    }
}
