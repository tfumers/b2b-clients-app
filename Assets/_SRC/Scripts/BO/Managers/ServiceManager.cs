using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ServiceManager : Manager
{
    public LocalFilesService localFilesService;

    public TrainerService trainerService;

    List<Service> services;

    public override void Initialize()
    {
        services = new List<Service>();
        if (localFilesService == null)
        {
            localFilesService = this.GetComponent<LocalFilesService>();
        }
        services.Add(localFilesService);

        if (trainerService == null)
        {
            trainerService = this.GetComponent<TrainerService>();
        }
        services.Add(trainerService);

        foreach (Service s in services)
        {
            s.Initialize();
        }
    }
}
