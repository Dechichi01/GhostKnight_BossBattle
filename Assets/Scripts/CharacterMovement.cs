using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
	public float maxSpeed = 6.0f;
	public bool facingRight = true;
	public float moveDirection;

	public bool doubleJump = false;
	public float jumpSpeed = 600.0f;
	public bool grounded = false;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;
    public Rigidbody rb;

	public float swordSpeed = 600.0f;
	public Transform swordSpawn;
	public Rigidbody swordPrefab;
    private AudioSource audioSrc;

    public AudioClip attackClip;
    public AudioClip jumpClip;

	Rigidbody clone;

    private Animator anim;

	void Awake()
	{
		groundCheck = GameObject.Find ("GroundCheck").transform;
		swordSpawn = GameObject.Find ("SwordSpawn").transform;
        audioSrc = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0.0f;
        anim = GetComponentInChildren<Animator>();
    }


	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        rb.velocity = new Vector2(moveDirection * maxSpeed, GetComponent<Rigidbody>().velocity.y);
        anim.SetFloat("speed", Mathf.Abs(moveDirection));
        anim.SetFloat("vSpeed", rb.velocity.y);
        anim.SetBool("isGrounded", grounded);

		if(grounded)
			doubleJump = false;

        if ((moveDirection > 0.0f && !facingRight) || (moveDirection < 0.0f && facingRight))
        {
            Flip();
        }
	}

	void Update () 
	{
		moveDirection = Input.GetAxis("Horizontal");

		if((grounded || !doubleJump) && Input.GetButtonDown("Jump"))
		{
			rb.AddForce(new Vector2(0, jumpSpeed));
            AudioSource.PlayClipAtPoint(jumpClip, transform.position, 0.1f);

			if(!doubleJump && !grounded)
				doubleJump = true;
		}

		if(Input.GetButtonDown ("Fire1"))
		{
			Attack();
		}

        FootStepsAudio();
	}

	void Flip()
	{
		facingRight = !facingRight;

		transform.Rotate (Vector3.up, 180.0f, Space.World);
        anim.SetBool("facingRight", facingRight);
    }

	void Attack()
	{
        if (!anim.GetCurrentAnimatorStateInfo(1).IsName("Knight_Attack_F") &&
            !anim.GetCurrentAnimatorStateInfo(1).IsName("Knight_Attack_B"))
        {
            AudioSource.PlayClipAtPoint(attackClip, transform.position, .5f);
            anim.SetTrigger("isAttacking");
        }
	}

    public void FireProjectile()
    {
        clone = Instantiate(swordPrefab, swordSpawn.position, swordSpawn.rotation) as Rigidbody;
        clone.AddForce(swordSpawn.transform.right * swordSpeed);
    }

    void FootStepsAudio()
    {
        if (grounded && moveDirection != 0)
        {
            if (!audioSrc.isPlaying) audioSrc.Play();
        }
        else audioSrc.Stop();
    }
}
