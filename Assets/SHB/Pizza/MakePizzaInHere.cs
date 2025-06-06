using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MakePizzaInHere : MonoBehaviour
{
    private int pizzaTask = 0;
    public GameObject finalPizza;

    [Header("보이기 위한 용도")]
    public GameObject dough;
    public GameObject hams;
    public GameObject hands;

    void Start()
    {
        dough.SetActive(false);
        hams.SetActive(false);
        hands.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);

        PizzaStuff pizzastuff = collision.gameObject.GetComponent<PizzaStuff>();
        if (pizzastuff == null) return;

        if (pizzastuff.dough == true && pizzaTask == 0)
        {
            pizzaTask++;
            Destroy(collision.gameObject);
            dough.SetActive(true);
        }

        if (pizzaTask == 1)
        {
            if (pizzastuff.hams == true)
            {
                Destroy(collision.gameObject);
                hams.SetActive(true);
                pizzaTask++;
            }

            else if (pizzastuff.hands == true)
            {
                Destroy(collision.gameObject);
                hands.SetActive(true);
                pizzaTask++;
            }
        }

        if (pizzaTask == 2)
        {
            finalPizza.GetComponent<XRGrabInteractable>().enabled = true;
            Destroy(gameObject);
        }
    }
}