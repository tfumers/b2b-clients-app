using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActivityComponent : AppComponent<Activity>
{
    [SerializeField] TMPro.TextMeshProUGUI txtTrainingName;
    [SerializeField] TMPro.TextMeshProUGUI txtActTypeValue;
    [SerializeField] TMPro.TextMeshProUGUI txtActType;
    [SerializeField] Image imgTraining;
    [SerializeField] Image imgBg;

    protected override void PrepareUI(Activity activity)
    {
        txtTrainingName.text = activity.Training.Name;
        if (activity.ActTypeId==Constant.ACTIVITY_TYPE_REPS)
        {
            txtActType.text = "REPS";
            txtActTypeValue.text = activity.TypeValue.ToString();
        }
        else
        {
            txtActType.text = "s";
            txtActTypeValue.text = activity.TypeValue.ToString();
        }

        imgTraining.sprite = activity.Training.TrainingImage.Image;

        if (activity.Completed)
        {
            SetCompletedColor();
        }

        //imgTraining.sprite = activity.TrainingId.Image();
        //Acá hay lugar para un método que establezca una imagen para el entrenamiento
    }

    public void SetCompletedColor()
    {
        imgBg.color = new Color32(30, 135, 61, 255);
    }
}
