using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

// change OnHealthChanged to flash red
public class PlayerManager : MonoBehaviour
{	
	// singleton
	private static PlayerManager _instance;
	public static PlayerManager Instance
	{
		get { return _instance; }
	}

	public static GameObject player;

	// health
	public Transform pfHealthBar;	
	public HealthSystem pHealth;
	public float positionOffset;
	
	// ultimate
	public int maxUlt = 100;
	public UltimateBar ultBar;
	public float invulnAfterHit;

	// animator
	public Animator anim;
	public bool animFinished;

	// for damage
	SpriteRenderer sr;
	public LayerMask enemyLayerMask;
	public int Attack;

	// player properties
	[SerializeField] private int maxHealth;
	public float range;
	public float cooldownRate;
	public RotateAroundPlayer playerAim;
	public float JumpDistance;
	public float JumpSpeed;
	public float MDD;
	public float AttackMoveDistance;

	// audio
	public AudioSource audioSource;
	public AudioClip slash1;
	public AudioClip hurt1;

	// death screen
	PostProcessVolume m_Volume;
	Vignette m_Vignette;
	ColorGrading m_ColorGrading;
	float w;
	public GameObject ppv;

	// etc
	private int wrongWayCount = 0;
	public GameObject wrongWayPanel;
	private bool canMove = true;
	public bool CanMove
	{
		get { return canMove;} 
		set { canMove = value; }
	}

	// delete if old
	private Transform hitbox;
	private BoxCollider2D boxCollider;

	private Vector3 lastMove;
	public Vector3 LastMove
	{
		get { return lastMove; }
		set { lastMove = value; }
	}


	private void Start()
	{
		if(player == null)
			player = GameObject.Find("Player");
		
		// health
		pHealth = new HealthSystem(maxHealth, invulnAfterHit);
		var healthBar = GameObject.Find("PlayerHealthBar").GetComponent<HealthBar>();
		healthBar.Setup(pHealth);
		
		// ult amt start of scene
		//ultBar.SetUlt(0);

		// death event
		pHealth.OnHealthChanged += OnDamage;
		anim = transform.GetComponent<Animator>();

		// damage
		sr = GetComponent<SpriteRenderer>();

		// wrong way dialogue box
		//wrongWayPanel = GameObject.Find("WrongWayDialogue");

		// aim
		playerAim = GetComponentInChildren<RotateAroundPlayer>();

		// audio
		audioSource = GetComponent<AudioSource>();

		// delete if old
		boxCollider = GetComponent<BoxCollider2D>();
		hitbox = transform.GetChild(3);
	}
	
	private void Awake()
	{
		// singleton
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}

		// remove post effects
		ppv.SetActive(false);

	}

	void Update()
	{
		var x = Input.GetAxis("Horizontal");
		var y = Input.GetAxis("Vertical");
		if(Mathf.Abs(x) > 0.1f || Mathf.Abs(y) > 0.1f)
		{
			LastMove = new Vector3(x,y,0);
		}
		anim.SetFloat("MoveX", LastMove.x);
		anim.SetFloat("MoveY", LastMove.y);

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.transform.CompareTag("Projectile"))
		{
			Instance.pHealth.Damage(20);
			audioSource.PlayOneShot(hurt1, 0.7f);
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{

		if(col.transform.CompareTag("RockShadow"))
		{
			wrongWayCount++;
			// 24.5f
			transform.position = new Vector3(-16.93f, transform.position.y, transform.position.z);
			if(wrongWayCount > 2)
				wrongWayPanel.gameObject.SetActive(true);

		}
	}

	private void OnDamage(object sender, System.EventArgs e) 
	{
		if(pHealth.GetHealth() <= 0)
		{
			// Death sequence
			anim.SetBool("isDead", true);
		}

		else
		{
			StartCoroutine(FlashRed());
		}
		
		

	}

	private IEnumerator FlashRed()
	{
		var timer = 0.28f;
		sr.color = Color.red;
		yield return new WaitForSeconds(timer);
		sr.color = Color.white;
		yield return new WaitForSeconds(timer);
		sr.color = Color.red;
		yield return new WaitForSeconds(timer);
		sr.color = Color.white;

	}

	private void Death() 
	{
		// triggered after death animation
		anim.enabled = false;
		gameObject.GetComponent<PlayerMove>().enabled = false;
		
		var reticle = transform.Find("Reticle");
		reticle.gameObject.SetActive(false);
		
		ppv.SetActive(true);
		// reset scene after timer
		StartCoroutine("LoadScene");
	}

	IEnumerator LoadScene()
	{
		yield return new WaitForSeconds(4);
		SceneManager.LoadScene("CellChamber", LoadSceneMode.Single);
	}

	// State pattern anim control
	public void FinishAnimation()
	{
		anim.Play("Player Idle", 0);
		animFinished = true;
	}

	public bool AnimFinished()
	{
		
		return animFinished;
	}


}
