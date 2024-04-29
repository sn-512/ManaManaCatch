using UnityEngine;
using System.Collections; // コルーチンを使用するために追加

public class ObjectVibration : MonoBehaviour
{
    public float vibrationMagnitude = 0.1f; // 振動の強さ
    public float vibrationDuration = 0.1f; // 振動の持続時間

    private Vector3 originalPosition; // オブジェクトの元の位置
    private float timer = 0f; // タイマー

    void Start()
    {
        originalPosition = transform.position; // オブジェクトの元の位置を保存
    }

    void Update()
    {
        timer += Time.deltaTime; // タイマーを更新

        // タイマーが振動の間隔を超えたら
        if (timer >= 3f)
        {
            // 振動を実行
            StartCoroutine(Vibrate());
            timer = 0f; // タイマーをリセット
        }
    }

    IEnumerator Vibrate()
    {
        // 振動の持続時間だけ振動
        float elapsed = 0f;
        while (elapsed < vibrationDuration)
        {
            // ランダムな方向に振動を加える
            Vector3 randomDirection = Random.insideUnitSphere * vibrationMagnitude;
            transform.position = originalPosition + randomDirection;

            // 経過時間を更新
            elapsed += Time.deltaTime;

            yield return null;
        }

        // 振動が終了したら元の位置に戻す
        transform.position = originalPosition;
    }
}