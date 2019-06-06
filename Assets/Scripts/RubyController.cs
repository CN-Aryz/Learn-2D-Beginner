using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float SPEED = 5.0f;

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    bool isInvincible;
    float invincibleTimer;

    public int health { get { return currentHealth; } }
    int currentHealth;


    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        rigidbody2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //Debug.Log(horizontal);
        float vertical = Input.GetAxis("Vertical");
        //Debug.Log(vertical);
        Vector2 position = transform.position;
        position.x = position.x + SPEED * horizontal*Time.deltaTime;
        position.y = position.y + SPEED * vertical*Time.deltaTime;

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
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        //Mathf.Clamp 保证第一个参数的值不会低于第二个参数 也不会高于第三个参数
        currentHealth = Mathf.Clamp(currentHealth + amount,0,maxHealth);

        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
