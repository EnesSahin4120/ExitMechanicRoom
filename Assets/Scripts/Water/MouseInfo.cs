using UnityEngine;
using System;

public class MouseInfo : MonoBehaviour
{
    private Vector2 firstMousePos;
    private Vector2 _diffMousePos;
    public Vector2 DiffMousePos
    {
        get
        {
            return _diffMousePos;
        }
        set
        {
            _diffMousePos = value;
        }
    }

    public static event Action onPressed;
    public static event Action onPressedUp;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _diffMousePos = Vector2.zero;
            firstMousePos = Input.mousePosition;
            Press();
        }

        //Set mouse X-Y differences to use on draggable object placement
        if (Input.GetMouseButton(0))
            _diffMousePos = (Vector2)Input.mousePosition - firstMousePos;

        if (Input.GetMouseButtonUp(0))
            PressUp();
    }

    public void Press()
    {
        if (onPressed != null)
        {
            onPressed();
        }
    }

    public void PressUp()
    {
        if (onPressedUp != null)
        {
            onPressedUp();
        }
    }
}