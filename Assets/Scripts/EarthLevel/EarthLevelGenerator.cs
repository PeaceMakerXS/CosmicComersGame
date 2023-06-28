using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthLevelGenerator : MonoBehaviour
{
    public Transform CellsParent;
    public Transform EnemiesParent;
    public Transform OtherObjectsParent;
    public Transform DeathLine;
    private float deathLineYCoord;

    private Transform player;

    public List<GameObject> CellVariants = new();
    public List<GameObject> EnemyVariants = new();
    public List<GameObject> LandscapeObjectsVariants = new();
    public List<GameObject> ItemsVariants = new();
    public List<GameObject> SuitPartsList = new();

    private float[] cellLenghts = { 0, 0, 0 };

    private float firstHeightLevel;
    private float secondHeightLevel;
    private float maxHeightLevelOffset;

    private float minXOffset_firstHeightLevel;
    private float maxXOffset_firstHeightLevel;
    private float minXOffset_secondHeightLevel;
    private float maxXOffset_secondHeightLevel;

    private int firstCellsCount;

    private float currentX_firstHeightLevel;
    private float currentX_secondHeightLevel;
    private int currentHeightLevel;

    private int suitPartsOnMap;
    private int suitPartsCollected;

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

        firstCellsCount = 16;

        currentX_firstHeightLevel = CellsParent.position.x;
        currentX_secondHeightLevel = CellsParent.position.x;
        currentHeightLevel = 0;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        deathLineYCoord = DeathLine.position.y;

        suitPartsOnMap = 0;
        suitPartsCollected = 0;
    }

    private void GenerateInitialEnvironment()
    {
        for (int i = 0; i < firstCellsCount; i++)
        {
            GenerateHeightLevel();
        }
    }

    private void GenerateHeightLevel()
    {
        if (currentHeightLevel == 0)
        {
            currentX_firstHeightLevel = GenerateRandomCell(currentX_firstHeightLevel,
                    firstHeightLevel + UnityEngine.Random.Range(-maxHeightLevelOffset, maxHeightLevelOffset));
            currentX_firstHeightLevel += UnityEngine.Random.Range(minXOffset_firstHeightLevel, maxXOffset_firstHeightLevel);
            currentHeightLevel = 1;
        }

        else
        {
            currentX_secondHeightLevel = GenerateRandomCell(currentX_secondHeightLevel,
                    secondHeightLevel + UnityEngine.Random.Range(-maxHeightLevelOffset, maxHeightLevelOffset));
            currentX_secondHeightLevel += UnityEngine.Random.Range(minXOffset_secondHeightLevel, maxXOffset_secondHeightLevel);
            currentHeightLevel = 0;
        }
    
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
        float cellLenght;

        if (cellLenghts[cellType] == 0)
        {
            Collider2D collider = cell.GetComponentInChildren<Collider2D>();
            cellLenght = collider.bounds.max.x - collider.bounds.min.x - 0.6f;
            cellLenghts[cellType] = cellLenght;
        }

        else
        {
            cellLenght = cellLenghts[cellType];
        }
        
        cell.transform.position = new Vector3(x + cellLenght/2, y, 0);
        float cellCenterX = cell.transform.position.x;
        cells.Add(cell);

        int objectsCount = UnityEngine.Random.Range(0, maxObjectsCount + 1);
        int landscapeObjectsCount = UnityEngine.Random.Range(0, maxObjectsCount + 1);
        float objectsDistance = cellLenght / (landscapeObjectsCount + 1);
        float currentX = cellCenterX - cellLenght / 2;

        // Генерация ландшафта на заднем фоне (кол-во не зависит от кол-ва осязаемых объектов)
        for (int i = 0; i < landscapeObjectsCount; i++)
        {
            var landscapeObject = Instantiate(LandscapeObjectsVariants[UnityEngine.Random.Range(0, LandscapeObjectsVariants.Count)], OtherObjectsParent);
            landscapeObject.transform.position = new Vector3(currentX, y + yCorrection, 0);
            otherObjects.Add(landscapeObject);
            currentX += objectsDistance;
        }

        if (objectsCount > 0)
        {
            int enemiesCount = 0;
            int otherObjectsCount = 0;

            objectsDistance = cellLenght / (objectsCount + 1);
            currentX = cellCenterX - cellLenght / 2;

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
                var otherObject = Instantiate(ItemsVariants[UnityEngine.Random.Range(0, ItemsVariants.Count)], OtherObjectsParent);
                otherObject.transform.position = new Vector3(currentX, y+yCorrection, 0);
                otherObjects.Add(otherObject);
                currentX += objectsDistance;
            }
        }
        return cellCenterX + cellLenght/2;
    }

    private float[] GenerateCellWithSuitPart()
    {
        var cell = Instantiate(CellVariants[0], CellsParent);
        float currentX = Mathf.Max(currentX_firstHeightLevel, currentX_secondHeightLevel) + cellLenghts[0]/2;
        float currentY = firstHeightLevel + (secondHeightLevel - firstHeightLevel) / 2;
        cell.transform.position = new Vector3(currentX, currentY, 0);
        cells.Add(cell);

        var suitPart = Instantiate(SuitPartsList[suitPartsCollected], OtherObjectsParent);
        suitPart.transform.position = new Vector3(currentX, currentY + GenerationConstants.SmallCell.yCorrection, 0);
        otherObjects.Add(suitPart);
        
        return new float[] { currentX + cellLenghts[0]/2 + maxXOffset_firstHeightLevel,
                currentX + cellLenghts[0]/2 + maxXOffset_secondHeightLevel};
    }

    private void CleanAndRecreateObjects()
    {
        if (player)
        {
            int index = 0;

            while (index < enemies.Count)
            {
                if (!enemies[index])
                {
                    enemies.RemoveAt(index);
                }

                else if (player.position.x > enemies[index].transform.position.x + GenerationConstants.requiredHeroOffset)
                {
                    Destroy(enemies[index]);
                    enemies.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            index = 0;

            while (index < otherObjects.Count)
            {   
                if (!otherObjects[index])
                {
                    otherObjects.RemoveAt(index);
                }

                else if (player.position.x > otherObjects[index].transform.position.x + GenerationConstants.requiredHeroOffset)
                {
                    if (otherObjects[index].CompareTag("GravitationSuite"))
                    {
                        suitPartsOnMap = 0;
                    }

                    Destroy(otherObjects[index]);
                    otherObjects.RemoveAt(index);

                }

                else
                {
                    index++;
                }
            }

            if (cells.Count == 0 || player.position.x > cells[0].transform.position.x + GenerationConstants.requiredHeroOffset)
            {
                if (cells[0])
                {
                    Destroy(cells[0]);
                    cells.RemoveAt(0);
                }

                DanilHero danilHero = FindObjectOfType<DanilHero>();

                if (suitPartsCollected < danilHero.suitPartsCollected)
                {
                    suitPartsOnMap = 0;
                    suitPartsCollected = danilHero.suitPartsCollected;
                }

                if (suitPartsCollected < GenerationConstants.suitPartsCount && suitPartsOnMap == 0 && (suitPartsCollected + 1) * GenerationConstants.neededCoinsForSuitePart <= danilHero.coinsCollected)
                {
                    float[] changedHeightLevels = GenerateCellWithSuitPart();
                    currentX_firstHeightLevel = changedHeightLevels[0];
                    currentX_secondHeightLevel = changedHeightLevels[1];
                    suitPartsOnMap = 1;
                }

                else
                {
                    GenerateHeightLevel();
                }

                DeathLine.position = new Vector3(player.position.x, deathLineYCoord, 0);
            }
        }
    }
}

 public static class GenerationConstants
{
    public const int cellTypesCount = 3;
    public const int suitPartsCount = 6;
    public const float requiredHeroOffset = 20f;
    public const int neededCoinsForSuitePart = 6;

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