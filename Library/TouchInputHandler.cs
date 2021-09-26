using UnityEngine;

public class TouchInputHandler : MonoBehaviour
{
    public void Update()
    {
        DeployTouches();
    }

    public void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DeployRay(Input.mousePosition);
        }
    }

    public void DeployTouches()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                DeployRay(touch.position);
            }
        }
    }

    public static void DeployRay(Vector2 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
        if (hit)
        {
            Note note = hit.collider.GetComponent<Note>();
            if (note)
            {
                note.HitNote();
            }
        }
    }

}