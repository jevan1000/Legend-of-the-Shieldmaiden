using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpawner : MonoBehaviour
{


	public float speed;//Set in Unity
	Transform CurrNode;//Node That the enemy will move to
	int CurrIndex;//Number that CurrNode is listed as in patrolnodes array
	string Enemy_Name;//name of the enemy-to-be-spawned's script
	private Transform target;//Player position
	public List<GameObject> children; //spawned ents
	private Transform[] childss;
	//GenericMelee other = (GenericMelee) go.GetComponent(typeof(GenericMelee));

	// Use this for initialization
	void Start()
	{
		//At start
		SpriteRenderer Sprit = this.gameObject.AddComponent<SpriteRenderer>();
		Sprit.sprite = Resources.Load<Sprite>("Sprite-SpawnerMan");
		Sprit.sortingOrder = 5;
		//transform
		//children.Add (new Transform().gameObject);
		//children.Add (new Transform().gameObject);
		//children.Add (new Transform().gameObject);
		//children.Add (new Transform().gameObject);
	}

	// Update is called once per frame
	void Update()
	{
		//check spawned count
		//if one is missing spawn another
		if (transform.childCount < 4) {
			//create transform
			//set transform parent = transform
		}

	}


}
