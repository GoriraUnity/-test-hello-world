using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSys : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        VolumeChange();
    }

    public void VolumeChange()
    {
        StartCoroutine("VolumeDown");
    }

    IEnumerator VolumeDown()//0.1•b–ˆ‚É0.01‚¸‚Â‰¹—Ê‚ð‰º‚°‚é
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.0075f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
