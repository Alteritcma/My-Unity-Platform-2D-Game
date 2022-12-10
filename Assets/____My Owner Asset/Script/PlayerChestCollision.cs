using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

public class PlayerChestCollision : Simulation.Event<PlayerCoinCollision>
{
    public PlayerController player;
    public ChestInstance deskripsiUIControl;

    PlatformerModel model = Simulation.GetModel<PlatformerModel>();

    public override void Execute()
    {
        AudioSource.PlayClipAtPoint(deskripsiUIControl.tokenCollectAudio,
            deskripsiUIControl.transform.position);

        model.metaGameController.ToggleDeskripsiCanvas();
    }
}
