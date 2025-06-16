using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MakePizzaInHere : MonoBehaviour
{
    private int pizzaTask = 0; // Tracks progress of pizza making steps
    public GameObject finalPizza; // Reference to the completed pizza object

    // (Visuals shown during the pizza assembly process)
    [Header("보이기 위한 용도")]
    public GameObject dough; // Visual for dough
    public GameObject hams; // Visual for hams
    public GameObject hands; // Visual for hands

    // Hides all ingredient visuals at the start
    void Start()
    {
        dough.SetActive(false);
        hams.SetActive(false);
        hands.SetActive(false);
    }

    // Handles collision logic when a pizza ingredient is added
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);

        PizzaStuff pizzastuff = collision.gameObject.GetComponent<PizzaStuff>();
        if (pizzastuff == null) return;

        // Step 1: Only accept dough first
        if (pizzastuff.dough == true && pizzaTask == 0)
        {
            pizzaTask++;
            Destroy(collision.gameObject);
            dough.SetActive(true);
            finalPizza.GetComponent<FinalPizza>().dough = true;
        }

        // Step 2: After dough, accept either hams or hands
        if (pizzaTask == 1)
        {
            if (pizzastuff.hams == true)
            {
                Destroy(collision.gameObject);
                hams.SetActive(true);
                pizzaTask++;
                finalPizza.GetComponent<FinalPizza>().hams = true;
            }
            else if (pizzastuff.hands == true)
            {
                Destroy(collision.gameObject);
                hands.SetActive(true);
                pizzaTask++;
                finalPizza.GetComponent<FinalPizza>().hands = true;
            }
        }

        // Step 3: When both steps are done, enable final pizza and destroy this station
        if (pizzaTask == 2)
        {
            finalPizza.GetComponent<XRGrabInteractable>().enabled = true;
            Destroy(gameObject);
        }
    }
}
