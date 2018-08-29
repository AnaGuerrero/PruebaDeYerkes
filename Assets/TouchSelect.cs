using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSelect : MonoBehaviour {

    List<GameObject> cubos;
    GameObject seleccionado;
    public Lean.Touch.LeanTranslate translate;

    public void touchSelect(List<GameObject> lista)
    {
        cubos = lista;

        int ultimo = cubos.Count;

        if(ultimo > 0)
        {
            seleccionado = cubos[ultimo - 1];
            seleccionado.AddComponent<Lean.Touch.LeanTranslate>();
        }
    }

    public GameObject GetSelect() {
        return seleccionado;
    }

    public void DeleteTranslate(GameObject objeto)
    {
        translate = objeto.GetComponent<Lean.Touch.LeanTranslate>();
        Destroy(translate);
    }

    public void DestroyObject()
    {
        AddComponent addComponent = new AddComponent();

        List<GameObject> cubos = addComponent.GetList();

        int ultimo = cubos.Count;

        if (ultimo > 0)
        {
            seleccionado = cubos[ultimo - 1];
            Destroy(seleccionado);
        }
        
    }
    
}
