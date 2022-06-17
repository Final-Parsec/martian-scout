using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public TagsAndEnums.ProjectileType projectileType;

    private new Collider2D collider;
    private new Rigidbody2D rigidbody;
    private Vector2 origin;
    private int Damage;
    private float Force
    {
        get
        {
            // TODO: Adjust force for future projectile types here.
            return 1000;
        }
    }
    void Awake()
    {
        this.collider = this.GetComponent<Collider2D>();
        this.rigidbody = this.GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        this.collider.enabled = true;
        this.origin = this.transform.position;        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        this.ReturnToPool();
    }

    void ReturnToPool()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -20);
        if (!PrefabAccessor.projectilePool.ContainsKey(projectileType))
        {
            PrefabAccessor.projectilePool.Add(projectileType, new List<Projectile>());
        }
            
        PrefabAccessor.projectilePool[projectileType].Add(this);
        this.rigidbody.velocity = Vector3.zero;
        this.rigidbody.angularVelocity = 0;
        this.collider.enabled = false;
        this.enabled = false;
    }

    public void Shoot()
    {
        this.rigidbody.AddForce(this.transform.up * this.Force);
    }    

    void Update()
    {
        if (Vector2.Distance(this.origin, this.transform.position) > 30)
        {
            this.ReturnToPool();
        }
    }
}
