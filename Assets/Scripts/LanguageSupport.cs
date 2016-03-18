using UnityEngine;
using SimpleJSON;
using System.ComponentModel;
using System.Reflection;
using System;

public class LanguageSupport {

    public enum Language
    {
        [Description("ru")]
        RU,
        [Description("en")]
        EN,
    }

    private const string LANG_PATH = "lang/";

    private JSONNode dectionary;
    private Language selectedLanguage;

    public void LoadLanguage(Language language)
    {
        string strLang = GetEnumDescription(language);
        TextAsset langResource = Resources.Load(LANG_PATH + strLang) as TextAsset;
        if(langResource == null)
        {
            throw new InvalidOperationException("Selected language doesn't exist");
        }
        selectedLanguage = language;
        dectionary = JSON.Parse(langResource.text);
        
    }

    public string GetString(string key)
    {
        return dectionary[key].Value;
    }

    public Language GetSelectedLanguage()
    {
        return selectedLanguage;
    }

    public static string GetEnumDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
            return attributes[0].Description;
        else
            return value.ToString();
    }

}
