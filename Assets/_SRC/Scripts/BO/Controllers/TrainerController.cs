using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrainerController : Controller
{
    NotificationManager notificationManager;

    WebConnectionManager webConnectionManager;

    ClientInfoController clientInfoController;

    LocalFilesService localFilesService;

    private Trainer selectedTrainer = null;

    private bool loadedTrainers = false;

    public bool LoadedTrainers { get => loadedTrainers; set => loadedTrainers = value; }

    public void Start()
    {
        notificationManager = B2BApplication.Instance.notificationManager;
        webConnectionManager = B2BApplication.Instance.webConnectionManager;
        clientInfoController = B2BApplication.Instance.controllerManager.clientInfoController;
        localFilesService = B2BApplication.Instance.serviceManager.localFilesService;
    }

    public async Task<List<Trainer>> GetTrainers()
    {
        List<Trainer> trainers = new List<Trainer>();
        //
        Task<ResponseDTO> getTrainersResponse = webConnectionManager.GetTrainers();

        await getTrainersResponse;

        try{
            trainers = DataAdapter.ResponseToTrainerList(getTrainersResponse.Result);
        }
        catch(Exception e)
        {
            notificationManager.GenericError(e.Message);
        }

        List<Avatar> avatars = new List<Avatar>();

        foreach(Trainer tr in trainers)
        {
            tr.AvatarImage = localFilesService.GetAvatarById(tr.Avatar);
        }

        return trainers;
    }

    public async Task<Trainer> GetSelectedTrainerFromWeb()
    {
        Trainer selected;

        Task<ResponseDTO> getSelectedTrainer = webConnectionManager.GetSelectedTrainer();
        await getSelectedTrainer;

        try
        {
            SimpleJSON.JSONObject trainerJSON = (SimpleJSON.JSONObject)SimpleJSON.JSONObject.Parse(getSelectedTrainer.Result.Message);

            selected = new Trainer(trainerJSON);
        }
        catch (Exception e)
        {
            selected = null;
            notificationManager.GenericError(e.Message);
        }

        selected.AvatarImage = localFilesService.GetAvatarById(selected.Avatar);

        return selected;
    }

    public void SelectTrainer(Trainer trainer)
    {
        if (trainer != null)
        {
            Debug.Log("Trainer elegido, con id: " + trainer.Id );
            selectedTrainer = trainer;
        }
    }

    public Trainer GetSelectedTrainer()
    {
        if (selectedTrainer != null)
        {
            return selectedTrainer;
        }
        else
        {
            notificationManager.GenericError("TRAINER No cargado");
            return null;
        }
    }

    public async Task<bool> CreateNewTrainerClientRelation()
    {
        NewTrainerClientRelationOutDTO newRelationOutDto = new NewTrainerClientRelationOutDTO(selectedTrainer.Id);

        Task<bool> postNewClientRelation = webConnectionManager.PostNewClientTrainerRelation(newRelationOutDto);

        await postNewClientRelation;

        if (postNewClientRelation.Result)
        {
            await UpdateClientInfo();
        }

        return postNewClientRelation.Result;
    }

    private async Task UpdateClientInfo()
    {
        Task<Client> updateClientInfo = clientInfoController.GetCurrentClientInfo();
        await updateClientInfo;
    }
}
