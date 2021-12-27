using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWall : MonoBehaviour
{
    GameObject player;

    int y;
    int w;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        y = (int)(player.transform.rotation.y * 10); // y = 0, 0.7, 1, -0.7
        w = (int)(player.transform.rotation.w * 10);
        if (y == 0)// +z
        {
            if (transform.position.z + 24 < player.transform.position.z)
                Destroy(gameObject);
        }
        if ((y == 7 || y == -7) && y == w)//+x
        {
            if (transform.position.x + 24 < player.transform.position.x)
                Destroy(gameObject);
        }
        if (y == 10 || y == -10)//-z
        {
            if (transform.position.z - 24 > player.transform.position.z)
                Destroy(gameObject);
        }
        if ((y == 7 || y == -7) && y == -w)//-x
        {
            if (transform.position.x - 24 > player.transform.position.x)
                Destroy(gameObject);
        }
    }
}
