using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
// using grab events, socket interactors, interaction manager

public class GrabVirtualHand : MonoBehaviour
{
    public XRSocketInteractor ghostHandSocket; //socket on the virtual hand
    public VirtualHandManager virtualHandManager; //virtual hand manager script on virtual hand

    private XRGrabInteractable grab;
    private bool transferring = false;   // 🔑 guard flag

    void Awake() //when the capsule is created 
    {
        grab = GetComponent<XRGrabInteractable>(); //find capsule's grab component
        grab.selectEntered.AddListener(OnGrabbed); //listen for capsule being grabbed event and then call onGrabbed
    }

    void OnDestroy()
    {
        grab.selectEntered.RemoveListener(OnGrabbed); //remove event listener after
    }

    void OnGrabbed(SelectEnterEventArgs args) //when the capsule is grabbed
    {
        //stop calling forever and prevent stack overflow crash
        if (transferring)
            return;

        if (virtualHandManager == null || ghostHandSocket == null) //check for virtual hand manager and virtual hand socket
            return;

        if (!virtualHandManager.secondHandActive) //if virtual hand is on or not
            return;

        var manager = grab.interactionManager; //make sure interaction manager exists
        if (manager == null)
            return;

        transferring = true;

        //release the capsule from controller
        manager.SelectExit(
            args.interactorObject,
            (IXRSelectInteractable)grab
        );

        //capsule held by virtual hand (socket)
        manager.SelectEnter(
            (IXRSelectInteractor)ghostHandSocket,
            (IXRSelectInteractable)grab
        );
    }
}

//listening for grab events, then check if virtual hand is active, release capsule from controller and give it to virtual hand socket
// if virtual hand is not active then grab will work normally

