using UnityEngine;

public class ScaleOnPoint_2p : MonoBehaviour
{
    public float minHeight = 1.0f; // Y軸の最小の大きさ
    public float maxHeight = 5.0f; // Y軸の最大の大きさ
    public float lastpoint_2p;
    public float point_2p = 0;

    private int scorePoint = 0;
    private int scoreMax = 0;

    private void Awake()
    {
        scorePoint = GameData.scoreList[1];

        scoreMax = GameData.scoreMax;
    }





    void Update()
    {
        if (point_2p < scorePoint)
        {
            point_2p += 0.007f;
        }
        lastpoint_2p = point_2p / scoreMax;
        
        // Y軸の大きさを計算する
        float scaleY = Mathf.Lerp(minHeight, maxHeight, lastpoint_2p);

        // オブジェクトの大きさを設定する
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
    
}