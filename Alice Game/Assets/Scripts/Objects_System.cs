using UnityEngine;

public class Objects_System : MonoBehaviour
{
    private Player_Controller playerController;
    private Interact_System interactSystem;
    

    [SerializeField] private int amountItens;
    [SerializeField] private Sprite icon;
    private bool touchedPlayer;
    private LayerMask defaultLayer;
    private float distanceTouch = 2f;
    private GameObject player;

    [System.Flags]
    public enum Objects
    {
        Milk,
        Cookies,
        Collectable,
        Interactable,
        None,
        Extra
    }

    public Objects thisObj;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<Player_Controller>();
        interactSystem = player.GetComponent<Interact_System>();
        defaultLayer = LayerMask.GetMask("Default");
    }

    private void Update()
    {
        touchedPlayer = Physics.CheckSphere(transform.position, distanceTouch, defaultLayer);

        if (touchedPlayer)
        {
            if (!Input.GetKeyDown(KeyCode.C))
            {
                return;
            }
            else
            {
                switch (thisObj)
                {
                    case Objects.Milk:
                        playerController.milk = playerController.milk + amountItens;                      //Aumenta Leite
                        playerController.milkAmount.text = "x" + playerController.milk.ToString();        //UI Texto
                        Destroy(gameObject);
                        break;
                    case Objects.Cookies:
                        playerController.cookies = playerController.cookies + amountItens;                //Aumenta Cookies
                        playerController.cookieAmount.text = "x" + playerController.cookies.ToString();   //UI Texto
                        Destroy(gameObject);
                        break;
                    case Objects.Collectable:
                        Collect();
                        break;
                    case Objects.Interactable:
                        Interact(playerController.holdingMaterial);
                        break;
                }
            }
        }
    }

    private void Collect() //Transformar em return string para o sistema de dialogo
    {
        playerController.holdingMaterial = gameObject.GetComponent<MeshRenderer>().material;
        interactSystem.inventoryImg.sprite = icon;
        Destroy(gameObject);
    }

    //alterar depois, meio sem sentido
    private void Interact(Material inventoryMaterial)
    {
        if (inventoryMaterial == null)
        {
            return;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = inventoryMaterial;
            interactSystem.inventoryImg.sprite = null;
            playerController.holdingMaterial = null;
        }
    }
}
