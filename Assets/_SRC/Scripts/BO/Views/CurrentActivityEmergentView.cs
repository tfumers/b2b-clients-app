using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CurrentActivityEmergentView : EmergentView 
{
    public UnityEvent<Activity> OnStartedActivity = new UnityEvent<Activity>();
    public UnityEvent<Activity> OnEndedActivity = new UnityEvent<Activity>();
    public UnityEvent<Activity> OnBackButtonClicked = new UnityEvent<Activity>();

    [SerializeField] TMPro.TextMeshProUGUI txtTitle;
    [SerializeField] TMPro.TextMeshProUGUI txtTrainingName;
    [SerializeField] TMPro.TextMeshProUGUI txtActTypeValue;
    [SerializeField] TMPro.TextMeshProUGUI txtActType;
    [SerializeField] TMPro.TextMeshProUGUI txtDescription;

    [SerializeField] Animator animator;

    [SerializeField] ActivityButton btnStartActivity;
    [SerializeField] Button backButton;
    [SerializeField] TimerComponent timer;

    ComponentController componentController;
    ClientInfoController clientInfoController;

    public override Task<bool> InitializeReferences()
    {
        componentController = B2BApplication.Instance.controllerManager.componentController;
        clientInfoController = B2BApplication.Instance.controllerManager.clientInfoController;

        if (!clientInfoController.ClientHasRoutine())
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }

    public async override Task<bool> LoadView()
    {
        Task<Activity> getActivity = componentController.CurrentActivityInfo();

        await getActivity;

        Activity currActivity = getActivity.Result;

        if(currActivity == null)
        {
            return false;
        }
        timer.ConfigureTimer(currActivity, btnStartActivity, OnStartedActivity, OnEndedActivity);

        txtTitle.text = "Ejercicio "+ currActivity.OrderNumber.ToString();

        txtTrainingName.text = currActivity.Training.Name;

        animator.runtimeAnimatorController = currActivity.Training.TrainingVideo.AnimatorController;

        if(currActivity.ActTypeId == Constant.ACTIVITY_TYPE_REPS)
        {
            txtActType.text = "Repeticiones: ";
            txtActTypeValue.text = currActivity.TypeValue.ToString();
        }
        else
        {
            txtActType.text = "Tiempo: ";
            txtActTypeValue.text = currActivity.TypeValue.ToString() + "s";
        }        

        txtDescription.text = currActivity.Training.Description;

        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() => OnBackButtonClicked.Invoke(currActivity));

        return true;
    }
}
