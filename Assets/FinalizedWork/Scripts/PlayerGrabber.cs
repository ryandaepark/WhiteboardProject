using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

//Add functionality where you can change the drag by using scroll wheel
//Add ability to cast objects
//Great way to prevent havinng to assign grabbable to every script
public class PlayerGrabber : MonoBehaviour
{
    [SerializeField]
    private InputAction mouseClick;
    [SerializeField]
    private InputAction scrollWheel;
    [SerializeField]
    private InputAction q_key;
    [SerializeField]
    private float mouseDragPhysicsSpeed = 10;
    [SerializeField]
    private float mouseDragSpeed = .1f;
    [SerializeField]
    private float scrollSpeed = .1f;

    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;

    private float mouseScrollY; //keeps track of scroll action
    private bool q_keyup; //q key pressed
    private ReadBoardBehind targetBoard; //board to cast on
    public bool WhiteBoarded; //board casted onto board
    private float distance; // how far is object from player
    private float speed = 0.3f;    //the speed of moving an item forward or back in 3d mode=

    //q button logic
    bool prevBool;
    bool currBool;

    //Scaling
    public Vector3 parentCenter = Vector3.zero;
    private float startDistance, currentScale;
    private Transform dot, note;
    private float initialScale;

    private void Awake()
    {
        mainCamera = Camera.main;
        targetBoard = mainCamera.GetComponent<ReadBoardBehind>();
        WhiteBoarded = false;
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;

        scrollWheel.Enable();
        scrollWheel.performed += x => mouseScrollY = x.ReadValue<float>();

        q_key.Enable();
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();

        scrollWheel.Disable();

        q_key.Disable();
    }

    //Used to get the key down for "Q" button
    private void Update()
    {
        prevBool = currBool;
        currBool = q_key.ReadValue<float>() > 0.1f;
        
        if (prevBool == true && currBool == false)
        {
            q_keyup = true;
            Debug.Log("q was hit");
        } 
        else
        {
            q_keyup = false;
        }
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Draggable"))
                {
                    StartCoroutine(DragUpdate(hit.collider.gameObject));
                }
                else if (hit.collider.gameObject.CompareTag("Resizeable"))
                {
                    dot = hit.collider.gameObject.transform;

                    //according to prefab structure
                    //Note: Realizing unavoidable because .root affects scaling
                    note = hit.collider.gameObject.transform.parent;
                    initialScale = dot.parent.localScale.x;

                    StartCoroutine(ResizeUpdate(hit.collider.gameObject));
                }
            }
        }
    }

    private IEnumerator ResizeUpdate(GameObject clickedObject)
    {
        Transform hitObject = clickedObject.transform;
        float resizeDistance = Vector3.Distance(hitObject.position, mainCamera.transform.position);
        clickedObject.TryGetComponent<Rigidbody>(out var rb);

        startDistance = (dot.position - note.position).magnitude;
        currentScale = (dot.position - note.position).magnitude / startDistance;

        while (mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            Resize(ray, resizeDistance, hitObject);
            yield return null;
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        //Has to remain as parent because root when post it is parents by the whiteboard, it will be the new root which 
        //will create error in 2D movement
        Transform hitObject = clickedObject.transform.parent.transform;

        float initialDistance = Vector3.Distance(hitObject.position, mainCamera.transform.position);
        distance = initialDistance;
        clickedObject.TryGetComponent<Rigidbody>(out var rb);

        q_keyup = false;

        while (mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (q_keyup && clickedObject.transform.parent.CompareTag("PostIt"))
            {
                WhiteBoarded = !WhiteBoarded;
                if (WhiteBoarded)
                {
                    if (targetBoard.board != null)
                    {
                        hitObject.SetParent(targetBoard.board.transform); // parenting needed to scale the board all together with notes
                    }
                    else
                    {
                        WhiteBoarded = !WhiteBoarded;
                    }
                }
                else
                {
                    hitObject.SetParent(null);
                }

            }
            if (WhiteBoarded && clickedObject.transform.parent.CompareTag("PostIt"))
            {
                Move2d(hitObject, ray);
            }
            else
            {
                Move3d(hitObject, ray);
            }
            yield return null;
        }
    }

    public void Move3d(Transform hitObject, Ray ray)
    {
        if (mouseScrollY > 0)
        {
            distance = distance + speed;
        }
        else if (mouseScrollY < 0)
        {
            distance = distance - speed;

            if (distance < 0.4f)
            {
                distance = 0.4f;
            }
        }
        hitObject.position = Vector3.SmoothDamp(hitObject.position, ray.GetPoint(distance), ref velocity, mouseDragSpeed);
        hitObject.rotation = mainCamera.transform.rotation;
    }

    public void Move2d(Transform hitObject, Ray ray)
    {
        if (hitObject.transform.parent == null)
        {
            WhiteBoarded = false;
        }
        else
        {
            Vector3 pos = mainCamera.transform.position + ray.direction * distance;
            Vector3 localPos = hitObject.parent.transform.InverseTransformPoint(pos);
            hitObject.transform.localPosition = new Vector3(localPos.x, localPos.y, -10.0f); 
            hitObject.rotation = hitObject.parent.rotation;
        }
    }

    public void Resize(Ray ray, float resizeDistance, Transform hitObject)
    {
        Vector3 pos = ray.GetPoint(resizeDistance); 
        Vector3 localPos = dot.transform.parent.transform.InverseTransformPoint(pos);
        dot.transform.localPosition = new Vector3(localPos.x, localPos.y, dot.localPosition.z);


        currentScale = (dot.position - note.position).magnitude / startDistance;
        if (currentScale > 0)
        {
        dot.parent.transform.localScale = Vector3.one * initialScale * currentScale;
        }
    }
}