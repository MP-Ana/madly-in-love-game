using TMPro;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 5f, gravity = -9.8f, groundDistance = 0.4f, jump = 1.5f;
    private Vector3 velocity;

    [SerializeField] private Transform groundCheck;
    public Transform playerBody;
    private LayerMask groundMask;

    //Acessado por Objects_System
    [HideInInspector] public int cookies = 0, milk = 0;
    public TextMeshProUGUI milkAmount, cookieAmount;
    [HideInInspector] public Material holdingMaterial;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        groundMask = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        /*
        ========================
            G R A V I D A D E
        ========================
        */

        //Cria uma esfera e checa se ela encostou em algo com a layermask groundMask
        bool isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0f) //velocity.y < 0f, pois estamos trabalhando com gravidade, portanto valores negativos no Y
        {
            velocity.y = -2f;
        }

        // ===== PULO =====
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity); //Fórmula física
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); //Time.deltatime novamente pois na fórmula da gravidade t é ao quadrado

        /*
        =========================
            M O V I M E N T O
        =========================
         */

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Mover para horizontal e vertical de acordo com a posição atual do transform
        Vector3 movement = transform.right * x + transform.forward * z;

        controller.Move(movement * speed * Time.deltaTime);


        /*
        =========================
              T A M A N H O
        =========================
         */

        if (Input.GetKeyDown(KeyCode.Q)) //Diminuir
        {
            if (milk >= 1)
            {
                milk--;
                milkAmount.text = "x" + milk;

                if (playerBody.localScale.y == 1)
                {
                    playerBody.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    Camera.main.transform.position = new Vector3(playerBody.position.x, playerBody.position.y + 0.3f, playerBody.position.z); //aumentar altura camera
                    controller.transform.localScale = playerBody.localScale;
                    controller.transform.position = playerBody.transform.position;
                    controller.height = 1;
                    jump = 0.5f;
                }
                else if (playerBody.localScale.y == 1.5f)
                {
                    playerBody.localScale = new Vector3(1f, 1f, 1f);
                    Camera.main.transform.position = new Vector3(playerBody.position.x, playerBody.position.y + 0.3f, playerBody.position.z); //aumentar altura camera
                    controller.transform.localScale = playerBody.localScale;
                    controller.transform.position = playerBody.transform.position;
                    controller.height = 2;
                    jump = 1.5f;
                }
                else if (playerBody.localScale.y == 2f)
                {
                    playerBody.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    Camera.main.transform.position = new Vector3(playerBody.position.x, playerBody.position.y + 0.3f, playerBody.position.z); //aumentar altura camera
                    controller.transform.localScale = playerBody.localScale;
                    controller.transform.position = playerBody.transform.position;
                    controller.height = 2f;
                    jump = 2f;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) //Aumentar
        {
            if (cookies >= 1)
            {
                cookies--;
                cookieAmount.text = "x" + cookies;

                if (playerBody.localScale.y == 0.5f)
                {
                    playerBody.localScale = new Vector3(1f, 1f, 1f);
                    Camera.main.transform.position = new Vector3(playerBody.position.x, playerBody.position.y + 0.3f, playerBody.position.z);
                    controller.transform.localScale = playerBody.localScale;
                    controller.transform.position = playerBody.transform.position;
                    controller.height = 2;
                    jump = 1.5f;
                }
                else if (playerBody.localScale.y == 1f)
                {
                    playerBody.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    Camera.main.transform.position = new Vector3(playerBody.position.x, playerBody.position.y + 0.6f, playerBody.position.z);
                    controller.transform.localScale = playerBody.localScale;
                    controller.transform.position = playerBody.transform.position;
                    controller.height = 2f;
                    jump = 2f;
                }
                else if (playerBody.localScale.y == 1.5f)
                {
                    playerBody.localScale = new Vector3(2f, 2f, 2f);
                    Camera.main.transform.position = new Vector3(playerBody.position.x, playerBody.position.y + 0.6f, playerBody.position.z);
                    controller.transform.localScale = playerBody.localScale;
                    controller.transform.position = playerBody.transform.position;
                    controller.height = 2f;
                    jump = 2.5f;
                }
            }
        }
    }
}
