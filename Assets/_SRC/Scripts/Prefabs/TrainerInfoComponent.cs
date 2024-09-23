using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TrainerInfoComponent : AppComponent<Trainer>
{
    [SerializeField] TMPro.TextMeshProUGUI txtUsername;
    [SerializeField] TMPro.TextMeshProUGUI txtFullname;
    [SerializeField] RawImage background;
    [SerializeField] RawImage avatar;

    protected override void PrepareUI(Trainer model)
    {

        avatar.texture = model.AvatarImage.Image;
        txtUsername.text = model.Username;
        txtFullname.text = model.Firstname + " " + model.Lastname;
    }
}
