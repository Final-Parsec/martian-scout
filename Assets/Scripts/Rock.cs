using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Rock : MonoBehaviour
{
    public TagsAndEnums.RockType rockType = TagsAndEnums.RockType.RegularRock;
    public int NumberOfFragments = 0;
    public float DeflectProbability = .1f;
    public int PointValue = 0;

    private new Collider2D collider;
    private new Rigidbody2D rigidbody;    
    private SpriteRenderer spriteRenderer;
    private float Speed = 1f;
    private AudioSource audioSource;

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

    void Awake()
    {
        this.collider = this.GetComponent<Collider2D>();
        this.rigidbody = this.GetComponent<Rigidbody2D>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.audioSource = this.GetComponent<AudioSource>();
    }

    public void ReturnToPool()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -20);
        if (!PrefabAccessor.rockPool.ContainsKey(rockType))
        {
            PrefabAccessor.rockPool.Add(rockType, new List<Rock>());
        }

        PrefabAccessor.rockPool[rockType].Add(this);
        this.rigidbody.velocity = Vector3.zero;
        this.rigidbody.angularVelocity = 0;
        this.collider.enabled = false;
        this.enabled = false;
    }

    void OnEnable()
    {
        this.collider.enabled = true;

        var positionCloseToPlayer = new Vector3(
            Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0, Screen.width), 0)).x,
            Camera.main.ScreenToWorldPoint(new Vector2(0, Random.Range(0, Screen.height))).y,
            0);

        this.rigidbody.velocity = (positionCloseToPlayer - this.transform.position).normalized * this.Speed;

        if (Random.Range(0, 1) > .5f)
        {
            this.rigidbody.angularVelocity = Random.Range(-50, -10);
        }
        else
        {
            this.rigidbody.angularVelocity = Random.Range(10, 50);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(TagsAndEnums.Projectile))
        {
            if (Random.value < this.DeflectProbability)
            {
                return;
            }

            this.audioSource.Play();

            var newRockPosition = this.transform.position;
            var fragments = new List<Rock>();
            for (int i = 0; i < this.NumberOfFragments; i++)
            {
                newRockPosition.x += Random.Range(0, .6f) * (Random.Range(0, 2) * 2 - 1);
                newRockPosition.y += Random.Range(0, .6f) * (Random.Range(0, 2) * 2 - 1);
                fragments.Add(PrefabAccessor.GetRock(TagsAndEnums.RockType.Fragment, newRockPosition));
            }

            EnvironmentManager.Instance.AddFragments(fragments);
            EnvironmentManager.Instance.DereferenceRock(this);
            this.ReturnToPool();
            HUD.Instance.targetScore += this.PointValue;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        this.audioSource.Play();
        if (collision.gameObject.tag == "Player")
        {
            HUD.Instance.SubtractLife();
        }
    }
}
