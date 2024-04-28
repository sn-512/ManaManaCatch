using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private HitBox m_hitbox;
    [SerializeField] private float m_timing = 3f;
    [SerializeField] private float m_elapsedTime = 0f;
    [SerializeField] private bool m_isStart = false;
    [SerializeField] private float m_interval = 0.1f;

    private Transform m_tf = null;
    private Vector3 m_startPos = Vector3.zero;
    private Vector3 m_endPos = Vector3.zero;
    private float m_canScoopTime = 0.5f;
    // Start is called before the first frame update
    void Awake()
    {
        m_tf = transform;
        m_startPos = m_tf.position;
        //m_endPos = m_hitbox.transform.position;
        m_endPos = FishManager.GetFishMoveEndPos();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            if (EvaluateTiming(m_elapsedTime, m_timing))
            {
                Debug.Log("Hit");
            }
            else
            {
                Debug.Log("Miss");
            }
        }
        Debug.Log(CanScoop());
        //経過時間+判定
        if (m_elapsedTime > m_timing + m_interval)
        {

        }
        //スタートフラグをtrueにしないと移動、経過時間の計測をしない
        //if (m_isStart)
        {
            m_elapsedTime += Time.deltaTime;
            var posY = m_tf.position.y;
            var pos = Vector3.LerpUnclamped(m_startPos, m_endPos, m_elapsedTime / m_timing);
            pos.y = posY;
            transform.position = pos;
        }
    }
    /// <summary>
    /// タイミングの評価
    /// </summary>
    /// <param 経過時間="elapsedTime"></param>
    /// <param タイミング="timing"></param>
    /// <returns>経過時間とタイミングの差がm_interval以内であればtrue</returns>
    private bool EvaluateTiming(float elapsedTime, float timing)
    {
        return Mathf.Abs(elapsedTime - timing) <= m_interval;

    }
    public bool CanScoop()
    {
        return Mathf.Abs(m_elapsedTime - m_timing) <= m_canScoopTime;
    }
}
