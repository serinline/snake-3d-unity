using System.Collections.Generic;
using UnityEngine;

public class PlaySnake : MonoBehaviour
{
    [SerializeField]
    private GameObject tail;

    public Direction direction;
    private List<Vector3> position;
    private Transform transf;

    private List<Rigidbody> nodes;
    private Rigidbody current_body;
    private Rigidbody current_head;

    public bool fruit_eaten;
    private bool moving;
    public bool game_over;

    public float frequency = 1f; 
    private float count_frequency;

    void Start()
    {
        transf = transform;
        current_body = GetComponent<Rigidbody>();
        game_over = false;
        this.InitSnake();
        this.InitPlay();
        this.InitPosition();
    }

    void Update()
    {
        this.CheckFrequency();
    }

    void FixedUpdate()
    {
        if (moving)
        {
            moving = false;
            this.MoveSnake();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(Tag.WALL) || collider.CompareTag(Tag.TAIL))
        {
            Time.timeScale = 0f;
            game_over = true;
        }
        if (collider.CompareTag(Tag.FRUIT))
        {
            collider.gameObject.SetActive(false);
            fruit_eaten = true;
        }
    }

    void InitPosition()
    {
        position = new List<Vector3> {
            new Vector3(-0.6f, 0f),
            new Vector3(0f, 0.6f), 
            new Vector3(0.6f, 0f),
            new Vector3(0f, -0.6f) 
        };
    }

    void InitSnake()
    {
        nodes = new List<Rigidbody> {
            transf.GetChild(0).GetComponent<Rigidbody>(),
            transf.GetChild(1).GetComponent<Rigidbody>(),
            transf.GetChild(2).GetComponent<Rigidbody>()
        };
        this.InitHead(nodes[0]);
    }

    void InitHead(Rigidbody head)
    {
        current_head = head;
    }

    void InitPlay()
    {
        direction = (Direction)Random.Range(0, 4);
        switch (direction) 
        {
            case Direction.UP:
                nodes[1].position = nodes[0].position - new Vector3(0f, 0.6f, 0f); 
                nodes[2].position = nodes[0].position - new Vector3(0f, 1.2f, 0f);
                break;
            case Direction.RIGHT:
                nodes[1].position = nodes[0].position - new Vector3(0.6f, 0f, 0f);
                nodes[2].position = nodes[0].position - new Vector3(1.2f, 0f, 0f);
                break;
            case Direction.LEFT:
                nodes[1].position = nodes[0].position + new Vector3(0.6f, 0f, 0f);
                nodes[2].position = nodes[0].position + new Vector3(1.2f, 0f, 0f);
                break;
            case Direction.DOWN:
                nodes[1].position = nodes[0].position + new Vector3(0f, 0.6f, 0f);
                nodes[2].position = nodes[0].position + new Vector3(0f, 1.2f, 0f);
                break;
        }

    }

    void CheckFrequency()
    {
        count_frequency += Time.deltaTime;
        if (count_frequency >= frequency )
        {
            count_frequency = 0;
            moving = true;
        }
    }

    void MoveSnake()
    {
        this.ChangePositions();

        if (fruit_eaten)
        {
            fruit_eaten = false;
            this.ExtendSnake();
        }
    }

    void ChangePositions()
    {
        int index = (int)direction;
        Vector3 delta_pos = position[index]; //kierunek
        Vector3 parent_pos = current_head.position;
        Vector3 prev_pos;
        current_head.position += delta_pos;
        current_body.position += delta_pos;

        for (int i = 1; i < nodes.Count; i++)
        {
            prev_pos = nodes[i].position;
            nodes[i].position = parent_pos;
            parent_pos = prev_pos;
        }
    }

    void ExtendSnake()
    {
        GameObject newTail = Instantiate(tail, nodes[nodes.Count - 1].position, Quaternion.identity);
        newTail.transform.SetParent(transform, true);
        nodes.Add(newTail.GetComponent<Rigidbody>());
    }

    public void SetDirectionFromInput(Direction direction_now)
    {
        if (this.CheckDirectionProper(direction_now))
        {
            return;
        }
        direction = direction_now;
        this.MoveSnakeNow();
    }

    bool CheckDirectionProper(Direction direction_now)
    {
        return direction_now == Direction.UP && direction == Direction.DOWN ||
            direction_now == Direction.DOWN && direction == Direction.UP ||
            direction_now == Direction.RIGHT && direction == Direction.LEFT ||
            direction_now == Direction.LEFT && direction == Direction.RIGHT;
    }

    private void MoveSnakeNow()
    {
        count_frequency = 0;
        moving = false;
        this.MoveSnake();
    }
}
