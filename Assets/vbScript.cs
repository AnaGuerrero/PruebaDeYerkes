using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.ComponentModel;

public class vbScript : MonoBehaviour, IVirtualButtonEventHandler {

    public GameObject addObject;
    int numero = 0;
    List<GameObject> cubos = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        addObject = GameObject.Find("addButton");
        addObject.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        Debug.Log("Boton virtual presionado");
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
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        
    }
    
}
