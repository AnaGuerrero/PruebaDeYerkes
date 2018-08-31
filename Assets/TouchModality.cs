using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class TouchModality : MonoBehaviour {

    public Transform cuboTex;
    int numero = 0;
    List<GameObject> cubos = new List<GameObject>();
    bool seleccionado;
    public Lean.Touch.LeanTranslate translate;
    int ultimo;

    public void addComponent()
    {
        Debug.Log("Añadiendo componente");

        Object prefab = Resources.Load("CuboTextura");
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
}
