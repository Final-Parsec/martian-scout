using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabAccessor : MonoBehaviour
{
    public List<GameObject> projectilePrefabs;
    public List<GameObject> rockPrefabs;

    public static Dictionary<TagsAndEnums.ProjectileType, List<Projectile>> projectilePool = new Dictionary<TagsAndEnums.ProjectileType, List<Projectile>>();
    public static Dictionary<TagsAndEnums.RockType, List<Rock>> rockPool = new Dictionary<TagsAndEnums.RockType, List<Rock>>();
    public static PrefabAccessor prefabAccessor;

    void Start()
    {
        prefabAccessor = this;
    }

    public static Projectile GetProjectile(TagsAndEnums.ProjectileType projectileType, Transform spawn)
    {
        Projectile projectile = null;
        if (projectilePool.ContainsKey(projectileType) &&
            projectilePool[projectileType].Count != 0)
        {
            projectile = projectilePool[projectileType][0];
            projectilePool[projectileType].RemoveAt(0);
            projectile.transform.position = spawn.position;
            projectile.transform.rotation = spawn.rotation;
        }

        if (projectile == null)
        {
            projectile = (Instantiate(
                prefabAccessor.projectilePrefabs[(int)projectileType],
                spawn.position,
                spawn.rotation) as GameObject).GetComponent<Projectile>();
        }

        projectile.enabled = true;
        return projectile;
    }

    public static Rock GetRock(TagsAndEnums.RockType rockType, Vector2 spawnPosition)
    {
        Rock rock = null;
        if (rockPool.ContainsKey(rockType) &&
            rockPool[rockType].Count != 0)
        {
            rock = rockPool[rockType][0];
            rockPool[rockType].RemoveAt(0);
            rock.transform.position = spawnPosition;
        }

        if (rock == null)
        {
            rock = (Instantiate(
                prefabAccessor.rockPrefabs[(int)rockType],
                spawnPosition,
                Quaternion.Euler(Vector3.zero)) as GameObject).GetComponent<Rock>();
        }

        rock.enabled = true;
        return rock;
    }
}
