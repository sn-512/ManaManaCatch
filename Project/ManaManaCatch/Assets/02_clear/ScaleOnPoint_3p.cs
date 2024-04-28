using UnityEngine;

public class ScaleOnPoint_3p : MonoBehaviour
{
    public float minHeight = 1.0f; // Y軸の最小の大きさ
    public float maxHeight = 5.0f; // Y軸の最大の大きさ
    public float lastpoint_3p;
    public float point_3p = 40;
    

    void Update()
    {
        if (point_3p < 32)
        {
            point_3p += 0.007f;
        }
        lastpoint_3p = point_3p / 50;
        lastpoint_3p = point_3p / 50;
        
        // Y軸の大きさを計算する
        float scaleY = Mathf.Lerp(minHeight, maxHeight, lastpoint_3p);

        // オブジェクトの大きさを設定する
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
}