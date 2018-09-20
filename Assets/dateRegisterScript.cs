using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class dateRegisterScript : MonoBehaviour {

    private Image Background;
    private List<string> lstDays;
    private List<string> lstMonth;
    private List<string> lstYear;
    private Dropdown ddnDay;
    private Dropdown ddnMonth;
    private Dropdown ddnYear;
    private string txtDay;
    private string txtMonth;
    private string txtYear;
    private bool blDay;
    private bool blMonth;
    private bool blYear;
    private Button btnBack;
    private Button btnNext;

    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.Landscape;

        Background = GameObject.Find("Background").GetComponent<Image>();
        var BackTransform = Background.transform as RectTransform;
        BackTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        btnBack = GameObject.Find("BackBtn").GetComponent<Button>();
        btnNext = GameObject.Find("NextBtn").GetComponent<Button>();

        lstDays = new List<string> { "Seleccione una opción", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"};
        lstMonth = new List<string> { "Seleccione una opción", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        lstYear = new List<string> { "Seleccione una opción", "1945", "1946", "1947", "1948", "1949", "1950", "1951", "1952", "1953", "1954", "1955", "1956", "1957", "1958" };

        ddnDay = GameObject.Find("ddnDay").GetComponent<Dropdown>();
        ddnDay.AddOptions(lstDays);

        ddnMonth = GameObject.Find("ddnMonth").GetComponent<Dropdown>();
        ddnMonth.AddOptions(lstMonth);

        ddnYear = GameObject.Find("ddnYear").GetComponent<Dropdown>();
        ddnYear.AddOptions(lstYear);

        btnNext.interactable = false;
        blDay = false;
        blMonth = false;
        blYear = false;
    }

    public void onDropdownValueChangeDay(int index)
    {
        txtDay = lstDays[index];
        if (index != 0)
        {
            blDay = true;
        }
        else
        {
            blDay = false;
        }
        activeBtn();
    }

    public void onDropdownValueChangeMonth(int index)
    {
        txtMonth = lstMonth[index];
        if (index != 0)
        {
            blMonth = true;
        }
        else
        {
            blMonth = false;
        }
        activeBtn();
    }

    public void onDropdownValueChangeYear(int index)
    {
        txtYear = lstYear[index];
        if (index != 0)
        {
            blYear = true;
        }
        else
        {
            blYear = false;
        }
        activeBtn();
    }

    public void activeBtn()
    {
        if(blDay && blMonth && blYear)
        {
            btnNext.interactable = true;
        }
        else
        {
            btnNext.interactable = false;
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene("selector");
    }
}
