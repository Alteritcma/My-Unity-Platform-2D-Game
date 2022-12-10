using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class PlayerEnterChest : Simulation.Event<PlayerEnterChest>
    {
        public ChestInstanceTwo chestInstanceTwo;
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            model.player.animator.SetTrigger("stoping");
            model.player.controlEnabled = false;
            if (model.player.animator.GetBool("stoping"))
            {
                model.chestInstance.OnPlayerEnter(model.player);
            }
        }
    }
}
