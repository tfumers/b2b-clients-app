using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotificationManager : Manager
{
    ViewManager viewManager;

    static GenericMessage emergentMessage;
    public static GenericMessage EmergentMessage { get => emergentMessage; set => emergentMessage = value; }

    public override void Initialize()
    {
        viewManager = B2BApplication.Instance.viewManager;

    }

    public void ActivitiesNotSaved(UnityEvent onAcceptButton)
    {
        string message = "Si no completas todas las actividades de hoy, no se guardarán los cambios. ¿Estás seguro que deseas salir?";
        EmergentMessage = new GenericMessage("¡Atención!", message, "Volver al menú", "Continuar entrenando", true, onAcceptButton, null);
        TurnNotificationEmergentOn();
    }

    public void UserDontHaveStatus(int status)
    {
        string message = "No puedes realizar esa acción: ";

        switch (status)
        {
            case Constant.CLIENT_STATUS_NO_SURVEY:
                message += "Antes necesitas completar la encuesta.";
                break;
            case Constant.CLIENT_STATUS_NO_ROUTINE:
                message += "Primero necesitas elegir un personal trainer.";
                break;
            case Constant.CLIENT_STATUS_WAITING:
                message += "Necesitas esperar tu rutina.";
                break;
            case Constant.CLIENT_STATUS_BANNED:
                message += "No puedes usar la aplicación hasta nuevo aviso.";
                break;
            default:
            case 0:
                    break;
        }

        EmergentMessage = new GenericMessage("Atención:", "");
        TurnNotificationEmergentOn();
    }

    public void NotClientLoggin()
    {
        EmergentMessage = new GenericMessage("Error", "");
        TurnNotificationEmergentOn();
    }

    public void GenericError(string arg)
    {
        EmergentMessage = new GenericMessage("Error", arg);
        TurnNotificationEmergentOn();
    }

    public void SuccessMessage(string titleMsg, UnityEvent onAcceptButton)
    {
        string message = "¡La operación se ha completado con éxito!";
        EmergentMessage = new GenericMessage(titleMsg, message, "Aceptar", "", false, onAcceptButton, null);
        TurnNotificationEmergentOn();
    }

    public void FailureMessageReTry(string titleMsg, UnityEvent onAcceptButton)
    {
        string message = "Las credenciales de ingreso son invalidas. Reintente nuevamente.";
        EmergentMessage = new GenericMessage(titleMsg, message, "Aceptar", "", false, onAcceptButton, null);
        TurnNotificationEmergentOn();
    }

    public void ClientDontHaveSurvey(UnityEvent onAcceptButton)
    {
        string message = "Antes que elijas un entrenador, necesitamos que completes la encuesta. Es necesaria para garantizar que tu entrenamiento sea personalizado. Presiona Aceptar para realizarla.";
        EmergentMessage = new GenericMessage("Atención:", message, "Aceptar", "Cancelar", true, onAcceptButton, null);
        TurnNotificationEmergentOn();
    }

    private void TurnNotificationEmergentOn()
    {
        Debug.Log("Emergent called");
        viewManager.TurnEmergentOn(viewManager.notifactionEmergentView);
    }
}
