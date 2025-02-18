using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.position += Vector3.forward * moveSpeed*Time.deltaTime;
    }
}
