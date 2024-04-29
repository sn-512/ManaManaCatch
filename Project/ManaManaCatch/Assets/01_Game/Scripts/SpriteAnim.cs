using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnim : MonoBehaviour
{
    [SerializeField]
    private float m_animTime = 0.5f;
    [SerializeField]
    private List<Sprite> m_sprites = new List<Sprite>();

    private float m_elapsedTime = 0f;
    private int m_animIndex = 0;
    private SpriteRenderer m_rend = null;

    private void Awake()
    {
        m_rend = GetComponent<SpriteRenderer>();
        if (m_sprites.Count > 0)
        {
            m_rend.sprite = m_sprites[0];
        }
    }

    private void Update()
    {
        if (m_sprites.Count < 2) return;

        if (m_elapsedTime >= m_animTime)
        {
            m_animIndex++;
            if (m_animIndex >= m_sprites.Count) m_animIndex -= m_sprites.Count;
            m_rend.sprite = m_sprites[m_animIndex];
            m_elapsedTime -= m_animTime;
        }

        m_elapsedTime += Time.deltaTime;
    }
}
