using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ClientInfoController : Controller
{

    Client currentClient = null;
    public Client CurrentClient { get => currentClient;}

    WebConnectionManager webConnectionManager;

    NotificationManager notificationManager;

    private void Start()
    {
        webConnectionManager = B2BApplication.Instance.webConnectionManager;
        notificationManager = B2BApplication.Instance.notificationManager;
    }

    public async Task<Client> GetCurrentClientInfo()
    {
        Task<ResponseDTO> getCurrentClient = webConnectionManager.GetCurrentClientInfo();
        await getCurrentClient;

        try
        {
            Client returnedClient = DataAdapter.StringToClient(getCurrentClient.Result.Message);
            currentClient = returnedClient;
        }
        catch (Exception e)
        {
            Debug.Log("error en el try catch del usuario " + e.Message);
            throw new Exception("client not found");
        }

        return currentClient;
    }

    public bool ClientHasRoutine()
    {
        if(currentClient.Status == Constant.CLIENT_STATUS_HAS_ROUTINE)
        {
            return true;
        }
        else
        {
            notificationManager.GenericError("El cliente no tiene una rutina");
            return false;
        }
    }

    public bool ClientHasSurvey()
    {
        return currentClient.Status > Constant.CLIENT_STATUS_NO_SURVEY;
    }


    private bool CompareClientStatus(int toCompare)
    {
        return (currentClient.Status == toCompare);
    }
}
