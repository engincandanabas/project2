using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Star_Booster"))
        {
            Debug.Log("Triggered Star_Booster");
        }
        else if (other.gameObject.CompareTag("Diamond"))
        {

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log(collision.gameObject.name);
            StackController.Instance.SpawnStack();
        }
    }
}
