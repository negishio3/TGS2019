using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM(AUDIO.BGM_BGM_MAOUDAMASHII_FANTASY13, AudioManager.BGM_FADE_SPEED_RATE_HIGH);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
