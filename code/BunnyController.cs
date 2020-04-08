using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour
{
	private Animator BunnyAnim;
	public Text ScoreUI;
	public Rigidbody PlayerRb;
	private bool jumpEnabled = true;
	private float speed = 3f;
	private float rotSpeed = 4.80f;
	public int carrotScore = 0;
	AudioSource audioData;
	void Start () 
	{
	    audioData = GetComponent<AudioSource>();
		PlayerRb = GetComponent<Rigidbody>();
    	BunnyAnim = GetComponent<Animator>();	
	}



	void Update()
	{
		BunnyAnim.ResetTrigger("Jump");
	}

	void FixedUpdate () 
	{
		
		if(Input.GetButtonDown("Jump") && jumpEnabled) //set it so we can only jump if we have hit the ground first
		{
			BunnyAnim.SetTrigger("Jump");
			PlayerRb.velocity = new Vector3(0,speed,0) + (transform.forward * speed);
			jumpEnabled = false;
		}		
	
		transform.Rotate(0, rotSpeed * Input.GetAxis("Mouse X"),0, Space.Self);
	}
	void OnCollisionEnter (Collision collision)
	{
		if(collision.gameObject.CompareTag("floor")) //we need to check floors vs walls separately to re-enable jumping
		{
			PlayerRb.velocity = Vector3.zero;
			jumpEnabled = true;
			BunnyAnim.ResetTrigger("Jump");
		}
		else if(collision.gameObject.CompareTag("wall"))
		{
			PlayerRb.velocity = Vector3.zero;
		}
		else if(collision.gameObject.CompareTag("carrot")) //scoring
		{
			Destroy(collision.gameObject);
			carrotScore++;
			ScoreUI.text = "Score: " + carrotScore.ToString();
			audioData.Play(0);
		}
		else //the only other collider that exists is the one that makes the level end, easy to add a tag and change this to an "endlevel" check if scope expands.
		{
		   ScoreUI.text = "Level Complete!  Final Score: " + carrotScore.ToString();
		}
	}


}
