using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSometing : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string nextName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DontDestroyOnLoad(transform.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextName);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.MoveGameObjectToScene(transform.gameObject, SceneManager.GetActiveScene());
        }
    }
}
