using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    private WeaponSystem weaponSystem;
    //[SerializeField] private int speed;


    [SerializeField] private int speedDirection; //по умолчанию 25 - скорость смещения (стрейф)
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private GameObject losePanel;
    //[SerializeField] private GameObject musicManager; //проигрыватель музыки на сцене
     

    [SerializeField] private int coins;
    [SerializeField] private Text coinsText;
    [SerializeField] private GameObject managerScore;
    private Animator anim;

    [SerializeField] private float speed;
    private const float MAX_SPEED = 50f;

    private GameObject fireObjectButton;
    private Button fireButton;

    private int lineToMove = 1;
    public float lineDistance = 1.5f;
    public bool CanPlay { get; private set; } = true;
    public bool IsDead { get; private set; } = false;

    private bool isSliding;
    private bool isImmortal;
    public bool onGround = true;
    private bool isFire = false;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private bool isMovingUp = false;

    private GameObject soundObject;
    private VolumeManager volumeManager;
    private bool isLosePanelVisible = false;

    void Start()
    {
        Time.timeScale=1;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        weaponSystem = GetComponent<WeaponSystem>();

        soundObject = GameObject.FindGameObjectWithTag("SoundManager");
        volumeManager =soundObject.GetComponent<VolumeManager>();
        //volumeManager = GetComponent<VolumeManager>();  


        isImmortal = false;

        Application.targetFrameRate = 30;
        coins = PlayerPrefs.GetInt("coinsCurrent");
        coinsText.text = coins.ToString();

        LiveAgain();
    }

   public void IsFire(bool _isFire)
    {
        isFire=_isFire;
    }
    
    public void IsMovingUp(bool _isMovingUp)
    {
        isMovingUp=_isMovingUp;
    }

    private void Update()
    {
       
        if (SwipeController.swipeRight || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
        if (SwipeController.swipeLeft|| Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        if (SwipeController.swipeUp|| Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W)||isMovingUp)
        {
            CheckForJump();
        }

        if (controller.isGrounded && !isSliding)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.Space) || isFire)
        {
            weaponSystem.TryFireWeapon();
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove==0) targetPosition+=Vector3.left*lineDistance;
        else
            if (lineToMove==2) targetPosition+=Vector3.right*lineDistance;

        if (transform.position == targetPosition) return;

        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized*speedDirection*Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude) controller.Move(moveDir);
        else controller.Move(diff);


    }

    public void CheckForJump()
    {
        if (controller.isGrounded);
        Jump();
    }

    public void MoveRight()
    {
        if (lineToMove<2) lineToMove++;
    }

    public void MoveLeft()
    {
        if (lineToMove>0) lineToMove--;
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            dir.y = jumpForce;
            onGround = false;
            anim.SetTrigger("toJump");
            
        }
           
    }

    void FixedUpdate()
    {
        Moving();
        if(controller.isGrounded)
        {
            onGround =true;
        }

    }
    private void Moving()
    {
        dir.z = speed;
        dir.y+=gravity*Time.deltaTime;
        controller.Move(dir * Time.deltaTime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (!CanPlay) return;

        //if (other.gameObject.CompareTag("obstacle"))
        //{
        //    if (isImmortal) Destroy(other.gameObject);
        //    else
        //    {
        //        Dead();
        //    }


        //}

        if (other.gameObject.CompareTag("coin"))
        {
            coins++;
            PlayerPrefs.SetInt("coinsCurrent", coins);
            coinsText.text = coins.ToString();
            

        }

        if (other.gameObject.CompareTag("shield"))
        {
            StopCoroutine(ShieldBonus());
            StartCoroutine(ShieldBonus());
            
        }


        
    }
    private IEnumerator ShieldBonus()
    {
        isImmortal = true;
        yield return new WaitForSeconds(50f);
        isImmortal = false;
        
    }

    public void Dead()
    {
        volumeManager.StopPlayMusic();
        volumeManager.SelectLosePanelMusic();
        volumeManager.StartPlayMusic();
        losePanel.SetActive(true);
        

        int lastDistanceScore = managerScore.GetComponent<Score>().GetDistanceCount();
        PlayerPrefs.SetInt("lastDistanceScore", lastDistanceScore);
        Time.timeScale = 0;
    }
    public void LiveAgain()
    {
        isLosePanelVisible = false;
        volumeManager.StopPlayMusic();
        volumeManager.SelectLevelMusic();
        volumeManager.StartPlayMusic();
        //Debug.Log("Check Play Music Script!!!!");
    }
}
