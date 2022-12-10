using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

public class ChestInstance : MonoBehaviour
{
    /// <summary>
    /// Boolean collected
    /// </summary>
    internal bool collected = false;
    /// <summary>
    /// CoinsController Function
    /// </summary>
    internal CoinsController controller;
    /// <summary>
    /// audio collected
    /// </summary>
    public AudioClip tokenCollectAudio;
    /// <summary>
    /// other
    /// </summary>
    internal int chestIndex = -1;
    public GameObject UIDeskripsi;
    internal Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    /// <summary>
    /// if get collision trigger
    /// </summary>
    /// <param name="other"></param>

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    //only exectue OnPlayerEnter if the player collides with this token.
    //    var player = other.gameObject.GetComponent<PlayerController>();
    //    if (player != null) OnPlayerEnter(player);
    //}


    /// <summary>
    /// if player enter to collision trigger
    /// </summary>
    /// <param name="player"></param>
    public void OnPlayerEnter(PlayerController player)
    {
        if (collected) return;
        //disable the gameObject and remove it from the controller update list.
        //frame = 0;
        //sprites = collectedAnimation;
        if (controller != null)
            collected = true;
        //send an event into the gameplay system to perform some behaviour.
        var ev = Schedule<PlayerChestCollision>();
        ev.deskripsiUIControl = this;
        ev.player = player;
    }
}
