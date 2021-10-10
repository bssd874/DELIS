using UnityEngine;

public class GameplayInput : MonoBehaviour
{
    public bool mouse;
    public bool touch;

    void Update()
    {
        MouseInput();
        TouchInput();
    }

    public void MouseInput()
    {
        if (!mouse) return;
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            RayInput(Input.mousePosition);
        }
}

    public void TouchInput()
    {
        if (!touch) return;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                RayInput(touch.position);
            }
        }
    }

    public void RayInput(Vector2 screen)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(screen), Vector2.zero);
 
        if(hit.collider != null)
        {
            Destroy(hit.collider);
            GameNote gameNote = hit.transform.GetComponent<GameNote>();
            gameNote.Hit();
        }
    }

}