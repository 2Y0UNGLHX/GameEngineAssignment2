using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : SingleMonoBehaviour<GameManager>
{
    public PlayerController m_PlayerController;
    public MonsterFactory m_MonsterFactory;
    public Transform[] monster1Root;
    public Transform[] monster2Root;
    [HideInInspector]
    public int monster1Count = 0;
    [HideInInspector]
    public int monster2Count = 0;

    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject endPanel;
    public GameObject[] lifes;
    public Text scoreUI;
    public bool isGameStart = false;
    private int lifeCount = 5;
    private int scoreValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        monster1Count = monster2Count = 0;

        m_PlayerController.active = false;
        lifeCount = lifes.Length;
        scoreValue = 0;
        scoreUI.text = scoreValue.ToString();
        isGameStart = false;

        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        endPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //  Create Monster By factory
        if (isGameStart)
        {
            if (monster1Count == 0)
            {
                StartCoroutine(SpawnMonster(monster1Root[Random.Range(0, monster1Root.Length)], MonsterType.MT_1));
                monster1Count++;
            }

            if (monster2Count == 0)
            {
                StartCoroutine(SpawnMonster(monster2Root[Random.Range(0, monster2Root.Length)], MonsterType.MT_2));
                monster2Count++;
            }        
        }
    }

    /// <summary>
    /// Delay 3 second
    /// </summary>
    /// <param name="root"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    IEnumerator SpawnMonster(Transform root,MonsterType type)
    {
        yield return new WaitForSeconds(2);

        var monster = m_MonsterFactory.CreateMonster(type);
        monster.transform.SetParent(root);
        monster.transform.localPosition = Vector3.zero;
        monster.transform.localScale = Vector3.one;
    }

    public void AddScore(int m_score)
    {
        scoreValue += m_score;
        scoreUI.text = scoreValue.ToString();
    }

    public void GameOver()
    {
        isGameStart = false;
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
    }

    public void KillLife()
    {
        lifeCount--;

        if (lifeCount < 0)
        {
            Debug.LogError("Game Over");
            GameOver();
        }

        for (int i = 0; i < lifes.Length; i++)
        {
            if (i >= lifeCount-1)
            {
                lifes[i].SetActive(false);
            }
            else
            {
                lifes[i].SetActive(true);
            }
        }
    }

    #region Button

    /// <summary>
    /// StartGame Button
    /// </summary>
    public void OnStartButton()
    {
        isGameStart = true;
        m_PlayerController.active = true;

        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    /// <summary>
    /// quit Button
    /// </summary>
    public void OnQuitButton()
    {
        Application.Quit();
    }

    /// <summary>
    /// 重新开始游戏按钮
    /// </summary>
    public void OnRestartButton()
    {
        SceneManager.LoadScene(0);
    }

    #endregion
}
