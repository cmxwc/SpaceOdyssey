using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] string name;
    //[SerializeField] Sprite sprite;

    private Vector2 input;
    public event Action OnEncountered;
    public event Action<Collider2D> OnEnterEnemysView;

    private Character character;


    private void Awake()
    {
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        if (!character.IsMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal"); // GetAxisRaw, inputs will be 1 
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0; // to get rid of the diagonal movement

            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, onMoveOver));
            }
        }
        character.HandleUpdate();
        if (Input.GetKeyDown(KeyCode.Return))
            Interact();
    }


    void Interact()
    {
        var facingDir = new Vector3(character.Animator.MoveX, character.Animator.MoveY);
        var interactPos = transform.position + facingDir;

        //Debug.DrawLine(transform.position, interactPos, Color.green, 0.5f);

        // Check if any interactable object in this
        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.i.InteractableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact(transform);
        }
    }

    private void onMoveOver()
    {
        CheckForEncounters();
        CheckIfInEnemysView();
    }

    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, GameLayers.i.GrassLayer) != null)
        {
            if (UnityEngine.Random.Range(1, 101) <= 10)
            {
                Debug.Log("Encountered an enemy");
                character.Animator.IsMoving = false;
                OnEncountered();
            }
        }
    }

    private void CheckIfInEnemysView()
    {
        var collider = Physics2D.OverlapCircle(transform.position, 0.2f, GameLayers.i.FovLayer);
        if (collider != null)
        {
            character.Animator.IsMoving = false;
            OnEnterEnemysView?.Invoke(collider);
        }
    }

    public string Name
    {
        get => "why";
    }
    //public Sprite Sprite
    //{
    //    get => sprite;
    //}

}
