using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ViewManager : Manager
{
    [SerializeField] MenuView starterView;

    [SerializeField] Canvas activeCanvas;

    [SerializeField] List<MenuView> AppMenues = new List<MenuView>();
    [SerializeField] List<EmergentView> AppEmergentWindows = new List<EmergentView>();

    List<View> AppViews;
    List<EmergentView> EmergentOrder = new List<EmergentView>();

    EmergentView activeEmergent;

    public MenuView activeMenu;

    public GenericMessageEmergentView notifactionEmergentView;

    public override void Initialize()
    {
        AppViews = new List<View>();
        //algun valor para comprobar si las listas no estan vacias. Si lo estan, comunicar

        AppMenues = new List<MenuView>(activeCanvas.GetComponentsInChildren<MenuView>(true));

        foreach (MenuView menu in AppMenues)
        {
            AppViews.Add(menu);

            TurnMenuOff(menu);
        }

        foreach (EmergentView emergent in AppEmergentWindows)
        {
            AppViews.Add(emergent);
            TurnEmergentOff(emergent);
        }

        TurnMenuOn(starterView);
    }

    bool CompareViewExistence(View view)
    {
        return AppViews.Contains(view);
    }

    void DebugError(string receivedMessage)
    {
        Debug.LogError(receivedMessage);
    }

    async Task<bool> TurnViewOn(View onView)
    {
        bool viewExist = CompareViewExistence(onView);
        if (!viewExist)
        {
            DebugError("That interface doesn't exist on none of the lists");
        }

        Task<bool> viewOn = onView.SetOn();
        await viewOn;
        return viewOn.Result;
    }

    void TurnViewOff(View offView)
    {
        if (offView != null)
        {
            offView.SetOff();
        }
    }

    void UpdateEmergentOrder(EmergentView receivedEmergent, string sender = "Add")
    {
        if (sender == "Remove")
        {
            EmergentOrder.Remove(receivedEmergent);
            return;
        }

        EmergentOrder.Add(receivedEmergent);
        receivedEmergent.ChangeSortingOrder(EmergentOrder.Count);
        activeEmergent = receivedEmergent;
    }

    public async void TurnEmergentOn(EmergentView onEmergent)
    {
        Task<bool> turnViewOn = TurnViewOn(onEmergent);
        await turnViewOn;
        UpdateEmergentOrder(onEmergent, "Add");
    }

    public void TurnEmergentOff(EmergentView offEmergent)
    {
        TurnViewOff(offEmergent);
        UpdateEmergentOrder(offEmergent, "Remove");
    }

    public async void TurnMenuOn(MenuView onMenu)
    {
        if (activeMenu != onMenu)
        {
            StartLoader(); //iniciamos la ventana emergente que nos indica si está cargando
            Task<bool> turnViewOn = TurnViewOn(onMenu);
            await turnViewOn;

            if (turnViewOn.Result)//Si el task da el resultado positivo, quiere decirque el menú cargó
            {
                TurnViewOff(activeMenu);
                activeMenu = onMenu;
            }

            if (activeMenu == null && turnViewOn.Result)
            {

                activeMenu = onMenu;
            }
        }

    }

    private void TurnMenuOff(MenuView offMenu)
    {
        TurnViewOff(offMenu);
    }

    private void StartLoader()
    {

    }

}

