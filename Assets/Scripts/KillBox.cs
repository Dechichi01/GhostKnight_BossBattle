using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour {

    public CharacterHealth charHealth;
    
    void Awake()
    {
        charHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            charHealth.health = 0;
    }
}
