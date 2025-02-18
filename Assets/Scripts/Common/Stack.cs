using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stack : MonoBehaviour
{
    [Header("Move Attributes")]
    [SerializeField] private float moveOffset;
    [SerializeField] private float moveSpeed=1.5f;
    [SerializeField] private Ease ease;
    [SerializeField] private float tolerance = 0.1f;
    [SerializeField] private float fallForce = 5f; // Force applied to falling piece

    bool isLeft = false;
    private Vector3 startPos;
    private bool hasStopped = false;


    public void StartMove()
    {
        startPos = transform.localPosition;
        moveOffset = 1.5f*transform.localScale.x;
        isLeft = (Random.Range(0, 2) == 0) ? true : false;

        transform.localPosition += new Vector3((isLeft) ? -moveOffset : moveOffset, 0, 0);
        StartAnimation();
    }

    private void StartAnimation()
    {
        var target = new Vector3((isLeft) ? moveOffset  : -moveOffset , 0, 0);
        transform.DOLocalMove(startPos + target, moveSpeed).SetEase(ease).OnComplete(() =>
        {
            isLeft = !isLeft;
            StartAnimation();
        });
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasStopped)
        {
            hasStopped = true;
            DOTween.Kill(transform);

            PlaceCube();
        }
    }

    private void PlaceCube()
    {
        Transform previousStack = transform.parent.GetChild(transform.GetSiblingIndex() - 1);
        float delta = transform.localPosition.x - previousStack.localPosition.x;

        if (Mathf.Abs(delta) <= tolerance)
        {
            transform.localPosition = new Vector3(previousStack.localPosition.x, transform.localPosition.y, transform.localPosition.z);
            StackController.Instance.SpawnStack();
            StackController.Instance.TriggerComboEvent();
            AudioManager.Instance.PlaySound("Place");
            return;
        }

        if (Mathf.Abs(delta) >= transform.localScale.x)
        {
            Destroy(gameObject);
            return;
        }

        AudioManager.Instance.PlaySound("Place");
        StackController.Instance.TriggerResetEvent();

        float newSize = transform.localScale.x - Mathf.Abs(delta);
        float fallingSize = transform.localScale.x - newSize;

        transform.localScale = new Vector3(newSize, transform.localScale.y, transform.localScale.z);

        float fallingPosX = transform.localPosition.x + (delta > 0 ? newSize : -fallingSize);
        Vector3 fallingPosition = new Vector3(fallingPosX, transform.localPosition.y, transform.localPosition.z);

        GameObject fallingCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        fallingCube.transform.localScale = new Vector3(fallingSize, transform.localScale.y, transform.localScale.z);
        fallingCube.transform.position = transform.position + new Vector3((delta > 0 ? fallingSize / 2 : -fallingSize / 2), 0, 0);
        fallingCube.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
        fallingCube.GetComponent<BoxCollider>().isTrigger = true;
        Rigidbody rb = fallingCube.AddComponent<Rigidbody>();

        Vector3 forceDirection = new Vector3((delta > 0 ? 1 : -1), -2, 0).normalized; // Increase downward force
        rb.AddForce(forceDirection * fallForce, ForceMode.Impulse);

        transform.localPosition -= new Vector3((delta > 0 ? fallingSize / 2 : -fallingSize / 2), 0, 0);



        Destroy(fallingCube, 1);

        StackController.Instance.SpawnStack();

        StackController.Instance.TriggerSpawnEvent(transform.localPosition.x);
    }
}
