using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nameRegisterScript : MonoBehaviour {

    private Image Background;
    private Button btnNext;

    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.Landscape;

        Background = GameObject.Find("Background").GetComponent<Image>();
        var BackTransform = Background.transform as RectTransform;
        BackTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        btnNext = GameObject.Find("NextBtn").GetComponent<Button>();
        btnNext.interactable = false;
    }

    public void nextBtn()
    {
        SceneManager.LoadScene("dateRegister");
    }

}
