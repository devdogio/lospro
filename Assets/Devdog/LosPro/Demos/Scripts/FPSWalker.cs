using UnityEngine;
using System.Collections;

namespace Devdog.LosPro.Demo
{
    [RequireComponent(typeof(CharacterController))]
    public class FPSWalker : MonoBehaviour
    {
        public float walkSpeed = 6.0f;
        public bool limitDiagonalSpeed = true;

        public float jumpSpeed = 8.0f;
        public float gravity = 20.0f;

        public float antiBumpFactor = .75f;
        public int antiBunnyHopFactor = 1;

        public KeyCode sneakKeyCode = KeyCode.LeftShift;

        [Header("Audio")]
        public float stepSoundRange = 12f;
        public AudioClip[] stepSounds = new AudioClip[0];
        public float sneakSoundRange = 5f;
        public AudioClip[] sneakStepSounds = new AudioClip[0];
        public float landSoundRange = 5f;
        public AudioClip[] landSounds = new AudioClip[0];

        [TargetCategory]
        public int categoryBitFlag = -1;

        private Vector3 _moveDirection = Vector3.zero;
        private bool _grounded = false;
        private CharacterController _controller;
        private int _jumpTimer;
        private float _inputX;
        private float _inputY;
        private bool _isSneaking = false;

        protected void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _jumpTimer = antiBunnyHopFactor;

            InvokeRepeating("DoStepSounds", 0f, 0.4f);
        }

        protected void DoStepSounds()
        {
            if (_inputX > 0f || _inputX < 0f || _inputY > 0f || _inputY < 0f)
            {
                if (_isSneaking)
                {
                    PlayAudioClip(sneakStepSounds, sneakSoundRange);
                }
                else
                {
                    PlayAudioClip(stepSounds, stepSoundRange);
                }
            }
        }

        protected void FixedUpdate()
        {
            _inputX = Input.GetAxis("Horizontal");
            _inputY = Input.GetAxis("Vertical");
            _isSneaking = Input.GetKey(sneakKeyCode);

            if (_grounded)
            {
                _moveDirection = new Vector3(_inputX, -antiBumpFactor, _inputY);
                _moveDirection = transform.TransformDirection(_moveDirection) * walkSpeed;

                if (!Input.GetButton("Jump"))
                {
                    _jumpTimer++;
                }
                else if (_jumpTimer >= antiBunnyHopFactor)
                {
                    _moveDirection.y = jumpSpeed;
                    _jumpTimer = 0;
                }
            }

            _moveDirection.y -= gravity * Time.deltaTime;
            bool wasGrounded = _grounded;
            _grounded = (_controller.Move(_moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
            if (wasGrounded == false && _grounded)
            {
                // Landed
                PlayAudioClip(landSounds, landSoundRange);
            }
        }

        private void PlayAudioClip(AudioClip[] audioClips, float range)
        {
            var audioSphere = AudioSourceManager.CreateAudioSourcePooled(transform.position, gameObject, categoryBitFlag);
            audioSphere.maxGrowthSize = range;
            if (audioClips.Length > 0)
            {
                audioSphere.audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
            }
            audioSphere.Emit();
        }
    }
}