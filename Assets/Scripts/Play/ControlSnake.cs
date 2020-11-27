using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSnake : MonoBehaviour
{
    private PlaySnake playSnake;
    private int horizontal;
    private int vertical;

    void Start()
    {
        this.ResetAxis();
        playSnake = GetComponent<PlaySnake>();
    }

    void Update()
    {
        this.ResetAxis();
        this.AxisInput();
        this.GetDirectionFromInput();
    }

    void ResetAxis()
    {
        horizontal = 0;
        vertical = 0;
    }

    void AxisInput()
    {
        horizontal = this.GetAxis(Axis.Horizontal);
        vertical = this.GetAxis(Axis.Vertical);

        if (horizontal != 0)
        {
            vertical = 0;
        }
    }

    int GetAxis(Axis axis)
    {
        if (axis == Axis.Horizontal)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                return -1;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                return 1;
            }
        }
        else if (axis == Axis.Vertical)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                return 1;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                return -1;
            }
        }
        return 0;
    }

    void GetDirectionFromInput()
    {
        if (vertical != 0)
        {
            if (vertical == 1)
            {
                playSnake.SetDirectionFromInput(Direction.UP);
            }
            else
            {
                playSnake.SetDirectionFromInput(Direction.DOWN);
            }
        }
        else if (horizontal != 0)
        {
            if (horizontal == 1)
            {
                playSnake.SetDirectionFromInput(Direction.RIGHT);
            }
            else
            {
                playSnake.SetDirectionFromInput(Direction.LEFT);
            }
        }
    }
}