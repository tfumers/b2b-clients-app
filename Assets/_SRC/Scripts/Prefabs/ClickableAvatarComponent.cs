using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickableAvatarComponent : AppComponent<Avatar>
{

    public RawImage avatarImage;
    public RawImage bgColor;

    Avatar avatar;

    Color selectedColor = Color.green;
    Color deselectedColor = Color.white;

    public Avatar Avatar { get => avatar;}

    protected override void PrepareUI(Avatar model)
    {
        this.avatar = model;
        avatarImage.texture = model.Image;
    }

    public void SelectAvatar()
    {
        ChangeBgColor(selectedColor);
    }

    public void DeselectAvatar()
    {
        ChangeBgColor(deselectedColor);
    }

    private void ChangeBgColor(Color color)
    {
        bgColor.color = color;
    }


}
