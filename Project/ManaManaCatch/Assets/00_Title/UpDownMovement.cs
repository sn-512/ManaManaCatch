using UnityEngine;

public class UpDownMovement : MonoBehaviour
{
    public float speed = 2.0f; // 移動速度
    public float distance = 2.0f; // 上下の移動距離

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // オブジェクトの初期位置を記録
    }

    void Update()
    {
        // 上下に移動する
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * distance;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}