using UnityEngine;

public class TouchInputHandler : MonoBehaviour
{
    public void Update()
    {
        DeployTouches();
    }

    public void DeployTouches()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
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
    }
}