using UnityEngine;

public class CoinsController : MonoBehaviour
{

    public CoinInstance[] coins;
    public int ValueCoin;
    //public ChestInstance[] deskripsiUIControl;


    void FindAllTokensInScene()
    {
        coins = UnityEngine.Object.FindObjectsOfType<CoinInstance>();
    }

    //void FindAllChestsInScene()
    //{
    //    deskripsiUIControl = UnityEngine.Object.FindObjectsOfType<ChestInstance>();
    //}

    void Awake()
    {
        //if tokens are empty, find all instances.
        //if tokens are not empty, they've been added at editor time.
        if (coins.Length == 0)
            FindAllTokensInScene();
        //Register all tokens so they can work with this controller.
        for (var i = 0; i < coins.Length; i++)
        {
            coins[i].tokenIndex = i;
            coins[i].controller = this;
        }


        //if (deskripsiUIControl.Length == 0)
        //    FindAllChestsInScene();
        ////Register all tokens so they can work with this controller.
        //for (var j = 0; j < deskripsiUIControl.Length; j++)
        //{
        //    deskripsiUIControl[j].chestIndex = j;
        //    deskripsiUIControl[j].controller = this;
        //}
    }

    private void Update()
    {
        //update all tokens with the next animation frame.
        for (var i = 0; i < coins.Length; i++)
        {
            var token = coins[i];
            //if token is null, it has been disabled and is no longer animated.
            if (token != null)
            {
                if (token.collected)
                {
                    ValueCoin++;
                    token.gameObject.SetActive(false);
                    coins[i] = null;
                }
            }
        }


        //for (var j = 0; j < deskripsiUIControl.Length; j++)
        //{
        //    var chest = deskripsiUIControl[j];
        //    //if token is null, it has been disabled and is no longer animated.
        //    if (chest != null)
        //    {
        //        if (chest.collected)
        //        {
        //            chest.UIDeskripsi.SetActive(true);
        //            chest.gameObject.SetActive(false);
        //            deskripsiUIControl[j] = null;
        //        }
        //    }
        //}
    }
}
