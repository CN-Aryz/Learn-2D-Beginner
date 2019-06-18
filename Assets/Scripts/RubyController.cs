using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    //移动速度
    public float SPEED = 5.0f;

    //满血量
    public int maxHealth = 5;
    //无敌时间
    public float timeInvincible = 2.0f;

    //是否无敌
    bool isInvincible;
    //无敌时间倒计时
    float invincibleTimer;

    //当前生命
    public int health { get { return currentHealth; } }
    int currentHealth;

    Vector2 lookDirection = new Vector2(1, 0);

    Rigidbody2D rigidbody2d;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        rigidbody2d = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        currentHealth = maxHealth;



    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //Debug.Log(horizontal);
        float vertical = Input.GetAxis("Vertical");
        //Debug.Log(vertical);

        Vector2 move = new Vector2(horizontal,vertical);

        Debug.Log(move);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y,0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        Debug.Log(lookDirection);
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = transform.position;
        position = position + SPEED * move *Time.deltaTime;

        rigidbody2d.MovePosition(position);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }

    }

    public void ChangeHealth(int amount) {
        if (amount < 0)
        {
            if (isInvincible) {
                return;
            }
            animator.SetTrigger("Hit");
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        //Mathf.Clamp 保证第一个参数的值不会低于第二个参数 也不会高于第三个参数
        currentHealth = Mathf.Clamp(currentHealth + amount,0,maxHealth);

        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
