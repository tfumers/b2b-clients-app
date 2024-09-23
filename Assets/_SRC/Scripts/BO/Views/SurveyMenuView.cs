using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class SurveyMenuView : MenuView
{
    public UnityEvent OnSaveSurveySuccess = new UnityEvent();

    public UnityEvent OnSaveSurveyFailure = new UnityEvent();

    UpdateClientInfoController updateClientInfoController;

    NotificationManager notificationManager;

    Dictionary<string, string> DictionaryInputRegisterInfo = new Dictionary<string, string>();

    string selectedInputKey = "someValue";

    public override Task<bool> InitializeReferences()
    {
        updateClientInfoController = B2BApplication.Instance.controllerManager.updateClientInfoController;
        notificationManager = B2BApplication.Instance.notificationManager;
        return Task.FromResult(true);
    }

    public override Task<bool> LoadView()
    {
        return base.LoadView();
    }

    public void SaveSurveyInfoValue(string value)
    {
        updateClientInfoController.SaveClientSurveyInfo(value, selectedInputKey);
    }

    public void ChangeInputKey(string currentKey)
    {
        selectedInputKey = currentKey;
    }

    public async void TrySaveSurveyInfo()
    {
        Task<bool> saveSurveyInfo = updateClientInfoController.UpdateSurveyInfoTask();
        await saveSurveyInfo;
        if (saveSurveyInfo.Result)
        {
            notificationManager.SuccessMessage("Encuesta", OnSaveSurveySuccess);
        }
        else
        {
            notificationManager.FailureMessageReTry("Encuesta", OnSaveSurveyFailure);
        }
    }
}
