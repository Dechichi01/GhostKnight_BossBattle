using UnityEngine;
using System.Collections;

public class EventHandler : MonoBehaviour {

    private CharacterMovement characterMov;
    private CharacterHealth characterHealth;
    private BossController bossCtrl;
    private Animator anim;

    void Awake()
    {
        characterMov = FindObjectOfType<CharacterMovement>();
        characterHealth = FindObjectOfType<CharacterHealth>();
        bossCtrl = FindObjectOfType<BossController>();
    }

	void EnableBossBattle()
    {
        characterMov.enabled = true;
        bossCtrl.inBattle = true;
    }

    void UpdateBossAttack()
    {
        bossCtrl.UpdateAttackCount();
    }

    void CallFireProjectile()
    {
        characterMov.FireProjectile();
    }

    void CallFallApart()
    {
        characterHealth.FallApart();
    }

    void CallEnableEye()
    {
        bossCtrl.EnableEye();
    }

    void CallDisableEye()
    {
        bossCtrl.DisableEye();
    }

    void CallPlayIntroAudio()
    {
        bossCtrl.PlayIntroAudio();
    }

    void CallSmashAudio()
    {
        bossCtrl.PlaySmashAudio();
    }
}
