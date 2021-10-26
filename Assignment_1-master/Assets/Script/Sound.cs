using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AudioUtility;

public class Sound : MonoBehaviour
{
    public AudioClip m_AudioClip;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (!AudioManager.Instance.IsPlayingEffect(m_AudioClip))
            {
                AudioManager.Instance.PlayEffect(m_AudioClip);
            }
        }
    }
}
