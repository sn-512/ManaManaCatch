using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackedFishes : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_particle = null;

    [System.Serializable]
    private class DispData
    {
        public GameObject obj = null;
        public int dispCount = 0;
    }
    [SerializeField]
    private List<DispData> m_dispData = new List<DispData>();

    private void Awake()
    {
        UpdateDisp(0);
    }

    public void UpdateDisp(int point)
    {
        foreach (var data in m_dispData)
        {
            data.obj.SetActive(point >= data.dispCount);
        }
    }

    public void PlayParticle()
    {
        m_particle.Play();
    }
}
