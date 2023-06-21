using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGeneration : MonoBehaviour
{
    //острова
    private GameObject Cell;
    public List<GameObject> Cells_toc = new List<GameObject>();
    public Transform Zero;
    public int Height, Width;
    private List<GameObject> Cells = new List<GameObject>();

    int r_cell = 0;
    int length = 0;
    int r_square = 0;
    int r_square_q = 0;

    float y = 0;
    float x = 6;

    //мобы квадраты
    private GameObject Square;
    public Transform squaresParent;
    private List<GameObject> Squares= new List<GameObject>();
    public List<GameObject> Squares_toc = new List<GameObject>();


    //спайки
    public GameObject Spike;
    public Transform spikeParent;
    private List<GameObject> Spikes = new List<GameObject>();
    private GameObject CellSpike;

    //бочки
    public GameObject Barrel;
    public Transform barrelParent;
    private List<GameObject> Barrels = new List<GameObject>();
    private GameObject CellBarrel;

    private void Start()
    {
        Generate();
        GenerateSpike();
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
        if (Hero.Instance.transform.position.x > Spikes[0].transform.position.x + 35f)
        {
            Destroy(Spikes[0]);
            Spikes.RemoveAt(0);
            GenerateSpike();
        }
        if (Hero.Instance.transform.position.x > Cells[0].transform.position.x + 35f)
        {
            Destroy(Cells[0]);
            Cells.RemoveAt(0);
            GenerateNextCell();
            
            GenerateEnemy();

            GenerateBarrel();
        }
        if (Squares.Count > 10)
        {
            Destroy(Squares[0]);
            Squares.RemoveAt(0);
        }
        if (Barrels.Count > 10)
        {
            Destroy(Barrels[0]);
            Barrels.RemoveAt(0);
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
    }

    private void GenerateEnemy()
    {   
        for (int i = 0; i < r_square_q; i++)
        {
            var square = Instantiate(Square, squaresParent);
            square.transform.localPosition = new Vector3(x, y + 3f, 0);
            Squares.Add(square);
            x += 1;
        }
    }

    private void GenerateBarrel()
    {
        int r_barrel_q = Random.Range(1, 4);
        CellBarrel = Cells[Cells.Count-1];
        for (int i = 0; i < r_barrel_q; i++)
        {
            x = (int)CellBarrel.transform.localPosition.x + Random.Range(-2, 4);
            y = CellBarrel.GetComponent<Collider2D>().bounds.max.y;
            var barrel = Instantiate(Barrel, barrelParent);
            Barrels.Add(barrel);
            barrel.transform.localPosition = new Vector3(x, y + 2, 0);
            x += 1;
        }
    }

    private void GenerateSpike()
    {
        CellSpike = Cells[Cells.Count - 1];
        x = (int)CellSpike.transform.localPosition.x + Random.Range(-2, 4); ;
        y = CellSpike.GetComponent<Collider2D>().bounds.max.y;
        var spike = Instantiate(Spike, spikeParent);
        Spikes.Add(spike);
        spike.transform.localPosition = new Vector3(x, y+1, 0);
    }

    private void Ran()
    {
        r_square = Random.Range(0, 2); //на выбор
        Square = Squares_toc[r_square];
        r_square_q = Random.Range(0, 4);//на количество

        r_cell = Random.Range(0, 5);
        length = Random.Range(16, 20);
        Cell = Cells_toc[r_cell];
    }
}