using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MenuView
{
    ClientInfoController clientInfoController;
    ComponentController componentController;
    public RawImage avatarImage;

    [SerializeField] LevelBarComponent levelBarComponent;

    public override async Task<bool> InitializeReferences()
    {
        clientInfoController = B2BApplication.Instance.controllerManager.clientInfoController;
        componentController = B2BApplication.Instance.controllerManager.componentController;

        return true;
    }

    public override async Task<bool> LoadView()
    {
        Task<Client> getClientInfo = clientInfoController.GetCurrentClientInfo();
        await getClientInfo;

        ChangeLoadedAvatarImage();

        levelBarComponent.PrepareLevelBarComponent(getClientInfo.Result);

        return true;
    }

    private void ChangeLoadedAvatarImage()
    {
        if (avatarImage != null)
        {
            Avatar avatar = componentController.GetClientAvatar();

            avatarImage.texture = avatar.Image;
        }
        else
        {
            Debug.Log("NO se ha seleccionado el avatar image, del Main Menu");
        }
        
    }
}
