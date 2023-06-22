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
    private List<GameObject> Cells = new List<GameObject>();

    int random_cell = 0;
    int random_length_for_cells = 0;
    int random_square = 0;
    int random_square_borl = 0;
    int random_amount_squares = 0;

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
    public GameObject Barrel;
    public Transform barrelParent;
    private List<GameObject> Barrels = new List<GameObject>();
    private GameObject CellBarrel;

    private void Start()
    {
        Ran();
        StartGenerate();
    }

    private void Update()
    {   
        if (Hero.Instance.transform.position.x > Cells[0].transform.position.x + 35f)
        {
            Destroy(Cells[0]);
            Cells.RemoveAt(0);

            Ran();

            GenerateNextCell();
            
            GenerateEnemies();
        }
        if (Spikes.Count > 5)
        {
            Destroy(Spikes[0]);
            Spikes.RemoveAt(0);
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

    private void StartGenerate()
    {
        for (int i = 0; i < Width; i++)
        {
            Ran();
            var cell = Instantiate(Cell, Zero);
            Cells.Add(cell);
            y += Random.Range(-2, 1);
            cell.transform.localPosition = new Vector3(x, y, 0);
            x += random_length_for_cells;
        }
    }

    private void GenerateNextCell()
    {
        var lastCell = Cells[Cells.Count - 1];
        x = (float)lastCell.transform.localPosition.x + random_length_for_cells;
        y = (float)lastCell.transform.localPosition.y + Random.Range(-2,2);
        var cell = Instantiate(Cell, Zero);
        Cells.Add(cell);
        cell.transform.localPosition = new Vector3(x, y, 0);
    }

    private void GenerateEnemies()
    {
        int situation = Random.Range(0, 4);

        switch (situation)
        {
            case 0: //������ 
                break;
            case 1: //��������
                random_square_borl = Random.Range(0, 11);

                if (random_square_borl <= 8)
                    for (int i = 0; i < random_amount_squares; i++) //���������
                    {
                        var square = Instantiate(Square, squaresParent);
                        square.transform.localPosition = new Vector3(x, y + 3f, 0);
                        Squares.Add(square);
                        x += 1;
                    }
                else
                {
                    Square = Squares_toc[2]; //�������
                    var square = Instantiate(Square, squaresParent);
                    square.transform.localPosition = new Vector3(x, y + 3f, 0);
                    Squares.Add(square);
                }
                break;
            case 2://�����
                int random_barrel_amount = Random.Range(1, 4);
                CellBarrel = Cells[Cells.Count - 1];
                for (int i = 0; i < random_barrel_amount; i++)
                {
                    var barrel = Instantiate(Barrel, barrelParent);
                    Barrels.Add(barrel);
                    barrel.transform.localPosition = new Vector3(x, y + 1, 0);
                    x += 1;
                }
                break;
            case 3://�����
                CellSpike = Cells[Cells.Count - 1];
                var spike = Instantiate(Spike, spikeParent);
                Spikes.Add(spike);
                spike.transform.localPosition = new Vector3(x + Random.Range(-2, 4), y + 0.5f, 0);
                break;
        }
    }
    private void Ran()
    {
        

        random_square = Random.Range(0, 2); //�� �����
        Square = Squares_toc[random_square];
        random_amount_squares = Random.Range(1, 4);//�� ����������

        random_cell = Random.Range(0, 5);
        random_length_for_cells = Random.Range(16, 20);
        Cell = Cells_toc[random_cell];
    }
}