using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    public LayerMask solidObjectsLayer;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal"); // GetAxisRaw, inputs will be 1 
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0; // to get rid of the diagonal movement

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);   // for animation of movement if input is not 0
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;  // current pos of player, plus input
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) //find the diff betwn curr and target position
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime); //keep moving until very small diff
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false; //otherwise only moves once
    }

    // Check if there is an object
    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }

}
