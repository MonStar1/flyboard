using UnityEngine;
using System.Collections;
using System;

public class Movesisarp : MonoBehaviour {
	private GameObject player;
    private Rigidbody playerRB;
    private Camera mainCamera;
    private Ray cameraRay;

    private Vector3 hitPoint;

	public int acceleration = 300;
    public int maxSpeed = 70;

	void Start () {
        player = (GameObject)this.gameObject;
        playerRB = player.GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hitPoint, 2);
        Debug.Log(cameraRay.direction * 10);
    }

    void FixedUpdate(){
        cameraRay  = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Physics.Raycast(cameraRay, out hit);
        if(hit.collider != null)
        {
            hitPoint = hit.point;
        }

        Vector3 relativePos = hitPoint - playerRB.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        playerRB.rotation = rotation;


        if (Input.GetAxis("Vertical") == 1) 
		{
            playerRB.AddRelativeForce(Vector3.forward * acceleration * Input.GetAxis("Vertical"));
        } else if (Input.GetAxis("Vertical") == -1)
        {
            playerRB.AddForce(Vector3.forward * acceleration * Input.GetAxis("Vertical"));
        }

        if (Input.GetAxis("Horizontal") != 0) 
		{
            playerRB.AddForce(Vector3.right * acceleration * Input.GetAxis("Horizontal"));
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

        MoveCamera();    
	}

    private void MoveCamera()
    {
        mainCamera.transform.position = new Vector3(player.transform.position.x, mainCamera.transform.position.y, player.transform.position.z);
    }

    private void stopForce()
    {
        //Debug.logger.Log("stop force: " + -playerRB.velocity);
        playerRB.AddForce(-playerRB.velocity);
    }
}