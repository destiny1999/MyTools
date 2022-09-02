using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] float moveSpeed;
    Vector3 move = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
    }
    void FixedUpdate()
    {
        characterController.Move(move * moveSpeed * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
}
