using UnityEngine;
using System.Collections;

public class CliffTrigger : MonoBehaviour {

    public bool playerRight;
    private Animator anim;
	
    void Awake()
    {
        anim = GameObject.Find("Boss_Rig").GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("playerRight", playerRight);
        }
    }
}
