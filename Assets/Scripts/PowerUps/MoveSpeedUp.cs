using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedUp : MonoBehaviour {

    public Player play;
    public AudioClip powerUpSound;
     public string  Name = "Move Up!";
     public float Odds = .3f;
    public AudioSource audio;
    private Renderer rend;

    private void Start()
    { 
        audio = GetComponent<AudioSource>();

        rend = GetComponentInChildren<Renderer>();

        audio.clip = powerUpSound;
        
    }


    private void OnTriggerEnter(Collider other)
    {
        play = other.gameObject.GetComponent<Player>();
        if(play.playerNumber == 1)
        {
            play.moveSpeed += 1f;
        }
        else
        {
            play.playerTwoMoveSpeed += 1f;
        }
        rend.gameObject.SetActive(false);
        audio.Play();

        Destroy(gameObject, powerUpSound.length);
    }


}
