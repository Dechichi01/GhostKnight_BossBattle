using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterHealth : MonoBehaviour 
{
	public int health = 3;
	private CharacterMovement characterMovement;
	public float timer;
	public float waitTime = 10.0f;
	public ParticleSystemRenderer aura;
    public bool playerDead = false;
    public GameObject deadModel;
    public GameObject clone;

    private Color auraColor;
    private Animator anim;
    private Renderer[] renderers;
    private SceneFadeInOut sceneFader;

	// Use this for initialization
	void Awake () 
	{
		characterMovement = GetComponent<CharacterMovement>();
		//aura = GetComponent<ParticleSystemRenderer>();

        renderers = GetComponentsInChildren<Renderer>();

        sceneFader = GameObject.FindGameObjectWithTag("Fader").GetComponent<SceneFadeInOut>();
    }
	
	// Update is called once per frame
	void Update () 
	{
		if(health == 3)
		{
			auraColor = new Color(0.0f, 1.0f, 1.0f, 0.05f);
			aura.material.SetColor("_TintColor", auraColor);
            anim = GetComponentInChildren<Animator>();

            foreach (Renderer r in renderers)
                r.material.SetColor("_OutlineColor", auraColor);
        }

		if(health == 2)
		{
			auraColor = new Color(1.0f, 1.0f, 0.0f, 0.05f);
			aura.material.SetColor("_TintColor", auraColor);

            foreach (Renderer r in renderers)
                r.material.SetColor("_OutlineColor", auraColor);
        }

		if(health == 1)
		{
			auraColor = new Color(1.0f, 0.0f, 0.0f, 0.05f);
			aura.material.SetColor("_TintColor", auraColor);

            foreach (Renderer r in renderers)
                r.material.SetColor("_OutlineColor", auraColor);
        }

		if(health <= 0)
		{
            if (!playerDead) PlayerDying();
            else
            {
                PlayerDead();
                LevelReset();
            }
			characterMovement.enabled = false;
		}
	
	}

    void PlayerDying()
    {
        playerDead = true;
        anim.SetBool("isDead", playerDead);
    }

    void PlayerDead()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knight_Death_01_F")) anim.SetBool("isDead", false);

        anim.SetFloat("speed", 0.0f);
        characterMovement.enabled = false;

    }

    public void FallApart()
    {
        clone = Instantiate(deadModel, transform.position, transform.rotation) as GameObject;
        foreach (Renderer r in renderers)
            r.enabled = false;
    }

	void LevelReset()
	{
		timer += Time.deltaTime;

		if(timer >= waitTime)
		{
            sceneFader.EndScene();
		}
	}
}
