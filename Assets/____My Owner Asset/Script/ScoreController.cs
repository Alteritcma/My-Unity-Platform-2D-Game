using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    public CoinsController coinsController;
    // Start is called before the first frame update
    void Awake()
    {
        textScore.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = coinsController.ValueCoin.ToString();
    }
}
