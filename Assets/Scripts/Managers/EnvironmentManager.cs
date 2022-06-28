using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnvironmentManager : MonoBehaviour
{
    private int rockCount = 0;
    private float despawnDistance = 11;
    private float spawnDistance = 10;

    private List<Rock> rocks = new List<Rock>();

    private static EnvironmentManager instance;
    public static EnvironmentManager Instance
    {
        get { return instance; }
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

    public void EnableWinText() => PrefabAccessor.GetWinText().SetActive(true);
    public void DisableWinText() => PrefabAccessor.GetWinText().SetActive(false);
    public void EnableLoseText() => PrefabAccessor.GetLoseText().SetActive(true);
    public void DisableLoseText() => PrefabAccessor.GetLoseText().SetActive(false);
    public void SetRocks(int rockNum) => rockCount += rockNum;

    public void AddFragments(List<Rock> fragments)
    {
        this.rocks.AddRange(fragments);
    }

    public void DereferenceRock(Rock rock)
    {
        this.rocks.Remove(rock);
    }

    public void ClearRocks()
    {
        for (int i = this.rocks.Count - 1; i >= 0; i--)
        {
            var rock = this.rocks[i];
            rock.ReturnToPool();
            this.rocks.RemoveAt(i);
        }
    }

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        for (int i = this.rocks.Count - 1; i >= 0; i--)
        {
            var rock = this.rocks[i];
            if (Vector2.Distance(this.transform.position, rock.transform.position) > this.despawnDistance)
            {
                rock.ReturnToPool();
                this.rocks.RemoveAt(i);
            }
        }

        while (this.rockCount - this.rocks.Count > 0)
        {
            var randomDirectionFromPlayer = Random.insideUnitCircle.normalized * this.spawnDistance;

            var newRockPosition = new Vector2(
                this.PlayerMovement.transform.position.x + randomDirectionFromPlayer.x,
                this.PlayerMovement.transform.position.y + randomDirectionFromPlayer.y);

            this.rocks.Add(PrefabAccessor.GetRock(TagsAndEnums.RockType.RegularRock, newRockPosition));
        }
    }
}
