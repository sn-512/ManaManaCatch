using UnityEngine;

public class UpDownMovement : MonoBehaviour
{
    public float speed = 2.0f; // �ړ����x
    public float distance = 2.0f; // �㉺�̈ړ�����

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // �I�u�W�F�N�g�̏����ʒu���L�^
    }

    void Update()
    {
        // �㉺�Ɉړ�����
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * distance;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}