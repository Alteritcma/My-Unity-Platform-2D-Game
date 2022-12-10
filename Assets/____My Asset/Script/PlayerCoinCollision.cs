using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using Platformer.Gameplay;
using UnityEngine;


/// <summary>
/// Fired when a player collides with a token.
/// </summary>
/// <typeparam name="PlayerCollision"></typeparam>
public class PlayerCoinCollision : Simulation.Event<PlayerCoinCollision>
{
    public PlayerController player;
    public CoinInstance coin;

    PlatformerModel model = Simulation.GetModel<PlatformerModel>();

    public override void Execute()
    {
        AudioSource.PlayClipAtPoint(coin.tokenCollectAudio, coin.transform.position);
    }
}
