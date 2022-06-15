using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    float thrust = 1;
    float turnSpeed = 200;
    public float boostTime;
    [HideInInspector]
    public float maxBoostTime = 15f;


    public bool MovingInput;
    public bool BoostInput;

    private PlayerCombat playerCombat;
    private new Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        this.playerCombat = this.GetComponent<PlayerCombat>();
        this.rigidbody = this.GetComponent<Rigidbody2D>();
        this.boostTime = this.maxBoostTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.playerCombat.IsDead)
        {
            this.MovingInput = false;
            return;
        }

        MoveForwardAndBackward();
        TurnRightAndLeft();
    }

    void MoveForwardAndBackward()
    {
        this.MovingInput = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        this.BoostInput = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) &&
                this.boostTime > 0;

        if (this.BoostInput)
        {
            rigidbody.AddForce(transform.up * this.thrust * 3);

            this.boostTime -= Time.deltaTime;
            if (this.boostTime < 0)
            {
                this.boostTime = 0;
            }
        }
        else if (this.MovingInput)
        {
            rigidbody.AddForce(transform.up * (this.thrust));
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rigidbody.AddForce(transform.up * -this.thrust);
        }
    }

    void TurnRightAndLeft()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * this.turnSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * this.turnSpeed * Time.deltaTime);
        }
    }

}
