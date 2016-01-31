using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchBookPage : MonoBehaviour {

    private Image image;

    public Sprite page1;
    public Sprite page2;

    void Start ()
    {
        if(image == null) {
            image = GetComponent<Image>();
        }
    }

    public void SwitchPage ()
    {
        if(image.sprite == page1) {
            image.sprite = page2;
        } else {
            image.sprite = page1;
        }
    }
}
