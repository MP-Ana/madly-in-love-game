using UnityEngine;

public class Objects_System : MonoBehaviour
{
    private Player_Controller playerController;

    [SerializeField] private int amountItens;
    private bool touchedPlayer;
    private LayerMask defaultLayer;
    private float distanceTouch = 2f;


    [System.Flags]
    public enum Objetcs
    {
        Milk,
        Cookies,
        Extra
    }

    public Objetcs thisObj;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        defaultLayer = LayerMask.GetMask("Default");
    }

    private void Update()
    {
        touchedPlayer = Physics.CheckSphere(transform.position, distanceTouch, defaultLayer);

        if (touchedPlayer)
        {
            if (Input.GetKeyDown(KeyCode.C) || Input.GetKey(KeyCode.C))
            {
                switch (thisObj)
                {
                    case Objetcs.Milk:
                        playerController.milk = playerController.milk + amountItens;                      //Aumenta Leite
                        playerController.milkAmount.text = "x" + playerController.milk.ToString();        //UI Texto
                        Destroy(gameObject);
                        break;
                    case Objetcs.Cookies:
                        playerController.cookies = playerController.cookies + amountItens;                //Aumenta Cookies
                        playerController.cookieAmount.text = "x" + playerController.cookies.ToString();   //UI Texto
                        Destroy(gameObject);
                        break;
                }
            }
        }
    }
}
