using UnityEngine;
using System.Collections; // �R���[�`�����g�p���邽�߂ɒǉ�

public class ObjectVibration : MonoBehaviour
{
    public float vibrationMagnitude = 0.1f; // �U���̋���
    public float vibrationDuration = 0.1f; // �U���̎�������

    private Vector3 originalPosition; // �I�u�W�F�N�g�̌��̈ʒu
    private float timer = 0f; // �^�C�}�[

    void Start()
    {
        originalPosition = transform.position; // �I�u�W�F�N�g�̌��̈ʒu��ۑ�
    }

    void Update()
    {
        timer += Time.deltaTime; // �^�C�}�[���X�V

        // �^�C�}�[���U���̊Ԋu�𒴂�����
        if (timer >= 3f)
        {
            // �U�������s
            StartCoroutine(Vibrate());
            timer = 0f; // �^�C�}�[�����Z�b�g
        }
    }

    IEnumerator Vibrate()
    {
        // �U���̎������Ԃ����U��
        float elapsed = 0f;
        while (elapsed < vibrationDuration)
        {
            // �����_���ȕ����ɐU����������
            Vector3 randomDirection = Random.insideUnitSphere * vibrationMagnitude;
            transform.position = originalPosition + randomDirection;

            // �o�ߎ��Ԃ��X�V
            elapsed += Time.deltaTime;

            yield return null;
        }

        // �U�����I�������猳�̈ʒu�ɖ߂�
        transform.position = originalPosition;
    }
}