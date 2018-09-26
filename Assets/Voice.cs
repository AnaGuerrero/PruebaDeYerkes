using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System.ComponentModel;

public class Voice : MonoBehaviour {

    KeywordRecognizer keywordRecognizer;
    private string[] keywords_array = new string[12];
    int numero = 0;
    List<GameObject> cubos = new List<GameObject>();
    bool seleccionado = false;
    int ultimo;
    int y = 0;
    int index;
    int count;

    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.Landscape;

        keywords_array[0] = "Arriba";
        keywords_array[1] = "Abajo";
        keywords_array[2] = "Izquierda";
        keywords_array[3] = "Derecha";
        keywords_array[4] = "Adelante";
        keywords_array[5] = "Atrás";
        keywords_array[6] = "Añadir";
        keywords_array[7] = "Borrar";
        keywords_array[8] = "Imagen";
        keywords_array[9] = "Instrucciones";
        keywords_array[10] = "Salir";
        keywords_array[11] = "Cambiar";
        keywordRecognizer = new KeywordRecognizer(keywords_array);
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
	}

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        switch(args.text)
        {
            case "Arriba":
                if (seleccionado)
                {
                    y = y + 2;
                    cubos[ultimo - 1].transform.Translate(0, y, 0);
                }
                break;
            case "Abajo":
                if (seleccionado)
                {
                    int aux = y - 2;
                    if (aux > 0)
                    {
                        y = y - 2;
                        cubos[ultimo - 1].transform.Translate(0, y, 0);
                    }
                }
                break;
            case "Izquierda":
                if (seleccionado)
                {
                    cubos[ultimo - 1].transform.Translate(-2, 0, 0);
                }
                break;
            case "Derecha":
                if (seleccionado)
                {
                    cubos[ultimo - 1].transform.Translate(2, 0, 0);
                }
                break;
            case "Adelante":
                if (seleccionado)
                {
                    cubos[ultimo - 1].transform.Translate(0, 0, 2);
                }
                break;
            case "Atrás":
                if (seleccionado)
                {
                    cubos[ultimo - 1].transform.Translate(0, 0, -2);
                }
                break;
            case "Añadir":
                addComponent();
                break;
            case "Borrar":
                if (seleccionado)
                {
                    Destroy(cubos[ultimo - 1]);
                    cubos.Remove(cubos[ultimo - 1]);
                    voiceSelect();
                }
                break;
            case "Imagen":
                break;
            case "Instrucciones":
                break;
            case "Salir":
                break;
        }
    }

    private void addComponent()
    {
        UnityEngine.Object prefab = Resources.Load("CuboTextura");
        GameObject cube = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        numero = numero + 1;
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
        string sNumero = (string)converter.ConvertTo(numero, typeof(string));
        cube.name = "Cubo" + sNumero;
        cubos.Add(cube);
        count = cubos.Count - 1;
        index = count;
        seleccionado = true;
    }

    private bool GetSelect()
    {
        return seleccionado;
    }

    private void voiceSelect()
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

    private void changeSelect()
    {
        if (seleccionado)
        {

        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
