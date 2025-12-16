using UnityEngine;


public class VirtualHandManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject ghostHand;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor ghostSocket;

    public bool secondHandActive = false;

    void Start()
    {
        ghostHand.SetActive(false);
    }

    // Update is called once per frame
    public void ToggleSecondHand()
    {
        secondHandActive = !secondHandActive;
        ghostHand.SetActive(secondHandActive);
    }
}
