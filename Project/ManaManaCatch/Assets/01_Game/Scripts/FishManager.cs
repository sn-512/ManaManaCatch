using System.Collections;
using System.Collections.Generic;
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
    }
    // ���[���f�[�^�̃��X�g
    private List<LaneData> m_laneData = new List<LaneData>();

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

    private void Update()
    {
        for (int i = 0; i < LANE_COUNT; i++)
        {
            var lane = m_laneData[i];

            // �����_���ŕY�����𐶐�����e�X�g�����i�폜�\��j
            UpdateRandomTest(lane);
        }
    }
}
