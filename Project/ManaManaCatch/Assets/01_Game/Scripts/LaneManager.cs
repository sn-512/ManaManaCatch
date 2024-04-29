using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaneManager : MonoBehaviour
{
    private static LaneManager ms_instance = null;
    public static LaneManager instance { get { return ms_instance; } }

    [SerializeField]
    private float m_laneWidth = 1f;
    [SerializeField]
    private RectTransform m_uiRT = null;

    [System.Serializable]
    public class LaneData
    {
        public Transform laneTF = null;
        public Transform areaTF = null;
        public Player player = null;
        public RectTransform laneRT = null;
        public PointUI pointUI = null;
        public int point = 0;
        public StackedFishes stackedFishes = null;
    }
    [SerializeField]
    private List<LaneData> m_laneData = new List<LaneData>();

    [ContextMenu("Apply")]
    private void Apply()
    {
        float size = 9f;
        float height = 1080f;
        m_uiRT.anchoredPosition = new Vector2(0f, (transform.position.y / size) * height);

        int count = m_laneData.Count;
        float width = m_laneWidth * count;
        float startPosY = width * 0.5f;
        for (int i = 0; i < count; i++)
        {
            var lane = m_laneData[i];
            var posY = startPosY - m_laneWidth * i;
            lane.laneTF.localPosition = new Vector3(0f, posY - m_laneWidth * 0.5f, 0f);

            var scale = lane.areaTF.localScale;
            scale.y = m_laneWidth;
            lane.areaTF.localScale = scale;

            //lane.laneRT.anchoredPosition = new Vector2(0f, ((posY - m_laneWidth) / size) * height);
        }
    }

    private void Awake()
    {
        ms_instance = this;

        Apply();

        int count = m_laneData.Count;
        for (int i = 0; i < count; i++)
        {
            var lane = m_laneData[i];
            lane.pointUI.SetPoint(lane.point);
        }
    }

    public void AddPoint(int index, int point)
    {
        if (!(0 <= index && index < m_laneData.Count)) return;

        var lane = m_laneData[index];

        var lastPoint = lane.point;
        lane.point += point;
        if (lane.point < 0) lane.point = 0;

        lane.pointUI.SetPoint(lane.point);
        lane.stackedFishes.UpdateDisp(lane.point);

        if (lastPoint - lane.point >= 2)
        {
            lane.stackedFishes.PlayParticle();
        }
    }

    public float GetLanePosY(int no)
    {
        if (!(0 <= no && no < m_laneData.Count)) return 0f;
        return m_laneData[no].laneTF.position.y;
    }

    public int GetLaneScore(int no)
    {
        if (!(0 <= no && no < m_laneData.Count)) return 0;
        return m_laneData[no].point;
    }
}
