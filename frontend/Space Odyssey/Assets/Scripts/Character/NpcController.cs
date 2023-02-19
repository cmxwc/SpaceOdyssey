using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//implement the interactable interface
public class NpcController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    // [SerializeField] List<Sprite> sprites;

    // SpriteAnimator spriteAnimator;

    // private void Start()
    // {
    //     spriteAnimator = new SpriteAnimator(sprites, GetComponent<SpriteRenderer>());
    //     spriteAnimator.Start();
    // }

    // private void Update()
    // {
    //     spriteAnimator.HandleUpdate();
    // }

    public void Interact()   //create implementation of interaction
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
}
