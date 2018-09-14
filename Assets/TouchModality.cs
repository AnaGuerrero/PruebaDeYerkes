using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using UnityEngine.UI;
using System;

public class TouchModality : MonoBehaviour {

    public Transform cuboTex;
    int numero = 0;
    List<GameObject> cubos = new List<GameObject>();
    bool seleccionado;
    public Lean.Touch.LeanTranslate translate;
    int ultimo;
    private string mMessage = "Mensaje de prueba!";
    private bool mYesPressed = false;
    public Texture texture;
    private Image Background;
    private Image image;
    public Sprite sprite;
    public Button cerrarFlyer;
    public Button shape;
    private Button closeInstr;
    private Text txtButton;
    private Text Instructions;
    private Text txtInstr;

    public void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;

        System.Random random = new System.Random();
        int aleatorio = random.Next(1, 4);
        if(aleatorio == 1)
        {
            sprite = Resources.Load<Sprite>("sprite1");
        }
        if (aleatorio == 2)
        {
            sprite = Resources.Load<Sprite>("sprite2");
        }
        if (aleatorio == 3)
        {
            sprite = Resources.Load<Sprite>("sprite3");
        }
        if (aleatorio == 4)
        {
            sprite = Resources.Load<Sprite>("sprite4");
        }
        image = GameObject.Find("Image").GetComponent<Image>();
        image.sprite = sprite;
        image.enabled = true;

        Background = GameObject.Find("Background").GetComponent<Image>();

        cerrarFlyer = GameObject.Find("CerrarImagen").GetComponent<Button>();
        txtButton = GameObject.Find("CerrarImagen").GetComponentInChildren<Text>();
        txtButton.text = "Volver";
        txtButton.alignment = TextAnchor.MiddleCenter;
        txtButton.fontSize = 14;
        cerrarFlyer.onClick.AddListener(ShapeOnClick);

        shape = GameObject.Find("Shape").GetComponent<Button>();
        shape.image.overrideSprite = sprite;

        Instructions = GameObject.Find("txtInstructions").GetComponent<Text>();
        Instructions.text = "Estas van a ser las instrucciones.";
        Instructions.alignment = TextAnchor.MiddleCenter;
        Instructions.fontSize = 14;

        closeInstr = GameObject.Find("CloseInstructions").GetComponent<Button>();
        txtInstr = GameObject.Find("CloseInstructions").GetComponentInChildren<Text>();
        txtInstr.text = "Volver";
        txtInstr.alignment = TextAnchor.MiddleCenter;
        txtInstr.fontSize = 14;
        closeInstr.onClick.AddListener(CloseInstructions);

    }

    public void addComponent()
    {
        Debug.Log("Añadiendo componente");

        UnityEngine.Object prefab = Resources.Load("CuboTextura");
        GameObject cube = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        numero = numero + 1;
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
        string sNumero = (string)converter.ConvertTo(numero, typeof(string));
        cube.name = "Cubo" + sNumero;
        cubos.Add(cube);
        Debug.Log(cube.name);
        if (GetSelect())
        {
            DeleteTranslate();
        }
        touchSelect();
    }

    public List<GameObject> GetList()
    {
        return cubos;
    }

    public void touchSelect()
    {
        ultimo = cubos.Count;

        if (ultimo > 0)
        {
            cubos[ultimo - 1].AddComponent<Lean.Touch.LeanTranslate>();
            seleccionado = true;
        }
        else
        {
            seleccionado = false;
        }
    }

    public bool GetSelect()
    {
        return seleccionado;
    }

    public void DeleteTranslate()
    {
        translate = cubos[ultimo - 1].GetComponent<Lean.Touch.LeanTranslate>();
        Destroy(translate);
    }

    public void DeleteObject()
    {
        if (seleccionado)
        {
            Destroy(cubos[ultimo - 1]);
            cubos.Remove(cubos[ultimo - 1]);
            touchSelect();
        }
    }

    public void FinishTest()
    {

    }

    public void ShowDialog()
    {
#if UNITY_EDITOR
        Debug.Log("No compatible con esta plataforma");
#endif
#if UNITY_ANDROID
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            AndroidJavaObject alertDialogBuilder = new AndroidJavaObject("android/app/AlertDialog$Builder", activity);
            alertDialogBuilder.Call<AndroidJavaObject>("setMessage", mMessage);
            alertDialogBuilder.Call<AndroidJavaObject>("setPositiveButton", "De acuerdo", new PositiveButtonListener(this));
            alertDialogBuilder.Call<AndroidJavaObject>("setCancelable", true);
            AndroidJavaObject dialog = alertDialogBuilder.Call<AndroidJavaObject>("create");
            dialog.Call("show");
        }
        ));
#endif
    }

#if UNITY_ANDROID
    private class PositiveButtonListener : AndroidJavaProxy
    {
        private TouchModality mDialog;
        
        public PositiveButtonListener(TouchModality tm): base("android.content.DialogInterface$OnClickListener")
        {
            mDialog = tm;
        }

        public void onClick(AndroidJavaObject obj, int value)
        {
            mDialog.mYesPressed = true;
        }
    }
#endif

    public void ShowImage()
    {
        var RectTransform = image.transform as RectTransform;
        RectTransform.sizeDelta = new Vector2(300, 300);

        var ButtonTransform = cerrarFlyer.transform as RectTransform;
        ButtonTransform.sizeDelta = new Vector2(100, 50);

        var BackTransform = Background.transform as RectTransform;
        BackTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
    
    public void ShapeOnClick()
    {
        var RectTransform = image.transform as RectTransform;
        RectTransform.sizeDelta = new Vector2(0, 0);

        var ButtonTransform = cerrarFlyer.transform as RectTransform;
        ButtonTransform.sizeDelta = new Vector2(0, 0);

        var BackTransform = Background.transform as RectTransform;
        BackTransform.sizeDelta = new Vector2(0, 0);
    }

    public void GetInstructions()
    {
        var RectTransform = Instructions.transform as RectTransform;
        RectTransform.sizeDelta = new Vector2(Screen.width, 300);

        var BackTransform = Background.transform as RectTransform;
        BackTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        var ButtonTransform = closeInstr.transform as RectTransform;
        ButtonTransform.sizeDelta = new Vector2(100, 50);
    }

    private void CloseInstructions()
    {
        var RectTransform = Instructions.transform as RectTransform;
        RectTransform.sizeDelta = new Vector2(0, 0);

        var BackTransform = Background.transform as RectTransform;
        BackTransform.sizeDelta = new Vector2(0, 0);

        var ButtonTransform = closeInstr.transform as RectTransform;
        ButtonTransform.sizeDelta = new Vector2(0, 0);
    }

    public void Salir()
    {
        ScreenCapture.CaptureScreenshot("screenshot.jpg");
        Application.Quit();
    }
}
