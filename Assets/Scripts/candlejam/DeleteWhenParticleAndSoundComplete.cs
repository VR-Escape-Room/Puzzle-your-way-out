using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWhenParticleAndSoundComplete : MonoBehaviour {

    private ParticleSystem ps;
    private AudioSource audiosrc;


    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
        audiosrc = GetComponent<AudioSource>();
    }

    public void Update()
    {
        bool psDoesNotExistOrIsDone = true;
        if (ps)
        {
            if (ps.IsAlive())
            {
                psDoesNotExistOrIsDone = false;
            }
        }

        bool audioDoesNotExistOrIsDone = true;
        if (audiosrc)
        {
            if (audiosrc.isPlaying)
            {
                audioDoesNotExistOrIsDone = false;
            }
        }

        if (psDoesNotExistOrIsDone && audioDoesNotExistOrIsDone)
            Destroy(gameObject);
    }
}
