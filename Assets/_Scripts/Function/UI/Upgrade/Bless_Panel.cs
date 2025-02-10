using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bless_Panel : Panel
{
    public List<Button> attack_Left;
    public List<Button> attack_Right;
    public List<Button> deffence_Left;
    public List<Button> deffence_Right;
    public List<Button> utility_Left;
    public List<Button> utility_Right;

    private void Awake()
    {
        ButtonInit(attack_Left);
        ButtonInit(attack_Right);
        ButtonInit(deffence_Left);
        ButtonInit(deffence_Right);
        ButtonInit(utility_Left);
        ButtonInit(utility_Right);
    }

    private void ButtonInit(List<Button> buttons)
    {

        for (int i = 0; i < buttons.Count - 3; i++)
        {
            int k = i + 1;
            buttons[i].onClick.AddListener(() => buttons[k].interactable = true);
        }
        buttons[buttons.Count - 2].onClick.AddListener(() => buttons[buttons.Count - 1].interactable = true);
    }

}
