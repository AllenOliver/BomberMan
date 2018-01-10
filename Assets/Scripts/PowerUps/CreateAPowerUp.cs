using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAPowerUp : MonoBehaviour {

    private GameObject player;
    private MoveSpeedUp moveSpeedUp;
    private Random rand;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SelectAPowerUp();
        }
    }

    public void SelectAPowerUp()
    {
        var number = Random.Range(1, 100);
        if(number >= 40)
        {
            return;
        }
        else if(number >= 41 && number <= 70)
        {
            //moveSpeedUp.speedUp();
        }
        else 
        {
            return;
        }
    }


}
