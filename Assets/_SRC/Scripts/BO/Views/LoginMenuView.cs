using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using UnityEngine.Events;

public class LoginMenuView : MenuView
{
    public UnityEvent OnLoginSuccess = new UnityEvent();

    public UnityEvent OnLoginFailed = new UnityEvent();

    AuthController authController;
    NotificationManager notificationManager;

    Dictionary<string, string> DictionaryInputLoginInfo = new Dictionary<string, string>();

    string selectedInputKey = "someValue";

    public override Task<bool> InitializeReferences()
    {
        authController = B2BApplication.Instance.controllerManager.authController;
        notificationManager = B2BApplication.Instance.notificationManager;
        //Acá se podría recurrir a un autoLogin


        return Task.FromResult(true);
    }


    public async void TryLoginUser()
    {

        /*
        foreach (var keyValue in DictionaryInputLoginInfo)
        {
            if (!RequiredFormFields.Contains(keyValue.Key))
            {
                DictionaryInputLoginInfo.Remove(keyValue.Key);
            }
        }*/
        
        Debug.Log("Task Iniciado");
        Task<bool> loginTask = authController.LoginTask(DictionaryInputLoginInfo);
        //esperamos el resultado del task
        await loginTask;
        //acá obtenemos el resultado del task
        if(loginTask.Result)
        {
            Debug.Log("Todo Ok");
            OnLoginSuccess.Invoke();
        }
        else
        {
            Debug.Log("Todo mal");
            notificationManager.FailureMessageReTry("Inicio de Sesión", OnLoginFailed);
        }
        
    }

    public void ChangeInputKey(string currentKey)
    {
        selectedInputKey = currentKey;
    }

    public void SaveInputValue(string value)
    {
        if (DictionaryInputLoginInfo == null)
        {
            DictionaryInputLoginInfo = new Dictionary<string, string>();
        }

        if (DictionaryInputLoginInfo.ContainsKey(selectedInputKey))
        {
            DictionaryInputLoginInfo[selectedInputKey] = value;
            Debug.Log("saved " + value + " at " + selectedInputKey);
        }
        else
        {
            try
            {
                DictionaryInputLoginInfo.Add(selectedInputKey, value);
                Debug.Log("saved " + value + " at " + selectedInputKey);
            }
            catch (Exception e)
            {
                Debug.LogError("Excepcion: la key es invalida " + e);
            }
        }
    }
}
