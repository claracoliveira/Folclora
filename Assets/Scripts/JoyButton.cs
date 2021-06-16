using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool Pressed;

    // Detecta os toques no botão até o momento que ele for solto, setando o bool "Pressed" como true.
    public void OnPointerDown(PointerEventData eventdata)
    {
      Pressed = true;
    }

    // Detecta quando o botão não está sendo tocado/é solto, setando o bool"Pressed" como false.
    public void OnPointerUp(PointerEventData eventdata)
    {
      Pressed = false;
    }

}
