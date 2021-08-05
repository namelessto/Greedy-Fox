using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] CharacterController2D controller;
    [SerializeField] GameObject loseMenu;
    [SerializeField] GameObject pauseButton;

    Animator enemyAnimator;
    //------------ Movement --------------
    [SerializeField] float runSpeed = 40f;

    private float horMove = 0f;

    private bool jump = false;
    private bool crouch = false;
    //------------ Movement --------------

    //------------ Damage -----------
    [SerializeField] float fallThreshold = -9.5f;
    [SerializeField] long damageVib = 100;
    [SerializeField] int enemyValue = 1000;

    public static bool enemyHurt = false;

    Vector2 afterFallPos;

    bool playerHurt = false;
    //bool hardmode = IsHardMode.hardMode;
    bool invincible = false;
    //------------ Damage -----------

    //---------- Health ----------
    public static int health = 3;
    [SerializeField] int numOfHearts = 3;

    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    // Start is called before the first frame update
    void Start()
    {
        afterFallPos.x = transform.position.x;
        afterFallPos.y = transform.position.y;
        if (IsHardMode.hardMode == false)
            health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        //------------ Movement --------------
        animator.SetFloat("Speed", Mathf.Abs(horMove));

        if (CrossPlatformInputManager.GetButtonDown("Jump") && CharacterController2D.m_Grounded==true)
        {
            AudioManager.instance.Play("Jump");
            jump = true;
            animator.SetBool("IsJumping", jump);
        }
        if (CrossPlatformInputManager.GetButton("Crouch"))
        {
            crouch = true;
            animator.SetBool("IsCrouching", crouch);
        }
        else if (CrossPlatformInputManager.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        //------------ Movement --------------

        //------------ Damage -----------
        if (playerHurt)
        {
            animator.SetBool("IsHurting", playerHurt);
            Invoke("Hurt", 0.15f);
        }



        if (health == 0)
        {
            AudioManager.instance.Play("Gameover");
            hearts[0].sprite = emptyHeart;
            loseMenu.SetActive(true);
            pauseButton.SetActive(false);
            Time.timeScale = 0f;
            health = 3;
        }

        if (rb.position.y <= fallThreshold)
        {
            rb.position = afterFallPos;
            AudioManager.instance.Play("LifeLost");

            health--;
        }
        //------------ Damage -----------

    }

    void FixedUpdate()
    {
        //------------ Movement --------------
        horMove = CrossPlatformInputManager.GetAxis("Horizontal") * runSpeed;

        controller.Move(horMove * Time.fixedDeltaTime, crouch, jump);
        //jump = false;
        //------------ Movement --------------

        //---------- Health ----------
        if (health > numOfHearts)
            health = numOfHearts;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;


            if (i < numOfHearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;

        }
        //---------- Health ----------
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        //---Damage from enemies---
        if (col.gameObject.tag.Equals("Enemy"))
        {
            foreach (ContactPoint2D point in col.contacts)
            {
                //Cheacking where the collision was
                if (point.normal.y >= 0.75f)
                {
                    enemyHurt = true;
                    AudioManager.instance.Play("HitEnemy");
                    ScoreManager.instance.ChangeScore(enemyValue);
                    Vector2 velocity = rb.velocity;
                    velocity.y = controller.m_JumpForce * 0.013f;
                    rb.velocity = velocity;
                    enemyAnimator = col.gameObject.GetComponent<Animator>();
                    enemyAnimator.SetBool("IsHurt", enemyHurt);
                }
            }

            if (!enemyHurt && !invincible)
            {
                AudioManager.instance.Play("LifeLost");
                invincible = true;
                playerHurt = true;
                health--;
#if UNITY_IPHONE || UNITY_ANDROID
                Vibrator.Vibrate(damageVib);
#endif
                Invoke("resetInvulnerability", 0.7f);
            }

            if (enemyHurt)
                Destroy(col.gameObject, 0.3f);
            enemyHurt = false;
        }

    }

    public void OnLanding()
    {
        jump = false;
        animator.SetBool("IsJumping", jump);
    }

    public void OnCrouching(bool isCrouch)
    {
        animator.SetBool("IsCrouching", isCrouch);
    }
    void resetInvulnerability()
    {
        invincible = false;
    }

    void Hurt()
    {
        playerHurt = false;
        animator.SetBool("IsHurting", playerHurt);
    }
}
