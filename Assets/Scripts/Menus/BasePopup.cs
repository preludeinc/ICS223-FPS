using UnityEngine;

public class BasePopup : MonoBehaviour
{
    virtual public void Open()
    {
        if (!IsActive())
        {
            this.gameObject.SetActive(true);
            Messenger.Broadcast(GameEvent.POPUP_OPENED);
        } else
        {
            Debug.LogError(this + ".Open() - trying to open a popup that is active!");
        }
    }

    virtual public void Close()
    {
        if (IsActive())
        {
            this.gameObject.SetActive(false);
            Messenger.Broadcast(GameEvent.POPUP_CLOSED);
        } else
        {
            Debug.LogError(this + ".Closed() - trying to close a popup that is not open!");
        }
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}
