using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    
    public GameObject lifePrefab;
    public int targetScore;

    private int lives = 3;
    private int displayScore = 0;
    private List<GameObject> lifeDisplay = new List<GameObject>();
    private int lifePadding = 5;
    private Vector2 lifeStartPosition = new Vector2(15, -60);
    public GameObject winText;
    
    private float boostBarMaxWidth;

    private Text scoreDisplay;
    private Text ScoreDisplay
    {

        get
        {
            if (this.scoreDisplay == null)
            {
                this.scoreDisplay = GameObject.FindGameObjectWithTag(TagsAndEnums.ScoreDisplay).GetComponent<Text>();
            }

            return this.scoreDisplay;
        }
    }

    private PlayerCombat playerCombat;
    private PlayerCombat PlayerCombat
    {
        get
        {
            if (this.playerCombat == null)
            {
                this.playerCombat = GameObject.FindGameObjectWithTag(TagsAndEnums.Player).GetComponent<PlayerCombat>();
            }

            return this.playerCombat;
        }
    }

    private PlayerMovement playerMovement;
    private PlayerMovement PlayerMovement
    {
        get
        {
            if (this.playerMovement == null)
            {
                this.playerMovement = GameObject.FindGameObjectWithTag(TagsAndEnums.Player).GetComponent<PlayerMovement>();
            }

            return this.playerMovement;
        }
    }

    private RectTransform boostBar;
    private RectTransform BoostBar
    {
        get
        {
            if (this.boostBar == null)
            {
                this.boostBar = GameObject.FindGameObjectWithTag(TagsAndEnums.BoostBar).GetComponent<RectTransform>();
                this.boostBarMaxWidth = this.boostBar.rect.width;
            }

            return this.boostBar;
        }
    }

    public static HUD Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < this.lives; x++)
        {
            this.AddLife();
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        this.BoostBar.sizeDelta = new Vector2(
                (this.PlayerMovement.boostTime / this.PlayerMovement.maxBoostTime) * this.boostBarMaxWidth
                ,this.BoostBar.rect.height
            );

        if (this.targetScore > this.displayScore)
        {
            this.displayScore += 5;
            if (this.displayScore > this.targetScore)
            {
                this.displayScore = this.targetScore;
            }

            this.ScoreDisplay.text = String.Format("{0:0000000}", this.displayScore);
        }
    }

    public void AddLife()
    {
        var life = GameObject.Instantiate(this.lifePrefab);
        life.transform.SetParent(this.transform, false);

        var rect = life.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(this.lifeStartPosition.x + (this.lifeDisplay.Count * (rect.rect.height + this.lifePadding)), this.lifeStartPosition.y);

        this.lifeDisplay.Add(life);
    }

    public void SubtractLife()
    {
        if (this.lives == 0)
        {
            this.PlayerCombat.Kill(false);
        }
        else
        {
            Destroy(this.lifeDisplay[--this.lives].gameObject);
            this.PlayerCombat.Kill(true);
        }        
    }
}
