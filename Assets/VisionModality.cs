using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.ComponentModel;

public class VisionModality : MonoBehaviour, IVirtualButtonEventHandler{

    private GameObject btnAddCube;
    private GameObject btnLeftCube;
    List<GameObject> cubos = new List<GameObject>();
    int numero = 0;

    void Start(){
        VirtualButtonBehaviour[] vbList = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbList.Length; ++i)
        {
            vbList[i].RegisterEventHandler(this);
        }

        btnAddCube = GameObject.Find("addButton");
        btnLeftCube = GameObject.Find("btnLeft");

        btnAddCube.SetActive(true);
        btnLeftCube.SetActive(true);
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
                break;
            case "btnLeft":
                Debug.Log("Botón Izquierdo");
                /*btnAddCube.SetActive(true);
                btnLeftCube.SetActive(false);*/
                break;

        }
    }

    void IVirtualButtonEventHandler.OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        
    }
}
