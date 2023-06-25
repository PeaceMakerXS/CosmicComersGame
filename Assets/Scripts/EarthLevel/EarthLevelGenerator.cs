using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class EarthLevelGenerator : MonoBehaviour
{
    public Transform CellsParent;
    public Transform EnemiesParent;
    public Transform OtherObjectsParent;

    public List<GameObject> CellVariants = new();
    public List<GameObject> EnemyVariants = new();
    public List<GameObject> OtherObjectsVariants = new();

    private float firstHeightLevel;
    private float secondHeightLevel;
    private float maxHeightLevelOffset;

    private float minXOffset_firstHeightLevel;
    private float maxXOffset_firstHeightLevel;
    private float minXOffset_secondHeightLevel;
    private float maxXOffset_secondHeightLevel;

    private int firstCellsPerLevelCount;

    private float currentX_firstHeightLevel;
    private float currentX_secondHeightLevel;

    private List<GameObject> cells = new();
    private List<GameObject> enemies = new();
    private List<GameObject> otherObjects = new();

    private void Start()
    {
        SetAllFields();
        GenerateInitialEnvironment();
    }

    private void Update()
    {
        CleanAndRecreateObjects();
    }

    // Update is called once per frame

    private void SetAllFields()
    {
        firstHeightLevel = -2.5f;
        secondHeightLevel = 2f;
        maxHeightLevelOffset = 0.5f;

        minXOffset_firstHeightLevel = 0.5f;
        maxXOffset_firstHeightLevel = 2.5f;
        minXOffset_secondHeightLevel = 1.5f;
        maxXOffset_secondHeightLevel = 5f;

        firstCellsPerLevelCount = 5;

        currentX_firstHeightLevel = CellsParent.position.x;
        currentX_secondHeightLevel = CellsParent.position.x;
    }

    private void GenerateInitialEnvironment()
    {
        for (int i = 0; i < firstCellsPerLevelCount; i++)
        {
            GenerateHeightLevels();
        }
    }

    private void GenerateHeightLevels()
    {

        currentX_firstHeightLevel = GenerateRandomCell(currentX_firstHeightLevel,
                firstHeightLevel + UnityEngine.Random.Range(-maxHeightLevelOffset, maxHeightLevelOffset));
        currentX_secondHeightLevel = GenerateRandomCell(currentX_secondHeightLevel,
                secondHeightLevel + UnityEngine.Random.Range(-maxHeightLevelOffset, maxHeightLevelOffset));

        currentX_firstHeightLevel += UnityEngine.Random.Range(minXOffset_firstHeightLevel, maxXOffset_firstHeightLevel);
        currentX_secondHeightLevel += UnityEngine.Random.Range(minXOffset_secondHeightLevel, maxXOffset_secondHeightLevel);
    
    }

    private float GenerateRandomCell(float x, float y)
    {
        int cellNumber = UnityEngine.Random.Range(0, GenerationConstants.cellTypesCount);
        float xChanged = 0;

        switch(cellNumber)
        {
            case 0:
                xChanged = GenerateSmartCell(x, y, GenerationConstants.SmallCell.maxEnemiesCount, GenerationConstants.SmallCell.maxOtherObjectsCount,
                    GenerationConstants.SmallCell.maxObjectsCount, 0, GenerationConstants.SmallCell.yCorrection);
                break;
            case 1:
                xChanged = GenerateSmartCell(x, y, GenerationConstants.CommonCell.maxEnemiesCount, GenerationConstants.CommonCell.maxOtherObjectsCount,
                    GenerationConstants.CommonCell.maxObjectsCount, 1, GenerationConstants.CommonCell.yCorrection);
                break;
            case 2:
                xChanged = GenerateSmartCell(x, y, GenerationConstants.BigCell.maxEnemiesCount, GenerationConstants.BigCell.maxOtherObjectsCount,
                    GenerationConstants.BigCell.maxObjectsCount, 2, GenerationConstants.BigCell.yCorrection);
                break;
        }
        return xChanged;
    }

    private float GenerateSmartCell(float x, float y, int maxEnemiesCount, int maxOtherObjectsCount, int maxObjectsCount, int cellType, float yCorrection)
    {
        var cell = Instantiate(CellVariants[cellType], CellsParent);
        Collider2D collider = cell.GetComponentInChildren<Collider2D>();
        float cellLenght = collider.bounds.max.x - collider.bounds.min.x - 0.6f;
        cell.transform.position = new Vector3(x + cellLenght/2, y, 0);
        float cellCenterX = cell.transform.position.x;
        cells.Add(cell);

        int objectsCount = UnityEngine.Random.Range(0, maxObjectsCount + 1);

        if (objectsCount > 0)
        {
            int enemiesCount = 0;
            int otherObjectsCount = 0;

            float objectsDistance = cellLenght / (objectsCount + 1);
            float currentX = cellCenterX - cellLenght / 2;

            while (enemiesCount + otherObjectsCount != objectsCount)
            {
                enemiesCount = UnityEngine.Random.Range(0, maxEnemiesCount + 1);
                otherObjectsCount = UnityEngine.Random.Range(0, maxOtherObjectsCount + 1);
            }

            
            for (int i = 0; i < enemiesCount; i++)
            {
                var enemy = Instantiate(EnemyVariants[UnityEngine.Random.Range(0, EnemyVariants.Count)], EnemiesParent);
                enemy.transform.position = new Vector3(currentX, y+yCorrection, 0);
                enemies.Add(enemy);
                currentX += objectsDistance;
            }

            for (int i = 0; i < otherObjectsCount; i++)
            {
                var otherObject = Instantiate(OtherObjectsVariants[UnityEngine.Random.Range(0, OtherObjectsVariants.Count)], OtherObjectsParent);
                otherObject.transform.position = new Vector3(currentX, y+yCorrection, 0);
                otherObjects.Add(otherObject);
                currentX += objectsDistance;
            }
        }
        return cellCenterX + cellLenght/2;
    }

    private void CleanAndRecreateObjects()
    {
        if (DanilHero.Instance)
        {
            if (enemies[0])
            {
                if (DanilHero.Instance.transform.position.x > enemies[0].transform.position.x + GenerationConstants.requiredHeroOffset)
                {
                    Destroy(enemies[0]);
                    enemies.RemoveAt(0);
                }
            }

            if (otherObjects[0])
            {
                if (DanilHero.Instance.transform.position.x > otherObjects[0].transform.position.x + GenerationConstants.requiredHeroOffset)
                {
                    Destroy(otherObjects[0]);
                    otherObjects.RemoveAt(0);
                }
            }

            if (cells[0])
            {
                if (DanilHero.Instance.transform.position.x > cells[0].transform.position.x + GenerationConstants.requiredHeroOffset)
                {
                    Destroy(cells[0]);
                    cells.RemoveAt(0);
                    GenerateHeightLevels();
                }
            }
        }
    }
}
 public static class GenerationConstants
{
    public const int cellTypesCount = 3;
    public const float requiredHeroOffset = 15f;

    public struct SmallCell
    {
        public const int maxEnemiesCount = 1;
        public const int maxOtherObjectsCount = 2;
        public const int maxObjectsCount = 2;
        public const float yCorrection = 0.5f;
    }

    public struct CommonCell
    {
        public const int maxEnemiesCount = 2;
        public const int maxOtherObjectsCount = 2;
        public const int maxObjectsCount = 3;
        public const float yCorrection = 0.5f;
    }

    public struct BigCell
    {
        public const int maxEnemiesCount = 4;
        public const int maxOtherObjectsCount = 3;
        public const int maxObjectsCount = 4;
        public const float yCorrection = 0f;
    }
}