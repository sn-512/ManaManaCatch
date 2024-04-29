using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointUI : MonoBehaviour
{
    private Color m_addColor = new Color(0.25f, 1f, 0.25f);
    private Color m_subColor = new Color(1f, 0.25f, 0.25f);

    private RectTransform m_rt = null;
    private TextMeshProUGUI m_text = null;
    private Coroutine m_animCor = null;
    private Coroutine m_numCor = null;

    private Vector2 m_startPos = Vector2.zero;
    private int m_lastPoint = 0;

    private int m_subStartPoint = 0;
    private int m_subEndPoint = 0;

    private void Awake()
    {
        m_rt = GetComponent<RectTransform>();
        m_text = GetComponentInChildren<TextMeshProUGUI>();

        m_startPos = m_rt.anchoredPosition;

        m_text.text = m_lastPoint.ToString();
    }

    public void SetPoint(int point)
    {
        if (point > m_lastPoint)
        {
            StopAnim();
            m_animCor = StartCoroutine(Co_AddAnim());
            m_text.text = point.ToString();
        }
        else if (point < m_lastPoint)
        {
            StopAnim();
            m_animCor = StartCoroutine(Co_SubAnim(m_lastPoint, point));
            m_text.text = point.ToString();
        }
        m_lastPoint = point;
    }

    private void StopAnim()
    {
        if (m_animCor == null) return;

        if (m_numCor != null)
        {
            StopCoroutine(m_numCor);
            m_numCor = null;
        }

        StopCoroutine(m_animCor);
        m_rt.anchoredPosition = m_startPos;
        m_rt.localScale = Vector3.one;
        m_text.color = Color.white;
        m_text.text = m_lastPoint.ToString();
    }

    private IEnumerator Co_AddAnim()
    {
        m_text.color = m_addColor;

        var elapsed = 0f;
        var time = 0.25f;

        while (elapsed < time)
        {
            var alpha = Easing.BackOut(elapsed, time, 0f, 1f, 5f);
            m_rt.localScale = Vector3.one * alpha;
            yield return null;

            elapsed += Time.deltaTime;
        }

        m_rt.localScale = Vector3.one;

        m_text.color = Color.white;

        m_animCor = null;
    }

    private IEnumerator Co_SubNumAnim(int start, int end)
    {
        var elapsed = 0f;
        var time = 0.25f;

        while (elapsed < time)
        {
            var alpha = elapsed / time;
            var num = Mathf.FloorToInt(Mathf.Lerp(start, end, alpha));
            m_text.text = num.ToString();
            yield return null;

            elapsed += Time.deltaTime;
        }
        m_text.text = end.ToString();

        m_numCor = null;
    }

    private IEnumerator Co_SubAnim(int startPoint, int endPoint)
    {
        m_text.color = m_subColor;

        m_numCor = StartCoroutine(Co_SubNumAnim(startPoint, endPoint));

        var shake = new Vector2(5f, 0f);
        var shakeCount = 3;

        var elapsed = 0f;
        var time = 0.1f;

        var count = 0;
        while (true)
        {
            if (elapsed >= time)
            {
                count++;
                elapsed -= time;
                if (count >= shakeCount && m_numCor == null) break;
            }

            var alpha = elapsed / time;
            var alpha2 = Mathf.Sin(Mathf.PI * 2f * alpha);
            m_rt.anchoredPosition = m_startPos + shake * alpha2;
            yield return null;

            elapsed += Time.deltaTime;
        }

        m_rt.anchoredPosition = m_startPos;

        m_text.color = Color.white;

        m_animCor = null;
    }
}
