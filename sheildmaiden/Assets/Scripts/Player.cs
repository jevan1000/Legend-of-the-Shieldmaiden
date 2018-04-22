﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity {



    public int Player_Health = 8;
    public int Player_Damage = 2;

    SpriteRenderer m_SpriteRenderer;
    Color m_NewColor;
    

    [HideInInspector]
    public bool blink = false; // if true enemy will flash a color

    [HideInInspector]
    public SpriteRenderer spriteR;

    private SpriteRenderer myRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    [HideInInspector]
    public AudioSource[] sounds;
    public AudioSource noise1;
    public AudioSource noise2;

    [HideInInspector]
    public bool hit_sound = false;

    // Use this for initialization
    protected override void Start () 
	{

        //sound = GetComponent<AudioSource>();

        sounds = GetComponents<AudioSource>();
        noise1 = sounds[0];
        noise2 = sounds[1];

        spriteR = gameObject.GetComponent<SpriteRenderer>();
        base.Start ();

        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever
    }
	
	// Update is called once per frame
	protected override void Update () 
	{
        if (Player_Health == 0)
        {
            //Destroy(this.gameObject);
            //SceneManager.LoadScene("mainScene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        GetInput ();

		base.Update ();
	}

	private void GetInput()
	{
		direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.J))
        {
            attackRoutine = StartCoroutine(Attack());
        }

        if (isAttacking == false)
        { 
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                direction += Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                direction += Vector2.down;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                direction += Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                direction += Vector2.right;
            }
        }

    }

    private IEnumerator Attack()
    {
        if (!isAttacking && !IsMoving)
        {
            noise2.Play();

            isAttacking = true;

            animator.SetBool("attack", isAttacking);

            yield return new WaitForSeconds(0.25F);

            //Debug.Log("attack was a success");

            StopAttack();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy_Hit") //Checks for weapon hit
        {
            if (Player_Health != 0)
            {

                //Player_Health -= GameObject.Find("Demo Enemy").GetComponent<SkelyAI>().damage;
                GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth -= GameObject.Find("Demo Enemy").GetComponent<SkelyAI>().damage;
                Player_Health = GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth;

                blink = true;
                if (blink == true)
                {
                    StartCoroutine(blinking());
                }
                hit_sound = true;
                if (hit_sound == true)
                {
                    noise1.Play();
                    hit_sound = false;
                }
            }    

        }
    }


    void whiteSprite()
    {
        myRenderer.material.shader = shaderGUItext;
        myRenderer.color = Color.white;
    }

    void normalSprite()
    {
        myRenderer.material.shader = shaderSpritesDefault;
        myRenderer.color = Color.white;
    }

    public IEnumerator blinking()
    {
        for (int i = 0; i < 3; i++)
        {
            whiteSprite();
            yield return new WaitForSeconds(.05f);
            normalSprite();
            yield return new WaitForSeconds(.05f);
        }
        blink = false;
    }

}
