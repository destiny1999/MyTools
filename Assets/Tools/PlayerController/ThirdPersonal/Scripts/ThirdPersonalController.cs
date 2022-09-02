using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonalController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string planeTag = "Plane";
    float rayLength = 100f;
    [SerializeField] GameObject moveTipMark;
    [SerializeField] float moveSpeed;
    List<Vector3> targetPositions = new List<Vector3>();
    bool moving = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //int layerMask = 1 << 8; // if need layer
            
            if(Physics.Raycast(ray, out hit, rayLength))
            {
                if (hit.transform.CompareTag(planeTag))
                {
                    if(GameObject.Find("moveTip"))
                    {
                        DestroyImmediate(GameObject.Find("moveTip"));
                    }
                    Vector3 targetPosition = hit.point;
                    targetPosition.y += 0.01f;
                    GameObject newMoveTipMark = Instantiate(moveTipMark);
                    newMoveTipMark.name = "moveTip";
                    newMoveTipMark.transform.position = targetPosition;
                    targetPositions.Add(targetPosition);
                    if (moving) targetPositions.RemoveAt(0);
                    else StartCoroutine(MoveToTargetPosition());
                }
            }
        }
    }
    IEnumerator MoveToTargetPosition()
    {
        moving = true;
        while(Vector3.Distance(transform.position, targetPositions[0]) >= 0.1f)
        {
            transform.LookAt(targetPositions[0]);
            transform.position = Vector3.MoveTowards(transform.position, targetPositions[0], moveSpeed * Time.deltaTime);
            yield return null;
        }
        moving = false;
        targetPositions.RemoveAt(0);
    }
}
