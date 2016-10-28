using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public BossController bossCtrl;
    public BossHealth bossHealth;
    public AudioSource bossAudio;
    public AudioSource audioSrc;
    public float musicFadeSpeed = 0.1f;

    void Awake()
    {
        bossAudio = transform.GetChild(0).GetComponent<AudioSource>();
        bossCtrl = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
        audioSrc = GetComponent<AudioSource>();
        bossHealth = GameObject.FindGameObjectWithTag("Boss").GetComponentInChildren<BossHealth>();

    }

    void Update()
    {
        MusicFading();
    }

    void MusicFading()
    {
        if (bossCtrl.bossAwake && bossHealth.health > 0)
        {
            audioSrc.volume = Mathf.Lerp(audioSrc.volume, 0.0f, musicFadeSpeed * Time.deltaTime);
            bossAudio.volume = Mathf.Lerp(bossAudio.volume, 0.1f, musicFadeSpeed * Time.deltaTime);
        }
        else
        {
            audioSrc.volume = Mathf.Lerp(audioSrc.volume, 0.1f, musicFadeSpeed * Time.deltaTime);
            bossAudio.volume = Mathf.Lerp(bossAudio.volume, 0.0f, musicFadeSpeed * Time.deltaTime);
        }
    }
}
