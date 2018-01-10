
using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    //Player parameters
    [Range(1, 2)] //Enables a nifty slider in the editor
    public int playerNumber = 1; //Indicates what player this is: P1 or P2
    public float moveSpeed = 5f;
    public float playerTwoMoveSpeed = 5f;
    public bool canDropBombs = true; //Can the player drop bombs?
    public bool canMove = true; //Can the player move?
    public bool dead = false;
    public bool isCollide = false;
    public int bombs = 2; //Amount of bombs the player has left to drop, gets decreased as the player drops a bomb, increases as an owned bomb explodes
    public int playerTwoBombs = 2;
    public int explosionSize = 2;
    public int playerTwoExplosionSize = 2;

    //Prefabs
    public GameObject bombPrefab1;
    public GameObject bombPrefab2;
    public GlobalStateManager StateMgr;

    //Cached components
    private Rigidbody rigidBody;
    private Transform myTransform;
    private Animator animator;

    // Use this for initialization
    void Start() {
        //Cache the attached components for better performance and less typing
        rigidBody = GetComponent<Rigidbody>();
        myTransform = transform;
        animator = myTransform.FindChild("PlayerModel").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        UpdateMovement();
    }

    private void UpdateMovement() {
        animator.SetBool("Walking", false);

        if (!canMove) { //Return if player can't move
            return;
        }

        //Depending on the player number, use different input for moving
        if (playerNumber == 1) {
            UpdatePlayer1Movement();
        }
        else {
            UpdatePlayer2Movement();
        }
    }

    /// <summary>
    /// Updates Player 1's movement and facing rotation using the WASD keys and drops bombs using Space
    /// </summary>
    private void UpdatePlayer1Movement() {
        if (Input.GetKey(KeyCode.W)) { //Up movement
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
            myTransform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("Walking",true);
        }

        else if (Input.GetKey(KeyCode.A)) { //Left movement
            rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler(0, 270, 0);
            animator.SetBool("Walking", true);
        }

       else if (Input.GetKey(KeyCode.S)) { //Down movement
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
            myTransform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("Walking", true);
        }

       else if (Input.GetKey(KeyCode.D)) { //Right movement
            rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler(0, 90, 0);
            animator.SetBool("Walking", true);
        }

        if (canDropBombs && Input.GetKeyDown(KeyCode.Space)) { //Drop bomb
            DropBombPlayerOne();
        }
    }

    /// <summary>
    /// Updates Player 2's movement and facing rotation using the arrow keys and drops bombs using Enter or Return
    /// </summary>
    private void UpdatePlayer2Movement() {
        if (Input.GetKey(KeyCode.UpArrow)) { //Up movement
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, playerTwoMoveSpeed);
            myTransform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) { //Left movement
            rigidBody.velocity = new Vector3(-playerTwoMoveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler(0, 270, 0);
            animator.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.DownArrow)) { //Down movement
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -playerTwoMoveSpeed);
            myTransform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.RightArrow)) { //Right movement
            rigidBody.velocity = new Vector3(playerTwoMoveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler(0, 90, 0);
            animator.SetBool("Walking", true);
        }

        if (canDropBombs && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))) { //Drop Bomb. For Player 2's bombs, allow both the numeric enter as the return key or players without a numpad will be unable to drop bombs
            DropBombPlayerTwo();
        }
    }

    /// <summary>
    /// Drops a bomb beneath the player
    /// </summary>
    private void DropBombPlayerOne() {

        if (bombPrefab1)
        {  
            if (bombs > 0)
            {
                bombs--;
                GameObject newBomb = Instantiate(bombPrefab1, new Vector3(Mathf.RoundToInt(myTransform.position.x),
                bombPrefab1.transform.position.y, Mathf.RoundToInt(myTransform.position.z)),
                bombPrefab1.transform.rotation) as GameObject;
                newBomb.GetComponent<Bomb>().owner = this;

            }
        }
    }
    private void DropBombPlayerTwo()
    {
        if (bombPrefab2)
        { 
            if(playerTwoBombs > 0)
            {
                playerTwoBombs--;
                GameObject newBomb = Instantiate(bombPrefab2, new Vector3(Mathf.RoundToInt(myTransform.position.x),
                bombPrefab1.transform.position.y, Mathf.RoundToInt(myTransform.position.z)),
                bombPrefab1.transform.rotation) as GameObject;
                newBomb.GetComponent<Bomb>().owner = this;
            }

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion"))
        {
            if (isCollide)
                return;
            isCollide = true;
            Debug.Log("P" + playerNumber + " hit by explosion!");
            dead = true; // 1
            StateMgr.PlayerDied(playerNumber); // 2
            Destroy(gameObject, .3f); // 3 
        }

    }


}
