using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private float direction = 0f;
    public float jumpspeed = 8f;
    private Rigidbody2D player;

    public Transform groundcheck;
    public float groundcheckradius;
    public LayerMask groundlayer;
    private bool isTouchingGround;

    private Animator playeranimation;

    private Vector3 respawnpoint;
    public GameObject FallDetector;
    public GameObject Obstacle;
    public Text ScoreText;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playeranimation = GetComponent<Animator>();
        respawnpoint = transform.position;
        ScoreText.text = "Crystal : " + Scoring.Totalscore;
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundcheck.position, groundcheckradius, groundlayer);
        direction = Input.GetAxis("Horizontal");
        
        if(direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(0.26638f, 0.26638f);
        }
        else if(direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(-0.26638f, 0.26638f);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        if(Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpspeed);
        }
        playeranimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        playeranimation.SetBool("Onground", isTouchingGround);

        FallDetector.transform.position = new Vector2(transform.position.x, FallDetector.transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FallDetector")
        {
            transform.position = respawnpoint;
            healthBar.Damage(0.1f);
        }
        else if (collision.tag == "Obstacle")
        {
            transform.position = respawnpoint;
            healthBar.Damage(0.1f);
        }
        else if(collision.tag == "Checkpoint")
        {
            respawnpoint = transform.position;
        }
        else if(collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            respawnpoint = transform.position;
        }
        else if (collision.tag == "PreviousLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            respawnpoint = transform.position;
        }
        else if (collision.tag == "Crystal")
        {
            Scoring.Totalscore += 1;
            ScoreText.text = "Crystal : " + Scoring.Totalscore;
            collision.gameObject.SetActive(false);
        }
        if(Health.totalHealth <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
