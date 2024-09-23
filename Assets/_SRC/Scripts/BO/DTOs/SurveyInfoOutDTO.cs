using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveyInfoOutDTO : OutDTO
{
    private string sex;

    private string birthDate;

    private double height;

    private double starterWeight;

    private bool pregnancy;

    private string trainingOrSportsRecord;

    private string availableTrainingItems;

    private string trainingObjectives;

    private string illnesses;

    private string wounds;

    private string availableTrainingDays;

    WWWForm surveyInfoToSend;

    public SurveyInfoOutDTO()
    {
        this.sex = "x";
        this.height = 0;
        this.starterWeight = 0;
        this.pregnancy = false;
        this.trainingOrSportsRecord = "";
        this.availableTrainingItems = "";
        this.trainingObjectives = "";
        this.illnesses = "";
        this.availableTrainingDays = "";
    }

    public SurveyInfoOutDTO(ClientSurveyInfo surveyInfo)
    {
        this.sex = surveyInfo.Sex;
        this.height = surveyInfo.Height;
        this.starterWeight = surveyInfo.StarterWeight;
        this.pregnancy = surveyInfo.Pregnancy;
        this.trainingOrSportsRecord = surveyInfo.TrainingOrSportsRecord;
        this.availableTrainingItems = surveyInfo.AvailableTrainingItems;
        this.trainingObjectives = surveyInfo.TrainingObjectives;
        this.illnesses = surveyInfo.Illnesses;
        this.availableTrainingDays = surveyInfo.AvailableTrainingDays;
        this.birthDate = surveyInfo.BirthDate;
        this.wounds = surveyInfo.Wounds;
    }

    public SurveyInfoOutDTO(WWWForm form)
    {
        surveyInfoToSend = form;
    }

    public string Sex { get => sex; set => sex = value; }
    public double Height { get => height; set => height = value; }
    public double Starter_weight { get => starterWeight; set => starterWeight = value; }
    public bool Pregnancy { get => pregnancy; set => pregnancy = value; }
    public string TrainingOrSportsRecord { get => trainingOrSportsRecord; set => trainingOrSportsRecord = value; }
    public string AvailableTrainingItems { get => availableTrainingItems; set => availableTrainingItems = value; }
    public string TrainingObjectives { get => trainingObjectives; set => trainingObjectives = value; }
    public string Illnesses { get => illnesses; set => illnesses = value; }
    public string AvailableTrainingDays { get => availableTrainingDays; set => availableTrainingDays = value; }
    public string Wounds { get => wounds; set => wounds = value; }
    public string BirthDate { get => birthDate; set => birthDate = value; }

    public override WWWForm ToForm()
    {
        if (surveyInfoToSend != null)
        {
            return surveyInfoToSend;
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("sex", sex);
            form.AddField("height", height.ToString());
            form.AddField("starterWeight", starterWeight.ToString());
            form.AddField("pregnancy", pregnancy.ToString());
            form.AddField("trainingOrSportsRecord", trainingOrSportsRecord);
            form.AddField("availableTrainingItems", availableTrainingItems);
            form.AddField("trainingObjectives", trainingObjectives);
            form.AddField("illnesses", illnesses);
            form.AddField("availableTrainingDays", availableTrainingDays);
            form.AddField("wounds", wounds);
            form.AddField("birthDate", birthDate);
            return form;
        }
    }
}
