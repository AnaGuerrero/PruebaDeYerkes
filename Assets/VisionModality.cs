using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.ComponentModel;
using UnityEngine.UI;
using System;

public class VisionModality : MonoBehaviour, IVirtualButtonEventHandler{

    private GameObject btnAddCube;
    private GameObject btnLeftCube;
    private GameObject btnRightCube;
    private GameObject btnAheadCube;
    private GameObject btnBehindCube;
    private GameObject btnUpCube;
    private GameObject btnDownCube;
    private GameObject btnDeleteCube;
    private GameObject btnExit;
    private GameObject btnInstructions;
    private GameObject btnImage;
    List<GameObject> cubos = new List<GameObject>();
    int numero = 0;
    int ultimo;
    bool seleccionado;
    int y = 0;
    private UnityEngine.UI.Image Background;
    private UnityEngine.UI.Image image;
    private Text Instructions;
    public Sprite sprite;
    private Plane plano;
    private Texture texture;
    int index;
    int count;
    UnityEngine.Object prefab;
    UnityEngine.Object prefabSelect;


    void Start()
    {

        Screen.orientation = ScreenOrientation.Landscape;

        prefab = Resources.Load("CuboTextura");
        prefabSelect = Resources.Load("CuboSelect");

        VirtualButtonBehaviour[] vbList = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbList.Length; ++i)
        {
            vbList[i].RegisterEventHandler(this);
        }

        btnAddCube = GameObject.Find("addButton");
        btnLeftCube = GameObject.Find("btnLeft");
        btnRightCube = GameObject.Find("btnRight");
        btnAheadCube = GameObject.Find("btnAhead");
        btnBehindCube = GameObject.Find("btnBehind");
        btnUpCube = GameObject.Find("btnUp");
        btnDownCube = GameObject.Find("btnDown");
        btnDeleteCube = GameObject.Find("btnDelete");
        btnExit = GameObject.Find("btnExit");
        btnInstructions = GameObject.Find("btnInstructions");
        btnImage = GameObject.Find("btnImage");

        btnAddCube.SetActive(true);
        btnLeftCube.SetActive(true);
        btnRightCube.SetActive(true);
        btnAheadCube.SetActive(true);
        btnBehindCube.SetActive(true);
        btnUpCube.SetActive(true);
        btnDownCube.SetActive(true);
        btnDeleteCube.SetActive(true);
        btnExit.SetActive(true);
        btnInstructions.SetActive(true);
        btnImage.SetActive(true);

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

        image = GameObject.Find("Image").GetComponent<UnityEngine.UI.Image>();
        image.sprite = sprite;

        Background = GameObject.Find("Background").GetComponent<UnityEngine.UI.Image>();

        Instructions = GameObject.Find("txtInstructions").GetComponent<Text>();
        Instructions.text = "Estas van a ser las instrucciones.";
        Instructions.alignment = TextAnchor.MiddleCenter;
        Instructions.fontSize = 14;

        texture = Resources.Load<Texture>("shape1");
        Material material = new Material(Shader.Find("Diffuse"));
        material.SetTexture("textura1", texture);

        //plano = GameObject.Find("btnImage").GetComponentInChildren<Plane>();
    }

    void IVirtualButtonEventHandler.OnButtonPressed(VirtualButtonAbstractBehaviour vb){

        Debug.Log("Botón presionado");

        switch (vb.VirtualButtonName)
        {
            case "addButton":
                addCube();
                break;
            case "btnLeft":
                Debug.Log("Botón Izquierdo");
                if (seleccionado)
                {
                    cubos[index].transform.Translate(-2, 0, 0);
                }
                break;
            case "btnRight":
                if (seleccionado)
                {
                    cubos[index].transform.Translate(2, 0, 0);
                }
                break;
            case "btnAhead":
                if (seleccionado)
                {
                    cubos[index].transform.Translate(0, 0, 2);
                }
                break;
            case "btnBehind":
                if (seleccionado)
                {
                    cubos[index].transform.Translate(0, 0, -2);
                }
                break;
            case "btnUp":
                if (seleccionado)
                {
                    y = y + 2;
                    cubos[index].transform.Translate(0, y, 0);
                }
                break;
            case "btnDown":
                if (seleccionado)
                {
                    int aux = y - 2;
                    if (aux > 0)
                    {
                        y = y - 2;
                        cubos[index].transform.Translate(0, y, 0);
                    }
                }
                break;
            case "btnDelete":
                if (seleccionado)
                {
                    DeleteObject();
                }
                break;
            case "btnChange":
                visionSelect();
                break;
            case "btnExit":
                AppExit();
                break;
            case "btnInstructions":
                ShowInstructions();
                break;
            case "btnImage":
                ShowImage();
                break;

        }
    }

    void IVirtualButtonEventHandler.OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        switch (vb.VirtualButtonName)
        {
            case "btnInstructions":
                HideInstructions();
                break;
            case "btnImage":
                HideImage();
                break;
        }
    }

    public List<GameObject> GetList()
    {
        return cubos;
    }

    private void addCube()
    {
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

    public void ChangePrefab()
    {
        for (int cuenta = cubos.Count - 1;  cuenta >= 0; cuenta--)
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
            }

        }

    }

    public void visionSelect()
    {
        if (seleccionado)
        {
            if (cubos.Count > 0)
            {
                if(index == 0)
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

    public void DeleteObject()
    {
        if (seleccionado)
        {
            Debug.Log(index.ToString());
            Destroy(cubos[index]);
            cubos.Remove(cubos[index]);
            visionSelect();
        }
    }

    private void ShowImage()
    {
        var RectTransform = image.transform as RectTransform;
        RectTransform.sizeDelta = new Vector2(Screen.height, Screen.height);

        var BackTransform = Background.transform as RectTransform;
        BackTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    private void HideImage()
    {
        var RectTransform = image.transform as RectTransform;
        RectTransform.sizeDelta = new Vector2(0, 0);

        var BackTransform = Background.transform as RectTransform;
        BackTransform.sizeDelta = new Vector2(0, 0);
    }

    private void ShowInstructions()
    {
        var BackTransform = Background.transform as RectTransform;
        BackTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        var RectTransform = Instructions.transform as RectTransform;
        RectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    private void HideInstructions()
    {
        var RectTransform = Instructions.transform as RectTransform;
        RectTransform.sizeDelta = new Vector2(0, 0);

        var BackTransform = Background.transform as RectTransform;
        BackTransform.sizeDelta = new Vector2(0, 0);
    }

    private void AppExit()
    {
        ScreenCapture.CaptureScreenshot("screenshot.jpg");
        Application.Quit();
    }
}
