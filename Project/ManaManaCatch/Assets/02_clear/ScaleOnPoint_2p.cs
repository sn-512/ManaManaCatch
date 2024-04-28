using UnityEngine;

public class ScaleOnPoint_2p : MonoBehaviour
{
    public float minHeight = 1.0f; // Y���̍ŏ��̑傫��
    public float maxHeight = 5.0f; // Y���̍ő�̑傫��
    public float lastpoint_2p;
    public float point_2p = 0;
    
    
    

    void Update()
    {
        if (point_2p < 48)
        {
            point_2p += 0.007f;
        }
        lastpoint_2p = point_2p / 50;
        
        // Y���̑傫�����v�Z����
        float scaleY = Mathf.Lerp(minHeight, maxHeight, lastpoint_2p);

        // �I�u�W�F�N�g�̑傫����ݒ肷��
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
    
}