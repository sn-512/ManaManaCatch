using UnityEngine;

public class ScaleOnPoint_4p : MonoBehaviour
{
    public float minHeight = 1.0f; // Y軸の最小の大きさ
    public float maxHeight = 5.0f; // Y軸の最大の大きさ
    public float lastpoint_4p;
    public float point_4p = 40;

    private int scorePoint = 0;
    private int scoreMax = 0;

    private void Awake()
    {
        scorePoint = GameData.scoreList[3];

        scoreMax = GameData.scoreMax;
    }


    void Update()
    {
        if (point_4p < scorePoint)
        {
            point_4p += 0.007f;
        }
        lastpoint_4p = point_4p / scoreMax;
        
        // Y軸の大きさを計算する
        float scaleY = Mathf.Lerp(minHeight, maxHeight, lastpoint_4p);

        // オブジェクトの大きさを設定する
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
}