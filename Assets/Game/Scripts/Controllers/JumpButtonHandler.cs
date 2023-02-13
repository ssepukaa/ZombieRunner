using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class JumpButtonHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private PlayerControll _playerController;

    public void OnPointerDown(PointerEventData eventData)
    {
        _playerController.CheckForJump();
    }
}
