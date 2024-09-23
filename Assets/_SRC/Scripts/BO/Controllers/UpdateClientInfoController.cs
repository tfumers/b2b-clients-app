using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UpdateClientInfoController : Controller
{
    ClientInfoController clientInfoController;

    WebConnectionManager webConnectionManager;

    WWWForm clientSurveyInfoForm;

    public void Start()
    {
        webConnectionManager = B2BApplication.Instance.webConnectionManager;
        clientInfoController = B2BApplication.Instance.controllerManager.clientInfoController;
    }

    public async Task<bool> UpdateSurveyInfoTask()
    {
        SurveyInfoOutDTO surveyInfoOutDTO = new SurveyInfoOutDTO(clientSurveyInfoForm);

        Task<bool> surveyInfoTask = webConnectionManager.UpdateUserSurveyInfo(surveyInfoOutDTO);

        await surveyInfoTask;

        Debug.Log("controller update surveyInfo: " + surveyInfoTask.Result);

        if (surveyInfoTask.Result)
        {
            await UpdateClientInfo();
        }

        return surveyInfoTask.Result;
    }


    public async Task<bool> UpdateAvatar(Avatar selectedAvatar)
    {
        Task<bool> updateAvatarTask = webConnectionManager.UpdateClientAvatar(new AvatarOutDTO(selectedAvatar));

        await updateAvatarTask;

        Debug.Log("controller update avatar info: " + updateAvatarTask.Result);


        if (updateAvatarTask.Result)
        {
            await UpdateClientInfo();
        }

        return updateAvatarTask.Result;
    }

    public void SaveClientSurveyInfo(string value, string key)
    {


        if (clientSurveyInfoForm == null)
        {
            clientSurveyInfoForm = new WWWForm();
        }

        try
        {
            

            if (key != null && value != null)
            {
                clientSurveyInfoForm.AddField(key, value);
                Debug.Log("on key: " + key);
                Debug.Log("saved value: " + value);
            }
            else
            {
                Debug.Log("Some of the fields were blank. key: " + key + ", value: " + value);
            }
            
                /*

            switch (key)
            {
                case "sex":
                    clientSurveyInfo.Sex = value;
                    
                    break;
                case "height":
                    clientSurveyInfo.Height = long.Parse(value);
                    Debug.Log("saved value: " + value);
                    break;
                case "birthDate":
                    clientSurveyInfo.BirthDate = value;
                    Debug.Log("saved value: " + value);
                    break;
                case "wounds":
                    clientSurveyInfo.Wounds = value;
                    Debug.Log("saved value: " + value);
                    break;
                case "starterWeight":
                    clientSurveyInfo.StarterWeight = long.Parse(value);
                    Debug.Log("saved value: " + value);
                    break;
                case "pregnancy":
                    clientSurveyInfo.Pregnancy = bool.Parse(value);
                    Debug.Log("saved value: " + value);
                    break;
                case "trainingOrSportsRecord":
                    clientSurveyInfo.TrainingOrSportsRecord = value;
                    Debug.Log("saved value: " + value);
                    break;
                case "availableTrainingItems":
                    clientSurveyInfo.AvailableTrainingItems = value;
                    Debug.Log("saved value: " + value);
                    break;
                case "trainingObjectives":
                    clientSurveyInfo.TrainingObjectives = value;
                    Debug.Log("saved value: " + value);
                    break;
                case "illnesses":
                    clientSurveyInfo.Illnesses = value;
                    Debug.Log("saved value: " + value);
                    break;
                case "availableTrainingDays":
                    clientSurveyInfo.AvailableTrainingDays = value;
                    Debug.Log("saved value: " + value);
                    break;
                default:
                    Debug.Log("The current key doesnt belong to the object");
                    break;
            }*/
        }
        catch(Exception e)
        {
            Debug.Log("Parsing exception: " + e);
        }

    }

    private async Task UpdateClientInfo()
    {
        Task<Client> updateInfo = clientInfoController.GetCurrentClientInfo();
        await updateInfo;
    }
}
