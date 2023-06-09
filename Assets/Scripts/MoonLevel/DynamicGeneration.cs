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

    //�����
    public GameObject Barrel;
    public Transform barrelParent;
    private List<GameObject> Barrels = new List<GameObject>();

    //stars
    public GameObject Star;
    public Transform StarParent;
    public int stars_amount = 0;

    //details
    public List<GameObject> Details_toc = new List<GameObject>();
    public int details_amount = 0;

    private GameObject[] Stars;

    private void Start()
    {
        Ran();
        StartGenerate();
        Stars = GameObject.FindGameObjectsWithTag("Star");
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
            case 0: //������ 
                GenerateStars();
                GenerateDetails();
                break;
            case 1: //��������
                random_square_borl = Random.Range(0, 11); //������� ��� � ���������

                if (random_square_borl <= 7)
                {
                    random_amount_squares = Random.Range(1, 4);//�� ����������
                    Square = Squares_toc[Random.Range(0, 2)];
                    for (int i = 0; i < random_amount_squares; i++) //���������
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
            case 2://�����
                int random_barrel_amount = Random.Range(1, 4);
                for (int i = 0; i < random_barrel_amount; i++)
                {
                    GenerateBarrel(x);
                    x += 1;
                }

                GenerateDetails();
                break;
            case 3://�����
                x += Random.Range(-1, 4);
                GenerateSpike(x);

                x += 1;
                GenerateStars();
                GenerateDetails();
                break;
            case 4://2 �������� � �����
                Square = Squares_toc[Random.Range(0, 2)];
                GenerateLittleSquare(x);

                GenerateBarrel(x+ 1.5f);

                GenerateLittleSquare(x+ 3);
                break;
            case 5: //2 ������ � 1 �������
                GenerateSpike(x-1);

                Square = Squares_toc[Random.Range(0, 2)];
                GenerateLittleSquare(x+1);

                GenerateSpike(x+3);
                break;
            case 6://������� �� �����
                Square = Squares_toc[Random.Range(0, 2)];
                GenerateBarrel(x);
                GenerateLittleSquare(x);

                x += 2;
                GenerateStars();
                GenerateDetails();
                break;
            case 7://���� �� ����� ��������
                Square = Squares_toc[Random.Range(0, 2)];
                GenerateLittleSquare(x);
                GenerateLittleSquare(x);

                x += 2;
                GenerateStars();
                GenerateDetails();
                break;
            case 8://����� ���������
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
        Square = Squares_toc[2]; //�������
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

    private void GenerateStars()
    {
        int stars_q = Random.Range(0, 6);
        y += Random.Range(0, 3);
        for (int i = 0; i <= stars_q; i++)
        {
            var star = Instantiate(Star, StarParent);
            star.transform.localPosition = new Vector3(x, y, 0);
            x++;
        }
        Stars = GameObject.FindGameObjectsWithTag("Star");
        if (Stars.Length > 0)
            foreach (var star in Stars)
            {
                if (Hero.Instance.transform.position.x > star.transform.position.x + 35f)
                {
                    Destroy(star);
                }
            }
    }

    private void GenerateDetails()
    {
        if (Details_toc.Count>0)
            if (stars_amount >= 10)
            {
                int chance = Random.Range(0, 11);
                if (chance<=9)
                {
                    y += Random.Range(0, 2);
                    int random_detail = Random.Range(0, Details_toc.Count);
                    var detail = Instantiate(Details_toc[random_detail], StarParent);
                    detail.transform.localPosition = new Vector3(x, y, 0);
                    Details_toc.RemoveAt(random_detail);
                }
            }
    }
    private void Ran()
    {
        random_cell = Random.Range(0, 5);
        random_length_for_cells = Random.Range(16, 20);
        Cell = Cells_toc[random_cell];
    }
}