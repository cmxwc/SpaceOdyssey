using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public LayerMask grassLayer;
    public event Action OnEncountered;
    private CharacterAnimator animator;

    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal"); // GetAxisRaw, inputs will be 1 
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0; // to get rid of the diagonal movement

            if (input != Vector2.zero)
            {
                animator.MoveX = input.x;   // for animation of movement if input is not 0
                animator.MoveY = input.y;

                var targetPos = transform.position;  // current pos of player, plus input
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        animator.IsMoving = isMoving;

        if (Input.GetKeyDown(KeyCode.Return))
            Interact();
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.MoveX, animator.MoveY);
        var interactPos = transform.position + facingDir;

        //Debug.DrawLine(transform.position, interactPos, Color.green, 0.5f);

        // Check if any interactable object in this
        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
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
        CheckForEncounters();
    }

    // Check if there is an object or if it is a interactable layeyer
    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) != null)
        {
            return false;
        }
        return true;
    }

    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null)
        {
            if (UnityEngine.Random.Range(1, 101) <= 10)
            {
                Debug.Log("Encountered wild animal");
                animator.IsMoving = false;
                OnEncountered();
            }


        }

    }

}
