using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class AuthController : Controller
{
    StringBuilder lastErrorMsg = new StringBuilder();

    string userSession = "";

    [SerializeField] bool userLogedIn = false;

    WebConnectionManager webConnectionManager;

    NotificationManager errorManager;

    ClientInfoController clientInfoController;

    RegisterClientDTO registerClientDTO;

    public string LastErrorMsg { get => lastErrorMsg.ToString(); set { lastErrorMsg.Clear(); lastErrorMsg.Append(value); } }

    //espacio para la session

    private void Start()
    {
        webConnectionManager = B2BApplication.Instance.webConnectionManager;
        clientInfoController = B2BApplication.Instance.controllerManager.clientInfoController;
        errorManager = B2BApplication.Instance.notificationManager;
    }

    public async Task<bool> LoginTask(Dictionary<string, string> loginData)
    {
        Task<bool> loginTask = webConnectionManager.UserLogin(loginData);

        await loginTask;

        Debug.Log("controller: " + loginTask.Result);

        if(loginTask.Result == true)
        {
            await clientInfoController.GetCurrentClientInfo();
            Debug.Log("usuario logeado");
            return true;
        }
        else
        {
            LastErrorMsg = "Not logged in";
            return false;
        }

    }

    public async Task<bool> RegisterTask()
    {
        CheckRegisterData();

        Task<bool> registerTask = webConnectionManager.UserRegister(registerClientDTO);

        await registerTask;

        Debug.Log("controller: " + registerTask.Result);

        return registerTask.Result;
    }

    private void CheckRegisterData()
    {
        if ((registerClientDTO == null)||(registerClientDTO.Email=="")||(registerClientDTO.PassTest=="")||(registerClientDTO.Username==""))
        {
            errorManager.GenericError("Faltan campos de completar para el registro");
        }
    }

    public void SaveRegisterInfo(string value, string key)
    {
        if (registerClientDTO == null)
        {
            registerClientDTO = new RegisterClientDTO();
        }

        switch (key)
        {
            case "email":
                registerClientDTO.Email = value;
                break;
            case "passTest":
                registerClientDTO.PassTest = value;
                break;
            case "nationality":
                registerClientDTO.Nationality = value;
                break;
            case "username":
                registerClientDTO.Username = value;
                break;
            case "firstname":
                registerClientDTO.Firstname = value;
                break;
            case "lastname":
                registerClientDTO.Lastname = value;
                break;
            case "dni":
                registerClientDTO.Dni = value;
                break;
            case "sex":
                registerClientDTO.Sex = value;
                break;
            case "icon":
                registerClientDTO.Icon = value;
                break;
            case "avatar":
                registerClientDTO.Avatar = value;
                break;
            default:
                Debug.Log("invalid field");
                break;
        }
    }

    public bool CheckUserLogin(bool throwError)
    {
        bool result = userLogedIn;
        
        if(throwError && !userLogedIn)
        {
            errorManager.GenericError("MACHO, NO ESTÁS LOGEADO");
        }

        return result;
    }


}
