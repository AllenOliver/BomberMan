using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePowerUp : MonoBehaviour {

    public Player play;
    public AudioClip powerUpSound;

    public string Name = "Fire Up!";
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
            play.explosionSize += 1;
        }
        else
        {
            play.playerTwoExplosionSize += 1;
        }
        rend.gameObject.SetActive(false);
        audio.Play();
        Destroy(gameObject, powerUpSound.length);
    }
}
