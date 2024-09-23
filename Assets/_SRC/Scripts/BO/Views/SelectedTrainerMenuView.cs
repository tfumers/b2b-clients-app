using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectedTrainerMenuView : MenuView
{
    public UnityEvent OnNewRelationSuccess = new UnityEvent();
    public UnityEvent OnNewRelationFailure = new UnityEvent();
    public UnityEvent OnClientSurveyMissing = new UnityEvent();

    NotificationManager notificationManager;
    TrainerController trainerController;
    UpdateClientInfoController updateClientInfoController;
    ClientInfoController clientInfoController;

    public TextMeshProUGUI txtTrainerName;
    public TextMeshProUGUI txtTrainerUsername;
    public TextMeshProUGUI txtTrainerDescription;

    public override Task<bool> InitializeReferences()
    {
        trainerController = B2BApplication.Instance.controllerManager.trainerController;
        updateClientInfoController = B2BApplication.Instance.controllerManager.updateClientInfoController;
        clientInfoController = B2BApplication.Instance.controllerManager.clientInfoController;
        notificationManager = B2BApplication.Instance.notificationManager;

        return Task.FromResult(true);
    }

    public override Task<bool> LoadView()
    {
        Trainer trainer = trainerController.GetSelectedTrainer();
        bool res = LoadTrainerInfoOnMenu(trainer);
        return Task.FromResult(res);
    }

    private bool LoadTrainerInfoOnMenu(Trainer currentTrainer)
    {
        if (currentTrainer != null)
        {
            if (txtTrainerName != null)
            {
                txtTrainerName.text = "Nombre: " + currentTrainer.Firstname + " " + currentTrainer.Lastname;
            }
            if (txtTrainerUsername != null)
            {
                txtTrainerUsername.text = "Username: " + currentTrainer.Username;
            }
            if (txtTrainerDescription != null)
            {
                txtTrainerDescription.text = "Descripción: " + currentTrainer.Description;
            }

            return true;
        }

        return false;
    }

    public async void TryCreateNewTrainerClientRelation()
    {
        bool userHasSurvey = clientInfoController.ClientHasSurvey();
        Debug.Log("user has survey " + userHasSurvey);

        if(userHasSurvey)
        {
            //Se envía la info del trainer para crear la nueva relación
            Task<bool> createNewTrainerClientRelationTask = trainerController.CreateNewTrainerClientRelation();
            //ESPERAMOS LA TASK
            await createNewTrainerClientRelationTask;
            //si el resultado es un exito
            if (createNewTrainerClientRelationTask.Result)
            {
                OnNewRelationSuccess.Invoke();
            }
            else
            {
                OnNewRelationFailure.Invoke();
            }
            return;
        }
        else
        {
            //Se mantiene la información del trainer en la bdd
            notificationManager.ClientDontHaveSurvey(OnClientSurveyMissing);
        }

    }
}
