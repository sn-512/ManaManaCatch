using UnityEngine;

public class ScaleOnPoint_4p : MonoBehaviour
{
    public float minHeight = 1.0f; // Y���̍ŏ��̑傫��
    public float maxHeight = 5.0f; // Y���̍ő�̑傫��
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
        
        // Y���̑傫�����v�Z����
        float scaleY = Mathf.Lerp(minHeight, maxHeight, lastpoint_4p);

        // �I�u�W�F�N�g�̑傫����ݒ肷��
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
}