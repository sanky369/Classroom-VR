using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
//using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;
using UnityEngine.XR;

public class UI_InteractionController : MonoBehaviour
{
    [SerializeField]
    GameObject UIController;

    [SerializeField]
    GameObject BaseController;

    [SerializeField]
    private XRNode xrNode = XRNode.LeftHand;
    //InputActionReference inputActionReference_UISwitcher;

    private List<InputDevice> devices = new List<InputDevice>();

    private InputDevice device;

    bool isUICanvasActive = false;

    //[SerializeField]
    //GameObject UICanvasGameobject;

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(xrNode, devices);
        device = devices[0];
    }
    private void OnEnable()
    {
        //inputActionReference_UISwitcher.action.performed += ActivateUIMode;
        if (!device.isValid)
        {
            GetDevice();
        }
    }
    private void OnDisable()
    {
        //inputActionReference_UISwitcher.action.performed -= ActivateUIMode;

    }

    private void Start()
    {
        //Deactivating UI Canvas Gameobject by default
        //if (UICanvasGameobject != null)
        //{
            //UICanvasGameobject.SetActive(false);

        //}

        //Deactivating UI Controller by default
        UIController.GetComponent<XRRayInteractor>().enabled = false;
        UIController.GetComponent<XRInteractorLineVisual>().enabled = false;
    }

    private void Update()
    {
        bool triggerValue;
        if(device.TryGetFeatureValue(CommonUsages.primaryButton, out triggerValue) && triggerValue)
        {
            ActivateUIMode();
        }
    }

    /// <summary>
    /// This method is called when the player presses UI Switcher Button which is the input action defined in Default Input Actions.
    /// When it is called, UI interaction mode is switched on and off according to the previous state of the UI Canvas.
    /// </summary>
    /// <param name="obj"></param>
    //private void ActivateUIMode(InputAction.CallbackContext obj)
    private void ActivateUIMode()
    {
        if (!isUICanvasActive)
        {
            isUICanvasActive = true;

            //Activating UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
            UIController.GetComponent<XRRayInteractor>().enabled = true;
            UIController.GetComponent<XRInteractorLineVisual>().enabled = true;

            //Deactivating Base Controller by disabling its XR Direct Interactor
            BaseController.GetComponent<XRDirectInteractor>().enabled = false;



            //Activating the UI Canvas Gameobject
            //UICanvasGameobject.SetActive(true);
        }
        else
        {
            isUICanvasActive = false;

            //De-Activating UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
            UIController.GetComponent<XRRayInteractor>().enabled = false;
            UIController.GetComponent<XRInteractorLineVisual>().enabled = false;

            //Activating Base Controller by disabling its XR Direct Interactor
            BaseController.GetComponent<XRDirectInteractor>().enabled = true;

            //De-Activating the UI Canvas Gameobject
            //UICanvasGameobject.SetActive(false);
        }

    }
}
