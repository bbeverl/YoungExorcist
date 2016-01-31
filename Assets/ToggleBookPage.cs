using UnityEngine;
using System.Collections;

public class ToggleBookPage : MonoBehaviour {

    public GameObject BookPage;

    public void SwitchActive ()
    {
        if(BookPage != null) {
            BookPage.SetActive (!BookPage.activeSelf);
        }
    }
}
