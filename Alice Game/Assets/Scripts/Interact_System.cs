using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Interact_System : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactText;
    private LayerMask objLayer;
    private bool touchedObj = false;
    private float distanceTouch = 1.8f;
    public Image inventoryImg;

    private void Start()
    {
        interactText.enabled = false;
        objLayer = LayerMask.GetMask("Objects");
    }

    private void Update()
    {
        touchedObj = Physics.CheckSphere(transform.position, distanceTouch, objLayer);

        if (touchedObj)
        {
            interactText.enabled = true;
        }
        else
        {
            interactText.enabled = false;
        }
    }
}
