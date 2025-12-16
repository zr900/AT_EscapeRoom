using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabVirtualHand : MonoBehaviour
{
    public XRSocketInteractor ghostHandSocket;
    public VirtualHandManager virtualHandManager;

    private XRGrabInteractable grab;

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
        if (virtualHandManager == null || ghostHandSocket == null)
            return;

        if (!virtualHandManager.secondHandActive)
            return;

        var manager = grab.interactionManager;
        if (manager == null)
            return;

        // Release from the controller that grabbed it
        manager.SelectExit(
            args.interactorObject,              // IXRSelectInteractor
            (IXRSelectInteractable)grab         // IXRSelectInteractable
        );

        // Force the ghost hand socket to select it
        manager.SelectEnter(
            (IXRSelectInteractor)ghostHandSocket,
            (IXRSelectInteractable)grab
        );
    }
}
