using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ViewEntity : MonoBehaviour
{
    private Tilemap tilemap;

    void Start()
    {
        tilemap = this.GetComponentInParent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
            tilemap.color = new Color(1f, 1f, 1f, 0.4f);
    
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
            tilemap.color = new Color(1f, 1f, 1f, 1f);
    }

}
