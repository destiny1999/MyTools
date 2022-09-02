using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] float destroyTime;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        time = destroyTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= 1 * Time.deltaTime;
        }
        else
        {
            DestroyImmediate(transform.gameObject);
        }
    }
}
