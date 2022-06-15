using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAudioManager))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerCombat : MonoBehaviour
{
    public TagsAndEnums.PlayerWeapon playerWeapon = TagsAndEnums.PlayerWeapon.StarterShooter;
    public Transform[] muzzlePoints;
    public bool IsDead { get; private set; }

    private PlayerAudioManager playerAudioManager;
    private new PolygonCollider2D collider;
    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private float lastShootTime = 0;
    private float respawnTimer = 0;
    private float respawnWaitTime = 0;
    private float shootSpeed
    {
        get
        {
            // TODO: Adjust rate of fire for future weapon types here.
            return .2f;
        }
    }
    private TagsAndEnums.ProjectileType projectileType
    {
        get
        {
            // TODO: Adjust project type for future weapon types here.
            return TagsAndEnums.ProjectileType.BasicBullet;
        }
    }

    public void Kill(bool respawn)
    {
        this.rigidbody.velocity = Vector2.zero;

        this.collider.enabled = false;
        this.IsDead = true;
        this.respawnTimer = 0;
        this.respawnWaitTime = respawn ? 3 : float.MaxValue;
    }

    private void Start()
    {
        this.collider = this.GetComponent<PolygonCollider2D>();
        this.playerAudioManager = this.GetComponent<PlayerAudioManager>();
        this.rigidbody = this.GetComponent<Rigidbody2D>();
        this.spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (IsDead)
        {
            this.respawnTimer += Time.deltaTime;
            if (this.respawnTimer > this.respawnWaitTime)
            {
                this.rigidbody.velocity = Vector3.zero;
                this.respawnTimer = 0;
                this.spriteRenderer.color = Color.white;
                this.IsDead = false;
                this.collider.enabled = true;
                return;
            }

            if ((int)(this.respawnTimer * 3) % 2 == 0)
            {
                this.spriteRenderer.color = Color.red;
            }
            else
            {
                this.spriteRenderer.color = Color.white;
            }

            return;
        }

        if ((Input.GetMouseButton(0) || Input.GetKey("space")) &&
            Time.time > lastShootTime + shootSpeed)
        {
            this.lastShootTime = Time.time;

            List<Projectile> projectiles = new List<Projectile>();
            projectiles.Add(PrefabAccessor.GetProjectile(projectileType, muzzlePoints[0]));
            foreach (var projectile in projectiles)
            {
                projectile.Shoot();
                this.playerAudioManager.ShootSound();
            }
        }
    }
}
