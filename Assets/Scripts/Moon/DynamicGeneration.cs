using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class DynamicGeneration : MonoBehaviour
{
    //острова
    private GameObject Cell;
    public List<GameObject> Cells_toc = new List<GameObject>();
    public Transform Zero;
    public int Height, Width;
    private List<GameObject> Cells = new List<GameObject>();

    int random_cell = 0;
    int random_length_for_cells = 0;
    int random_square_borl = 0;
    int random_amount_squares = 0;

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

    //бочки
    public GameObject Barrel;
    public Transform barrelParent;
    private List<GameObject> Barrels = new List<GameObject>();

    private void Start()
    {
        Ran();
        StartGenerate();
    }

    private void Update()
    {   
        if (Hero.Instance!= null)
        {
            if (Hero.Instance.transform.position.x > Cells[0].transform.position.x + 35f)
            {
                Destroy(Cells[0]);
                Cells.RemoveAt(0);

                Ran();

                GenerateNextCell();

                GenerateEnemies();
            }
            if (Spikes.Count > 10)
            {
                Destroy(Spikes[0]);
                Spikes.RemoveAt(0);
            }

            if (Squares.Count > 15)
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
        int situation = Random.Range(0, 9);

        switch (situation)
        {
            case 0: //ничего 
                break;
            case 1: //квадраты
                random_square_borl = Random.Range(0, 11); //большой или м маленький

                if (random_square_borl <= 8)
                {
                    random_amount_squares = Random.Range(1, 4);//на количество
                    Square = Squares_toc[Random.Range(0, 2)];
                    for (int i = 0; i < random_amount_squares; i++) //маленькие
                    {
                        GenerateLittleSquare(x);
                        x += 1;
                    }
                }
                else
                {
                    GenerateBigSquare(x);
                }
                break;
            case 2://бочки
                int random_barrel_amount = Random.Range(1, 4);
                for (int i = 0; i < random_barrel_amount; i++)
                {
                    GenerateBarrel(x);
                    x += 1;
                }
                break;
            case 3://спайк
                GenerateSpike(x + Random.Range(-2, 4));
                break;
            case 4://2 квадрата и бочка
                Square = Squares_toc[Random.Range(0, 2)];
                GenerateLittleSquare(x);

                GenerateBarrel(x+ 1.5f);

                GenerateLittleSquare(x+ 3);
                break;
            case 5: //2 спайка и 1 квадрат
                GenerateSpike(x-1);

                Square = Squares_toc[Random.Range(0, 2)];
                GenerateLittleSquare(x+1);

                GenerateSpike(x+3);
                break;
            case 6://квадрат на бочке
                Square = Squares_toc[Random.Range(0, 2)];
                GenerateBarrel(x);
                GenerateLittleSquare(x);
                break;
            case 7://друг на друге квадраты
                Square = Squares_toc[Random.Range(0, 2)];
                GenerateLittleSquare(x);
                GenerateLittleSquare(x);
                break;
            case 8://башня квадратов
                Square = Squares_toc[Random.Range(0, 2)];
                for (int i =0; i < 3; i++){
                    GenerateLittleSquare(x);
                    x+= 1;
                }
                x -= 3;
                for (int i = 0; i < 3; i++)
                {
                    GenerateLittleSquare(x);
                    x += 1;
                }
                break;
        }
    }

    private void GenerateLittleSquare(float x)
    {
        var square = Instantiate(Square, squaresParent);
        square.transform.localPosition = new Vector3(x, y + 1, 0);
        Squares.Add(square);
    }

    private void GenerateBigSquare(float x)
    {
        Square = Squares_toc[2]; //большой
        var square = Instantiate(Square, squaresParent);
        square.transform.localPosition = new Vector3(x, y + 3f, 0);
        Squares.Add(square);
    }

    private void GenerateBarrel(float x)
    {
        var barrel = Instantiate(Barrel, barrelParent);
        barrel.transform.localPosition = new Vector3(x, y + 1, 0);
        Barrels.Add(barrel);
    }

    private void GenerateSpike(float x)
    {
        var spike = Instantiate(Spike, spikeParent);
        Spikes.Add(spike);
        spike.transform.localPosition = new Vector3(x , y + 0.5f, 0);
    }

    private void Ran()
    {
        random_cell = Random.Range(0, 5);
        random_length_for_cells = Random.Range(16, 20);
        Cell = Cells_toc[random_cell];
    }
}