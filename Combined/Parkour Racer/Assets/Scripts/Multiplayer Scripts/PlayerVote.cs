using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerVote : NetworkBehaviour {

    private int priorVote;
    private Canvas canvas;

	// Use this for initialization
	void Start () {
        priorVote = -1;
        canvas = FindObjectOfType<Canvas>();
    }
	
	// Update is called once per frame
	public void vote (int mapNumber) {
        if(this.GetComponent<NetworkIdentity>().hasAuthority && !isServer)
            CmdVote(mapNumber, priorVote);

        if (priorVote == -1)
        {
            //canvas.GetComponent<VoteTally>().voteTally[mapNumber-1]++;
            
            switch(mapNumber)
            {
                case 1:
                    canvas.GetComponent<VoteTally>().voteTally1++;
                    break;
                case 2:
                    canvas.GetComponent<VoteTally>().voteTally2++;
                    break;
                case 3:
                    canvas.GetComponent<VoteTally>().voteTally3++;
                    break;
                case 4:
                    canvas.GetComponent<VoteTally>().voteTally4++;
                    break;
                case 5:
                    canvas.GetComponent<VoteTally>().voteTally5++;
                    break;
                case 6:
                    canvas.GetComponent<VoteTally>().voteTally6++;
                    break;
                case 7:
                    canvas.GetComponent<VoteTally>().voteTally7++;
                    break;
                case 8:
                    canvas.GetComponent<VoteTally>().voteTally8++;
                    break;
                default:
                    break;
            }
        }
        else
        {
            //canvas.GetComponent<VoteTally>().voteTally[priorVote-1]--;
            switch (priorVote)
            {
                case 1:
                    canvas.GetComponent<VoteTally>().voteTally1--;
                    break;
                case 2:
                    canvas.GetComponent<VoteTally>().voteTally2--;
                    break;
                case 3:
                    canvas.GetComponent<VoteTally>().voteTally3--;
                    break;
                case 4:
                    canvas.GetComponent<VoteTally>().voteTally4--;
                    break;
                case 5:
                    canvas.GetComponent<VoteTally>().voteTally5--;
                    break;
                case 6:
                    canvas.GetComponent<VoteTally>().voteTally6--;
                    break;
                case 7:
                    canvas.GetComponent<VoteTally>().voteTally7--;
                    break;
                case 8:
                    canvas.GetComponent<VoteTally>().voteTally8--;
                    break;
                default:
                    break;
            }

            //canvas.GetComponent<VoteTally>().voteTally[mapNumber-1]++;
            switch (mapNumber)
            {
                case 1:
                    canvas.GetComponent<VoteTally>().voteTally1++;
                    break;
                case 2:
                    canvas.GetComponent<VoteTally>().voteTally2++;
                    break;
                case 3:
                    canvas.GetComponent<VoteTally>().voteTally3++;
                    break;
                case 4:
                    canvas.GetComponent<VoteTally>().voteTally4++;
                    break;
                case 5:
                    canvas.GetComponent<VoteTally>().voteTally5++;
                    break;
                case 6:
                    canvas.GetComponent<VoteTally>().voteTally6++;
                    break;
                case 7:
                    canvas.GetComponent<VoteTally>().voteTally7++;
                    break;
                case 8:
                    canvas.GetComponent<VoteTally>().voteTally8++;
                    break;
                default:
                    break;
            }
        }

        if (!this.GetComponent<NetworkIdentity>().hasAuthority && !isServer)
        {
            GameObject[] g = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject go in g)
                if(go.GetComponent<NetworkIdentity>().hasAuthority)
                    go.GetComponent<PlayerVote>().CmdVote(mapNumber, priorVote);
        }

        priorVote = mapNumber;
    }

    [Command]
    public void CmdVote(int mN, int pV)
    {
        if (pV == -1)
        {
            //canvas.GetComponent<VoteTally>().voteTally[mN - 1]++;
            switch (mN)
            {
                case 1:
                    canvas.GetComponent<VoteTally>().voteTally1++;
                    break;
                case 2:
                    canvas.GetComponent<VoteTally>().voteTally2++;
                    break;
                case 3:
                    canvas.GetComponent<VoteTally>().voteTally3++;
                    break;
                case 4:
                    canvas.GetComponent<VoteTally>().voteTally4++;
                    break;
                case 5:
                    canvas.GetComponent<VoteTally>().voteTally5++;
                    break;
                case 6:
                    canvas.GetComponent<VoteTally>().voteTally6++;
                    break;
                case 7:
                    canvas.GetComponent<VoteTally>().voteTally7++;
                    break;
                case 8:
                    canvas.GetComponent<VoteTally>().voteTally8++;
                    break;
                default:
                    break;
            }
        }
        else
        {
            //canvas.GetComponent<VoteTally>().voteTally[pV - 1]--;
            switch (pV)
            {
                case 1:
                    canvas.GetComponent<VoteTally>().voteTally1--;
                    break;
                case 2:
                    canvas.GetComponent<VoteTally>().voteTally2--;
                    break;
                case 3:
                    canvas.GetComponent<VoteTally>().voteTally3--;
                    break;
                case 4:
                    canvas.GetComponent<VoteTally>().voteTally4--;
                    break;
                case 5:
                    canvas.GetComponent<VoteTally>().voteTally5--;
                    break;
                case 6:
                    canvas.GetComponent<VoteTally>().voteTally6--;
                    break;
                case 7:
                    canvas.GetComponent<VoteTally>().voteTally7--;
                    break;
                case 8:
                    canvas.GetComponent<VoteTally>().voteTally8--;
                    break;
                default:
                    break;
            }

            //canvas.GetComponent<VoteTally>().voteTally[mN - 1]++;
            switch (mN)
            {
                case 1:
                    canvas.GetComponent<VoteTally>().voteTally1++;
                    break;
                case 2:
                    canvas.GetComponent<VoteTally>().voteTally2++;
                    break;
                case 3:
                    canvas.GetComponent<VoteTally>().voteTally3++;
                    break;
                case 4:
                    canvas.GetComponent<VoteTally>().voteTally4++;
                    break;
                case 5:
                    canvas.GetComponent<VoteTally>().voteTally5++;
                    break;
                case 6:
                    canvas.GetComponent<VoteTally>().voteTally6++;
                    break;
                case 7:
                    canvas.GetComponent<VoteTally>().voteTally7++;
                    break;
                case 8:
                    canvas.GetComponent<VoteTally>().voteTally8++;
                    break;
                default:
                    break;
            }
        }
    }
}
