using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainerContactInfoComponent : AppComponent<Trainer>
{
    [SerializeField] RawImage avatarImage;
    [SerializeField] TMPro.TextMeshProUGUI txtUserName;
    [SerializeField] TMPro.TextMeshProUGUI txtNumber;
    [SerializeField] TMPro.TextMeshProUGUI txtTrainerName;

    protected override void PrepareUI(Trainer model)
    {
        txtUserName.text = model.Username;
        txtTrainerName.text = model.Firstname + " " + model.Lastname;

        avatarImage.texture = model.AvatarImage.Image;
        txtNumber.text = model.Phone;
    }
}
