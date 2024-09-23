using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LocalFilesService : Service
{
    NotificationManager errorManager;

    [SerializeField] Texture[] avatartexture = new Texture[0];

    [SerializeField] Sprite[] trainingImageSprite = new Sprite[0];

    [SerializeField] RuntimeAnimatorController[] trainingAnimatorController = new RuntimeAnimatorController[0];

    List<Avatar> avatars;

    List<TrainingImage> trainingImages;

    List<TrainingVideo> trainingVideos;

    public Transform avatarTransform;

    [SerializeField] Avatar avatarPrefab;

    public override void Initialize()
    {
        errorManager = B2BApplication.Instance.notificationManager;

        InitializeAvatars();

        trainingImages = InitializeTrainingImages();

        trainingVideos = InitializeTrainingVideos();


        Debug.Log("Iniciado");
    }

    public Avatar GetAvatarById(string stringId)
    {
        int id = 0;

        try
        {
            id = int.Parse(stringId);
        }
        catch(Exception e)
        {
            //Debug.Log("Avatar Exception: " + e);
        }


        foreach(Avatar av in avatars)
        {
            if (av.Id == id)
            {
                Debug.Log("Avatar found. ID: " + id);
                return av;

            }
        }
        Debug.Log("Avatar NOT found. ID: " + id);
        return avatars[0];
    }

    public List<Avatar> GetAvatars()
    {
        if (avatars != null)
        {
            return avatars;
        }

        errorManager.GenericError("No hay avatares cargados. Falló Initializer");
        return new List<Avatar>();
    }

    public TrainingImage GetTrainingImageByName(string name)
    {
        if (trainingImages == null)
        {
            errorManager.GenericError("Fallo en el training img, no está inicializado");
        }


        foreach(TrainingImage trnImg in trainingImages)
        {
            if(trnImg.Name == name)
            {
                return trnImg;
            }
        }

        if (trainingImages.Count > 0)
        {
            return trainingImages[0];
        }
        else
        {
            errorManager.GenericError("No existe un objeto en el trainingImage por defecto");
            return null;
        }
    }

    public TrainingVideo GetTrainingVideoByName(string name)
    {
        if (trainingImages == null)
        {
            errorManager.GenericError("Fallo en el training img, no está inicializado");
        }


        foreach (TrainingVideo trnVideo in trainingVideos)
        {
            if (trnVideo.Name == name)
            {
                return trnVideo;
            }
        }

        if (trainingVideos.Count > 0)
        {
            return trainingVideos[0];
        }
        else
        {
            errorManager.GenericError("No existe un objeto en el trainingImage por defecto");
            return null;
        }
    }

    private void InitializeAvatars()
    {
        if (avatarPrefab != null)
        {
            avatars = new List<Avatar>();
            for(int i = 0; i < avatartexture.Length; i++)
            {
                Avatar currAvatar = Instantiate(avatarPrefab, avatarTransform);
                currAvatar.CreateAvatar(i + 1, avatartexture[i]);
                currAvatar.gameObject.name = "Avatar" + currAvatar.Id;
                avatars.Add(currAvatar);
            }
            return;
        }

        errorManager.GenericError("The avatar prefab does not exist!");
        return;
    }

    private List<TrainingImage> InitializeTrainingImages()
    {
        List<TrainingImage> obtTrainingImages = new List<TrainingImage>();

        for(int i = 0; i < trainingImageSprite.Length; i++)
        {
            obtTrainingImages.Add(new TrainingImage(i, trainingImageSprite[i].name, trainingImageSprite[i]));
        }

        return obtTrainingImages;
    }

    private List<TrainingVideo> InitializeTrainingVideos()
    {
        List<TrainingVideo> obtainedTrainingVideos = new List<TrainingVideo>();
        for(int i = 0; i < trainingAnimatorController.Length; i++)
        {
            obtainedTrainingVideos.Add(new TrainingVideo(i, trainingAnimatorController[i].name, trainingAnimatorController[i]));
        }

        return obtainedTrainingVideos;
    }
}
