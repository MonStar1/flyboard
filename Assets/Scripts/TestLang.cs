using UnityEngine;

public class TestLang : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LanguageSupport ls = new LanguageSupport();
        ls.LoadLanguage(LanguageSupport.Language.RU);
        Debug.logger.Log(ls.GetString("d_exit"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
