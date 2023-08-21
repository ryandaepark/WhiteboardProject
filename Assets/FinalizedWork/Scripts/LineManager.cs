using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LineManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 lineBegin;
    private Vector3 lineEnd;

    [SerializeField]
    private InputAction mouseClick;

    private Camera mainCamera;
    private GameObject first_anchor;
    private GameObject second_anchor;

    private LinkedList<Line> lineData = new LinkedList<Line>();

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        int layerMask = 1 << 8;
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("LineAnchor"))
                {
                    first_anchor = hit.collider.gameObject;
                    StartCoroutine(FindSecondAnchorUpdate());
                }
            }
        }
    }

    private IEnumerator FindSecondAnchorUpdate()
    {
        while (mouseClick.ReadValue<float>() != 0)
        {
            yield return null;
        }
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        int layerMask = 1 << 8;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            if (mouseClick.ReadValue<float>() == 0)
            {
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider);
                    if (hit.collider.gameObject.CompareTag("LineAnchor"))
                    {
                        second_anchor = hit.collider.gameObject;
                    }
                }
            }
        }
        CreateLineRenderer();
        yield return null;
    }

    private void CreateLineRenderer()
    {
        if (first_anchor == null || second_anchor == null || first_anchor == second_anchor)
        {
            first_anchor = null;
            second_anchor = null;
        }
        else
        {
            lineRenderer = new GameObject().AddComponent<LineRenderer>();
            lineRenderer.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(0, .025f);
            curve.AddKey(1, .025f);
            lineRenderer.widthCurve = curve;

            lineBegin = first_anchor.transform.position;
            lineRenderer.SetPosition(0, lineBegin);
            lineEnd = second_anchor.transform.position;
            lineRenderer.SetPosition(1, lineEnd);

            lineData.AddLast(new Line(first_anchor, second_anchor, lineRenderer));
        }

    }

    private void Update()
    {
        foreach (Line line in lineData)
        {
            if (line.Anchor_1 == null || line.Anchor_2 == null)
            {
                lineData.Remove(line);
                Destroy(line.RenderedLine);
            }
            line.RenderedLine.SetPosition(0, line.Anchor_1.transform.position);
            line.RenderedLine.SetPosition(1, line.Anchor_2.transform.position);
        }
    }
}

class Line
{
    public GameObject Anchor_1 { get; set; }
    public GameObject Anchor_2 { get; set; }
    public LineRenderer RenderedLine { get; set; }


    public Line(GameObject anchor1, GameObject anchor2, LineRenderer line)
    {
        this.Anchor_1 = anchor1;
        this.Anchor_2 = anchor2;
        this.RenderedLine = line;
    }
}

