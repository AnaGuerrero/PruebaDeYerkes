using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using UnityEngine.UI;
using System;

public class TouchModality : MonoBehaviour
{

    public Transform cuboTex;
    int numero = 0;
    List<GameObject> cubos = new List<GameObject>();
    bool seleccionado;
    public Lean.Touch.LeanTranslate translate;
    int ultimo;
    private string mMessage = "Mensaje de prueba!";
    private bool mYesPressed = false;
    private Image Background;
    private Image image;
    public Sprite sprite;
    public Button cerrarFlyer;
    public Button shape;
    public Button btnChangeIzq;
    public Button btnChangeDer;
    private Button closeInstr;
    private Text txtButton;
    private Text Instructions;
    private Text txtInstr;
    UnityEngine.Object prefab;
    UnityEngine.Object prefabSelect;
    int index;
    int count;

    public void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;

        prefab = Resources.Load("CuboTextura");
        prefabSelect = Resources.Load("CuboSelect");

        System.Random random = new System.Random();
        int aleatorio = random.Next(1, 4);
        if (aleatorio == 1)
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

        btnChangeIzq = GameObject.Find("ChangeIzq").GetComponent<Button>();
        btnChangeIzq.interactable = false;
        btnChangeDer = GameObject.Find("ChangeDer").GetComponent<Button>();
        btnChangeDer.interactable = false;
    }

    public void addComponent()
    {
        Debug.Log("Añadiendo componente");

        GameObject cube = (GameObject)Instantiate(prefabSelect, new Vector3(0, 0, 0), Quaternion.identity);
        numero = numero + 1;
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
        string sNumero = (string)converter.ConvertTo(numero, typeof(string));
        cube.name = "Cubo" + sNumero;
        cubos.Add(cube);
        count = cubos.Count;
        index = count - 1;
        seleccionado = true;
        ChangePrefab();
    }

    public List<GameObject> GetList()
    {
        return cubos;
    }

    public void touchSelect()
    {
        if (seleccionado)
        {
            if (cubos.Count > 0)
            {
                if (index == 0)
                {
                    count = cubos.Count;
                    index = count - 1;

                }
                else
                {
                    index = index - 1;
                }

                seleccionado = true;
                ChangePrefab();
            }
            else
            {
                seleccionado = false;
            }
        }
    }

    public void ChangePrefab()
    {
        for (int cuenta = cubos.Count - 1; cuenta >= 0; cuenta--)
        {
            if (cuenta != index)
            {
                Destroy(cubos[cuenta]);
                Vector3 vector3 = cubos[cuenta].transform.position;
                cubos[cuenta] = (GameObject)Instantiate(prefab, vector3, Quaternion.identity);
            }
            else
            {
                Destroy(cubos[cuenta]);
                Vector3 vector3 = cubos[cuenta].transform.position;
                cubos[cuenta] = (GameObject)Instantiate(prefabSelect, vector3, Quaternion.identity);
                cubos[cuenta].AddComponent<Lean.Touch.LeanTranslate>();

            }

        }

        btnInteractable();

    }

    public void btnInteractable()
    {
        if (cubos.Count >= 2)
        {
            if (index == 0)
            {
                btnChangeIzq.interactable = false;
                btnChangeDer.interactable = true;
            }
            else
            {
                if (index == cubos.Count - 1)
                {
                    btnChangeIzq.interactable = true;
                    btnChangeDer.interactable = false;
                }
                else
                {
                    btnChangeIzq.interactable = true;
                    btnChangeDer.interactable = true;
                }
            }
        }
        else
        {
            btnChangeIzq.interactable = false;
            btnChangeDer.interactable = false;
        }
    }

    public void ChangeIzq()
    {
        if (index - 1 <= cubos.Count - 1 && index - 1 > -1)
        {
            index = index - 1;
            ChangePrefab();
        }
    }

    public void ChangeDer()
    {
        if (index + 1 <= cubos.Count - 1)
        {
            index = index + 1;
            ChangePrefab();
        }
    }

    public bool GetSelect()
    {
        return seleccionado;
    }

    public void DeleteTranslate()
    {
        translate = cubos[index].GetComponent<Lean.Touch.LeanTranslate>();
        Destroy(translate);
    }

    public void DeleteObject()
    {
        if (seleccionado)
        {
            DeleteTranslate();
            Destroy(cubos[index]);
            cubos.Remove(cubos[index]);
            touchSelect();
        }
    }

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
