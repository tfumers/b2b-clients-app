using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class WebConnectionManager : Manager
{
    public const string BASE_URL = "http://localhost:8080/api/v1.0";

    public string JSESSION = "";

    private Dictionary<string, string> urlDictionary;

    public override void Initialize()
    {
        urlDictionary = new Dictionary<string, string>();
        //Se podrían cargar desde un archivo o una base de datos las url

        urlDictionary.Add("login", BASE_URL + "/login");
        urlDictionary.Add("register", BASE_URL + "/clients");

        //Updates
        urlDictionary.Add("updateSurveyInfo", BASE_URL + "/clients/current/surveyInfo");
        urlDictionary.Add("updateLoginInfo", BASE_URL + "/clients/current/loginInfo");
        urlDictionary.Add("updateClientAvatar", BASE_URL + "/clients/current/avatar");
        urlDictionary.Add("updateCompletedDailyActivities", BASE_URL + "/clients/current/routines/daily");

        //Get
        urlDictionary.Add("getSelectedTrainer", BASE_URL + "/clients/current/selectedTrainer");
        urlDictionary.Add("getRoutines", BASE_URL + "/clients/current/routines");
        urlDictionary.Add("getCurrentClient", BASE_URL + "/clients/current");
        urlDictionary.Add("getTrainers", BASE_URL + "/clients/current/availableTrainers");

        //Post
        urlDictionary.Add("postTrainerClientRelation", BASE_URL + "/clients/current/newRelation");
        urlDictionary.Add("postAndReturnRequiredTrainings", BASE_URL + "/clients/current/trainings");
        //throw new NotImplementedException();
    }

    public async Task<bool> UserLogin(Dictionary<string, string> loginData)
    {
        Task<ResponseDTO> userLogin = this.ConnectionTask(urlDictionary["login"], DictionaryToForm(loginData));

        await userLogin;

        return CheckHTTPStatusOk(userLogin.Result);
    }

    public async Task<bool> UserRegister(RegisterClientDTO registerClientDTO)
    {
        Task<ResponseDTO> userRegister = this.ConnectionTask(urlDictionary["register"], registerClientDTO.ToForm());

        await userRegister;

        return CheckHTTPStatusOk(userRegister.Result);
    }


    //Updates
    public async Task<bool> UpdateUserSurveyInfo(SurveyInfoOutDTO surveyInfoOutDTO)
    {

        Task<ResponseDTO> surveyInfo = this.ConnectionTask(urlDictionary["updateSurveyInfo"], surveyInfoOutDTO.ToForm());

        await surveyInfo;

        return CheckHTTPStatusOk(surveyInfo.Result);
    }


    public async Task<bool> UpdateClientAvatar(AvatarOutDTO avatarOutDTO)
    {
        Task<ResponseDTO> postNewClientAvatar = this.ConnectionTask(urlDictionary["updateClientAvatar"], avatarOutDTO.ToForm());

        await postNewClientAvatar;

        return CheckHTTPStatusOk(postNewClientAvatar.Result);
    }

    public async Task<ResponseDTO> UpdateCompletedDailyActivities(CompletedDailyActivitiesOutDTO completedActivityOutDTO)
    {
        Task<ResponseDTO> updateCompletedDailyActivity = this.ConnectionTask(urlDictionary["updateCompletedDailyActivities"], completedActivityOutDTO.ToForm());

        await updateCompletedDailyActivity;

        return updateCompletedDailyActivity.Result;
    }

    //Get
    public async Task<ResponseDTO> GetSelectedTrainer()
    {
        Task<ResponseDTO> selectedTrainer = this.ConnectionTask(urlDictionary["getSelectedTrainer"], null);

        await selectedTrainer;

        return selectedTrainer.Result;
    }

    public async Task<ResponseDTO> GetRoutines()
    {
        Task<ResponseDTO> clientRoutines = this.ConnectionTask(urlDictionary["getRoutines"], null);

        await clientRoutines;

        return clientRoutines.Result;
    }
    public async Task<ResponseDTO> GetCurrentClientInfo()
    {

        Task<ResponseDTO> getCurrentClient = this.ConnectionTask(urlDictionary["getCurrentClient"], null);

        await getCurrentClient;

        return getCurrentClient.Result;
    }

    public async Task<ResponseDTO> GetTrainers()
    {

        Task<ResponseDTO> getTrainers = this.ConnectionTask(urlDictionary["getTrainers"], null);

        await getTrainers;

        return getTrainers.Result;
    }

    //Post
    public async Task<bool> PostNewClientTrainerRelation(NewTrainerClientRelationOutDTO newTrainerClientRelationOutDTO)
    {

        Task<ResponseDTO> postNewClientRelation = this.ConnectionTask(urlDictionary["postTrainerClientRelation"], newTrainerClientRelationOutDTO.ToForm());

        await postNewClientRelation;

        return CheckHTTPStatusOk(postNewClientRelation.Result);
    }

    public async Task<ResponseDTO> PostAndReturnRequiredTrainings(TrainingIDsOutDTO trainingIDsOutDTO)
    {

        Task<ResponseDTO> postAndReturnRequiredTrainings = this.ConnectionTask(urlDictionary["postAndReturnRequiredTrainings"], trainingIDsOutDTO.ToForm());

        await postAndReturnRequiredTrainings;

        return postAndReturnRequiredTrainings.Result;
    }

    protected async Task<ResponseDTO> ConnectionTask(string url, WWWForm form)
    {
        bool IsDone = false;
        ResponseDTO newResponse = new ResponseDTO();

        Action<ResponseDTO> ConnectionCallback = (returnedResponse) =>
        {
            newResponse = returnedResponse;
            //Debug.Log("desde el callback: " + response);//
            IsDone = true;
        };
        if (form!=null)
        {

            StartCoroutine(PostCoroutine(url, form, ConnectionCallback));
        }
        else
        {
            StartCoroutine(GetCoroutine(url, ConnectionCallback));
        }


        while (!IsDone)
        {
            await Task.Yield();

        }

        //Debug.Log("salio del bucle, retorno una response = " + response.ToString());//
        return newResponse;
    }

    protected IEnumerator PostCoroutine(string url, WWWForm form, Action<ResponseDTO> responseCallback)
    {

        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        ResponseDTO newResponse;

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            newResponse = new ResponseDTO(www.responseCode, "unexpected error");
        }
        else
        {
            newResponse = new ResponseDTO(www.responseCode, www.downloadHandler.text);
        }

        responseCallback(newResponse);
    }

    protected IEnumerator GetCoroutine(string url, Action<ResponseDTO> responseCallback)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        ResponseDTO newResponse;

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            newResponse = new ResponseDTO(www.responseCode, "unexpected error");
        }
        else
        {
            newResponse = new ResponseDTO(www.responseCode, www.downloadHandler.text);
        }

        responseCallback(newResponse);
    }

    protected WWWForm DictionaryToForm(Dictionary<string, string> data)
    {
        WWWForm form = new WWWForm();
        foreach (var item in data)
        {
            form.AddField(item.Key, item.Value);
        }
        return form;
    }

    private bool CheckHTTPStatusOk(ResponseDTO response)
    {
        if (response.HttpStatus == 200) {
            return true;
        }

        return false;
    }

}

