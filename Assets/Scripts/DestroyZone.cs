using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Obstacle>())
        {
            collision.gameObject.SetActive(false);
        }
    }
}
