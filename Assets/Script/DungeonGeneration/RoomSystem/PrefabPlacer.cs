using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PrefabPlacer : MonoBehaviour
{
    [SerializeField]
    private GameObject itemPrefab;

    public List <GameObject> PlaceEnemies(List <EnemyPlacementData> enemyPlacementData, ItemPlacementHelper itemPlacementHelper)
    {
        List<GameObject> placedObjects = new List<GameObject>();

        foreach (var placementData in enemyPlacementData)
        {
            for (int i = 0; i < placementData.Quantity; i++)
            {
                Vector2? possiblePlacementSpot = itemPlacementHelper.GetItemPlacementPosition(
                    PlacementType.OpenSpace,
                    100,
                    placementData.enemySize,
                    false
                    );
                if (possiblePlacementSpot.HasValue )
                {

                    placedObjects.Add(CreateObject(placementData.enemyPrefab, possiblePlacementSpot.Value + new Vector2 (0.5f, 0.5f)));
                }
            }
        }
        return placedObjects;
    }    public List <GameObject> PlaceRocks(List <RockPlacementData> enemyPlacementData, ItemPlacementHelper itemPlacementHelper)
    {
        List<GameObject> placedObjects = new List<GameObject>();

        foreach (var placementData in enemyPlacementData)
        {
            for (int i = 0; i < placementData.Quantity; i++)
            {
                Vector2? possiblePlacementSpot = itemPlacementHelper.GetItemPlacementPosition(
                    PlacementType.OpenSpace,
                    100,
                    placementData.rockSize,
                    false
                    );
                if (possiblePlacementSpot.HasValue )
                {

                    placedObjects.Add(CreateObject(placementData.rockPrefab, possiblePlacementSpot.Value + new Vector2 (0.5f, 0.5f)));
                }
            }
        }
        return placedObjects;
    }

    public List<GameObject> PlacePowerUps(List<PowerUpPlacementData> powerUpPlacementData, ItemPlacementHelper itemPlacementHelper)
    {
        List<GameObject> placedObjects = new List<GameObject>();

        foreach (var placementData in powerUpPlacementData)
        {
            for (int i = 0; i < placementData.Quantity; i++)
            {
                Vector2? possiblePlacementSpot = itemPlacementHelper.GetItemPlacementPosition(
                    PlacementType.OpenSpace,
                    100,
                    placementData.powerUpSize,
                    false
                    );
                if (possiblePlacementSpot.HasValue)
                {

                    placedObjects.Add(CreateObject(placementData.powerUpPrefab, possiblePlacementSpot.Value + new Vector2(0.5f, 0.5f)));
                }
            }
        }
        return placedObjects;
    }
    public List<GameObject> PlaceAllItems(List<ItemPlacementData> itemPlacementData, ItemPlacementHelper itemPlacementHelper)
    {
        List<GameObject> placedObjects = new List<GameObject>();

        IEnumerable<ItemPlacementData> sortedList = new List<ItemPlacementData>(itemPlacementData).OrderByDescending(placementData => placementData.itemData.size.x * placementData.itemData.size.y);

        foreach (var placementData in sortedList)
        {
            for (int i = 0; i < placementData.Quantity; i++)
            {
                Vector2? possiblePlacementSpot = itemPlacementHelper.GetItemPlacementPosition(
                    placementData.itemData.placementType,
                    100,
                    placementData.itemData.size,
                    placementData.itemData.addOffset);


                if (possiblePlacementSpot.HasValue)
                {

                    placedObjects.Add(PlaceItem(placementData.itemData, possiblePlacementSpot.Value));
                }
            }
        }
        return placedObjects;
    }

    public GameObject PlaceItem(ItemData item, Vector2 placementPosition)
    {
        GameObject newItem = CreateObject(itemPrefab, placementPosition);
        newItem.GetComponent<Item>().Initialize(item);
        return newItem;
    }
    public GameObject CreateObject(GameObject prefab, Vector2 placementPoint)
    {
        if(prefab == null) 
        {
            return null;
        }
        GameObject newItem;
        if (Application.isPlaying)
        {
            newItem = Instantiate(prefab, placementPoint, Quaternion.identity);
        }
        else
        {
            newItem = Instantiate(prefab) as GameObject;
            newItem.transform.position = placementPoint;
            newItem.transform.rotation = Quaternion.identity;
        }
        return newItem;
    }

    public GameObject PlacePlayer(GameObject player, Vector2 placementPoint)
    {
        if (player == null)
        {
            return null;
        }
        if (Application.isPlaying)
        {
            player.transform.position = placementPoint;
            player.transform.rotation = Quaternion.identity;
        }
        else
        {
            GameObject newItem = Instantiate(player) as GameObject;
            newItem.transform.position = placementPoint;
            newItem.transform.rotation = Quaternion.identity;
            return newItem;
        }
        return player;
    }
}
