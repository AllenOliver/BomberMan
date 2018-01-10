using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Range(1, 2)]
    public int player = 1;
    public float walkSpeed;
    public bool canBomb = true;
    public bool movable = true;
    [Header("Number Of Drop-able bombs")]
    private int bombs = 1;

    public GameObject Bomb;

    private Rigidbody rBody;
    private Transform me;
    private Animator anim;
	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody>();
        me = transform;
        anim = me.FindChild("PlayerModel").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateMovement();
	}

    private void UpdateMovement()
    {
        anim.SetBool("Walking", false);

        if(!movable)
        {
            return;
        }
    }

    private void UpdatePlayer1Movement()
    {
        if (Input.GetKey(KeyCode.W))
        { //Up movement
            rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y, walkSpeed);
            me.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.A))
        { //Left movement
            rBody.velocity = new Vector3(-walkSpeed, rBody.velocity.y, rBody.velocity.z);
            me.rotation = Quaternion.Euler(0, 270, 0);
            anim.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.S))
        { //Down movement
            rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y, -walkSpeed);
            me.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.D))
        { //Right movement
            rBody.velocity = new Vector3(walkSpeed, rBody.velocity.y, rBody.velocity.z);
            me.rotation = Quaternion.Euler(0, 90, 0);
            anim.SetBool("Walking", true);
        }

        if (canBomb && Input.GetKeyDown(KeyCode.Space))
        { //Drop bomb
            DropBomb();
        }
    
    }
    private void DropBomb()
    {
        if (Bomb != null)
        {

        }
    }
}
