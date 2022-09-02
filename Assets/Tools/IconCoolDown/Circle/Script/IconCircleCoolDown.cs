using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconCircleCoolDown : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Image coolDownPanel;
    [SerializeField] float coolDownTime;
    float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCoolDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // just for test
            ResetAndStartCoolDown();
        }
    }
    public void ResetAndStartCoolDown()
    {
        time = 0f;
        StartCoroutine(StartCoolDown());
    }
    IEnumerator StartCoolDown()
    {
        while (time < coolDownTime)
        {
            time = Mathf.Clamp(time + Time.deltaTime * 1, time, coolDownTime);

            coolDownPanel.GetComponent<Image>().fillAmount = 1 - time / coolDownTime;
            yield return null;
        }
    }
}
