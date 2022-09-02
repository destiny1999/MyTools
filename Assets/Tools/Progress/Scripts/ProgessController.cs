using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgessController : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Image fill;
    [SerializeField][Tooltip("total time that will pass")] float totalTime = 10f;
    [SerializeField][Tooltip("pass this time will add new value")] float passTime = 0.01f;
    [SerializeField][Tooltip("after pass time this value will be add")] float addValue = 0.01f;
    float time = 0f;
    float fullWidth;
    // Start is called before the first frame update
    void Start()
    {
        fullWidth = background.GetComponent<RectTransform>().sizeDelta.x;
        StartCoroutine(StartTimer());
    }
    void Update()
    {
        
    }
    IEnumerator StartTimer()
    {
        while(time < totalTime)
        {
            time = Mathf.Clamp(time + addValue, time, totalTime);
            fill.GetComponent<RectTransform>().sizeDelta =
            new Vector2(time / totalTime * fullWidth, fill.GetComponent<RectTransform>().sizeDelta.y);

            fill.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(fill.GetComponent<RectTransform>().sizeDelta.x / 2,
                            fill.GetComponent<RectTransform>().anchoredPosition.y);
            yield return new WaitForSeconds(passTime);
        }
    }
}
