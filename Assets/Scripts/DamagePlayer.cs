using UnityEngine;
using System.Collections;

public class DamagePlayer : MonoBehaviour {

    private CharacterHealth characterHealth;

    void Awake()
    {
        characterHealth = FindObjectOfType<CharacterHealth>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            characterHealth.health--;
            if (characterHealth.health <=0)
            {
                //TODO Play character animation
                print("Player is dead");
            }
        }
    }
}
