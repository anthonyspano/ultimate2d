using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Interactable
{
    public GameObject book;
    protected override void Trigger()
    {
        book.SetActive(true);

        StartCoroutine("CloseBook");
    }

    IEnumerator CloseBook()
    {
        yield return new WaitForSeconds(1.5f);
        yield return new WaitUntil(() => PlayerInput.Interact());
        book.SetActive(false);
    }
}
