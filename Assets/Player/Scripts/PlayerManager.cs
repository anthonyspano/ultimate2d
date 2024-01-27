using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using com.ultimate2d.combat;

// change OnHealthChanged to flash red
public class PlayerManager : MonoBehaviour
{
	public float drawMagnitude;
	
	// singleton
	private static PlayerManager _instance;
	public static PlayerManager Instance
	{
		get { return _instance; }
	}

	public static GameObject player;



	// health
	[Header("Health")]
	[SerializeField] private int maxHealth;
	public Transform pfHealthBar;
	public HealthSystem pHealth;
	public float positionOffset;

	// ultimate
	[Header("Ultimate")]
	public int maxUlt = 100;
	public int ultCost = 20;
	public UltimateBar ultBar;
	public float invulnAfterHit;
	public int ultAddedOnHit;

	// animator
	private Animator anim;
	private bool animFinished;

	// for damage
	[Header("Damage")]
	public LayerMask enemyLayerMask;
	public int Attack;
	private SpriteRenderer sr;

	// player properties
	[Header("Special Properties")]
	public float range;
	public float attackCooldownRate; // for attack - 0.21
	public float attackCooldown;
	public float jumpCooldownRate;
	public float jumpCooldown;
	public float JumpDistance;
	public float JumpSpeed;
	public float MDD;
	public float AttackMoveDistance;
	private RotateAroundPlayer playerAim;
	public float moveSpeed;

	// audio
	[Header("Audio")]
	public AudioClip slash1;
	public AudioClip hurt1;
	private AudioSource audioSource;

	// death screen
	[Header("Death Screen")]
	public GameObject ppv;
	PostProcessVolume m_Volume;
	Vignette m_Vignette;
	ColorGrading m_ColorGrading;
	float w;

	// etc
	[Header("Etc")]
	public GameObject wrongWayPanel;
	private bool canMove = true;
	private int wrongWayCount = 0;
	public float pushBackIntensity;
	public bool isBusy;

	public bool CanMove
	{
		get { return canMove;}
		set { canMove = value; }
	}

	private Transform hitbox;
	private BoxCollider2D boxCollider;

	private Vector3 lastMove;
	public Vector3 LastMove
	{
		get { return lastMove; }
		set { lastMove = value; }
	}

	public enum Direction {left, right, up, down};
	public Direction pFacingDir;


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
			LastMove = new Vector3(x,y,0).normalized;
		}
		anim.SetFloat("MoveX", LastMove.x);
		anim.SetFloat("MoveY", LastMove.y);

		float facingDir = Mathf.Atan2(LastMove.y, LastMove.x) * Mathf.Rad2Deg;
		if(facingDir < 45 && facingDir >= -45) // facing right
			pFacingDir = Direction.right;
		else if(facingDir < 135 && facingDir >= 45) // facing up
			pFacingDir = Direction.up;
		else if(facingDir >= -135 && facingDir < -45) // facing down
			pFacingDir = Direction.down;
		else if(facingDir >= 135 || facingDir < -135) // facing left	
		{
			pFacingDir = Direction.left;
		}

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.transform.CompareTag("Projectile"))
		{
			Instance.pHealth.Damage(20);
			audioSource.PlayOneShot(hurt1, 0.7f);
		}
		if(collision.transform.CompareTag("BigCultist"))
		{
			Instance.pHealth.Damage(70);
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
			Death();
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
		canMove = false;
		isBusy = true;

		var reticle = transform.Find("Reticle");
		reticle.gameObject.SetActive(false);

		ppv.SetActive(true);
		// reset scene after timer
		StartCoroutine("LoadScene");
	}

	IEnumerator LoadScene()
	{
		yield return new WaitForSeconds(2.5f);
		SceneManager.LoadScene("CellChamber", LoadSceneMode.Single);
	}

	// State pattern anim control
	public void FinishAnimation()
	{
		anim.Play("Player Idle", 0);
		anim.SetBool("isAttacking", false);
		anim.SetBool("SecondAttack", false);
		animFinished = true;
		CanMove = true;
	}

	public bool AnimFinished()
	{
		return animFinished;
	}

	public void FinishJumpAnimation()
	{
		anim.SetBool("isJumping", false);
	}

	private bool jumping;
	private bool attacking;

	public void StartJumpCD()
	{
		if(!jumping)
			StartCoroutine("JumpCooldown");
	}

	private IEnumerator JumpCooldown()
	{
		jumping = true;
		yield return new WaitForSeconds(jumpCooldownRate);
		jumpCooldown = 0;
		jumping = false;
	}

	public void StartAttackCD()
	{
		//if(!attacking)
		StartCoroutine("AttackCooldown");
		
	}

	private IEnumerator AttackCooldown()
	{
		attacking = true;
		yield return new WaitForSeconds(attackCooldownRate);
		attackCooldown = 0;
		attacking = false;
	}

	public void PushBack()
	{
		// get position of beam 
		var cursorPos = GetComponentInChildren<PowerManager>().transform.position;
		// translate in opposite direction in relation to player (take the difference)
		transform.Translate((transform.position - cursorPos) * Time.deltaTime * pushBackIntensity);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + LastMove * drawMagnitude);
	}
	


}
