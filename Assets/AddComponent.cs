using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class AddComponent : MonoBehaviour {

    public Transform cuboTex;
    int numero=0;
    List<GameObject> cubos = new List<GameObject>();
    TouchSelect touchSelect = new TouchSelect();

    public void addComponent() {
        Debug.Log("Añadiendo componente");

        Object prefab = Resources.Load("CuboTextura"); 
        GameObject cube = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        numero = numero + 1;
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
        string sNumero = (string)converter.ConvertTo(numero, typeof(string));
        cube.name = "Cubo" + sNumero;
        cubos.Add(cube);
        Debug.Log(cube.name);
        if(touchSelect.GetSelect() != null)
        {
            touchSelect.DeleteTranslate(touchSelect.GetSelect());
        }
        touchSelect.touchSelect(GetList());
    }

    public List<GameObject> GetList()
    {
        return cubos;
    }

}
