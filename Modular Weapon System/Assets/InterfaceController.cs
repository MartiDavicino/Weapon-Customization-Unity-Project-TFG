using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentCursor
{
    NORMAL,
    CLICK,
    INTERACT
}
public class InterfaceController : MonoBehaviour
{
    public Texture2D cursor, cursorClick, cursorInteract;
    // Start is called before the first frame update
    private Vector2 cursorHotspot;

    CurrentCursor currentCursor;
    void Start()
    {
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.Auto);
        currentCursor = CurrentCursor.NORMAL;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCursorTexture(CurrentCursor currentCursor)
    {
        switch (currentCursor)
        {
            case CurrentCursor.CLICK:
                Cursor.SetCursor(cursorClick, cursorHotspot, CursorMode.Auto);
                break;
            case
                CurrentCursor.INTERACT:
                Cursor.SetCursor(cursorInteract, cursorHotspot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(cursor, cursorHotspot, CursorMode.Auto);
                break;
        }

    }
}
