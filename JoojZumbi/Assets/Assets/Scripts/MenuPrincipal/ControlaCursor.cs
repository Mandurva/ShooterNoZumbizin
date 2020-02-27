using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCursor : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot;
    private void Start()
    {
        hotSpot = new Vector2(cursorTexture.width / 8f, cursorTexture.height / 8f);
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

}
