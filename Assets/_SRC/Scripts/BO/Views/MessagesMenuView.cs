using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MessagesMenuView : MenuView 
{
    ComponentController componentController;
    ClientInfoController clientInfoController;
    NotificationManager notificationManager;

    [SerializeField] TrainerContactInfoComponent trainerContactInfoComponent;
    public async override Task<bool> InitializeReferences()
    {
        componentController = B2BApplication.Instance.controllerManager.componentController;
        clientInfoController = B2BApplication.Instance.controllerManager.clientInfoController;
        notificationManager = B2BApplication.Instance.notificationManager;

        if (!clientInfoController.ClientHasSurvey())
        {
            notificationManager.GenericError("El cliente no cuenta con una rutina y, por ello, no cuenta con entrenador. Elija un entrenador y solicite su rutina para ver su información.");

            return false;
        }
        else
        {
            return true;
        }
    }

    public async override Task<bool> LoadView()
    {
        Task<TrainerContactInfoComponent> configureContactInfoComponent = componentController.PrepareContactInfoComponent(trainerContactInfoComponent);

        await configureContactInfoComponent;

        trainerContactInfoComponent = configureContactInfoComponent.Result;

        return true;
    }


}
