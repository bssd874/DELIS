using UnityEngine;

public class GameplayInput : MonoBehaviour
{
    public bool mouse = true;
    public bool touch = true;

    void Update()
    {
        if (mouse) MouseInput();
        if (touch) TouchInput();
    }

    public static void MouseInput()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            RayInput(Input.mousePosition);
        }
    }

    public static void TouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                RayInput(touch.position);
            }
        }
    }

    public static void RayInput(Vector2 screen)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(screen), Vector2.zero);

        if (hit.collider != null)
        {
            Destroy(hit.collider);
            GameNote gameNote = hit.transform.GetComponent<GameNote>();
            gameNote.Hit();
        }
    }

}