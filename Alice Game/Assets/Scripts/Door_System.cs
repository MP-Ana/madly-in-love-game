using UnityEngine;

public class Door_System : MonoBehaviour
{
    [SerializeField] private Transform nextLevelDoor;
    private float distanceTouch = 2f;
    private LayerMask defaultLayer;
    private bool touchedPlayer;
    private GameObject player;

    private void Start()
    {
        defaultLayer = LayerMask.GetMask("Default");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        touchedPlayer = Physics.CheckSphere(transform.position, distanceTouch, defaultLayer);

        if (!touchedPlayer)
        {
            return;
        }
        else
        {
            player.transform.position = new Vector3(nextLevelDoor.position.x + 1f, nextLevelDoor.position.y, nextLevelDoor.position.z);
        }
    }
}
