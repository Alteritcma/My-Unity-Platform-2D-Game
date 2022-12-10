using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using UnityEngine.UI;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        public bool isMaju;
        public bool isMundur;
        public bool isLompat;
        Vector2 move, newMove;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        public ButtonIsPressed buttonMaju, buttonMundur, buttonLompat;
        public GameObject UIKontroller;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                UIKontroller.SetActive(true);
                newMove = Vector2.zero;

                isMaju = buttonMaju.buttonPressed;
                isMundur = buttonMundur.buttonPressed;
                isLompat = buttonLompat.buttonPressed;

                if (isMaju || Input.GetKey(KeyCode.RightArrow))
                {
                    newMove = new Vector2(1, 0);
                }

                if (isMundur || Input.GetKey(KeyCode.LeftArrow))
                {
                    newMove = new Vector2(-1, 0);
                }


                if (jumpState == JumpState.Grounded && isLompat ||
                    jumpState == JumpState.Grounded && Input.GetButtonDown("AltJump"))
                {
                    jumpState = JumpState.PrepareToJump;
                }
                    
                else if (!isLompat || Input.GetButtonUp("AltJump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }

            }
            else
            {
                buttonMaju.buttonPressed = false;
                buttonMundur.buttonPressed = false;
                buttonLompat.buttonPressed = false;
                UIKontroller.SetActive(false);
                newMove = Vector2.zero;
            }


            move.x = newMove.x;

            //Debug.Log(newJump);

            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
            //Debug.Log(targetVelocity);
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}