using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ChooseTrainerMenuView : MenuView 
{
    public UnityEvent<Trainer> OnTrainerSelected = new UnityEvent<Trainer>();

    ComponentController componentController;

    ClientInfoController clientInfoController;

    NotificationManager notificationManager;

    public Transform scrollViewTransform; 

    // Start is called before the first frame update
    public async override Task<bool> InitializeReferences()
    {
        componentController = B2BApplication.Instance.controllerManager.componentController;
        clientInfoController = B2BApplication.Instance.controllerManager.clientInfoController;
        notificationManager = B2BApplication.Instance.notificationManager;

        if (clientInfoController.ClientHasSurvey())
        {
            notificationManager.GenericError("El usuario ya hizo el formulario. Debe esperar hasta que se lo entreguen o acabe el período común.");

            return false;
        }
        else
        {
            return true;
        }
    }

    public override async Task<bool> LoadView()
    {

        Task<bool> createTrainersInfoComponent = componentController.CreateTrainersInfoComponent(scrollViewTransform, OnTrainerSelected);

        await createTrainersInfoComponent;

        if (!createTrainersInfoComponent.Result)
        {
            notificationManager.FailureMessageReTry("Encuesta", null);
            return false;
        }

        return true;
    }

}
