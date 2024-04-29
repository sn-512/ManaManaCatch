using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [System.Serializable]
    private class AnimElem
    {
        public float time = 0.5f;
        public Sprite sprite = null;
    }

    [System.Serializable]
    private class AnimData
    {
        public bool loop = false;
        public List<AnimElem> elements = new List<AnimElem>();
        public AnimElem GetElem(int index)
        {
            if (!(0 <= index && index < elements.Count)) return null;
            return elements[index];
        }
    }

    [SerializeField]
    private int m_no = 0;
    [SerializeField]
    private AnimData m_idleAnimData = null;
    [SerializeField]
    private AnimData m_scoopAnimData = null;
    [SerializeField]
    private AnimData m_catchAnimData = null;
    [SerializeField]
    private AnimData m_failedAnimData = null;

    private enum AnimType
    {
        Idle,
        Scoop,
        Catch,
        Failed,
    }
    private AnimType m_animType = AnimType.Idle;
    private float m_elapsedTime = 0f;
    private int m_animIndex = 0;

    private SpriteRenderer m_rend = null;

    public bool isPlaying { get { return m_animType != AnimType.Idle; } }

    private void Awake()
    {
        m_rend = GetComponentInChildren<SpriteRenderer>();
    }

    private void ChangeAnim(AnimType type)
    {
        m_elapsedTime = 0f;
        m_animIndex = 0;
        m_animType = type;
        Apply();
    }

    public void PlayCatchAnim(bool success)
    {
        if (isPlaying) return;
        if (success)
        {
            ChangeAnim(AnimType.Catch);
            LaneManager.instance.AddPoint(m_no, 5);
        }
        else
        {
            ChangeAnim(AnimType.Failed);
            LaneManager.instance.AddPoint(m_no, -5);
        }
    }

    public void PlayScoopAnim()
    {
        if (isPlaying) return;
        ChangeAnim(AnimType.Scoop);
    }

    public void PlayBombAnim()
    {
    }

    private AnimData GetAnimData()
    {
        switch (m_animType)
        {
            case AnimType.Idle:
                return m_idleAnimData;
            case AnimType.Scoop:
                return m_scoopAnimData;
            case AnimType.Catch:
                return m_catchAnimData;
            case AnimType.Failed:
                return m_failedAnimData;
        }
        return null;
    }

    private void Apply()
    {
        var animData = GetAnimData();
        var elm = animData.GetElem(m_animIndex);
        m_rend.sprite = elm.sprite;
    }

    private void Update()
    {
        var animData = GetAnimData();
        var elm = animData.GetElem(m_animIndex);
        if (m_elapsedTime < elm.time)
        {
            m_elapsedTime += Time.deltaTime;
        }
        else
        {
            m_animIndex++;
            if (m_animIndex >= animData.elements.Count)
            {
                if (animData.loop)
                {
                    m_animIndex = 0;
                }
                else
                {
                    ChangeAnim(AnimType.Idle);
                }
            }
            else
            {
                Apply();
            }
            m_elapsedTime -= elm.time;
        }
    }
}
