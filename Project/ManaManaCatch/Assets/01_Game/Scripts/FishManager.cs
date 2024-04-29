using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    private static FishManager ms_instance = null;

    // レーンの数
    public static readonly int LANE_COUNT = 4;

    private float m_gameTime = 0f;

    private bool m_isStart = false;

    // レーンの位置判断用のTransformのリスト
    [SerializeField]
    private List<Transform> m_laneTfs = new List<Transform>();
    // 魚系のプレハブリスト
    [SerializeField]
    private List<GameObject> m_fishPrefabs = new List<GameObject>();
    // ゴミ系のプレハブリスト
    [SerializeField]
    private List<GameObject> m_trashPrefabs = new List<GameObject>();
    // 爆弾系のプレハブリスト
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

    // レーンのデータ
    private class LaneData
    {
        // レーンの位置用のTransform
        public Transform laneTf = null;
        // 生成済みの漂流物のリスト
        public List<Fish> fishes = new List<Fish>();
        // 次に漂流物を生成するまでの時間（テスト用なので削除予定）
        public float nextTime = 0f;

        public List<TableData> tableList = new List<TableData>();
    }
    // レーンデータのリスト
    private List<LaneData> m_laneData = new List<LaneData>();
    enum ePlayerType
    {
        Player1,
        Player2,
        Player3,
        Player4,
    }
    /// <summary>
    /// インデックスに応じてキーコードを返す
    /// </summary>
    /// <param name="num">インデックス</param>
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

        // レーンの数だけレーンデータをリストに追加
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
            // ランダムで漂流物を生成する
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
    // 魚の移動終了位置を返す
    public static Vector3 GetFishMoveEndPos()
    {
        if (ms_instance == null) return new Vector3(-4.69f, 0f, 0f);
        return new Vector3(-4.69f, 0f, 0f);
    }

    // ランダムで漂流物を生成するテスト処理（削除予定）
    private void UpdateRandomTest(LaneData lane)
    {
        if (lane.nextTime <= 0f)
        {
            GameObject prefab = null;

            // ランダムで漂流物を生成する
            int rand = Random.Range(0, 100);
            // 70%で魚
            if (rand < 70) prefab = m_fishPrefabs[Random.Range(0, m_fishPrefabs.Count)];
            // 20%でゴミ
            else if (rand < 20) prefab = m_trashPrefabs[Random.Range(0, m_trashPrefabs.Count)];
            // 10%で爆弾
            else prefab = m_bombPrefabs[Random.Range(0, m_bombPrefabs.Count)];

            // 漂流物のプレハブをオブジェクト化
            GameObject obj = Instantiate(prefab);

            // 生成した漂流物の高さをレーンに合わせる
            var pos = obj.transform.position;
            pos.y = lane.laneTf.position.y;
            obj.transform.position = pos;

            // 生成したFishクラスを各レーンのリストに追加
            Fish fish = obj.GetComponent<Fish>();
            lane.fishes.Add(fish);

            // 次に生成する時間をランダムで設定
            lane.nextTime += Random.Range(3f, 5f);
        }
        else
        {
            lane.nextTime -= Time.deltaTime;
        }
    }
    /// <summary>
    /// laneDataのfishQuantityで魚の発生を制限
    /// </summary>
    private void FishSpawner(LaneData lane)
    {
        //lane.fishes=
    }


    /// <summary>
    /// 入力の受け付けとFishとの判定
    /// まだ未完成
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

        // 漂流物のプレハブをオブジェクト化
        GameObject obj = Instantiate(prefab);

        // 生成した漂流物の高さをレーンに合わせる
        var pos = obj.transform.position;
        pos.y = LaneManager.instance.GetLanePosY(index);
        obj.transform.position = pos;

        // 生成したFishクラスを各レーンのリストに追加
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

            // ランダムで漂流物を生成するテスト処理（削除予定）
            //UpdateRandomTest(lane);

            //FishSpawner(lane);

            //RemoveFish(i);
            //Hoge(i);
        }
    }
}
