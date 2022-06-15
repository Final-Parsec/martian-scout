using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private AudioSource playerMovementAudio;
    private AudioSource playerShootAudio;
    private int audioType = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.playerMovementAudio = GameObject.FindGameObjectWithTag(TagsAndEnums.PlayerMovement).GetComponent<AudioSource>();
        this.playerCombat = GameObject.FindGameObjectWithTag(TagsAndEnums.Player).GetComponent<PlayerCombat>();
        this.playerMovementAudio.loop = true;
        this.playerShootAudio = this.gameObject.GetComponent<AudioSource>();


        this.playerMovement = GameObject.FindGameObjectWithTag(TagsAndEnums.Player).GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.playerCombat.IsDead)
        {
            if (this.playerMovementAudio.isPlaying)
            {
                this.playerMovementAudio.Stop();
            }

            return;
        }

        if (this.playerMovement.MovingInput && !this.playerMovementAudio.isPlaying && this.audioType != 1)
        {
            this.audioType = 1;
            this.playerMovementAudio.volume = .25f;
            this.playerMovementAudio.Play();
        }
        if (this.playerMovement.BoostInput && !this.playerMovementAudio.isPlaying && this.audioType != 2)
        {
            this.audioType = 2;
            this.playerMovementAudio.volume = .5f;
            this.playerMovementAudio.Play();
        }
        else if (!this.playerMovement.MovingInput && !this.playerMovement.BoostInput)
        {
            this.audioType = 0;
            this.playerMovementAudio.Stop();
        }
    }

    public void ShootSound()
    {
        if (!this.playerShootAudio.isPlaying)
        {
            this.playerShootAudio.Play();
        }
    }
}
