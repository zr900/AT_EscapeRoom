using UnityEngine;


public class VirtualHandManager : MonoBehaviour
{
    
    public GameObject ghostHand; //virtual hand 
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor ghostSocket; //socket on virtual hand

    public bool secondHandActive = false; 

    void Start()
    {
        ghostHand.SetActive(false); //virtual hand is hidden in the beginning
    }

    // Update is called once per frame
    public void ToggleSecondHand()
    {
        secondHandActive = !secondHandActive;
        ghostHand.SetActive(secondHandActive);
    }
    //function called by UI button. If virtual hand is off when pressed virtual hand becomes active and state becomes active, and vice versa
}
