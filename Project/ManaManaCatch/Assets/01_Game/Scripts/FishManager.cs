using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    private static FishManager ms_instance = null;

    // ���[���̐�
    public static readonly int LANE_COUNT = 4;

    // ���[���̈ʒu���f�p��Transform�̃��X�g
    [SerializeField]
    private List<Transform> m_laneTfs = new List<Transform>();

    // ���n�̃v���n�u���X�g
    [SerializeField]
    private List<GameObject> m_fishPrefabs = new List<GameObject>();
    // �S�~�n�̃v���n�u���X�g
    [SerializeField]
    private List<GameObject> m_trashPrefabs = new List<GameObject>();
    // ���e�n�̃v���n�u���X�g
    [SerializeField]
    private List<GameObject> m_bombPrefabs = new List<GameObject>();

    // ���[���̃f�[�^
    private class LaneData
    {
        // ���[���̈ʒu�p��Transform
        public Transform laneTf = null;
        // �����ς݂̕Y�����̃��X�g
        public List<Fish> fishes = new List<Fish>();
        // ���ɕY�����𐶐�����܂ł̎��ԁi�e�X�g�p�Ȃ̂ō폜�\��j
        public float nextTime = 0f;
        //���������鋛�̃J�E���^�[
        public int fishQuantity = 10;
    }
    // ���[���f�[�^�̃��X�g
    private List<LaneData> m_laneData = new List<LaneData>();
    enum ePlayerType
    {
        Player1,
        Player2,
        Player3,
        Player4,
    }
    /// <summary>
    /// �C���f�b�N�X�ɉ����ăL�[�R�[�h��Ԃ�
    /// </summary>
    /// <param name="num">�C���f�b�N�X</param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    private KeyCode GetKeyBind(int num) => num switch
    {
        (int)ePlayerType.Player1 => KeyCode.Alpha1,
        (int)ePlayerType.Player2 => KeyCode.Alpha2,
        (int)ePlayerType.Player3 => KeyCode.Alpha3,
        (int)ePlayerType.Player4 => KeyCode.Alpha4,
        _ => throw new System.InvalidOperationException()
    };
    private void Awake()
    {
        ms_instance = this;

        // ���[���̐��������[���f�[�^�����X�g�ɒǉ�
        for (int i = 0; i < LANE_COUNT; i++)
        {
            var lane = new LaneData();
            lane.laneTf = m_laneTfs[i];
            m_laneData.Add(lane);
        }
    }

    // ���̈ړ��I���ʒu��Ԃ�
    public static Vector3 GetFishMoveEndPos()
    {
        if (ms_instance == null) return new Vector3(-4.69f, 0f, 0f);
        return new Vector3(-4.69f, 0f, 0f);
    }

    // �����_���ŕY�����𐶐�����e�X�g�����i�폜�\��j
    private void UpdateRandomTest(LaneData lane)
    {
        if (lane.nextTime <= 0f)
        {
            GameObject prefab = null;

            // �����_���ŕY�����𐶐�����
            int rand = Random.Range(0, 100);
            // 70%�ŋ�
            if (rand < 70) prefab = m_fishPrefabs[Random.Range(0, m_fishPrefabs.Count)];
            // 20%�ŃS�~
            else if (rand < 20) prefab = m_trashPrefabs[Random.Range(0, m_trashPrefabs.Count)];
            // 10%�Ŕ��e
            else prefab = m_bombPrefabs[Random.Range(0, m_bombPrefabs.Count)];

            // �Y�����̃v���n�u���I�u�W�F�N�g��
            GameObject obj = Instantiate(prefab);

            // ���������Y�����̍��������[���ɍ��킹��
            var pos = obj.transform.position;
            pos.y = lane.laneTf.position.y;
            obj.transform.position = pos;

            // ��������Fish�N���X���e���[���̃��X�g�ɒǉ�
            Fish fish = obj.GetComponent<Fish>();
            lane.fishes.Add(fish);

            // ���ɐ������鎞�Ԃ������_���Őݒ�
            lane.nextTime += Random.Range(3f, 5f);
        }
        else
        {
            lane.nextTime -= Time.deltaTime;
        }
    }
    /// <summary>
    /// laneData��fishQuantity�ŋ��̔����𐧌�
    /// </summary>
    private void FishSpawner(LaneData lane)
    {
        if (lane.nextTime <= 0f)
        {
            GameObject prefab = null;

            // �����_���ŕY�����𐶐�����
            int rand = Random.Range(0, 100);
            // 70%����fishQuantity��1�ȏ�Ȃ狛
            if (rand < 70 && lane.fishQuantity > 0)
            {
                prefab = m_fishPrefabs[Random.Range(0, m_fishPrefabs.Count)];
                lane.fishQuantity--;
                Debug.Log(lane.fishQuantity);
            }
            // 20%�ŃS�~
            else if (rand < 20) prefab = m_trashPrefabs[Random.Range(0, m_trashPrefabs.Count)];
            // 10%�Ŕ��e
            else prefab = m_bombPrefabs[Random.Range(0, m_bombPrefabs.Count)];

            // �Y�����̃v���n�u���I�u�W�F�N�g��
            GameObject obj = Instantiate(prefab);

            // ���������Y�����̍��������[���ɍ��킹��
            var pos = obj.transform.position;
            pos.y = lane.laneTf.position.y;
            obj.transform.position = pos;

            // ��������Fish�N���X���e���[���̃��X�g�ɒǉ�
            Fish fish = obj.GetComponent<Fish>();
            lane.fishes.Add(fish);

            // ���ɐ������鎞�Ԃ������_���Őݒ�
            lane.nextTime += Random.Range(3f, 5f);
        }
        else
        {
            lane.nextTime -= Time.deltaTime;
        }
    }
    /// <summary>
    /// �S�Ẵ��[���̋��̎c�ʂ��Ď�
    /// </summary>
    /// <returns>�S�Ẵ��[����1�������Ȃ����true �S�Ẵ��[����1�ł�����1�ȏ゠���false</returns>
    private bool IsNotEnptyFishQuantity()
    {
        bool isEnptyFish = true;
        for (int i = 0; i < LANE_COUNT; i++)
        {
            if (m_laneData[i].fishQuantity > 0)
            {
                isEnptyFish = false;
            }
        }
        return isEnptyFish;
    }
    /// <summary>
    /// ���͂̎󂯕t����Fish�Ƃ̔���
    /// �܂�������
    /// </summary>
    void Hoge(int i)
    {
        //if (Input.GetKeyDown(GetKeyBind(i)))
        {
            float Min = float.MaxValue;
            Fish result = null;
            foreach (Fish fish in m_laneData[i].fishes)
            {
                if (!fish.CanScoop()) continue;

                if (fish.EvaluateTiming() == true)
                {
                    if (Min > fish.DiffElapsedTimeAndTiming())
                    {
                        Min = fish.DiffElapsedTimeAndTiming();
                        result = fish;
                    }
                }
                if (result != null) Debug.Log(Min);
            }

        }
    }
    private void RemoveFish(int i)
    {
        List<Fish> fishList = m_laneData[i].fishes;
        if (fishList.Count <= 0) return;
        foreach (Fish fish in fishList)
        {
            if (fish.IsIgnore())
            {
                m_laneData[i].fishes.Remove(fish);
            }

            if (fishList.Count >= 0) break;
        }
    }
    private void Update()
    {
        for (int i = 0; i < LANE_COUNT; i++)
        {
            var lane = m_laneData[i];

            // �����_���ŕY�����𐶐�����e�X�g�����i�폜�\��j
            //UpdateRandomTest(lane);

            FishSpawner(lane);

            RemoveFish(i);
            Hoge(i);
        }
    }
}
