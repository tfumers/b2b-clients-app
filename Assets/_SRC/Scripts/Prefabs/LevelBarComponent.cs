using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarComponent : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI txtLVL;
    [SerializeField] TMPro.TextMeshProUGUI txtXP;

    [SerializeField] RectTransform loaderTransform;

    [SerializeField] RectTransform backTransform;

    public void PrepareLevelBarComponent(Client client)
    {
        int completeSize = (int)backTransform.sizeDelta.x;
        int x = (int)client.Level; 

        int requiredExperience = (int)((x + 5) * (x * 1.5) + 75);

        txtLVL.text = client.Level.ToString();
        txtXP.text = client.Experience.ToString() + "/" + requiredExperience.ToString();

        loaderTransform.sizeDelta = new Vector2((completeSize * client.Experience) / (requiredExperience), loaderTransform.sizeDelta.y);

    }


}
