using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class AppComponent<T> : MonoBehaviour
{
    public Button button;

    private T model;

    public T Model { get => model; set => model = value; }

    public void LoadComponent(T model, UnityEvent<T> OnClickedEvent)
    {
        this.model = model;
        PrepareUI(model);
        button.onClick.AddListener(() => {
            OnClickedEvent.Invoke(model);
        });
    }

    protected abstract void PrepareUI(T model);
}
