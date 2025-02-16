using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Panel : MonoBehaviour
{
    public List<Button> buttons;

    public virtual void PanelOpen()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void PanelClose()
    {
        this.gameObject.SetActive(false);
    }
}
