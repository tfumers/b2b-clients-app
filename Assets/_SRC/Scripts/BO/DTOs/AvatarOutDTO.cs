using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarOutDTO : OutDTO
{
    long id;

    public AvatarOutDTO(Avatar avatar)
    {
        this.id = avatar.Id;
    }

    public override WWWForm ToForm()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id.ToString());
        return form;
    }
}
