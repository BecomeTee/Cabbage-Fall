using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerMove : MonoBehaviour
{
    /////////
    /*private float usualSpeed;
    private float unusualSpeed;

    private float timeStart = 0;
    private int target = 10;
    private int pass = 0;
    private float score;*/

    public Score a;

    //int complite = 0;
   // public bool qteDone = false;
    /// /////
    //public Timer b;
    ///////
    //Таймер
    /*public float TimeLeft;
    public bool TimerOn = false;*/


    //public bool TimerEnd = false;
    ////////



    [SerializeField] private float Speed = 10f;
    [SerializeField] private float fallSpeed = -15f;
    //[SerializeField] private float fallNitroSpeed = 1f;
    [SerializeField] private float NitroSpeed = 2f;
    [SerializeField] private LayerMask Platform;

    //[SerializeField] public float TimeLeft = Time.deltaTime; 

    private Rigidbody2D rb;
    private BoxCollider2D call;
    ///////
    private new Transform transform;
    private Animator anime;
    private float dirX;
    private int PlayerLayer;
    private int Cnt = 0;
    private int PlatformLayer;
    private bool flip_key = true;
    
    private MoveState state;
    private enum MoveState { idle, walk, fallScoringCarrot, fallMoneuver, fallScoringPotato, fallScoringWall };

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlatformLayer = LayerMask.NameToLayer("Platform");
        PlayerLayer = LayerMask.NameToLayer("Player");
        anime = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        call = GetComponent<BoxCollider2D>();
        a = GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        // dirY = Input.GetAxisRaw("Vertical");
        if (IsManeuvered() && !IsGrounded())
        {
            rb.velocity = new Vector2(dirX * 20f ,fallSpeed + 10f);
        }
        else if (rb.velocity.y < fallSpeed)
        {
            rb.velocity = new Vector2(dirX * Speed, fallSpeed);
            
        }
        else
        {
            rb.velocity = new Vector2(dirX * Speed, rb.velocity.y);
        }


        if (Input.GetKeyDown("s") && IsGrounded())
        {
            Physics2D.IgnoreLayerCollision(PlayerLayer, PlatformLayer, true);
        }

        Cnt++;
        if((Cnt % 1000 == 0))
        {
            fallSpeed--;
        }
        UpdateAnimationState();
    }


    private void UpdateAnimationState()
    {
        

        //бег
        if (dirX > 0f && rb.velocity.y == 0f && IsGrounded())
        {
            state = MoveState.walk;
        }
        else if (dirX < 0f && rb.velocity.y == 0f && IsGrounded())
        {
            state = MoveState.walk;
        }
        else if(rb.velocity.y == 0 && IsGrounded())
        {
            state = MoveState.idle;
        }

        //Направление
        if ((dirX > 0f) && !(flip_key))
        {
            flip((transform.position.x), 0);
            flip_key = true;
        }
        else if ((dirX < 0f) && flip_key)
        {
            flip((transform.position.x), -180);
            flip_key = false;
        }

        //состояние
        anime.SetInteger("State", (int)state);
    }


    private bool IsGrounded()
    {
        //Debug.Log("Земля!");
        return Physics2D.BoxCast(call.bounds.center, call.bounds.size, 0f, Vector2.down, .1f, Platform);
    }

    private bool IsManeuvered()
    {
        return (Input.GetKey(KeyCode.Space));
    }

    private void flip(float pos_x, float direction)
    {
        if (rb.bodyType != RigidbodyType2D.Static)
        {
            //sprite.flipX = false;
            transform.eulerAngles = new Vector3(0, direction, 0);
            transform.position = new Vector3(pos_x, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ( collision.gameObject.CompareTag("Carrot") && (rb.velocity.y < -.1f) && (!IsManeuvered()))
        {
            state = MoveState.fallScoringCarrot;
        }
        else if ( collision.gameObject.CompareTag("Potato") && (rb.velocity.y < -.1f) && (!IsManeuvered()))
        {
            state = MoveState.fallScoringPotato;
        }
        else if ( collision.gameObject.CompareTag("WallPaper") && (rb.velocity.y < -.1f )&& (!IsManeuvered()))
        {
            state = MoveState.fallScoringWall;
        }
        else
        {
            state = MoveState.fallMoneuver;
        }
        anime.SetInteger("State", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("death"))
        {
            Physics2D.IgnoreLayerCollision(PlayerLayer, PlatformLayer, false);
        }
    }
}
