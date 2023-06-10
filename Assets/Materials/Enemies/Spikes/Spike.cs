using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Spike : MonoBehaviour
{
    GroundGeneration Ground;
    private GameObject Cell;

    public GameObject SpikeCell;
    private List<GameObject> Spikes = new List<GameObject>();

    public Transform Zero;

    float length = 0;

    float y = 0;
    float x = 0;

    private void Start()
    {
        if (SpikeCell == null)
        {
            Debug.LogError("SpikeCell is not assigned in the Inspector");
            return;
        }
        Spikes.Add(SpikeCell);
        Ground = FindObjectOfType<GroundGeneration>();
        Generate();
    }
    private void Update()
    {
        if (Spikes.Count > 0)
            if (Hero.Instance.transform.position.x > Spikes[0].transform.position.x + 35f)
        {
            Destroy(Spikes[0]);
            Spikes.RemoveAt(0);
            Generate();
        }
    }

    private void Generate()
    {
        Ran();
        x = (int)Cell.transform.localPosition.x + length;
        y = Cell.GetComponent<Collider2D>().bounds.max.y+1f;
        var spike = Instantiate(SpikeCell, Zero);
        Spikes.Add( spike );
        spike.transform.localPosition = new Vector3(x, y, 0);
    }

    private void Ran()
    {
        length = Random.Range(-2,4);
        Cell = Ground.Cells[Ground.Cells.Count-1];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }
}