using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public GameObject explosion;
    public int explosionSize ;
    public LayerMask levelMask;
    private bool exploded;
    public Player owner;
    public MoveSpeedUp moveUp;
    public FirePowerUp fireUp;
    public BombPowerUp bombUp;

    // Use this for initialization
    void Start () {
        exploded = false;
        explosionSize = 2;
        owner.GetComponent<Player>();
        Invoke("Explode", 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));

        GetComponent<MeshRenderer>().enabled = false;
        exploded = true;
        transform.FindChild("Collider").gameObject.SetActive(false); 
        Destroy(gameObject, .3f);

        if (owner.playerNumber == 1)
        {
            owner.bombs++;
        }
        else
        {
            owner.playerTwoBombs++;
        }
        
    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        for(int i = 1; i < owner.explosionSize; i++)
        {
            RaycastHit hit;

            Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit, i, levelMask);

            if (!hit.collider)
                Instantiate(explosion, transform.position + (i * direction), explosion.transform.rotation);
            else
            {
                if (hit.collider.tag == "Destructable")
                {
                    
                    var number = Random.Range(1, 100);
                    Debug.Log("Number: " + number.ToString());
                    if(number <= 15)
                    {
                        Instantiate(moveUp, new Vector3(Mathf.RoundToInt(hit.collider.transform.localPosition.x),
                        hit.collider.transform.position.y, Mathf.RoundToInt(hit.collider.transform.position.z)),
                        moveUp.transform.rotation);
                    }
                    else if (number >= 16 && number <= 30)
                    {
                        Instantiate(fireUp, new Vector3(Mathf.RoundToInt(hit.collider.transform.localPosition.x),
                        hit.collider.transform.position.y, Mathf.RoundToInt(hit.collider.transform.position.z)),
                        fireUp.transform.rotation);
                    }
                    else if(number >= 31 && number <= 45)
                    {
                        Instantiate(bombUp, new Vector3(Mathf.RoundToInt(hit.collider.transform.localPosition.x),
                        hit.collider.transform.position.y, Mathf.RoundToInt(hit.collider.transform.position.z)),
                        bombUp.transform.rotation);
                    }
                    Destroy(hit.collider.gameObject, .2f);

                }
                break;
            }
        }


       yield return new WaitForSeconds(.05f); // placeholder for now
    }

    public void OnTriggerEnter(Collider col)
    {
        if(!exploded && col.CompareTag("Explosion"))
        {
            CancelInvoke("Explode"); 
            Explode(); 
        }




    }
}
