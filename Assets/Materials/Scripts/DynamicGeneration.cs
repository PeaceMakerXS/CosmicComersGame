using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGeneration : MonoBehaviour
{
    //�������
    private GameObject Cell;
    public List<GameObject> Cells_toc = new List<GameObject>();
    public Transform Zero;
    public int Height, Width;
    public List<GameObject> Cells = new List<GameObject>();

    int r = 0;
    int length = 0;
    int r_square = 0;
    int r_square_q = 0;

    float y = 0;
    float x = 6;

    //���� ��������
    private GameObject Square;
    public Transform squaresParent;
    private List<GameObject> Squares= new List<GameObject>();
    public List<GameObject> Squares_toc = new List<GameObject>();


    //������
    public GameObject Spike;
    public Transform spikeParent;
    private List<GameObject> Spikes = new List<GameObject>();
    private GameObject CellSpike;

    //�����

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < Width; i++)
        {
            Ran();
            var cell = Instantiate(Cell, Zero);
            Cells.Add(cell);
            y += Random.Range(-2, 1);
            cell.transform.localPosition = new Vector3(x, y, 0);
            x += length;
        }
    }

    private void Update()
    {
        if (Hero.Instance.transform.position.x > Cells[0].transform.position.x + 35f)
        {
            Destroy(Cells[0]);
            Cells.RemoveAt(0);
            GenerateNextCell();
            GenerateEnemy();
            GenerateSpike();
        }
        if (Squares.Count > 10)
        {
            Destroy(Squares[0]);
            Squares.RemoveAt(0);
        }
        if (Spikes.Count > 1)
        {
            Destroy(Spikes[0]);
            Spikes.RemoveAt(0);
        }
    }

    private void GenerateNextCell()
    {
        Ran();
        var lastCell = Cells[Cells.Count - 1];
        x = (float)lastCell.transform.localPosition.x + length;
        y = (float)lastCell.transform.localPosition.y + Random.Range(-2,2);
        var cell = Instantiate(Cell, Zero);
        Cells.Add(cell);
        cell.transform.localPosition = new Vector3(x, y, 0);
        Debug.Log("New cell");
    }

    private void GenerateEnemy()
    {
        for (int i = 0; i < r_square_q; i++)
        {
            var square = Instantiate(Square,squaresParent);
            square.transform.localPosition = new Vector3(x, y + 3f, 0);
            Squares.Add(square);
            x += 1;
            Debug.Log("New enemy");
        }
    }

    private void GenerateSpike()
    {
        //Ran();
        CellSpike = Cells[Cells.Count - 1];
        x = (int)CellSpike.transform.localPosition.x + Random.Range(-2, 4); ;
        y = CellSpike.GetComponent<Collider2D>().bounds.min.y +3f;
        var spike = Instantiate(Spike, spikeParent);
        Spikes.Add(spike);
        spike.transform.localPosition = new Vector3(x, y, 0);
    }

    private void Ran()
    {
        r_square = Random.Range(0, 2);
        Square = Squares_toc[r_square];
        r_square_q = Random.Range(0, 4);
        r = Random.Range(0, 5);
        length = Random.Range(16, 20);
        Cell = Cells_toc[r];
        Debug.Log(Cells.Count);
    }
}