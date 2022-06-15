using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public List<SpriteRenderer> backgroundList;

    private float halfBackgroundHeight;
    private float halfBackgroundWidth;

    private GameObject player;
    private GameObject Player
    {
        get
        {
            if (this.player == null)
            {
                this.player = GameObject.FindGameObjectWithTag(TagsAndEnums.Player);
            }

            return this.player;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var position = this.Player.transform.position;
        this.backgroundList[0].transform.position = position;

        this.halfBackgroundWidth = this.backgroundList[0].bounds.size.x / 2.0f;
        position.x = this.halfBackgroundWidth;
        this.backgroundList[1].transform.position = position;

        this.halfBackgroundHeight = this.backgroundList[0].bounds.size.y / 2.0f;
        position.y = this.halfBackgroundHeight;
        this.backgroundList[2].transform.position = position;

        position.x = this.backgroundList[0].transform.position.x;
        this.backgroundList[3].transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.backgroundList[0].transform.position.x - this.halfBackgroundWidth <= this.Player.transform.position.x &&
            this.backgroundList[0].transform.position.x + this.halfBackgroundWidth >= this.Player.transform.position.x &&
            this.backgroundList[0].transform.position.y - this.halfBackgroundHeight <= this.Player.transform.position.y &&
            this.backgroundList[0].transform.position.y + this.halfBackgroundHeight >= this.Player.transform.position.y)
        {
            FixBackgroundPositions();
        }
        else
        {
            for (var x = 1; x < this.backgroundList.Count; x++)
            {
                if (this.backgroundList[x].transform.position.x - this.halfBackgroundWidth <= this.Player.transform.position.x &&
                this.backgroundList[x].transform.position.x + this.halfBackgroundWidth >= this.Player.transform.position.x &&
                this.backgroundList[x].transform.position.y - this.halfBackgroundHeight <= this.Player.transform.position.y &&
                this.backgroundList[x].transform.position.y + this.halfBackgroundHeight >= this.Player.transform.position.y)
                {
                    var temp = this.backgroundList[0];
                    this.backgroundList[0] = this.backgroundList[x];
                    this.backgroundList[x] = temp;
                    break;
                }
            }

            FixBackgroundPositions();
        }
    }

    private void FixBackgroundPositions()
    {
        if (this.Player.transform.position.x > this.backgroundList[0].transform.position.x)
        {
            this.backgroundList[1].transform.position = new Vector2(
                this.backgroundList[0].transform.position.x + 2 * this.halfBackgroundWidth,
                this.backgroundList[0].transform.position.y);

            if (this.Player.transform.position.y > this.backgroundList[0].transform.position.y)
            {
                this.backgroundList[2].transform.position = new Vector2(
                    this.backgroundList[0].transform.position.x,
                    this.backgroundList[0].transform.position.y + 2 * this.halfBackgroundHeight);

                this.backgroundList[3].transform.position = new Vector2(
                    this.backgroundList[0].transform.position.x + 2 * this.halfBackgroundWidth,
                    this.backgroundList[0].transform.position.y + 2 * this.halfBackgroundHeight);
            }

            if (this.Player.transform.position.y < this.backgroundList[0].transform.position.y)
            {
                this.backgroundList[2].transform.position = new Vector2(
                    this.backgroundList[0].transform.position.x,
                    this.backgroundList[0].transform.position.y - 2 * this.halfBackgroundHeight);

                this.backgroundList[3].transform.position = new Vector2(
                    this.backgroundList[0].transform.position.x + 2 * this.halfBackgroundWidth,
                    this.backgroundList[0].transform.position.y - 2 * this.halfBackgroundHeight);
            }
        }
        else
        {
            this.backgroundList[1].transform.position = new Vector2(
                this.backgroundList[0].transform.position.x - 2 * this.halfBackgroundWidth,
                this.backgroundList[0].transform.position.y);

            if (this.Player.transform.position.y > this.backgroundList[0].transform.position.y)
            {
                this.backgroundList[2].transform.position = new Vector2(
                    this.backgroundList[0].transform.position.x,
                    this.backgroundList[0].transform.position.y + 2 * this.halfBackgroundHeight);

                this.backgroundList[3].transform.position = new Vector2(
                    this.backgroundList[0].transform.position.x - 2 * this.halfBackgroundWidth,
                    this.backgroundList[0].transform.position.y + 2 * this.halfBackgroundHeight);
            }

            if (this.Player.transform.position.y < this.backgroundList[0].transform.position.y)
            {
                this.backgroundList[2].transform.position = new Vector2(
                    this.backgroundList[0].transform.position.x,
                    this.backgroundList[0].transform.position.y - 2 * this.halfBackgroundHeight);

                this.backgroundList[3].transform.position = new Vector2(
                    this.backgroundList[0].transform.position.x - 2 * this.halfBackgroundWidth,
                    this.backgroundList[0].transform.position.y - 2 * this.halfBackgroundHeight);
            }
        }
    }
}
