using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{

    protected Dictionary<string, string> ValidateFields(Dictionary<string, string> receivedDictionary, List<string> validFieldsList)
    {
        Dictionary<string, string> validDictionary = new Dictionary<string, string>();

        foreach (var keyValue in receivedDictionary)
        {
            if (validFieldsList.Contains(keyValue.Key))
            {
                validDictionary.Add(keyValue.Key, keyValue.Value);
            }
        }

        return validDictionary;
        //throw new NotImplementedException();
    }

    protected string DictionaryValuesToString(Dictionary<string, string> dictionary)
    {
        StringBuilder text = new StringBuilder();


        text.AppendLine("The dictionary " + dictionary.ToString() + " contains:");
        foreach(var keyValue in dictionary)
        {
            text.AppendLine("Key: " + keyValue.Key + ", Value:" + keyValue.Value);
        }

        return text.ToString();
    }
}
