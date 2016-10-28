using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {

    public float emitTime = 0f;
    public float emitWaitTime;
    public int minEmitTime = 1;
    public int maxEmitTime = 5;

    private ParticleSystem rockPS;

    void Awake()
    {
        emitWaitTime = Random.Range(minEmitTime, maxEmitTime);
        rockPS = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        emitTime += Time.deltaTime;
        if (emitTime >= emitWaitTime)
            EmitParticles();
    }

    void EmitParticles()
    {
        rockPS.Play();
        emitTime = 0f;
        emitWaitTime = Random.Range(minEmitTime, maxEmitTime);
    }
}
