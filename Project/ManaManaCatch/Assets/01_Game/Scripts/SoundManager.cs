using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager ms_instance = null;
    public static SoundManager instance { get { return ms_instance; } }

    [SerializeField]
    private AudioSource m_bgm = null;
    [SerializeField]
    private List<AudioSource> m_seList = new List<AudioSource>();

    private void Awake()
    {
        ms_instance = this;
    }

    public void PlayBGM()
    {
        m_bgm.Play();
    }

    public void StopBGM()
    {
        m_bgm.Stop();
    }

    public void PlaySE(int no)
    {
        if (!(0 <= no && no < m_seList.Count)) return;

        var se = m_seList[no];
        se.Stop();
        se.Play();
    }
}
