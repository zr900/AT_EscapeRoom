using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabVirtualHand : MonoBehaviour
{
    public XRSocketInteractor ghostHandSocket;
    public VirtualHandManager virtualHandManager;

    private XRGrabInteractable grab;
    private bool transferring = false;   // 🔑 guard flag

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrabbed);
    }

    void OnDestroy()
    {
        grab.selectEntered.RemoveListener(OnGrabbed);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        // Stop infinite recursion
        if (transferring)
            return;

        if (virtualHandManager == null || ghostHandSocket == null)
            return;

        if (!virtualHandManager.secondHandActive)
            return;

        var manager = grab.interactionManager;
        if (manager == null)
            return;

        transferring = true;

        // Release from controller
        manager.SelectExit(
            args.interactorObject,
            (IXRSelectInteractable)grab
        );

        // Hand to ghost hand socket
        manager.SelectEnter(
            (IXRSelectInteractor)ghostHandSocket,
            (IXRSelectInteractable)grab
        );
    }
}

