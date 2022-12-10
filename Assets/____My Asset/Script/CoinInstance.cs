using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

public class CoinInstance : MonoBehaviour
{
    public AudioClip tokenCollectAudio;
    //[Tooltip("If true, animation will start at a random position in the sequence.")]
    //public bool randomAnimationStartTime = false;
    //[Tooltip("List of frames that make up the animation.")]
    //public Sprite[] idleAnimation, collectedAnimation;

    //internal Sprite[] sprites = new Sprite[0];

    //internal SpriteRenderer _renderer;

    //unique index which is assigned by the TokenController in a scene.
    internal int tokenIndex = -1;
    internal CoinsController controller;
    //active frame in animation, updated by the controller.
    //internal int frame = 0;
    internal bool collected = false;

    void Awake()
    {
        //_renderer = GetComponent<SpriteRenderer>();
        //if (randomAnimationStartTime)
        //    frame = Random.Range(0, sprites.Length);
        //sprites = idleAnimation;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //only exectue OnPlayerEnter if the player collides with this token.
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player != null) OnPlayerEnter(player);
    }

    void OnPlayerEnter(PlayerController player)
    {
        if (collected) return;
        //disable the gameObject and remove it from the controller update list.
        //frame = 0;
        //sprites = collectedAnimation;
        if (controller != null)
            collected = true;
        //send an event into the gameplay system to perform some behaviour.
        var ev = Schedule<PlayerCoinCollision>();
        ev.coin = this;
        ev.player = player;
    }
}