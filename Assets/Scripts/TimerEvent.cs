using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerEvent : MonoBehaviour
{
    public GameObject Player1, Player2;
    public float Timer = 30;
    private float counter;

    public TextMeshProUGUI VictoryText;



    // Start is called before the first frame update
    void Start()
    {
        counter = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;

        if (counter <= 0)
        {
            Time.timeScale = 0;
            VictoryText.transform.parent.gameObject.SetActive(true);

        }



        if (counter <= 0)
        {
            int p1P = Player1.GetComponent<Points>().points;
            int p2P = Player2.GetComponent<Points>().points;


            if (p1P > p2P)
            {
                VictoryText.text = "<color= \"red\"> PLAYER 1 </color> VINCE";
            }
            else if (p1P < p2P)
            {
                VictoryText.text = "<color= \"green\"> PLAYER 2 </color> VINCE";

            }
            else
            {
                VictoryText.text = "PAREGGIO";

            }

        }

    }
}
