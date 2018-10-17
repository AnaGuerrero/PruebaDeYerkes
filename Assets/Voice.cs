using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using KKSpeech;
using UnityEngine.UI;
using System.Threading;

public class Voice : MonoBehaviour {

    //KeywordRecognizer keywordRecognizer;
    private string[] keywords_array = new string[12];
    int numero = 0;
    List<GameObject> cubos = new List<GameObject>();
    bool seleccionado = false;
    int ultimo;
    int y = 0;
    int index;
    int count;
    Button btnRecording;
    Text txtRecording;

    // Use this for initialization
    void Start() {
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

        btnRecording = GameObject.Find("btnStartRecording").GetComponent<Button>();
        txtRecording = GameObject.Find("txtVoice").GetComponent<Text>();

        SpeechRecognizer.SetDetectionLanguage("es-MX");
        SpeechRecognizer.StartRecording(true);

        if (SpeechRecognizer.ExistsOnDevice())
        {
            SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();
            listener.onAuthorizationStatusFetched.AddListener(OnAuthorizationStatusFetched);
            listener.onAvailabilityChanged.AddListener(OnAvailabilityChange);
            listener.onErrorDuringRecording.AddListener(OnError);
            listener.onErrorOnStartRecording.AddListener(OnError);
            listener.onFinalResults.AddListener(OnFinalResult);
            listener.onPartialResults.AddListener(OnPartialResult);
            listener.onEndOfSpeech.AddListener(OnEndOfSpeech);
            SpeechRecognizer.RequestAccess();
        }
        else
        {
            txtRecording.text = "Sorry, but this device doesn't support speech recognition";
        }
    }

    public void OnFinalResult(string result)
    {
        txtRecording.text = "F " + result;
        OnPhraseRecognized(result);
    }

    public void OnPartialResult(string result)
    {
        txtRecording.text = "P " + result;
        if (SpeechRecognizer.IsRecording())
        {
            SpeechRecognizer.StopIfRecording();
        }
    }

    public void OnAvailabilityChange(bool available)
    {
        if (!available)
        {
            txtRecording.text = "Speech Recognition not available";
        }
    }

    public void OnAuthorizationStatusFetched(AuthorizationStatus status)
    {
        switch (status)
        {
            case AuthorizationStatus.Authorized:

                break;
            default:

                break;
        }
    }

    public void OnEndOfSpeech()
    {
        if (SpeechRecognizer.IsRecording())
        {
            SpeechRecognizer.StopIfRecording();
        }
    }

    public void OnError(string error)
    {
        Debug.LogError(error);

    }

    public void OnStopRecordingPressed()
    {
        if (!SpeechRecognizer.IsRecording())
        {
            SpeechRecognizer.SetDetectionLanguage("es-MX");
            SpeechRecognizer.StartRecording(true);
        }
    }

    private void OnPhraseRecognized(string args)
    {

        switch (args)
        {
            case "arriba":
                if (seleccionado)
                {
                    y = y + 2;
                    cubos[index].transform.Translate(0, y, 0);
                }
                break;
            case "abajo":
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
            case "izquierda":
                if (seleccionado)
                {
                    cubos[index].transform.Translate(-2, 0, 0);
                }
                break;
            case "derecha":
                if (seleccionado)
                {
                    cubos[index].transform.Translate(2, 0, 0);
                }
                break;
            case "adelante":
                if (seleccionado)
                {
                    cubos[index].transform.Translate(0, 0, 2);
                }
                break;
            case "atrás":
                if (seleccionado)
                {
                    cubos[index].transform.Translate(0, 0, -2);
                }
                break;
            case "añadir":
                addComponent();
                break;
            case "borrar":
                if (seleccionado)
                {
                    Destroy(cubos[index]);
                    cubos.Remove(cubos[index]);
                    voiceSelect();
                }
                break;
            case "imagen":
                break;
            case "instrucciones":
                break;
            case "salir":
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
        count = cubos.Count;
        index = count - 1;
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
    void Update() {

    }

    class HiloCorrer {

        public static void CorrerRecording()
        {
            if (SpeechRecognizer.IsRecording())
            {
                SpeechRecognizer.StopIfRecording();
                SpeechRecognizer.SetDetectionLanguage("es-MX");
                SpeechRecognizer.StartRecording(true);
            }else
            {
                SpeechRecognizer.SetDetectionLanguage("es-MX");
                SpeechRecognizer.StartRecording(true);
            }
        }
    }
}
