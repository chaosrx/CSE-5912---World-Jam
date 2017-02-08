using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.Networking;

public class Zap : NetworkBehaviour
{

    public float forceApplied = 500;

    IEnumerator OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (GameObject.Find("NetworkManager") == null || col.gameObject.GetComponent<NetworkIdentity>() == null || col.gameObject.GetComponent<NetworkIdentity>().hasAuthority)
            {
                Debug.Log("Zapped!");
                col.gameObject.GetComponent<Rigidbody>().AddForce(0, forceApplied, 0);
                Camera.main.GetComponent<BlurOptimized>().enabled = true;
                yield return new WaitForSeconds(0.75f);
                Camera.main.GetComponent<BlurOptimized>().enabled = false;
            }

        }


    }
}
