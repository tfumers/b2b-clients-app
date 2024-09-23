using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class SelectAvatarMenuView : MenuView
{
    public UnityEvent<Avatar> OnSelectedAvatarChanged = new UnityEvent<Avatar>();

    public UnityEvent OnSelectSuccess = new UnityEvent();

    public UnityEvent OnSelectFailed = new UnityEvent();

    ComponentController componentController;

    UpdateClientInfoController updateClientInfoController;

    public List<ClickableAvatarComponent> clickableAvatarComponents;

    [SerializeField] Transform pos;

    Avatar selectedAvatar;

    public override Task<bool> InitializeReferences()
    {
        componentController = B2BApplication.Instance.controllerManager.componentController;
        updateClientInfoController = B2BApplication.Instance.controllerManager.updateClientInfoController;
        return base.InitializeReferences();
    }

    public override async Task<bool> LoadView()
    {

        Task<List<ClickableAvatarComponent>> prepareClickableAvatars = componentController.CreateSelectableAvatarComponent(pos, OnSelectedAvatarChanged);
        await prepareClickableAvatars;
        clickableAvatarComponents = prepareClickableAvatars.Result;

        DeselectClickableAvatars(componentController.GetClientAvatar());

        return true;
    }

    public void DeselectClickableAvatars(Avatar avatar)
    {
        foreach(ClickableAvatarComponent cac in clickableAvatarComponents)
        {
            if (cac.Avatar != avatar)
            {
                cac.DeselectAvatar();
            }
            else
            {
                cac.SelectAvatar();
                selectedAvatar = avatar;
            }
        }
    }

    public async void TrySelectAvatar()
    {
        Task<bool> updateAvatar = updateClientInfoController.UpdateAvatar(selectedAvatar);
        await updateAvatar;

        if (updateAvatar.Result)
        {
            OnSelectSuccess.Invoke();
        }
        else
        {
            OnSelectFailed.Invoke();
        }
    }
}
