using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
    
{

    public AudioClip sound1;
    AudioSource audioSource;

    void Start()
    {
        //Component‚ğæ“¾
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ¶
        if (Input.GetKey(KeyCode.Space))
        {
            audioSource.Stop();

            //‰¹(sound1)‚ğ–Â‚ç‚·
            audioSource.PlayOneShot(sound1);
        }
    }
}