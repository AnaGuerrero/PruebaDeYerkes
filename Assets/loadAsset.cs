using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadAsset : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string url = "Assets/AssetBundles/cubotextura";
        WWW www = new WWW(url);
        WaitForReq(www);
    }

    IEnumerator WaitForReq(WWW www)
    {
        yield return www;
        AssetBundle bundle = www.assetBundle;
        if (www.error == "")
        {
            GameObject cubotextura = (GameObject)bundle.LoadAsset("cubotextura");
            Instantiate(cubotextura);
        }
        else
        {
            Debug.Log("Objeto cubo = " + www.error);
        }
    }

    // Update is called once per frame
    void Update () {
        
		
	}
}
