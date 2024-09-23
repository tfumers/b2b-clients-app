using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class RegisterMenuView : MenuView
{
    public UnityEvent OnRegisterSuccess = new UnityEvent();

    public UnityEvent OnRegisterFailed = new UnityEvent();

    NotificationManager notificationManager;

    AuthController authController;

    string selectedInputKey = "someValue";

    public override Task<bool> InitializeReferences()
    {
        authController = B2BApplication.Instance.controllerManager.authController;
        notificationManager = B2BApplication.Instance.notificationManager;
        return Task.FromResult(true);
    }

    public override Task<bool> LoadView()
    {
        return base.LoadView();
    }


    public async void TryRegisterUser()
    {
        Task<bool> registerTask = authController.RegisterTask();
        //esperamos el resultado del task
        await registerTask;

        Debug.Log("Estado del registro " + registerTask.Result);

        if (registerTask.Result)
        {
            notificationManager.SuccessMessage("Registro", OnRegisterSuccess);
        }
        else
        {
            notificationManager.FailureMessageReTry("Registro", OnRegisterFailed);
        }

    }

    public void ChangeInputKey(string currentKey)
    {
        selectedInputKey = currentKey;
    }

    public void SaveInputValue(string value)
    {
        authController.SaveRegisterInfo(value, selectedInputKey);
    }

}
