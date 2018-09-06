using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.ComponentModel;

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

    void Start(){
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
    }

    void IVirtualButtonEventHandler.OnButtonPressed(VirtualButtonAbstractBehaviour vb){

        Debug.Log("Botón presionado");

        switch (vb.VirtualButtonName)
        {
            case "addButton":
                Debug.Log("Entra a addButton");
                /*btnAddCube.SetActive(false);
                btnLeftCube.SetActive(true);*/
                Object prefab = Resources.Load("CuboTextura");
                GameObject cube = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                numero = numero + 1;
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
                string sNumero = (string)converter.ConvertTo(numero, typeof(string));
                cube.name = "Cubo" + sNumero;
                cube.AddComponent<Lean.Touch.LeanSelectable>();
                cube.AddComponent<Lean.Touch.LeanTranslate>();
                cubos.Add(cube);
                Debug.Log(cube.name);
                visionSelect();
                break;
            case "btnLeft":
                Debug.Log("Botón Izquierdo");
                if (seleccionado)
                {
                    cubos[ultimo - 1].transform.Translate(-2, 0, 0);
                }
                break;
            case "btnRight":
                if (seleccionado)
                {
                    cubos[ultimo - 1].transform.Translate(2, 0, 0);
                }
                break;
            case "btnAhead":
                if (seleccionado)
                {
                    cubos[ultimo - 1].transform.Translate(0, 0, 2);
                }
                break;
            case "btnBehind":
                if (seleccionado)
                {
                    cubos[ultimo - 1].transform.Translate(0, 0, -2);
                }
                break;
            case "btnUp":
                if (seleccionado)
                {
                    y = y + 2;
                    cubos[ultimo - 1].transform.Translate(0, y, 0);
                }
                break;
            case "btnDown":
                if (seleccionado)
                {
                    y = y - 2;
                    if (y > 0)
                    {
                        cubos[ultimo - 1].transform.Translate(0, y, 0);
                    }
                }
                break;
            case "btnDelete":
                if (seleccionado)
                {
                    Destroy(cubos[ultimo - 1]);
                    cubos.Remove(cubos[ultimo - 1]);
                    visionSelect();
                }
                break;
            case "btnExit":
                break;
            case "btnInstructions":
                break;
            case "btnImage":
                break;

        }
    }

    void IVirtualButtonEventHandler.OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        
    }

    public List<GameObject> GetList()
    {
        return cubos;
    }

    public void visionSelect()
    {
        ultimo = cubos.Count;

        if (ultimo > 0)
        {
            seleccionado = true;
        }
        else
        {
            seleccionado = false;
        }
    }

    public void DeleteObject()
    {
        if (seleccionado)
        {
            Destroy(cubos[ultimo - 1]);
            cubos.Remove(cubos[ultimo - 1]);
            visionSelect();
        }
    }
}
