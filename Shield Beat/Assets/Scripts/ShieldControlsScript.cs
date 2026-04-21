using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldControlsScript : MonoBehaviour
{
    private float moveInput;
    private float rotation;
    void Update()
    {
        RotateShield(); 
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
    }
    private void RotateShield()
    {
        rotation = moveInput * 450 * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }
}
