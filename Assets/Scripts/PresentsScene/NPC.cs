using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCItemContainer))]
[RequireComponent (typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class NPC : MonoBehaviour
{
    // TODO: Refactor to DRY.
    private const float Gravity = -9.81f;

    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _rotationSpeed = 4f;
    [SerializeField] private Transform _destinationPosition;
    [SerializeField] private Transform _exitDestinationPosition;
    [SerializeField] private AudioSource _walkingAudio;
    [SerializeField] private AudioSource _dialogueAudio;
    public bool NPCInTriggerArea { get; private set; } = false;

    private Animator _animator;
    private CharacterController _characterController;
    private Coroutine _coroutine;
    private NPCItemContainer _item;

    private bool _isInputGiven;
    private bool _isInteractionFinished = false;
    private bool _toStartNPC = false;

    private void Awake()
    {
        _item = GetComponent<NPCItemContainer>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _walkingAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_isInteractionFinished)
        {
            HandleAnimation();
            if (_toStartNPC)
                MovePlayer();
        }
        else
            MoveToExit();
    }

    public void ChangeNPCState(bool state) => NPCInTriggerArea = state;

    public void PlayNPCAudio() => _dialogueAudio.Play();
    public void StopNPCAudio() => _dialogueAudio.Stop();
    public void MuteNPCStateSwitch() => _dialogueAudio.mute = !_dialogueAudio.mute;

    public void StartNPC() => _toStartNPC = true;

    public NPCItemContainer GetNPCItem() => _item;

    public void BeginExitState()
    {
        StopNPCAudio();
        MoveToExit();
    }

    private void MovePlayer()
    {
        if (!gameObject.activeSelf) return;
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(MoveTowardsDestination(_destinationPosition.position));
    }

    private IEnumerator MoveTowardsDestination(Vector3 destination)
    {
        var playerDistanceToFloor = transform.position.y - destination.y;
        destination.y += playerDistanceToFloor;
        _isInputGiven = true;

        while (Vector3.Distance(transform.position, destination) > .1f)
        {
            var direction = destination - transform.position;
            var movement = direction.normalized * (_speed * Time.deltaTime);
            // Gravity
            if (_characterController.isGrounded) movement.y = -.05f;
            else movement.y += Gravity * Time.deltaTime;
            // Rotation
            var playerTransform = transform;
            var positionToLookAt = new Vector3
            {
                x = direction.normalized.x,
                y = 0.0f,
                z = direction.normalized.z
            };
            var currentRotation = playerTransform.rotation;
            transform.rotation = Quaternion.Slerp(currentRotation,
                Quaternion.LookRotation(positionToLookAt),
                _rotationSpeed * Time.deltaTime);
            // Movement
            _characterController.Move(movement);

            yield return null;
        }

        _isInputGiven = false;
    }

    private IEnumerator MoveTowardsExitDestination(Vector3 destination)
    {
        var playerDistanceToFloor = transform.position.y - destination.y;
        destination.y += playerDistanceToFloor;
        _isInputGiven = true;

        while (Vector3.Distance(transform.position, destination) > .1f)
        {
            var direction = destination - transform.position;
            var movement = direction.normalized * (_speed * Time.deltaTime);
            // Gravity
            if (_characterController.isGrounded) movement.y = -.05f;
            else movement.y += Gravity * Time.deltaTime;
            // Rotation
            var playerTransform = transform;
            var positionToLookAt = new Vector3
            {
                x = direction.normalized.x,
                y = 0.0f,
                z = direction.normalized.z
            };
            var currentRotation = playerTransform.rotation;
            transform.rotation = Quaternion.Slerp(currentRotation,
                Quaternion.LookRotation(positionToLookAt),
                _rotationSpeed * Time.deltaTime);
            // Movement
            _characterController.Move(movement);

            yield return null;
        }

        _isInputGiven = false;
        _isInteractionFinished = true;
        gameObject.SetActive(false);
    }

    private void HandleAnimation()
    {
        var isWalking = _animator.GetBool(AnimatorParameterIdList.IsWalking);
        switch (_isInputGiven)
        {
            case true when !isWalking:
                _walkingAudio.Play();
                _animator.SetBool(AnimatorParameterIdList.IsWalking, true);
                break;
            case false when isWalking:
                _walkingAudio.Pause();
                _animator.SetBool(AnimatorParameterIdList.IsWalking, false);
                break;
        }
    }
    
    private void MoveToExit()
    {
        _toStartNPC = false;
        if (!gameObject.activeSelf) return;
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(MoveTowardsExitDestination(_exitDestinationPosition.position));
    }
}
