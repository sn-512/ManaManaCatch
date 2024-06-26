using UnityEngine;

public class ScaleOnPoint_1p : MonoBehaviour
{
    public float minHeight = 1.0f; // Y軸の最小の大きさ
    public float maxHeight = 5.0f; // Y軸の最大の大きさ
    public float lastpoint_1p;
    public float point_1p = 40;

    private int scorePoint = 0;
    private int scoreMax = 0;

    private void Awake()
    {
        scorePoint = GameData.scoreList[0];

        scoreMax = GameData.scoreMax;
    }


    void Update()
    {
        if (point_1p < scorePoint)
        {
            point_1p += 0.007f;
        }
        lastpoint_1p = point_1p / scoreMax;
        
        
        // Y軸の大きさを計算する
        float scaleY = Mathf.Lerp(minHeight, maxHeight, lastpoint_1p);

        // オブジェクトの大きさを設定する
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
}