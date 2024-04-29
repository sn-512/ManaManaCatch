using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private float m_raycastLength = 1;
    private SpriteRenderer m_spriteRenderer = null;
    private Transform m_tf = null;
    private LayerMask m_layerMask;
    // Start is called before the first frame update
    void Start()
    {
        m_tf = transform;
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {


    }

}
