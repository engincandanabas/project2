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
        if(!GameManager.Instance.GameSuccess && !GameManager.Instance.GameFail)
            transform.position += Vector3.forward * moveSpeed*Time.deltaTime;
        if (this.transform.position.y < -5)
        {
            GameManager.Instance.GameFail = true;
        }
    }
    private void OnEnable()
    {
        StackController.OnStackSpawned += ChangePlayerPosition;
        GameManager.OnGameWin += ChangeAnimation;
    }
    private void OnDisable()
    {
        StackController.OnStackSpawned -= ChangePlayerPosition;
        GameManager.OnGameWin -= ChangeAnimation;
    }
    public void ChangePlayerPosition(float x)
    {
        transform.position=new Vector3 (x, transform.position.y, transform.position.z);
    }
    private void ChangeAnimation()
    {
        animator.SetBool("dance", true);
    }
}
