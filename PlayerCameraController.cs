using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float lookSensitivity = 1f;

    [SerializeField] private float smoothing = 1f;

    private Vector3 basecamh;

    private Vector3 crouchcamh;

    public float CrouchHeight = .9f;

    private GameObject player;

    private Vector2 smoothedVelocity;

    private Vector2 currentLookingPos;

    private void Awake()
    {
        basecamh = transform.localPosition;
        crouchcamh = transform.localPosition;
        crouchcamh.y = CrouchHeight;
    }


    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        inputValues = Vector2.Scale(inputValues, new Vector2(lookSensitivity * smoothing, lookSensitivity * smoothing));

        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing);

        currentLookingPos += smoothedVelocity;
        currentLookingPos.y = Mathf.Clamp(currentLookingPos.y, -80f, 80f);

        transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(currentLookingPos.x, player.transform.up);
    }

    private void cameracrouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            player.transform.localPosition = crouchcamh;

        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            player.transform.localPosition = basecamh;
        }
    }
}

