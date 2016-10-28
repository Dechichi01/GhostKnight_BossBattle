using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

    private BossController bossCtrl;
    private CharacterMovement characterCtrl;
    private SmoothFollow smoothFollow;
    Animator anim;

    void Awake()
    {
        bossCtrl = FindObjectOfType<BossController>();
        characterCtrl = FindObjectOfType<CharacterMovement>();
        smoothFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    }
	void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            //Change the camera behavior
            //Wake up the boss
            bossCtrl.bossAwake = true;
            //Disable character movement
            characterCtrl.enabled = false;
            anim.SetFloat("speed", 0.0f);

            smoothFollow.bossCameraActive = true;

            GetComponent<BoxCollider>().isTrigger = false;
            col.GetComponent<AudioSource>().Stop();
        }
    }
}
