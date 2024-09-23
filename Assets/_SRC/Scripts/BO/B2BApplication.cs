using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2BApplication : MonoBehaviour
{
    public static B2BApplication Instance;


    //Configurations

    //Managers
    //View
    public ViewManager viewManager;
    //WebConn
    public WebConnectionManager webConnectionManager;
    //Controller
    public ControllerManager controllerManager;
    //Error
    public NotificationManager notificationManager;
    //Repositories
    public RepositoryManager repositoryManager;
    //Services
    public ServiceManager serviceManager;

    private void Awake()
    {
        List<Manager> managers = new List<Manager>();

        if (Instance == null)
        {
            Instance = this;
            Debug.Log("Intancia principal creada");
        }

        if (viewManager == null)
        {
            viewManager = this.GetComponent<ViewManager>();
            managers.Add(viewManager);
        }

        if (webConnectionManager == null)
        {
            webConnectionManager = this.GetComponent<WebConnectionManager>();
            managers.Add(webConnectionManager);
        }

        if (controllerManager == null)
        {
            controllerManager = this.GetComponent<ControllerManager>();
            managers.Add(controllerManager);
        }

        if(notificationManager == null)
        {
            notificationManager = this.GetComponent<NotificationManager>();
            managers.Add(notificationManager);
        }

        if (repositoryManager == null)
        {
            repositoryManager = this.GetComponent<RepositoryManager>();
            managers.Add(repositoryManager);
        }

        if (serviceManager == null)
        {
            serviceManager = this.GetComponent<ServiceManager>();
            managers.Add(serviceManager);
        }

        foreach (Manager m in managers)
        {
            m.Initialize();
            Debug.Log(m.name + " initialized");
        }

    }
}
