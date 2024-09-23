using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DailyActivityMenuView : MenuView
{
    [SerializeField] UnityEvent OnBackButtonClicked = new UnityEvent();
    [SerializeField] UnityEvent<Activity> OnActivityClicked = new UnityEvent<Activity>();

    [SerializeField] Transform scrollViewTransform;
    [SerializeField] Button backButton;

    NotificationManager notificationManager;

    ComponentController componentController;
    RoutineController routineController;
    ClientInfoController clientInfoController;

    List<ActivityComponent> activityComponents = new List<ActivityComponent>();

    int completedActivitiesCount = 0;

    public override Task<bool> InitializeReferences()
    {
        componentController = B2BApplication.Instance.controllerManager.componentController;
        routineController = B2BApplication.Instance.controllerManager.routineController;
        clientInfoController = B2BApplication.Instance.controllerManager.clientInfoController;
        notificationManager = B2BApplication.Instance.notificationManager;

        if (!clientInfoController.ClientHasRoutine())
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }

    public async override Task<bool> LoadView()
    {
        if (activityComponents.Count > 0)
        {
            foreach(ActivityComponent ac in activityComponents)
            {
                Destroy(ac.gameObject);
            }
        }

        Task<List<ActivityComponent>> getActivityComponents = componentController.CreateActivityComponents(scrollViewTransform, OnActivityClicked);

        await getActivityComponents;

        activityComponents = getActivityComponents.Result;


        ControlActivitiesCount();

        PrepareBackButton();

        return true;
    }
    
    public void UpdateViews()
    {
        Activity currActivity = routineController.SelectedActivity;

        if (currActivity.Completed)
        {
            foreach (ActivityComponent actComp in activityComponents)
            {
                if (actComp.Model == currActivity)
                {
                    actComp.SetCompletedColor();
                }
            }
        }

        ControlActivitiesCount();

        PrepareBackButton();
    }
    
    private void PrepareBackButton()
    {
        backButton.onClick.RemoveAllListeners();

        if (completedActivitiesCount > 0 && !routineController.SelectedDaily.Completed)
        {
            backButton.onClick.AddListener(() => notificationManager.ActivitiesNotSaved(OnBackButtonClicked));
            return;
        }
        else
        {
            if (completedActivitiesCount == routineController.SelectedDaily.Activities.Count)
            {
                routineController.SelectedDaily.Completed = true;
            }

            backButton.onClick.AddListener(() => OnBackButtonClicked.Invoke());
            return;
        }

        
    }

    private void ControlActivitiesCount() 
    {
        completedActivitiesCount = 0;
        foreach (ActivityComponent actComp in activityComponents)
        {
            if (actComp.Model.Completed)
            {
                completedActivitiesCount++;
            }
        }

        Debug.Log("actividades completadas: " + completedActivitiesCount);
    }
}
