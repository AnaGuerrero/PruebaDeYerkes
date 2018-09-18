using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class dateRegisterScript : MonoBehaviour {

    private Image Background;

    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.Landscape;

        Background = GameObject.Find("Background").GetComponent<Image>();
        var BackTransform = Background.transform as RectTransform;
        BackTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
