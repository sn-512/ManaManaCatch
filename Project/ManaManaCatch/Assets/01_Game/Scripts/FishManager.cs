using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    private static FishManager ms_instance = null;

    // ���[���̐�
    public static readonly int LANE_COUNT = 4;

    private float m_gameTime = 0f;

    private bool m_isStart = false;

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
    enum eFishType
    {
        None,
        Fish,
        Trash,
        Bomb
    }
    private class TableData
    {
        public eFishType type = eFishType.None;
        public float time;
    }

    // ���[���̃f�[�^
    private class LaneData
    {
        // ���[���̈ʒu�p��Transform
        public Transform laneTf = null;
        // �����ς݂̕Y�����̃��X�g
        public List<Fish> fishes = new List<Fish>();
        // ���ɕY�����𐶐�����܂ł̎��ԁi�e�X�g�p�Ȃ̂ō폜�\��j
        public float nextTime = 0f;

        public List<TableData> tableList = new List<TableData>();
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
            TableSetup(lane.tableList, 100, 60, 30);
            m_laneData.Add(lane);
        }
    }
    private void Start()
    {
        GameStart();
    }
    public void GameStart()
    {
        m_isStart = true;
        m_gameTime = 0f;
    }
    private void TableSetup(List<TableData> tableList, int fishQuantity, int trashQuantity, int bombQuantity)
    {
        int x = fishQuantity + trashQuantity + bombQuantity;
        float time = 0f;
        List<eFishType> eFishTypes = new List<eFishType>();
        List<int> ratioList = new List<int>();


        for (int i = 0; i < x; i++)
        {
            eFishTypes.Clear();
            int randNum = 0;
            if (fishQuantity > 0)
            {
                eFishTypes.Add(eFishType.Fish);
                randNum += 70;
                ratioList.Add(70);
            }
            if (trashQuantity > 0)
            {
                eFishTypes.Add(eFishType.Trash);
                randNum += 20;
                ratioList.Add(20);
            }
            if (bombQuantity > 0)
            {
                eFishTypes.Add(eFishType.Bomb);
                randNum += 10;
                ratioList.Add(10);
            }


            TableData tableData = new TableData();
            // �����_���ŕY�����𐶐�����
            int rand = Random.Range(0, randNum);
            for (int j = eFishTypes.Count - 1; j >= 0; j--)
            {
                if (rand < ratioList[j])
                {
                    tableData.type = eFishTypes[j];
                    if (tableData.type == eFishType.Fish) fishQuantity--;
                    if (tableData.type == eFishType.Trash) trashQuantity--;
                    if (tableData.type == eFishType.Bomb) bombQuantity--;
                    break;
                }
            }

            tableData.time = time;
            time += Random.Range(0.5f, 1f);
            tableList.Add(tableData);

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
        //lane.fishes=
    }


    /// <summary>
    /// ���͂̎󂯕t����Fish�Ƃ̔���
    /// �܂�������
    /// </summary>
    void Hoge(int i)
    {
        if (Input.GetKeyDown(GetKeyBind(i)))
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
        //List<Fish> fishList = m_laneData[i].fishes;
        //if (fishList.Count <= 0) return;
        //foreach (Fish fish in fishList)
        //{
        //    if (fish.IsIgnore())
        //    {
        //        m_laneData[i].fishes.Remove(fish);
        //    }

        //    if (fishList.Count >= 0) break;
        //}

        m_laneData[i].fishes.RemoveAt(0);
    }

    private Fish SpawnFish(eFishType fishType , int index)
    {
        GameObject prefab = null;

        switch (fishType)
        {
            case eFishType.Fish:
                prefab = m_fishPrefabs[Random.Range(0, m_fishPrefabs.Count)];
                break;
            case eFishType.Trash:
                prefab = m_trashPrefabs[Random.Range(0, m_trashPrefabs.Count)];
                break;
            case eFishType.Bomb:
                prefab = m_bombPrefabs[Random.Range(0, m_bombPrefabs.Count)];
                break;
        }

        // �Y�����̃v���n�u���I�u�W�F�N�g��
        GameObject obj = Instantiate(prefab);

        // ���������Y�����̍��������[���ɍ��킹��
        var pos = obj.transform.position;
        pos.y = LaneManager.instance.GetLanePosY(index);
        obj.transform.position = pos;

        // ��������Fish�N���X���e���[���̃��X�g�ɒǉ�
        Fish fish = obj.GetComponent<Fish>();

        return fish;
    }
    private void Update()
    {
        if (!m_isStart) return;

        m_gameTime += Time.deltaTime;

        for (int i = 0; i < LANE_COUNT; i++)
        {
            var lane = m_laneData[i];
            if (lane.tableList.Count <= 0) continue;

            if (lane.tableList[0].time <= m_gameTime)
            {
                Fish fish = SpawnFish(lane.tableList[0].type, i);

                lane.fishes.Add(fish);

                lane.tableList.RemoveAt(0);
            }

            // �����_���ŕY�����𐶐�����e�X�g�����i�폜�\��j
            //UpdateRandomTest(lane);

            //FishSpawner(lane);

            //RemoveFish(i);
            //Hoge(i);
        }
    }
}
