using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : Manager
{
    public AuthController authController;

    public UpdateClientInfoController updateClientInfoController;

    public ComponentController componentController;

    public RoutineController routineController;

    public TrainerController trainerController;

    public ClientInfoController clientInfoController;

    public override void Initialize()
    {
        if(authController == null)
        {
            authController = this.gameObject.GetComponentInChildren<AuthController>(true);
        }

        if (updateClientInfoController == null)
        {
            updateClientInfoController = this.gameObject.GetComponentInChildren<UpdateClientInfoController>(true);
        }

        if (componentController == null)
        {
            componentController = this.gameObject.GetComponentInChildren<ComponentController>(true);
        }

        if (routineController == null)
        {
            routineController = this.gameObject.GetComponentInChildren<RoutineController>(true);
        }

        if (trainerController == null)
        {
            trainerController = this.gameObject.GetComponentInChildren<TrainerController>(true);
        }

        if (clientInfoController == null)
        {
            clientInfoController = this.gameObject.GetComponentInChildren<ClientInfoController>(true);
        }

        //throw new System.NotImplementedException();
    }
}
