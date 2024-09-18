using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class RoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro DoorItem;
    public TextMeshPro KeyItem;
    public TextMeshProUGUI DoorCard;
    public TextMeshProUGUI KeyCard;
    public TextMeshProUGUI LimitUI;
    public Player MainPlayer;

    public AudioManager RoomAudioManager;

    private short DoorState = 0;
    private short KeyState = 0;
    private int TimeLimit = 99;
    private bool DoorFound = false;
    private bool BoxFound = false;
    void Start()
    {
        LimitUI.text = TimeLimit.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float angle;
        Vector3 axis;
        MainPlayer.transform.rotation.ToAngleAxis(out angle, out axis);
        
        
        float lookAngle = Mathf.Atan2(DoorItem.transform.position.y - MainPlayer.transform.position.y, DoorItem.transform.position.x - MainPlayer.transform.position.x);
        float distance = (DoorItem.transform.position - MainPlayer.transform.position).magnitude;

        if (Mathf.Abs(lookAngle - angle * Mathf.Deg2Rad) < 0.2f && distance < 4f)
        {
            if (!DoorFound && DoorState < 2)
            {
                RoomAudioManager.PlayFound();
                DoorFound = true;
            }

            DoorCard.enabled = true;
        }
        else
        {
            DoorCard.enabled = false;
        }
        
        lookAngle = Mathf.Atan2(KeyItem.transform.position.y - MainPlayer.transform.position.y, KeyItem.transform.position.x - MainPlayer.transform.position.x);;
        distance = (KeyItem.transform.position - MainPlayer.transform.position).magnitude;

        if (Mathf.Abs(lookAngle - angle * Mathf.Deg2Rad) < 0.2f && distance < 4f)
        {            
            if (!BoxFound)
            {
                RoomAudioManager.PlayFound();
                BoxFound = true;
            }
            KeyCard.enabled = true;
        }
        else if (KeyState == 0)
        {
            KeyCard.enabled = false;
        }
    }

    public void OnDoorCardClicked()
    {
        switch (DoorState)
        {
            case 0:
                Debug.Log("DoorClicked0");
                MainPlayer.SetEndPos(DoorItem.transform.position, () =>
                {
                    RoomAudioManager.PlayDoorClose();
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
                    RoomAudioManager.PlayDoorOpen();
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
                    RoomAudioManager.PlayItem();
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
                    RoomAudioManager.PlayDoorOpen();
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
