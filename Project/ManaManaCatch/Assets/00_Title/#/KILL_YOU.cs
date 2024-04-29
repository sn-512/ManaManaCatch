using UnityEngine;

public class KILL_YOU : MonoBehaviour
{
    void Update()
    {
        // スペースキーが押されたら
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // オブジェクトを非アクティブにする（消える）
            gameObject.SetActive(false);
        }
    }
}