using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayaController : MonoBehaviour {

    static Animator anim;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))        //Play Peaceful Animations
        {
            PeacefulAnimation();
        }
        if (Input.GetKeyDown(KeyCode.W))        //Play Activity Animations
        {
            ActivityAnimation();
        }
        if (Input.GetKeyDown(KeyCode.E))        //Play Dance Animations
        {
            DanceAnimation();
        }
        if (Input.GetKeyDown(KeyCode.R))        //Play Comical Animations
        {
            ComicalAnimation();
        }
        if (Input.GetKeyDown(KeyCode.T))        //Default Animation
        {
            anim.Play("breathing_idle");
        }
    }

    public void PeacefulAnimation()            //Yawn, Blow Kiss, Listen to Music, Violin, Golf, Lay Down
    {
        anim.Play("breathing_idle");
        anim.SetTrigger("peaceful");
    }

    public void ActivityAnimation()         //Backflip, Headspin, Guitar, Gun, MMA Kick, Baseball
    {
        anim.Play("breathing_idle");
        anim.SetTrigger("activity");
    }

    public void DanceAnimation()            //Chicken, Salsa, Hokey Pokey, Macarena, Hip Hop, Silly
    {
        anim.Play("breathing_idle");
        anim.SetTrigger("dance");
    }

    public void ComicalAnimation()         //Mutant Run, Clown Walk, Zombie Kick, Zombie Die, Magic Attack, Nervous
    {
        anim.Play("breathing_idle");
        anim.SetTrigger("comical");
    }
}
