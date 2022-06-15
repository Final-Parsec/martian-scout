using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.playerMovement = GameObject.FindGameObjectWithTag(TagsAndEnums.Player).GetComponent<PlayerMovement>();
        this.animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.animator.SetBool("MovingInput", this.playerMovement.MovingInput);
        this.animator.SetBool("BoostInput", this.playerMovement.BoostInput);
    }
}
