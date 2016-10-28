using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

    private BossController bossCtrl;
    public Material hurtEyeMaterial;
    private GameObject eyeModel;

    public int health = 4;

    public float timer = 0f;
    public float waitTime = 2f;
    private Animator anim;

    public bool bossDead;

    private SceneFadeInOut sceneFader;

    void Awake()
    {
        bossCtrl = FindObjectOfType<BossController>();
        eyeModel = GameObject.FindGameObjectWithTag("EyeModel");
        sceneFader = GameObject.FindGameObjectWithTag("Fader").GetComponent<SceneFadeInOut>();
        anim = GameObject.Find("Boss_Rig").GetComponent<Animator>();
    }

    void Update()
    {
        if (health <= 0)
        {
            if (!bossDead) BossDying();
            else
            {
                BossDead();
                LevelReset();
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Projectile") && health > 0)
        {
            if (health != 1) anim.SetTrigger("wasHit");

            health--;
            Debug.Log("Boss Health: " + health);
            Destroy(col.gameObject);
            bossCtrl.isAttacking = true;
            bossCtrl.DisableEye();

            if (health == 2) eyeModel.GetComponent<SkinnedMeshRenderer>().material = hurtEyeMaterial;
        }
    }

    void BossDying()
    {
        bossDead = true;
        anim.SetBool("isDead", bossDead);
    }

    void BossDead()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_Death"))
            anim.SetBool("isDead", false);
    }

    void LevelReset()
    {
        Debug.Log("AA");
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            sceneFader.EndScene();
        }
    }
}
