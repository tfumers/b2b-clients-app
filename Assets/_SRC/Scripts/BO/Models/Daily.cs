using SimpleJSON;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Daily : OrderedEntity
{
    private List<Activity> activities = new List<Activity>();

    private bool completed;

    private DateTime proposedDate;

    private DateTime completionDate = DateTime.Now;

    public Daily()
    {
        this.id = 0;
    }

    public Daily(long id, List<Activity> activities, int dayNumber)
    {
        this.id = id;
        this.activities = activities;
    }

    public Daily(JSONObject json)
    {
        this.id = json["id"];
        //Acá se hizo un cambio exp´licito, para permitir el uso de funciones genéricas
        //Se mantiene el contexto de que es un tipo de orden, solo que cambia el nombre de la variable
        this.orderNumber = json["dayNumber"];
        JSONArray activitiesJsonArray = json["activities"].AsArray;

        try
        {
            proposedDate = DateTime.ParseExact(json["proposedDate"], Constant.DEFAULT_API_DATE_FORMAT, null);
        }
        catch (Exception e)
        {
            return;
        }

        try
        {
            completionDate = DateTime.ParseExact(json["completionDate"], Constant.DEFAULT_API_DATE_FORMAT, null);
        }
        catch (Exception e)
        {
            completionDate = Constant.DefaultDateTime ; //No completado, poniendo una fecha por defecto
        }

        if(completionDate != Constant.DefaultDateTime)
        {
            completed = true;
        }
        else
        {
            completed = false;
        }

        for (int i = 0; i < activitiesJsonArray.Count; i++)
        {
            Debug.Log(activitiesJsonArray[i].AsObject);
            Activity newActivity= new Activity(activitiesJsonArray[i].AsObject);

            newActivity.Completed = completed;

            /*if (completionDate == DateTime.Today)
            {
                newActivity.Completed = true;
            }*/
            activities.Add(newActivity);
        }
    }

    public long Id { get => id; set => id = value; }
    public List<Activity> Activities { get => activities; set => activities = value; }
    public int DayNumber { get => orderNumber; set => orderNumber = value; }
    public bool Completed { get => completed; set => completed = value; }
    public DateTime ProposedDate { get => proposedDate; set => proposedDate = value; }

    //private LocalDate proposedDate;

    //private LocalDate completionDate;
}
