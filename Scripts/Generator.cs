using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject ground;
    public GameObject wall;
    public GameObject wall2;
    public GameObject fence;
    public GameObject rock;
    public GameObject Coin;

    Vector3 location;
    Vector3 spawnposition;
    Vector3 triggerPosition;    
    Vector3 distance;
    Vector3 wall_position;    

    public int numbers;
    public int minNumber;
    public int maxNumber;    

    int straightCount = 0;
    int count = 0;
    int mode = 0;
    int y;
    int w;

    float playerPosition_Y;

    private void Start()
    {
        playerPosition_Y = transform.position.y - 1;
        for (int i  = 0; i < numbers; i++)
        {            
            distance = new Vector3(transform.position.x, playerPosition_Y, transform.position.z + i * 8);
            Instantiate(ground, distance, Quaternion.identity);
            wall_position = new Vector3(transform.position.x + 9, playerPosition_Y, transform.position.z + i * 8);
            Instantiate(wall, wall_position, wall.transform.rotation);
            wall_position = new Vector3(transform.position.x - 7, playerPosition_Y, transform.position.z + i * 8);
            Instantiate(wall, wall_position, wall.transform.rotation);
            if (i == numbers-1)
                spawnposition = distance;
            if (i == 0)
                triggerPosition = transform.position;            
        }
    }
    private void Update()
    {
        y = (int)(transform.rotation.y * 10); 
        w = (int)(transform.rotation.w * 10);
        location = transform.position;
        if (y == 0)// +z 前方
        {
            if(location.z - triggerPosition.z > 80)
            {
                location = triggerPosition;
                mode = Random.Range(1, 4);
                if (mode == 1)
                    straightCount++;
                else
                    straightCount = 0;
                if (straightCount == 2)
                    mode = Random.Range(2, 4);
                numbers = Random.Range(minNumber, maxNumber);
                Generate(mode, numbers);
            }
        }
        if ((y == 7 || y == -7) && y == w)//+x 右轉90
        {
            if(location.x - triggerPosition.x> 80)
            {
                location = triggerPosition;
                mode = Random.Range(1, 4);
                if (mode == 1)
                    straightCount++;
                else
                    straightCount = 0;
                if (straightCount == 2)
                    mode = Random.Range(2, 4);
                numbers = Random.Range(minNumber, maxNumber);
                Generate(mode, numbers);
            }
        }
        if (y == 10 || y == -10)//-z 轉180
        {
            if(triggerPosition.z - location.z > 80)
            {
                location = triggerPosition;
                mode = Random.Range(1, 4);
                if (mode == 1)
                    straightCount++;
                else
                    straightCount = 0;
                if (straightCount == 2)
                    mode = Random.Range(2, 4);
                numbers = Random.Range(minNumber, maxNumber);
                Generate(mode, numbers);
            }
        }
        if ((y == 7 || y == -7) && y == -w)//-x 左轉90
        {
            if(triggerPosition.x - location.x > 80)
            {
                location = triggerPosition;
                mode = Random.Range(1, 4);
                if (mode == 1)
                    straightCount++;
                else
                    straightCount = 0;
                if (straightCount == 2)
                    mode = Random.Range(2, 4);
                numbers = Random.Range(minNumber, maxNumber);
                Generate(mode, numbers);
            }
        }        
    }    

    private void Generate(int mode, int numbers)
    {
        int hole = Random.Range(2, 40);
        int fenceObstacle = Random.Range(3, 40);
        int rockObstacle = Random.Range(3, 40);
        int coinObstacle = Random.Range(3, 16);
        int coinPosition = Random.Range(2, 5);
        switch (mode)
        {
            case 1: // 直路
                if (y == 0)
                {
                    distance = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z + 8);
                    Instantiate(ground, distance, Quaternion.identity);
                    wall_position = new Vector3(spawnposition.x + 9, playerPosition_Y, spawnposition.z + 8);
                    Instantiate(wall, wall_position, wall.transform.rotation);
                    wall_position = new Vector3(spawnposition.x - 7, playerPosition_Y, spawnposition.z + 8);
                    Instantiate(wall, wall_position, wall.transform.rotation);
                }
                else if (y == w && (y == 7 || y == -7))
                {
                    distance = new Vector3(spawnposition.x + 8, playerPosition_Y, spawnposition.z);
                    Instantiate(ground, distance, Quaternion.identity);
                    wall_position = new Vector3(spawnposition.x + 8, playerPosition_Y, spawnposition.z + 9);
                    Instantiate(wall2, wall_position, wall.transform.rotation);
                    wall_position = new Vector3(spawnposition.x + 8, playerPosition_Y, spawnposition.z - 7);
                    Instantiate(wall2, wall_position, wall.transform.rotation);
                }
                else if (y == 10 || y == -10)
                {
                    distance = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z - 8);
                    Instantiate(ground, distance, Quaternion.identity);
                    wall_position = new Vector3(spawnposition.x - 7, playerPosition_Y, spawnposition.z - 8);
                    Instantiate(wall, wall_position, wall.transform.rotation);
                    wall_position = new Vector3(spawnposition.x + 9, playerPosition_Y, spawnposition.z - 8);
                    Instantiate(wall, wall_position, wall.transform.rotation);
                }
                else if (y == -w && (y == 7 || y == -7))
                {
                    distance = new Vector3(spawnposition.x - 8, playerPosition_Y, spawnposition.z);
                    Instantiate(ground, distance, Quaternion.identity);
                    wall_position = new Vector3(spawnposition.x - 8, playerPosition_Y, spawnposition.z - 7);
                    Instantiate(wall2, wall_position, wall.transform.rotation);
                    wall_position = new Vector3(spawnposition.x - 8, playerPosition_Y, spawnposition.z + 9);
                    Instantiate(wall2, wall_position, wall.transform.rotation);
                }
                spawnposition = distance;
                for (int i = 1; i <= numbers; i++)
                {
                    if (y == 0)
                    {
                        if (i == hole)
                            continue;                        
                        else
                        {
                            if (i == coinObstacle && i != fenceObstacle)
                                Instantiate(Coin, distance + new Vector3(0, 2, 0), Quaternion.Euler(90, 0, 0));
                            distance = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z + i * 8);
                            Instantiate(ground, distance, Quaternion.identity);
                            if (i == fenceObstacle)
                                Instantiate(fence, distance + new Vector3(0, 1, 0), Quaternion.identity);
                            wall_position = new Vector3(spawnposition.x + 9, playerPosition_Y, spawnposition.z + i * 8);
                            Instantiate(wall, wall_position, wall.transform.rotation);
                            wall_position = new Vector3(spawnposition.x - 7, playerPosition_Y, spawnposition.z + i * 8);
                            Instantiate(wall, wall_position, wall.transform.rotation);
                        }
                    }
                    else if (y == w && (y == 7 || y == -7))
                    {
                        if (i == hole)
                            continue;
                        else
                        {
                            if (i == coinObstacle && i != fenceObstacle)
                                Instantiate(Coin, distance + new Vector3(0, 2, 0), Quaternion.Euler(0, 0, 90));
                            distance = new Vector3(spawnposition.x + i * 8, playerPosition_Y, spawnposition.z);
                            Instantiate(ground, distance, Quaternion.identity);
                            wall_position = new Vector3(spawnposition.x + i * 8, playerPosition_Y, spawnposition.z + 9);
                            Instantiate(wall2, wall_position, wall.transform.rotation);
                            wall_position = new Vector3(spawnposition.x + i * 8, playerPosition_Y, spawnposition.z - 7);
                            Instantiate(wall2, wall_position, wall.transform.rotation);
                        }
                    }
                    else if (y == 10 || y == -10)
                    {
                        if (i == hole)
                            continue;
                        else
                        {
                            if (i == coinObstacle && i != fenceObstacle)
                                Instantiate(Coin, distance + new Vector3(0, 2, 0), Quaternion.Euler(90, 0, 0));
                            distance = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z - i * 8);
                            Instantiate(ground, distance, Quaternion.identity);
                            if (i == fenceObstacle)
                                Instantiate(fence, distance + new Vector3(0, 1, 0), Quaternion.identity);
                            wall_position = new Vector3(spawnposition.x - 7, playerPosition_Y, spawnposition.z - i * 8);
                            Instantiate(wall, wall_position, wall.transform.rotation);
                            wall_position = new Vector3(spawnposition.x + 9, playerPosition_Y, spawnposition.z - i * 8);
                            Instantiate(wall, wall_position, wall.transform.rotation);
                        }
                    }
                    else if (y == -w && (y == 7 || y == -7))
                    {
                        if (i == hole)
                            continue;
                        else
                        {
                            if (i == coinObstacle && i != fenceObstacle)
                                Instantiate(Coin, distance + new Vector3(0, 2, 0), Quaternion.Euler(0, 0, 90));
                            distance = new Vector3(spawnposition.x - i * 8, playerPosition_Y, spawnposition.z);
                            Instantiate(ground, distance, Quaternion.identity);
                            wall_position = new Vector3(spawnposition.x - i * 8, playerPosition_Y, spawnposition.z - 7);
                            Instantiate(wall2, wall_position, wall.transform.rotation);
                            wall_position = new Vector3(spawnposition.x - i * 8, playerPosition_Y, spawnposition.z + 9);
                            Instantiate(wall2, wall_position, wall.transform.rotation);
                        }
                    }
                    if (i == numbers)
                        spawnposition = distance;
                    if (i == 1)
                        triggerPosition = transform.position;
                }
                break;
            case 2: // 左轉
                if (y == 0) // 0 degree
                {
                    distance = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z + 8);
                    Instantiate(ground, distance, Quaternion.identity);
                    wall_position = new Vector3(spawnposition.x + 9, playerPosition_Y, spawnposition.z + 8);
                    Instantiate(wall, wall_position, wall.transform.rotation);
                    wall_position = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z + 16);
                    Instantiate(wall2, wall_position, wall.transform.rotation);
                }
                else if (y == w && (y == 7 || y == -7)) // 90 degrees
                {
                    distance = new Vector3(spawnposition.x + 8, playerPosition_Y, spawnposition.z);
                    Instantiate(ground, distance, Quaternion.identity);
                    wall_position = new Vector3(spawnposition.x + 16, playerPosition_Y, spawnposition.z);
                    Instantiate(wall, wall_position, wall.transform.rotation);
                    wall_position = new Vector3(spawnposition.x + 8, playerPosition_Y, spawnposition.z - 7);
                    Instantiate(wall2, wall_position, wall.transform.rotation);
                }
                else if (y == 10 || y == -10) // 180 degrees
                {
                    distance = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z - 8);
                    Instantiate(ground, distance, Quaternion.identity);
                    wall_position = new Vector3(spawnposition.x - 7, playerPosition_Y, spawnposition.z - 8);
                    Instantiate(wall, wall_position, wall.transform.rotation);
                    wall_position = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z - 16);
                    Instantiate(wall2, wall_position, wall.transform.rotation);
                }
                else if (y == -w && (y == 7 || y == -7)) // -90 degrees
                {
                    distance = new Vector3(spawnposition.x - 8, playerPosition_Y, spawnposition.z);
                    Instantiate(ground, distance, Quaternion.identity);
                    wall_position = new Vector3(spawnposition.x - 8, playerPosition_Y, spawnposition.z + 9);
                    Instantiate(wall2, wall_position, wall.transform.rotation);
                    wall_position = new Vector3(spawnposition.x - 16, playerPosition_Y, spawnposition.z);
                    Instantiate(wall, wall_position, wall.transform.rotation);
                }
                spawnposition = distance;
                for (int i = 1; i <= numbers; i++)
                {
                    if (y == 0)
                    {
                        if (i == hole)
                            continue;
                        else
                        {
                            if (i == coinObstacle && i != fenceObstacle)
                                Instantiate(Coin, distance + new Vector3(0, 2, 0), Quaternion.Euler(0, 0, 90));
                            distance = new Vector3(spawnposition.x - i * 8, playerPosition_Y, spawnposition.z);
                            Instantiate(ground, distance, Quaternion.identity);
                            wall_position = new Vector3(spawnposition.x - i * 8, playerPosition_Y, spawnposition.z + 9);
                            Instantiate(wall2, wall_position, wall.transform.rotation);
                            wall_position = new Vector3(spawnposition.x - i * 8, playerPosition_Y, spawnposition.z - 7);
                            Instantiate(wall2, wall_position, wall.transform.rotation);
                        }
                    }
                    else if (y == w && (y == 7 || y == -7))
                    {
                        if (i == hole)
                            continue;
                        else
                        {
                            if (i == coinObstacle && i != fenceObstacle)
                                Instantiate(Coin, distance + new Vector3(0, 2, 0), Quaternion.Euler(90, 0, 0));
                            distance = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z + i * 8);
                            Instantiate(ground, distance, Quaternion.identity);
                            if (i == fenceObstacle)
                                Instantiate(fence, distance + new Vector3(0, 1, 0), Quaternion.identity);
                            wall_position = new Vector3(spawnposition.x - 7, playerPosition_Y, spawnposition.z + i * 8);
                            Instantiate(wall, wall_position, wall.transform.rotation);
                            wall_position = new Vector3(spawnposition.x + 9, playerPosition_Y, spawnposition.z + i * 8);
                            Instantiate(wall, wall_position, wall.transform.rotation);
                        }
                    }
                    else if (y == 10 || y == -10)
                    {
                        if (i == hole)
                            continue;
                        else
                        {
                            if (i == coinObstacle && i != fenceObstacle)
                                Instantiate(Coin, distance + new Vector3(0, 2, 0), Quaternion.Euler(0, 0, 90));
                            distance = new Vector3(spawnposition.x + i * 8, playerPosition_Y, spawnposition.z);
                            Instantiate(ground, distance, Quaternion.identity);
                            wall_position = new Vector3(spawnposition.x + i * 8, playerPosition_Y, spawnposition.z - 7);
                            Instantiate(wall2, wall_position, wall.transform.rotation);
                            wall_position = new Vector3(spawnposition.x + i * 8, playerPosition_Y, spawnposition.z + 9);
                            Instantiate(wall2, wall_position, wall.transform.rotation);
                        }
                    }
                    else if (y == -w && (y == 7 || y == -7))
                    {
                        if (i == hole)
                            continue;
                        else
                        {
                            if (i == coinObstacle && i != fenceObstacle)
                                Instantiate(Coin, distance + new Vector3(0, 2, 0), Quaternion.Euler(90, 0, 0));
                            distance = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z - i * 8);
                            Instantiate(ground, distance, Quaternion.identity);
                            if (i == fenceObstacle)
                                Instantiate(fence, distance + new Vector3(0, 1, 0), Quaternion.identity);
                            wall_position = new Vector3(spawnposition.x + 9, playerPosition_Y, spawnposition.z - i * 8);
                            Instantiate(wall, wall_position, wall.transform.rotation);
                            wall_position = new Vector3(spawnposition.x - 7, playerPosition_Y, spawnposition.z - i * 8);
                            Instantiate(wall, wall_position, wall.transform.rotation);
                        }
                    }
                    if (i == numbers)
                        spawnposition = distance;
                    if (i == 1)
                        triggerPosition = transform.position;

                }
                break;
            case 3: // 右轉
                if (y == 0) // 0 degree
                {
                    distance = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z + 8);
                    Instantiate(ground, distance, Quaternion.identity);
                    wall_position = new Vector3(spawnposition.x - 7, playerPosition_Y, spawnposition.z + 8);
                    Instantiate(wall, wall_position, wall.transform.rotation);
                    wall_position = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z + 16);
                    Instantiate(wall2, wall_position, wall.transform.rotation);
                }
                else if (y == w && (y == 7 || y == -7)) // 90 degrees
                {
                    distance = new Vector3(spawnposition.x + 8, playerPosition_Y, spawnposition.z);
                    Instantiate(ground, distance, Quaternion.identity);
                    wall_position = new Vector3(spawnposition.x + 16, playerPosition_Y, spawnposition.z);
                    Instantiate(wall, wall_position, wall.transform.rotation);
                    wall_position = new Vector3(spawnposition.x + 8, playerPosition_Y, spawnposition.z + 9);
                    Instantiate(wall2, wall_position, wall.transform.rotation);
                }
                else if (y == 10 || y == -10) // 180 degrees
                {
                    distance = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z - 8);
                    Instantiate(ground, distance, Quaternion.identity);
                    wall_position = new Vector3(spawnposition.x + 9, playerPosition_Y, spawnposition.z - 8);
                    Instantiate(wall, wall_position, wall.transform.rotation);
                    wall_position = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z - 16);
                    Instantiate(wall2, wall_position, wall.transform.rotation);
                }
                else if (y == -w && (y == 7 || y == -7)) // -90 degrees
                {
                    distance = new Vector3(spawnposition.x - 8, playerPosition_Y, spawnposition.z);
                    Instantiate(ground, distance, Quaternion.identity);
                    wall_position = new Vector3(spawnposition.x - 8, playerPosition_Y, spawnposition.z - 7);
                    Instantiate(wall2, wall_position, wall.transform.rotation);
                    wall_position = new Vector3(spawnposition.x - 16, playerPosition_Y, spawnposition.z);
                    Instantiate(wall, wall_position, wall.transform.rotation);
                }
                spawnposition = distance;
                for (int i = 1; i <= numbers; i++)
                {
                    if (y == 0)
                    {
                        if (i == hole)
                            continue;
                        else
                        {
                            if (i == coinObstacle && i != fenceObstacle)
                                Instantiate(Coin, distance + new Vector3(0, 2, 0), Quaternion.Euler(0, 0, 90));
                            distance = new Vector3(spawnposition.x + i * 8, playerPosition_Y, spawnposition.z);
                            Instantiate(ground, distance, Quaternion.identity);
                            wall_position = new Vector3(spawnposition.x + i * 8, playerPosition_Y, spawnposition.z + 9);
                            Instantiate(wall2, wall_position, wall.transform.rotation);
                            wall_position = new Vector3(spawnposition.x + i * 8, playerPosition_Y, spawnposition.z - 7);
                            Instantiate(wall2, wall_position, wall.transform.rotation);
                        }
                    }
                    else if (y == w && (y == 7 || y == -7))
                    {
                        if (i == hole)
                            continue;
                        else
                        {
                            if (i == coinObstacle && i != fenceObstacle)
                                Instantiate(Coin, distance + new Vector3(0, 2, 0), Quaternion.Euler(90, 0, 0));
                            distance = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z - i * 8);
                            Instantiate(ground, distance, Quaternion.identity);
                            if (i == fenceObstacle)
                                Instantiate(fence, distance + new Vector3(0, 1, 0), Quaternion.identity);
                            wall_position = new Vector3(spawnposition.x - 7, playerPosition_Y, spawnposition.z - i * 8);
                            Instantiate(wall, wall_position, wall.transform.rotation);
                            wall_position = new Vector3(spawnposition.x + 9, playerPosition_Y, spawnposition.z - i * 8);
                            Instantiate(wall, wall_position, wall.transform.rotation);
                        }
                    }
                    else if (y == 10 || y == -10)
                    {
                        if (i == hole)
                            continue;
                        else
                        {
                             if (i == coinObstacle && i != fenceObstacle)
                                Instantiate(Coin, distance + new Vector3(0, 2, 0), Quaternion.Euler(0, 0, 90));
                            distance = new Vector3(spawnposition.x - i * 8, playerPosition_Y, spawnposition.z);
                            Instantiate(ground, distance, Quaternion.identity);
                            wall_position = new Vector3(spawnposition.x - i * 8, playerPosition_Y, spawnposition.z - 7);
                            Instantiate(wall2, wall_position, wall.transform.rotation);
                            wall_position = new Vector3(spawnposition.x - i * 8, playerPosition_Y, spawnposition.z + 9);
                            Instantiate(wall2, wall_position, wall.transform.rotation);
                        }
                    }
                    else if (y == -w && (y == 7 || y == -7))
                    {
                        if (i == hole)
                            continue;
                        else
                        {
                            if (i == coinObstacle && i != fenceObstacle)
                                Instantiate(Coin, distance + new Vector3(0, 2, 0), Quaternion.Euler(90, 0, 0));
                            distance = new Vector3(spawnposition.x, playerPosition_Y, spawnposition.z + i * 8);
                            Instantiate(ground, distance, Quaternion.identity);
                            if (i == fenceObstacle)
                                Instantiate(fence, distance + new Vector3(0, 1, 0), Quaternion.identity);
                            wall_position = new Vector3(spawnposition.x + 9, playerPosition_Y, spawnposition.z + i * 8);
                            Instantiate(wall, wall_position, wall.transform.rotation);
                            wall_position = new Vector3(spawnposition.x - 7, playerPosition_Y, spawnposition.z + i * 8);
                            Instantiate(wall, wall_position, wall.transform.rotation);
                        }
                    }
                    if (i == numbers)
                        spawnposition = distance;
                    if (i == 1)
                        triggerPosition = transform.position;

                }
                break;
            case 4: // T字形
                break;
        }
    }
}
