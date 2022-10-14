using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerDetail : MonoBehaviour
{
    static AudioSource audioSrc;
    public static bool instance { get; private set; }

    void Start()
    {
        if (!instance)
        {
            gameObject.AddComponent<AudioSource>();
            instance = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSrc = GetComponent<AudioSource>();
    }
    public static void PlaySound(string clip)
    {
        if(audioSrc != null)
            audioSrc.PlayOneShot(Resources.Load<AudioClip>(@"Sound/" + clip));
    }
}