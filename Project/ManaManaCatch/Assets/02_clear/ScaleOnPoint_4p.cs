using UnityEngine;

public class ScaleOnPoint_4p : MonoBehaviour
{
    public float minHeight = 1.0f; // Y軸の最小の大きさ
    public float maxHeight = 5.0f; // Y軸の最大の大きさ
    public float lastpoint_4p;
    public float point_4p = 40;
    

    void Update()
    {
        if (point_4p < 20)
        {
            point_4p += 0.007f;
        }
        lastpoint_4p = point_4p / 50;
        lastpoint_4p = point_4p / 50;
        
        // Y軸の大きさを計算する
        float scaleY = Mathf.Lerp(minHeight, maxHeight, lastpoint_4p);

        // オブジェクトの大きさを設定する
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
}