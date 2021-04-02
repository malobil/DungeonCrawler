using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 2f;
    [SerializeField] private float m_positionDeadZone = 0.2f;
    [SerializeField] private float m_rotationSpeed= 10f;
    [SerializeField] private float m_rotationDeadZone= 2f;

    private bool isMoving = false;
    private bool isTurning = false;

    private CustomCell currentCell ;
    private CustomCell cellDestination;

    private Vector3 currentPosition;
    private Vector3 destinationPosition;
    private Quaternion targetRotation ;

    // Start is called before the first frame update
    void Start()
    {
        currentCell = GridManager.Instance.GetCellByPosition(new Vector2(0,0));
        currentPosition = currentCell.CellWorldPosition;
    }

    // Update is called once per frame
    void Update()
    {
      if(isMoving)
      {
            float step = m_moveSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, destinationPosition, step);

            if(Vector3.Distance(transform.position, destinationPosition) <= m_positionDeadZone)
            {
                transform.position = destinationPosition;
                isMoving = false;
            }
       }

      if(isTurning)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,m_rotationSpeed * Time.deltaTime);
            Debug.Log(Quaternion.Angle(transform.rotation, targetRotation));

            if(Quaternion.Angle(transform.rotation, targetRotation) <= m_rotationDeadZone)
            {
                transform.rotation = targetRotation;
                isTurning = false;
            }
        }
            
    }

    public void MoveForward(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Move(GridManager.Instance.GetCellByDirection(currentCell, transform.forward));
        }
    }

    public void MoveBackward(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Move(GridManager.Instance.GetCellByDirection(currentCell, -transform.forward));
        }
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Move(GridManager.Instance.GetCellByDirection(currentCell, -transform.right));
        }
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
           Move(GridManager.Instance.GetCellByDirection(currentCell, transform.right));
        }
    }

    private void Move(CustomCell targetCell)
    {
        if(targetCell == null || isMoving)
        {
            return;
        }

        destinationPosition = targetCell.CellWorldPosition;
        currentCell = targetCell;
        isMoving = true;
    }

    public void TurnLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Turn(-90f);
        }
    }

    public void TurnRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Turn(90f);
        }
    }

    private void Turn(float turnRotation)
    {
        if(isTurning)
        {
            return;
        }

        targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + turnRotation, transform.rotation.eulerAngles.z);
        Debug.Log(targetRotation.eulerAngles);
        //targetRotation = Quaternion.Euler(transform.rotation + turnRotation);
        isTurning = true;
    }

}
