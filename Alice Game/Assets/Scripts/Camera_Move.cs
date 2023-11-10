using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    [HideInInspector] public float sensitivity = 250f;
    private float xRotation = 0f; //Rota��o da camera no eixo X (cima e baixo)
    [SerializeField] private Transform playerBody;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Cursor iniciar no centro da tela
    }

    private void Update()
    {
        //Mover a cabe�a (girar a camera) na horizontal e vertical (respectivamente)
        float mouseMoveX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseMoveY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        

        xRotation -= mouseMoveY; //Rota��o de acordo com a posi��o Y do mouse
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //Clamping


        //Girar a cabe�a (camera) na vertical
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //Girar o corpo (playerBody) na horizontal 
        playerBody.Rotate(Vector3.up * mouseMoveX); //Vector3.up = Y
    }
}
