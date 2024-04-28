using UnityEngine;

public class ScaleOnPoint_3p : MonoBehaviour
{
    public float minHeight = 1.0f; // Y���̍ŏ��̑傫��
    public float maxHeight = 5.0f; // Y���̍ő�̑傫��
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
        
        // Y���̑傫�����v�Z����
        float scaleY = Mathf.Lerp(minHeight, maxHeight, lastpoint_3p);

        // �I�u�W�F�N�g�̑傫����ݒ肷��
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
}