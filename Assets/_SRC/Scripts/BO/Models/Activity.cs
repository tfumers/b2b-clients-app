using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activity : OrderedEntity
{

    private Training training;

    private int trainingId;

    private int actTypeId;

    private int typeValue;

    private int elapsedTime;

    private bool completed = false;

    public Activity(JSONObject json) : base()
    {
        this.id = json["id"];
        this.trainingId = json["trainingId"];
        this.orderNumber = json["orderNumber"];
        this.actTypeId = json["actTypeId"];
        this.typeValue = json["actTypeValue"];
    }

    public int TrainingId { get => trainingId; set => trainingId = value; }
    public int ActTypeId { get => actTypeId; set => actTypeId = value; }
    public int TypeValue { get => typeValue; set => typeValue = value; }
    public Training Training { get => training; set => training = value; }
    public bool Completed { get => completed; 
                            set => completed = value; }
    public int ElapsedTime { get => elapsedTime; set => elapsedTime = value; }
}
