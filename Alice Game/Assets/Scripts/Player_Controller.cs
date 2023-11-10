using TMPro;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 5f, gravity = -9.8f, groundDistance = 0.4f, jump = 1.5f;
    private Vector3 velocity;

    [SerializeField] private Transform groundCheck, playerBody;
    private LayerMask groundMask;

    //Acessado por Objects_System
    [HideInInspector] public int cookies = 0, milk = 0;
    public TextMeshProUGUI milkAmount, cookieAmount;

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

        float maxSize = 2.5f, minSize = 0.5f;

        if (Input.GetKeyDown(KeyCode.Q)) //Diminuir
        {
            if (playerBody.localScale.y > minSize && milk >= 1)
            {
                milk--;
                milkAmount.text = "x" + milk;
                ChangeSize(-1);
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) //Aumentar
        {
            if (playerBody.localScale.y < maxSize && cookies >= 1)
            {
                cookies--;
                cookieAmount.text = "x" + cookies;
                ChangeSize(1);
            }
        }
    }

    private void ChangeSize(int operationSign)
    {
        //===== TAMANHO DO PLAYER =====
        playerBody.localScale = new Vector3(playerBody.localScale.x, playerBody.localScale.y + (0.5f * operationSign), playerBody.localScale.z);

        //===== POSIÇÃO DO PLAYER =====
        playerBody.position = new Vector3(playerBody.position.x, playerBody.position.y + (0.5f * operationSign), playerBody.position.z);

        //===== POSIÇÃO DA CAMERA =====
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + (1f * operationSign), Camera.main.transform.position.z);


        //Tamanho do Character Controller
        controller.transform.localScale = playerBody.localScale;

        jump = jump + (1f * operationSign);
    }
}
