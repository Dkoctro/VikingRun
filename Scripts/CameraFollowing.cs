using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public GameObject player;

    Vector3 location;
    Vector3 playerPosition;

    int y;
    int w;    
    private void Start()
    {
        playerPosition = transform.position;
    }
    void LateUpdate()
    {
        y = (int)(player.transform.rotation.y * 10);
        w = (int)(player.transform.rotation.w * 10);        
        if (y == 0)
        {
            if (player.transform.position.y < 0)
                location = transform.position;
            else
                location = player.transform.position + new Vector3(0, 2.5f, -6);            
        }
        else if(y == w && (y == 7 || y == -7))
        {
            if (player.transform.position.y < 0)
                location = transform.position;
            else
                location = player.transform.position + new Vector3(-6, 2.5f, 0);
        }
        else if(y == 10 || y == -10)
        {
            if (player.transform.position.y < 0)
                location = transform.position;
            else
                location = player.transform.position + new Vector3(0, 2.5f, 6);
        }
        else if(y == -w && (y == 7 || y == -7))
        {
            if (player.transform.position.y < 0)
                location = transform.position;
            else
                location = player.transform.position + new Vector3(6, 2.5f, 0);
        }
        transform.position = location;
        transform.rotation = new Quaternion(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z, player.transform.rotation.w);
    }
}
