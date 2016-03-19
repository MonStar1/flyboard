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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(temp, 2);
    }

    Vector3 temp;

    void FixedUpdate(){
        Vector3 camera = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));


        Debug.Log(camera);
        float line1 = Vector3.Distance(playerRB.position, camera);
        float line2 = Vector3.Distance(playerRB.position, temp);

        temp = new Vector3(playerRB.position.x, 5, camera.z);


        Debug.DrawLine(camera, playerRB.position, Color.red);

        if (Input.GetAxis("Vertical") != 0) 
		{
            playerRB.AddRelativeForce(Vector3.forward * acceleration * Input.GetAxis("Vertical"));
        }

        if (Input.GetAxis("Horizontal") != 0) 
		{
            playerRB.AddRelativeForce(Vector3.right * acceleration * Input.GetAxis("Horizontal"));
            //playerRB.AddTorque(Vector3.down * acceleration);
        }

        float velX = playerRB.velocity.x;
        float velZ = playerRB.velocity.z;
        Vector3 normal = playerRB.velocity.normalized;
        if (Math.Abs(velX) > maxSpeed)
        {
            playerRB.velocity = new Vector3(normal.x * maxSpeed, 0, velZ);
            velX = normal.x * maxSpeed;
        }

        if (Math.Abs(velZ) > maxSpeed)
        {
            playerRB.velocity = new Vector3(velX, 0, normal.z * maxSpeed);
            velZ = normal.z * maxSpeed;
        }

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            stopForce();
        }
        
	}

    private void stopForce()
    {
        //Debug.logger.Log("stop force: " + -playerRB.velocity);
        playerRB.AddForce(-playerRB.velocity);
    }
}