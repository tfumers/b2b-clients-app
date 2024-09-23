using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTrainerClientRelationOutDTO : OutDTO
{
    private long trainerId;

    public NewTrainerClientRelationOutDTO()
    {
        trainerId = 0;
    }

    public NewTrainerClientRelationOutDTO(long trainerId)
    {
        this.trainerId = trainerId;
    }

    public long TrainerId { get => trainerId; set => trainerId = value; }

    public override WWWForm ToForm()
    {
        WWWForm form = new WWWForm();

        form.AddField("trainerId", trainerId.ToString());

        return form;
    }
}
