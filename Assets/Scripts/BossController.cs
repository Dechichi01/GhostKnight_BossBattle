using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{

    public bool bossAwake;
    public bool inBattle;
    public bool isAttacking;

    public float idleTimer = 0f;
    public float idleWaitTime = 10f;

    public float attackTimer = 0f;
    public float attackWaitTime = 2f;
    public int attackCount = 0;

    private Animator animator;
    private BossHealth bossHealth;
    private Animator anim;
    private CharacterHealth characterHealth;
    private SphereCollider eyeTrigger;

    public float blinkTimer;
    public float blinkWaitTime = 2f;

    public BoxCollider handTrigger_L, handTrigger_R;

    public AudioClip bossIntroAudio;
    public AudioClip smashAudio;
    // Use this for initialization
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        bossHealth = GetComponentInChildren<BossHealth>();
        anim = GetComponentInChildren<Animator>();
        characterHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterHealth>();
        eyeTrigger = GameObject.Find("EyeTrigger").GetComponent<SphereCollider>();

        anim.SetInteger("attackCount", attackCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossAwake)
        {
            //Play intro animation
            animator.SetBool("bossAwake", true);
            anim.SetInteger("attackCount", attackCount);

            if (inBattle)
            {
                blinkTimer += Time.deltaTime;
                if (!isAttacking && blinkTimer >= blinkWaitTime)
                {
                    if (bossHealth.health > 0) anim.SetTrigger("blink");
                    blinkTimer = 0f;
                }

                if (!isAttacking)
                    idleTimer += Time.deltaTime;
                else
                {
                    idleTimer = 0f;
                    attackTimer += Time.deltaTime;
                    anim.SetBool("attackReady", true);

                    if (attackTimer >= attackWaitTime)
                    {
                        isAttacking = false;
                        anim.SetTrigger("bossAttack");
                        anim.SetBool("attackReady", false);
                        attackTimer = 0f;
                        handTrigger_L.enabled = true;
                        handTrigger_R.enabled = true;
                    }
                }
                if (idleTimer >= idleWaitTime)
                {
                    //Attack
                    Debug.Log("Boos is attacking");
                    isAttacking = true;
                    idleTimer = 0f;
                }
            }
        }
        else
        {
            idleTimer = 0f;
        }
        if (bossHealth.health > 0 && characterHealth.health > 0)
        {
            switch (bossHealth.health)
            {
                case 4:
                    attackCount = 1;
                    attackWaitTime = 1.0f;
                    break;
                case 3:
                    attackCount = 2;
                    attackWaitTime = 1.0f;
                    break;
                case 2:
                    attackCount = 3;
                    attackWaitTime = 0.5f;
                    break;
                case 1:
                    attackCount = 4;
                    attackWaitTime = 0.3f;
                    break;
            }
        }
    }

    public void EnableEye()
    {
        eyeTrigger.enabled = true;
    }

    public void DisableEye()
    {
        eyeTrigger.enabled = false;
    }

    public void UpdateAttackCount()
    {
        attackCount--;
    }

    public void PlayIntroAudio()
    {
        AudioSource.PlayClipAtPoint(bossIntroAudio, transform.position, 1f);
    }

    public void PlaySmashAudio()
    {
        AudioSource.PlayClipAtPoint(smashAudio, transform.position, 1f);
    }
}
