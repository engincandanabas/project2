using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public GameObject startExplosionPrefab;
    public GameObject coinExplosionPrefab;
    public GameObject stormExplosionPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Star_Booster"))
        {
            ExplosionEffect(startExplosionPrefab, other);

        }
        else if (other.gameObject.CompareTag("Diamond"))
        {
            ExplosionEffect(stormExplosionPrefab, other);
        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            ExplosionEffect(coinExplosionPrefab,other);
        }
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log(collision.gameObject.name);
            //StackController.Instance.SpawnStack();
            //this.GetComponent<PlayerController>().ChangePlayerPosition(collision.transform.localPosition.x);
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            GameManager.Instance.GameSuccess=true;
        }
    }
    private void ExplosionEffect(GameObject prefab,Collider other)
    {
        var effect = Instantiate(prefab);
        effect.transform.position = other.transform.position;
        Destroy(other.gameObject);
    }
}
