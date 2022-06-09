using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed;
    protected Animator myAnimator;
    protected Vector2 direction;
    private Rigidbody2D myrigidBody;
    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }
    protected bool isAttacking = false;

    protected Coroutine attackRoutine;

    protected virtual void Start()
    {
        myrigidBody = GetComponent<Rigidbody2D>();

        myAnimator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        myrigidBody.velocity = direction.normalized * speed;
    }

    public void HandleLayers()
    {
        if (IsMoving)
        {
            ActivateLayer("WalkLayer");

            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);

            StopAttack();
        }
        else if (isAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else
        {
            ActivateLayer("IdleLayer");
        }
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);

    }

    public void StopAttack()
    {
        if(attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            isAttacking = false;
            myAnimator.SetBool("attack", isAttacking);
        }
    }

}
