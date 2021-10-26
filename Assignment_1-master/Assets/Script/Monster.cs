using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioUtility;

public class Monster : MonoBehaviour
{
    public MonsterType m_MonsterType = MonsterType.MT_1;
    private SpriteRenderer m_SpriteRenderer;
    private Animator charAnim;
    private BoxCollider2D m_BoxCollider2D;
    public float moveSpeed = 5;
    public float moveTime = 2;
    public int score = 10;
    private float startTime = 0;
    private bool isForward = true;
    private bool canHurt = true;
    private float hurtTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        charAnim = GetComponent<Animator>();
        m_BoxCollider2D = GetComponent<BoxCollider2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canHurt)
        {
            hurtTime += Time.deltaTime;

            if (hurtTime > 2)
            {
                hurtTime = 0;
                canHurt = true;
            }
        }

        if (isForward)
        {
            m_SpriteRenderer.flipX = false;
            transform.Translate(new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime);
        }
        else
        {
            m_SpriteRenderer.flipX = true;
            transform.Translate(new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime);
        }

        startTime += Time.deltaTime;

        if (startTime >= moveTime)
        {
            startTime = 0;
            isForward = !isForward;
        }
    }

    public void Death()
    {
        AudioClip audio = Resources.Load("Death") as AudioClip;
        AudioManager.Instance.PlayEffect(audio,false);

        charAnim.SetBool("IsDeath", true);
        m_BoxCollider2D.enabled = false;
        Destroy(this.gameObject, 1f);

        GameManager.Instance.AddScore(score);

        switch (m_MonsterType)
        {
            case MonsterType.MT_1:
                GameManager.Instance.monster1Count--;
                break;
            case MonsterType.MT_2:
                GameManager.Instance.monster2Count--;
                break;
            case MonsterType.MT_None:
                break;
            default:
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canHurt)
        {
            canHurt = false;
            GameManager.Instance.KillLife();
        }
    }
}
