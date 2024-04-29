using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private float m_timing = 3f;
    /// <summary>
    /// タイミングとの差が＋ーinterval秒なら成功
    /// </summary>
    [SerializeField] private float m_interval = 0.5f;

    [SerializeField] private float m_elapsedTime = 0f;

    private Transform m_tf = null;
    private Vector3 m_startPos = Vector3.zero;
    private Vector3 m_endPos = Vector3.zero;
    private float m_canScoopTime = 1f;

    private SpriteRenderer spriteRenderer;

    public bool m_destroy = false;
    // Start is called before the first frame update
    void Awake()
    {
        m_tf = transform;
        m_startPos = m_tf.position;
        m_endPos = FishManager.GetFishMoveEndPos();
        m_interval = 0.3f;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_elapsedTime += Time.deltaTime;
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{

        //    if (EvaluateTiming())
        //    {
        //       Debug.Log("Hit");
        //    }
        //    else
        //    {
        //        Debug.Log("Miss");
        //    }
        //}

        if (CanScoop())
        {
            spriteRenderer.color = Color.red;
            if (EvaluateTiming())
            {
                spriteRenderer.color = Color.yellow;
            }
            //if (EvaluateTiming())
            //{
            //    Debug.Log("A");
            //}
        }
        else
        {
            spriteRenderer.color = Color.white;
        }


        //経過時間+判定　削除処理
        if (IsOutOfView())
        {
            Destroy();
        }
        //スタートフラグをtrueにしないと移動、経過時間の計測をしない(デバッグ用)
        //if (m_isStart)
        {
            var posY = m_tf.position.y;
            var pos = Vector3.LerpUnclamped(m_startPos, m_endPos, m_elapsedTime / m_timing);
            pos.y = posY;
            transform.position = pos;
        }
    }
    /// <summary>
    /// タイミングの評価
    /// </summary>
    /// <returns>経過時間とタイミングの差がm_interval以内であればtrue</returns>
    public bool EvaluateTiming()
    {
        return Mathf.Abs(m_elapsedTime - m_timing) <= m_interval;
    }
    /// <summary>
    /// タイミングの評価を行うか判定する
    /// </summary>
    /// <returns>経過時間とタイミングの差がm_canScoopTime以内であればtrue</returns>
    public bool CanScoop()
    {
        return Mathf.Abs(m_elapsedTime - m_timing) <= m_canScoopTime;
    }
    /// <summary>
    /// ボタンを押しても無視する
    /// </summary>
    /// <returns>経過時間がタイミング+判定間隔を超えるとtrue</returns>
    public bool IsIgnore()
    {
        return (m_elapsedTime) >= m_timing + m_interval;
    }
    /// <summary>
    /// 画面外へ行ったかをざっくりと判定
    /// </summary>
    /// <returns></returns>
    public bool IsOutOfView()
    {
        return m_elapsedTime >= m_timing + 2f;
    }
    /// <summary>
    ///　タイミングとの差
    /// </summary>
    /// <returns>経過時間とタイミングとの差</returns>
    public float DiffElapsedTimeAndTiming()
    {
        return m_elapsedTime - m_timing;
    }
    /// <summary>
    /// Destroyをクラス外から呼ぶ
    /// </summary>
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
