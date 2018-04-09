using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMelee : MonoBehaviour
{
	public Transform[] patrolnodes;//Number and object set in unity
	public float speed;//Set in Unity
	Transform CurrNode;//Node That the enemy will move to
	int CurrIndex;//Number that CurrNode is listed as in patrolnodes array

	private Transform target;//Player position

	private float attackRadius;
	private int moveType;
	private int dps;

	// Use this for initialization
	void Start()
	{
		//create rigidbody
		Rigidbody2D RBod = this.gameObject.AddComponent<Rigidbody2D>();
		RBod.bodyType = RigidbodyType2D.Kinematic;
		RBod.isKinematic = true;
		//RBod.material = 
		RBod.simulated = true;
		RBod.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		RBod.sleepMode = RigidbodySleepMode2D.StartAwake;
		RBod.interpolation = RigidbodyInterpolation2D.Interpolate;
		RBod.useFullKinematicContacts = true;

		//create circle collider
		CircleCollider2D CCol = this.gameObject.AddComponent<CircleCollider2D>();
		CCol.isTrigger = true;
		CCol.radius = 0.31f;
		//set Sprite
		SpriteRenderer Sprit = this.gameObject.AddComponent<SpriteRenderer>();
		Sprit.sprite = Resources.Load<Sprite>("Sprite-Swoleton");
		Sprit.sortingOrder = 5;
		//At start first node is set
		CurrIndex = 0;
		attackRadius = 0.1f;
		moveType = 2;
		dps = 1;
		speed = 1f;
		if (moveType == 1) {
			CurrNode = patrolnodes [CurrIndex];
		}
		target = null;//Target set to null(not detected)
	}

	// Update is called once per frame
	void Update()
	{
		//Moves enemy towards patrol node at set speed
		switch(moveType){
		case(1):
			if (target == null) {
				transform.position = Vector2.MoveTowards (transform.position, CurrNode.position, speed * Time.deltaTime);

				//Check if Patrol Node reached
				if (Vector2.Distance (transform.position, CurrNode.position) == 0) {
					//Have reached Patrol Node - get next else start over
					if (CurrIndex + 1 < patrolnodes.Length) {
						CurrIndex++;
					} else {
						CurrIndex = 0;
					}
					CurrNode = patrolnodes [CurrIndex];

				}

			}
			if (target != null) { //Will move to player if detected
				transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
			}
			break;
		case(2):
			if (target != null) { //Will move to player if detected
				transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
			}
			break;
		case(3):
			if (target != null) { //Will move to player if detected
				transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
			}
			break;
		default:
			break;
		}

	}

	/* *** PLAYER AND ENMEY MUST HAVE RIGIDBODY + COLLIDER *** */

	// Checks for Enter of 2D circle collider placed on enemy object
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")//Checks for PLayer
		{
			target = other.transform;
			speed = 1.2f;
			print("Detected");
		}
	}

	//function to determine if player is within a certain distance of the enemy, and to attack if so
	private void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player" ) {
			if(Vector2.Distance (transform.position, target.position) <= attackRadius){
				if(Time.deltaTime >= dps){
				//animate hit
				//target.curHealth -= 1;
					}
			}
		}
	}

	// Checks for Player exit of circle collider
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			speed = 0.8f;
			target = null;
		}
	}
}
