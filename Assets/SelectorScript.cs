using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectorScript : MonoBehaviour {

    private Image Background;
    private Text txtTouch;
    private Button btnTouch;
    private Text txtVision;
    private Button btnVision;
    private Text txtVoice;
    private Button btnVoice;
    private Button btnBack;
    private Button btnNext;
    private int scene;
    private Sprite touchSprite;
    private Sprite visionSprite;
    private Sprite voiceSprite;

    // Use this for initialization
    void Start () {

        Screen.orientation = ScreenOrientation.Landscape;

        Background = GameObject.Find("Background").GetComponent<Image>();
        var BackTransform = Background.transform as RectTransform;
        BackTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        txtTouch = GameObject.Find("TouchText").GetComponent<Text>();
        txtVision = GameObject.Find("VisionText").GetComponent<Text>();
        txtVoice = GameObject.Find("VoiceText").GetComponent<Text>();

        btnTouch = GameObject.Find("TouchBtn").GetComponent<Button>();
        btnVision = GameObject.Find("VisionBtn").GetComponent<Button>();
        btnVoice = GameObject.Find("VoiceBtn").GetComponent<Button>();

        btnBack = GameObject.Find("BackBtn").GetComponent<Button>();
        btnNext = GameObject.Find("NextBtn").GetComponent<Button>();

        btnNext.interactable = false;

        scene = 0;

    }

    public void TouchSelect()
    {
        txtTouch.color = Color.red;
        txtVision.color = Color.black;
        txtVoice.color = Color.black;
        touchSprite = Resources.Load<Sprite>("TactilRojo");
        visionSprite = Resources.Load<Sprite>("vision");
        voiceSprite = Resources.Load<Sprite>("voz");
        btnTouch.image.sprite = touchSprite;
        btnVision.image.sprite = visionSprite;
        btnVoice.image.sprite = voiceSprite;
        scene = 1;
        btnNext.interactable = true;
    }
	
	public void VisionSelect()
    {
        txtTouch.color = Color.black;
        txtVision.color = Color.red;
        txtVoice.color = Color.black;
        touchSprite = Resources.Load<Sprite>("tactil");
        visionSprite = Resources.Load<Sprite>("VisionRojo");
        voiceSprite = Resources.Load<Sprite>("voz");
        btnTouch.image.sprite = touchSprite;
        btnVision.image.sprite = visionSprite;
        btnVoice.image.sprite = voiceSprite;
        scene = 2;
        btnNext.interactable = true;
    }

    public void VoiceSelect()
    {
        txtTouch.color = Color.black;
        txtVision.color = Color.black;
        txtVoice.color = Color.red;
        touchSprite = Resources.Load<Sprite>("tactil");
        visionSprite = Resources.Load<Sprite>("vision");
        voiceSprite = Resources.Load<Sprite>("VozRojo");
        btnTouch.image.sprite = touchSprite;
        btnVision.image.sprite = visionSprite;
        btnVoice.image.sprite = voiceSprite;
        scene = 3;
        btnNext.interactable = true;
    }

    public void BackBtn()
    {

    }

    public void NextBtn()
    {
        switch (scene)
        {
            case 1:
                SceneManager.LoadScene("prueba");
                break;
            case 2:
                SceneManager.LoadScene("vision");
                break;
            case 3:
                break;
        }
    }
}
