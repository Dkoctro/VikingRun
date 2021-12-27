using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public GameObject ground;
    public GameObject wolf;

    public Text tutorial;
    public Text Coin;

    AudioSource ad;

    Rigidbody rb;

    Animator animator;

    public bool jump;
    bool die = false;
    bool run = false;

    Vector3 turn_Play;
    Vector3 rotate;

    public float player_Height;
    public float distance_X;
    public float distance_Z;
    public float returnTime = 0f;
    float moveSpeed;
    float jumpForce = 300f;
    float degree = 90f;    
    float side = 0.02f;
    float time = 0;    

    int timeInt = 0;
    int coin = 0;
    int count;
    int y; // 左右
    int w; // 會有BUG的地方    
        
    private void Start()
    {
        ad = GetComponent<AudioSource>();
        ad.Play();
        moveSpeed = 0.1f;
        distance_X = 0;
        distance_Z = 10;        
        transform.position = new Vector3(-19.05f, 1.16f, -15.88f);
        rb = GetComponent<Rigidbody>();
        turn_Play = new Vector3(0f, degree, 0f);        
        rotate = transform.rotation.eulerAngles;
        player_Height = transform.position.y;
        animator = GetComponent<Animator>();
    }
    

    private void OnCollisionEnter(Collision collision)
    {        
        if(collision.collider.tag == "Wall")
        {
            if(y == 7 || y == -7)
            {
                timeInt = (int)time;
                moveSpeed = 0;
                jump = false;
                die = true;
                side = 0;
                tutorial.enabled = true;
                tutorial.text += timeInt;
                Coin.enabled = true;
                Coin.text += coin;
            }
        }
        else if(collision.collider.tag == "Wall2")
        {
            if(y == 0 || y == 10 || y == -10)
            {
                timeInt = (int)time;
                moveSpeed = 0;
                jump = false;
                die = true;
                side = 0;
                tutorial.enabled = true;
                tutorial.text += timeInt;
                Coin.enabled = true;
                Coin.text += coin;
                
            }
        }
        else if (collision.collider.tag == "Fence")
        {
            timeInt = (int)time;
            moveSpeed = 0;
            jump = false;
            die = true;
            side = 0;
            tutorial.enabled = true;
            tutorial.text += timeInt;
            Coin.enabled = true;
            Coin.text += coin;
        }
        else if (collision.collider.tag == "Rock")
        {
            timeInt = (int)time;
            moveSpeed = 0;
            jump = false;
            die = true;
            side = 0;
            tutorial.enabled = true;
            tutorial.text += timeInt;
            Coin.enabled = true;
            Coin.text += coin;
        }
        else if (collision.collider.tag == "Wolf")
        {
            timeInt = (int)time;
            moveSpeed = 0;
            jump = false;
            die = true;
            side = 0;
            tutorial.enabled = true;
            tutorial.text += timeInt;
            Coin.enabled = true;
            Coin.text += coin;
        }
        if (collision.collider.tag == "ground")
        {
            count++;
            jump = true;
        }        
        if (count >= 20)
        {
            count = 0;
            moveSpeed += 0.01f;
        }                
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
            coin++;
    }
    // Update is called once per frame
    void Update()
    {
        if (die)
        {
            returnTime++;
            if (returnTime >= 1000)
                SceneManager.LoadScene(1);
        }
        time += Time.deltaTime;
        wolf.transform.rotation = transform.rotation;        
        if (rb.velocity.y < -10 && !die)
        {
            timeInt = (int)time;
            moveSpeed = 0;
            jump = false;
            die = true;
            side = 0;
            tutorial.enabled = true;
            tutorial.text += timeInt;
            Coin.enabled = true;
            Coin.text += coin;
        }
        if (Input.GetKeyDown(KeyCode.Space) && jump && transform.position.y <= player_Height + 0.05 && !die)
        {
            jump = false;
            rb.AddForce(0, jumpForce, 0);
        }
        y = (int)(transform.rotation.y * 10); // y = 0, 0.7, 1, -0.7
        w = (int)(transform.rotation.w * 10);
        if (die)
        {
            distance_Z = 0;
            distance_X = 0;
        }
        animator.SetBool("Run", run);
        run = false;
        if (Input.GetKey(KeyCode.W) && !die)
        {
            run = true;
            if(y == 0) // +z
            {
                transform.position += new Vector3(0f, 0f, moveSpeed);
                distance_Z += Time.deltaTime * 0.1f;
                if (distance_Z > 10)
                    distance_Z = 10;
            }
            if((y == 7 || y == -7) && y == w) // +x
            {
                transform.position += new Vector3(moveSpeed, 0f, 0f);
                distance_X += Time.deltaTime * 0.1f;
                if (distance_X > 10)
                    distance_X = 10;
            }
            if(y == 10 || y == -10) // -z
            {                
                transform.position += new Vector3(0f, 0f, -moveSpeed);
                distance_Z += Time.deltaTime * 0.1f;
                if (distance_Z > 10)
                    distance_Z = 10;
            }
            if ((y == 7 || y == -7) && y == -w) // -x
            {
                transform.position += new Vector3(-moveSpeed, 0f, 0f);
                distance_X += Time.deltaTime * 0.1f;
                if (distance_X > 10)
                    distance_X = 10;
            }
        } else
        {
            if (y == 0) // +z
            {
                if (rb.velocity.z < moveSpeed && !die)
                {
                    wolf.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance_Z);
                    distance_Z -= Time.deltaTime;
                }
                distance_X = distance_Z;

            }
            else if ((y == 7 || y == -7) && y == w) // +x
            {
                if (rb.velocity.z < moveSpeed && !die)
                {
                    wolf.transform.position = new Vector3(transform.position.x - distance_X, transform.position.y, transform.position.z);
                    distance_X -= Time.deltaTime;
                }
                distance_Z = distance_X;
            }
            else if (y == 10 || y == -10) // -z
            {
                if (rb.velocity.z < moveSpeed && !die)
                {
                    wolf.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance_Z);
                    distance_Z -= Time.deltaTime;
                }
                distance_X = distance_Z;
            }
            else if ((y == 7 || y == -7) && y == -w) // -x
            {
                if (rb.velocity.z < moveSpeed && !die)
                {
                    wolf.transform.position = new Vector3(transform.position.x + distance_X, transform.position.y, transform.position.z);
                    distance_X -= Time.deltaTime;
                }
                distance_Z = distance_X;
            }
        }
        if(Input.GetKeyDown(KeyCode.A) && !die)
        {            
            rotate -= turn_Play;
            transform.rotation = Quaternion.Euler(rotate);     
            
        }
        if (Input.GetKeyDown(KeyCode.D) && !die)
        {            
            rotate += turn_Play;
            transform.rotation = Quaternion.Euler(rotate);            
        }
        if (Input.GetKey(KeyCode.Q) && !die) // 向左偏移
        {
            if (y == 0) // 正前方
            {
                Vector3 position = transform.position;
                position.x -= side;
                transform.position = position;
            }
            if ((y == 7 || y == -7) && y == w) // 右轉90度
            {
                Vector3 position = transform.position;
                position.z += side;
                transform.position = position;
            }
            if (y == 10 || y == -10) // 轉180度
            {
                Vector3 position = transform.position;
                position.x += side;
                transform.position = position;
            }
            if ((y == 7 || y == -7) && y == -w) // 左轉90度
            {
                Vector3 position = transform.position;
                position.z -= side;
                transform.position = position;
            }
        }
        if (Input.GetKey(KeyCode.E) && !die) // 向右偏移
        {
            if (y == 0) // 正前方
            {
                Vector3 position = transform.position;
                position.x += side;
                transform.position = position;
            }
            if ((y == 7 || y == -7) && y == w) // 右轉90度
            {
                Vector3 position = transform.position;
                position.z -= side;
                transform.position = position;
            }
            if (y == 10 || y == -10) // 轉180度
            {
                Vector3 position = transform.position;
                position.x -= side;
                transform.position = position;
            }
            if ((y == 7 || y == -7) && y == -w) // 左轉90度
            {
                Vector3 position = transform.position;
                position.z += side;
                transform.position = position;
            }
        }        
    }
}
