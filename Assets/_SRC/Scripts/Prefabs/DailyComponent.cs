using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DailyComponent : AppComponent<Daily>
{
    [SerializeField] TMPro.TextMeshProUGUI txtDay;
    [SerializeField] TMPro.TextMeshProUGUI txtDate;
    [SerializeField] Image bgImage;

    protected override void PrepareUI(Daily model)
    {
        var culture = new CultureInfo("es-ES");
        txtDate.text = culture.DateTimeFormat.GetDayName(model.ProposedDate.DayOfWeek);

        if (model.Id == 0)
        {
            button.interactable = false;
            txtDay.text = "Dia Off";
        }
        else
        {
            txtDay.text = "Dia " + model.DayNumber.ToString();
        }
    }

    public void SetCompletedValue(bool completed)
    {
        if (completed)
        {
            bgImage.color = Color.green;
        }
        else
        {
            bgImage.color = Color.white;
        }

    }
}
