using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private float m_timing = 3f;
    /// <summary>
    /// �^�C�~���O�Ƃ̍����{�[interval�b�Ȃ琬��
    /// </summary>
    [SerializeField] private float m_interval = 0.3f;

    [SerializeField] private float m_elapsedTime = 0f;

    private Transform m_tf = null;
    private Vector3 m_startPos = Vector3.zero;
    private Vector3 m_endPos = Vector3.zero;
    private float m_canScoopTime = 0.5f;
    // Start is called before the first frame update
    void Awake()
    {
        m_tf = transform;
        m_startPos = m_tf.position;
        m_endPos = FishManager.GetFishMoveEndPos();
        m_interval = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        m_elapsedTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            if (EvaluateTiming())
            {
                Debug.Log("Hit");
            }
            else
            {
                Debug.Log("Miss");
            }
        }

        if (!CanScoop())
        {
            if (EvaluateTiming())
            {
                Debug.Log("A");
            }
        }


        //�o�ߎ���+����@�폜����
        if (IsIgnore())
        {
            Destroy(this.gameObject);
        }
        //�X�^�[�g�t���O��true�ɂ��Ȃ��ƈړ��A�o�ߎ��Ԃ̌v�������Ȃ�(�f�o�b�O�p)
        //if (m_isStart)
        {
            var posY = m_tf.position.y;
            var pos = Vector3.LerpUnclamped(m_startPos, m_endPos, m_elapsedTime / m_timing);
            pos.y = posY;
            transform.position = pos;
        }
    }
    /// <summary>
    /// �^�C�~���O�̕]��
    /// </summary>
    /// <returns>�o�ߎ��Ԃƃ^�C�~���O�̍���m_interval�ȓ��ł����true</returns>
    public bool EvaluateTiming()
    {
        return Mathf.Abs(DiffElapsedTimeAndTiming()) <= m_interval;
    }
    /// <summary>
    /// �^�C�~���O�̕]�����s�������肷��
    /// </summary>
    /// <returns>�o�ߎ��Ԃƃ^�C�~���O�̍���m_canScoopTime�ȓ��ł����true</returns>
    public bool CanScoop()
    {
        return Mathf.Abs(DiffElapsedTimeAndTiming()) <= m_canScoopTime;
    }
    /// <summary>
    /// ��������(�폜�t���O�Ƃ��Ďg�p)
    /// </summary>
    /// <returns>�o�ߎ���+����Ԋu���^�C�~���O�𒴂����true</returns>
    public bool IsIgnore()
    {
        return (m_elapsedTime + m_interval) >= m_timing;
    }
    /// <summary>
    ///�@�^�C�~���O�Ƃ̍�
    /// </summary>
    /// <returns>�o�ߎ��Ԃƃ^�C�~���O�Ƃ̍�</returns>
    public float DiffElapsedTimeAndTiming()
    {
        return m_elapsedTime - m_timing;
    }
}
