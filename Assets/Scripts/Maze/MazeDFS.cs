using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDFS : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    public Vector2Int size;
    public int startPos = 0;
    public GameObject[] space;
    public Vector2 offset;
    public int height;
    public List<SpaceState> rooms;
    public List<SpaceState> roomsForElev;

    List<Cell> board;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
        ElevatorSet();
        FireExSet();
    }

    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {

                Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];
                if(currentCell.visited)
                {
                    int randomRoom = Random.Range(0, space.Length);
                    var newRoom = Instantiate(space[randomRoom], new Vector3(i * offset.x, height, -j * offset.y), Quaternion.identity, transform).GetComponent<SpaceState>();
                    newRoom.UpdateRoom(currentCell.status);

                    newRoom.name += " " + i + " - " + j;

                    if(i == 0 && j == 0)
                    {
                        newRoom.walls[0].SetActive(false);
                        if(height == -10 || height == -20)
                        {
                            newRoom.mapChangePoint.SetActive(true);
                            if(height == -10)
                            {
                                newRoom.mapChangePoint.GetComponent<MapChange>().floor = 3;
                            }
                            else
                            {
                                newRoom.mapChangePoint.GetComponent<MapChange>().floor = 2;
                            }
                            //newRoom.endPoint.SetActive(true);
                        }
                    }
                    else if(i == 6 && j == 6)
                    {
                        newRoom.walls[1].SetActive(false);
                        newRoom.mapChangePoint.SetActive(true);
                        if (height == 0)
                        {
                            newRoom.mapChangePoint.GetComponent<MapChange>().floor = 4;
                        }
                        else if(height == -10)
                        {
                            newRoom.mapChangePoint.GetComponent<MapChange>().floor = 3;
                        }
                        else if(height == -20)
                        {
                            newRoom.mapChangePoint.GetComponent<MapChange>().floor = 2;
                        }
                        else if(height == -30)
                        {
                            newRoom.mapChangePoint.GetComponent<MapChange>().floor = 1;
                        }
                            //newRoom.endPoint.SetActive(true);
                    }

                    rooms.Add(newRoom);
                    

                }
                
            }
        }

    }
   
    void ElevatorSet()
    {
        foreach (var room in rooms)
        {
            int temp;
            temp = room.CheckWalls();

            if(temp != 0)
            {
                roomsForElev.Add(room);
            }

        }

        int select = Random.Range(4, roomsForElev.Count);
        //Debug.Log(select);
        roomsForElev[select].DoorUpdate();
      //rooms[select].DoorUpdate();

    }

    void FireExSet()
    {
        int select = Random.Range(4, rooms.Count);
        //Debug.Log(select);
        rooms[select].fireEx.SetActive(true);
    }
    void MazeGenerator()
    {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while (k < 1000)
        {
            k++;

            board[currentCell].visited = true;

            if (currentCell == board.Count - 1)
            {
                break;
            }

            //Check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if (newCell > currentCell)
                {
                    //down or right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }

            }

        }
        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        //check up neighbor
        if (cell - size.x >= 0 && !board[(cell - size.x)].visited)
        {
            neighbors.Add((cell - size.x));
        }

        //check down neighbor
        if (cell + size.x < board.Count && !board[(cell + size.x)].visited)
        {
            neighbors.Add((cell + size.x));
        }

        //check right neighbor
        if ((cell + 1) % size.x != 0 && !board[(cell + 1)].visited)
        {
            neighbors.Add((cell + 1));
        }

        //check left neighbor
        if (cell % size.x != 0 && !board[(cell - 1)].visited)
        {
            neighbors.Add((cell - 1));
        }

        return neighbors;
    }
}