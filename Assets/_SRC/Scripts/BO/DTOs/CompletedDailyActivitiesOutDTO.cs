using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedDailyActivitiesOutDTO : OutDTO
{
    long routineId;

    long dailyId;

    string completedDate;

    long time;

    public CompletedDailyActivitiesOutDTO(Daily daily, Routine routine)
    {
        this.routineId = routine.Id;
        this.dailyId = daily.Id;
        this.time = 0;
        foreach(Activity act in daily.Activities)
        {
            this.time += act.ElapsedTime;
        }

        this.completedDate = DateTime.Today.ToString("yyyy-MM-dd");
    }

    public long RoutineId { get => routineId; set => routineId = value; }
    public long DailyId { get => dailyId; set => dailyId = value; }
    public string CompletedDate { get => completedDate; set => completedDate = value; }
    public long Time { get => time; set => time = value; }

    public override WWWForm ToForm()
    {
        WWWForm form = new WWWForm();
        form.AddField("routineId", routineId.ToString());
        form.AddField("dailyId", dailyId.ToString());
        form.AddField("time", time.ToString());
        form.AddField("completedDate", completedDate);
        Debug.Log("Rutina id " + routineId + " daily id + " + dailyId + " y La fecha en que se completó fue: " + completedDate);
        return form;
    }
}
