using UnityEngine;

public class KILL_YOU : MonoBehaviour
{
    void Update()
    {
        // �X�y�[�X�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �I�u�W�F�N�g���A�N�e�B�u�ɂ���i������j
            gameObject.SetActive(false);
        }
    }
}