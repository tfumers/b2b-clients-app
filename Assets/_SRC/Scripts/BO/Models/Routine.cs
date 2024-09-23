using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Routine : OrderedEntity
{

    private int numberOfDays;

    //private LocalDate proposedDate;

    //private LocalDate completionDate;

    private List<Daily> dailyActivities = new List<Daily>();

    public Routine(JSONObject json)
    {
        this.id = json["id"];
        this.numberOfDays = json["days"];

        //Ncesitamos en bdd algo que nos diga el orden correcto de las rutinas

        JSONArray dailyActivitiesArray = json["dailyActivities"].AsArray;
        for(int i = 0; i < dailyActivitiesArray.Count; i++)
        {
            Debug.Log(dailyActivitiesArray[i].AsObject);
            Daily newDaily = new Daily(dailyActivitiesArray[i].AsObject);
            dailyActivities.Add(newDaily);
        }
        //this.testString = json["activities"];
    }
    public int NumberOfDays { get => numberOfDays; }
    public List<Daily> DailyActivities { get => dailyActivities; set => dailyActivities = value; }
}
