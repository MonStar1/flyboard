using UnityEngine;
using System.Collections;
using System;

public class Movesisarp : MonoBehaviour {
	private GameObject player;
    private Rigidbody playerRB;

	public int acceleration = 300;
    public int maxSpeed = 70;

	void Start () {
        player = (GameObject)this.gameObject;
        playerRB = player.GetComponent<Rigidbody>();
    }

	void Update(){
        Debug.logger.Log("speed: " + playerRB.velocity);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
		{
            if(playerRB.velocity.z < maxSpeed) { 
                playerRB.AddForce(Vector3.forward * acceleration);
            }
            //player.transform.position += Vector3.forward * speed * Time.deltaTime; 
        }

		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
		{
            if (playerRB.velocity.z > -maxSpeed) {
                playerRB.AddForce(Vector3.back * acceleration);
            }
            //player.transform.position += Vector3.back * speed * Time.deltaTime; 
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
		{
            if (playerRB.velocity.x > -maxSpeed)
            {
                playerRB.AddForce(Vector3.left * acceleration);
            }
            //player.transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
		{
            if (playerRB.velocity.x < maxSpeed)
            {
                playerRB.AddForce(Vector3.right * acceleration);
            }
            //player.transform.position += Vector3.right * speed * Time.deltaTime;
        } 

        if(!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            stopForce();
        }
        
	}

    private void stopForce()
    {
        Debug.logger.Log("stop force: " + -playerRB.velocity);
        playerRB.AddForce(-playerRB.velocity);
    }
}