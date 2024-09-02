using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro DoorItem;
    public TextMeshPro KeyItem;
    public TextMeshProUGUI DoorCard;
    public TextMeshProUGUI KeyCard;
    public TextMeshProUGUI LimitUI;
    public Player MainPlayer;

    private short DoorState = 0;
    private short KeyState = 0;
    private int TimeLimit = 99;
    void Start()
    {
        LimitUI.text = TimeLimit.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDoorCardClicked()
    {
        switch (DoorState)
        {
            case 0:
                Debug.Log("DoorClicked0");
                MainPlayer.SetEndPos(DoorItem.transform.position, () =>
                {
                    DoorState = 1;
                    DoorCard.text = "Door Closed";
                    DoorItem.text = "Door Closed";
                    TimeLimit -= 1;
                    LimitUI.text = TimeLimit.ToString();
                });
                break;
            case 1:
                if (KeyState == 1)
                {
                    Debug.Log("DoorClicked1");
                    OpenDoor();
                }
                break;
            default:
                break;
        }
    }

    public void OnKeyCardClicked()
    {
        switch (KeyState)
        {
            case 0:
                Debug.Log("KeyClicked0");
                MainPlayer.SetEndPos(KeyItem.transform.position, () => {
                    KeyState = 1;
                    KeyCard.text = "Key";
                    KeyItem.enabled = false;
                    TimeLimit -= 1;
                    LimitUI.text = TimeLimit.ToString();
                });
                break;
            case 1:
                if (DoorState == 1)
                {
                    Debug.Log("KeyClicked1");
                    OpenDoor();
                }
                break;
            case 2:
                Debug.Log("KeyClicked2");
                MainPlayer.SetEndPos(DoorItem.transform.position, () => {
                    DoorItem.text = "Run";
                    KeyState = 3;
                    MainPlayer.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    MainPlayer.enabled = false;
                    KeyCard.enabled = false;
                    TimeLimit -= 1;
                    LimitUI.text = TimeLimit.ToString();
                });
                break;
            default:
                break;
        }
    }

    private void OpenDoor()
    {
        MainPlayer.SetEndPos(DoorItem.transform.position, () => {
            DoorState = 2;
            KeyState = 2;
            KeyCard.text = "Door Open";
            DoorItem.text = "Door Open";
            DoorCard.enabled = false;
            TimeLimit -= 1;
            LimitUI.text = TimeLimit.ToString();
        });
    }
}
