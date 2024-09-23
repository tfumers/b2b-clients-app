using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// El component controller se encarga, como su nombre indica, de la creación de componentes en las distintas interfaces.
/// </summary>
public class ComponentController : Controller
{
    NotificationManager notificationManager;

    TrainerController trainerController;

    ClientInfoController clientInfoController;

    RoutineController routineController;

    LocalFilesService localFilesService;

    public TrainerInfoComponent trainerInfoComponentPrefab;

    public ClickableAvatarComponent clickableAvatarComponentPrefab;

    public DailyComponent dailyComponentPrefab;

    public ActivityComponent activityComponentPrefab;

    public List<ClickableAvatarComponent> loadedClickableAvatars = new List<ClickableAvatarComponent>();

    private void Start()
    {
        notificationManager = B2BApplication.Instance.notificationManager;
        trainerController = B2BApplication.Instance.controllerManager.trainerController;
        routineController = B2BApplication.Instance.controllerManager.routineController;
        clientInfoController = B2BApplication.Instance.controllerManager.clientInfoController;
        localFilesService = B2BApplication.Instance.serviceManager.localFilesService;
    }

    public Avatar GetClientAvatar()
    {
        string avatarString = "0";
        //este método se podría agregar al objeto client, así obtenemos la ref directa al avatar del usuario
        if (clientInfoController.CurrentClient != null)
        {
            avatarString = clientInfoController.CurrentClient.Avatar;
        }

        Avatar clientAvatar = localFilesService.GetAvatarById(avatarString);

        return clientAvatar;
    }

    public Task<List<ClickableAvatarComponent>> CreateSelectableAvatarComponent(Transform contentTransform, UnityEvent<Avatar> buttonSelect)
    {
        if (loadedClickableAvatars == null)
        {
            loadedClickableAvatars = new List<ClickableAvatarComponent>();
        }

        if (clickableAvatarComponentPrefab != null)
        {
            List<Avatar> avatars = localFilesService.GetAvatars();

            Debug.Log("lista de avatars: " + avatars.Count);

            foreach (Avatar avatar in avatars)
            {
                ClickableAvatarComponent cac = Instantiate(clickableAvatarComponentPrefab, contentTransform);
                cac.LoadComponent(avatar, buttonSelect);

                loadedClickableAvatars.Add(cac);

                Debug.Log("Avatar id " + avatar.Id);
            }
        }
        else
        {
            notificationManager.GenericError("No hay una instancia de prefab de clickable avatar component");
        }

        return Task.FromResult(loadedClickableAvatars);
    }

    public async Task<bool> CreateTrainersInfoComponent(Transform targetTransform, UnityEvent<Trainer> buttonUnityEvent)
    {
        if (trainerInfoComponentPrefab!=null)
        {
            List<Trainer> trainers;

            Task<List<Trainer>> getTrainers = trainerController.GetTrainers();
            await getTrainers;
            trainers = getTrainers.Result;

            Debug.Log(trainers.Count);

            foreach (Trainer trainer in trainers)
            {
                TrainerInfoComponent tic = Instantiate(trainerInfoComponentPrefab, targetTransform);
                tic.LoadComponent(trainer, buttonUnityEvent);
                Debug.Log("trainer info en TIC, tID = " + trainer.Id);
            }


            if (trainers.Count <= 0)
            {
                //Si no hay ningun entrenador, debería inicializar lo mismo, pero bueno
                return false;
            }
            else
            {
                //debe haber entrenadores
                return true;
            }

        }
        else
        {
            notificationManager.GenericError("No hay una instancia del Trianer Info Component Prefab");
            return false;
        }
    }

    public async Task<List<DailyComponent>> CreateDailyActivityComponents(Transform scrollViewTransform, UnityEvent<Daily> OnClickedEvent)
    {
        Task<List<Daily>> getDailyActivities = routineController.GetDailyActivities();
        await getDailyActivities;
        List<DailyComponent> dailyComponents = new List<DailyComponent>();

        //int i = 0;
        foreach(Daily da in getDailyActivities.Result)
        {
            DailyComponent dac = Instantiate(dailyComponentPrefab, scrollViewTransform);
            dac.LoadComponent(da, OnClickedEvent);
            Debug.Log("Dia completado?" + dac.Model.Completed);
            dac.SetCompletedValue(da.Completed);
            dailyComponents.Add(dac);
            //Debug.Log("instancia " + i++ + " creada");
        }

        return dailyComponents;
    }

    public async Task<List<ActivityComponent>> CreateActivityComponents(Transform scrollViewTransform, UnityEvent<Activity> OnClickedEvent)
    {
        Task<List<Activity>> getActivities = routineController.GetActivities();
        await getActivities;
        List<ActivityComponent> activityComponents = new List<ActivityComponent>();

        foreach (Activity act in getActivities.Result)
        {
            ActivityComponent actComp = Instantiate(activityComponentPrefab, scrollViewTransform);
            actComp.LoadComponent(act, OnClickedEvent);
            activityComponents.Add(actComp);
        }

        return activityComponents;
    }

    public Task<Activity> CurrentActivityInfo()
    {
        Activity activity = routineController.SelectedActivity;
        return Task.FromResult(activity);
    }

    public async Task<TrainerContactInfoComponent> PrepareContactInfoComponent(TrainerContactInfoComponent trainerContactInfoComponent)
    {
        Task<Trainer> currentTrainer = trainerController.GetSelectedTrainerFromWeb();

        await currentTrainer;

        if (currentTrainer != null)
        {
            Trainer trainerResult = currentTrainer.Result;

            UnityEvent<Trainer> OnDoingSomethingEvent = new UnityEvent<Trainer>();

            trainerContactInfoComponent.LoadComponent(trainerResult, OnDoingSomethingEvent);

        }


        return trainerContactInfoComponent;
    }
}
