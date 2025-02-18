using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private bool animate = false;
    private void Start()
    {
        animate = false;
    }
    private void Update()
    {
        if(animate)
            Camera.main.transform.RotateAround(player.position, Vector3.up, 50 * Time.deltaTime);
    }
    private void OnEnable()
    {
        GameManager.OnGameWin += AnimateCamera;
        GameManager.OnGameFail += DisableFollow;
    }
    private void OnDisable()
    {
        GameManager.OnGameWin -= AnimateCamera;
        GameManager.OnGameFail -= DisableFollow;
    }

    private void AnimateCamera()
    {
        this.GetComponent<CinemachineVirtualCamera>().enabled = false;
        animate = true;
    }
    private void DisableFollow()
    {
        this.GetComponent<CinemachineVirtualCamera>().enabled = false;
    }
}

