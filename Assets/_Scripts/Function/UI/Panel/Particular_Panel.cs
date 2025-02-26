using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particular_Panel : MonoBehaviour
{
    [SerializeField] GameObject particularMember;
    [SerializeField] RectTransform content_Rect;
    private List<ParticularMember> m_Members;

    private void Awake()
    {
        for (int i = 0; i < 19; i++)
        {
            ParticularMember element = Instantiate(particularMember, content_Rect).GetComponent<ParticularMember>();
            m_Members.Add(element);

            switch (i)
            {
                case 0:
                    element.m_Icon = null;
                    element.m_Name.text = "최대체력";
                    element.m_Value.text = "Value";
                    break;
                case 1:
                    element.m_Icon = null;
                    element.m_Name.text = "체력회복";
                    element.m_Value.text = "Value";
                    break;
                case 2:
                    element.m_Icon = null;
                    element.m_Name.text = "방어력";
                    element.m_Value.text = "Value";
                    break;
                case 3:
                    element.m_Icon = null;
                    element.m_Name.text = "이동속도";
                    element.m_Value.text = "Value";
                    break;
                case 4:
                    element.m_Icon = null;
                    element.m_Name.text = "공격력";
                    element.m_Value.text = "Value";
                    break;
                case 5:
                    element.m_Icon = null;
                    element.m_Name.text = "공격속도";
                    element.m_Value.text = "Value";
                    break;
                case 6:
                    element.m_Icon = null;
                    element.m_Name.text = "최명타 확률";
                    element.m_Value.text = "Value";
                    break;
                case 7:
                    element.m_Icon = null;
                    element.m_Name.text = "Name";
                    element.m_Value.text = "Value";
                    break;
                case 8:
                    element.m_Icon = null;
                    element.m_Name.text = "Name";
                    element.m_Value.text = "Value";
                    break;
                case 9:
                    element.m_Icon = null;
                    element.m_Name.text = "Name";
                    element.m_Value.text = "Value";
                    break;
                case 10:
                    element.m_Icon = null;
                    element.m_Name.text = "Name";
                    element.m_Value.text = "Value";
                    break;
                case 11:
                    element.m_Icon = null;
                    element.m_Name.text = "Name";
                    element.m_Value.text = "Value";
                    break;
                case 12:
                    element.m_Icon = null;
                    element.m_Name.text = "Name";
                    element.m_Value.text = "Value";
                    break;
                case 13:
                    element.m_Icon = null;
                    element.m_Name.text = "Name";
                    element.m_Value.text = "Value";
                    break;
                case 14:
                    element.m_Icon = null;
                    element.m_Name.text = "Name";
                    element.m_Value.text = "Value";
                    break;
                case 15:
                    element.m_Icon = null;
                    element.m_Name.text = "Name";
                    element.m_Value.text = "Value";
                    break;
                case 16:
                    element.m_Icon = null;
                    element.m_Name.text = "Name";
                    element.m_Value.text = "Value";
                    break;
                case 17:
                    element.m_Icon = null;
                    element.m_Name.text = "Name";
                    element.m_Value.text = "Value";
                    break;
                case 18:
                    element.m_Icon = null;
                    element.m_Name.text = "Name";
                    element.m_Value.text = "Value";
                    break;

            }
        }
    }

}
