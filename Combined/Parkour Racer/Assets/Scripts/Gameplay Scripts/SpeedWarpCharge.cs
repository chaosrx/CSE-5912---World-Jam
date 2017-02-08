using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SpeedWarpCharge : NetworkBehaviour
{

    public Text percent;
    public Slider charge;

    private float elapsedTime;
    private GameObject firstPlace;

    private float MaxSpeed = 0f;
    private bool UseingCharge = false;

    void Start()
    {
        MaxSpeed = this.GetComponent<PlayerControllerMulti>().MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (charge == null)
        {
            charge = GameObject.Find("Charge").GetComponent<Slider>();
        }

        if (percent == null)
        {
            percent = GameObject.Find("Percent").GetComponent<Text>();
        }

        elapsedTime += Time.deltaTime;

        if (charge.value < 100 && !UseingCharge)
        {
            if (elapsedTime > 1)
            {
                /*
                GameObject[] players = GameObject.FindGameObjectsWithTag("Players");
                foreach (GameObject player in players)
                    if (player.GetComponent<PlayerPosition>().currentPosition == 1)
                        firstPlace = player;
                 */
                int currentPosition = this.gameObject.GetComponent<PlayerPositionMulti>().currentPosition;

                if (currentPosition == 1)
                    charge.value += 1;
                if (currentPosition == 2)
                    charge.value += 2;
                if (currentPosition == 3)
                    charge.value += 3;
                if (currentPosition == 4)
                    charge.value += 4;

                percent.text = charge.value + "%";

                elapsedTime = 0.0f;
            }
        }
        else if (!UseingCharge && (Input.GetButton("Left Bumper") || Input.GetButton("Right Bumper")))
        {
            this.GetComponent<PlayerControllerMulti>().MaxSpeed = MaxSpeed * 2;
            UseingCharge = true;
            //Debug.Log("Max Player speed was " +  MaxSpeed + " now it is " + this.GetComponent<PlayerControllerMulti>().MaxSpeed);
        }

        if (UseingCharge && charge.value > 0)
        {
            if (elapsedTime > 0.05)
            {
                charge.value--;
                percent.text = charge.value + "%";
                elapsedTime = 0.0f;
            }
        }
        if (UseingCharge && charge.value <= 0)
        {
            this.GetComponent<PlayerControllerMulti>().MaxSpeed = MaxSpeed;
            charge.value = 0;
            UseingCharge = false;
            //Debug.Log("Max Player speed was " + (MaxSpeed *2) + " now it is " + this.GetComponent<PlayerControllerMulti>().MaxSpeed);
        }


    }
}

