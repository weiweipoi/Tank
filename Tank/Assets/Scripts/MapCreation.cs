using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    //用来装饰初始化地图所需物体的数组
    //0.基地 1.墙 2.障碍 3.出生效果 4.河流 5.草 6.空气墙
    public GameObject[] item;

    //已经有东西的位置列表
    private List<Vector3> itemPositionList = new List<Vector3>();

    private void Awake()
    {
        InitMap();
    }

    private void InitMap()
    {
        //实例化基地
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        
        //墙围基地
        for (int i = -1 ; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        
        //实例化最外围空气墙
        for (int i = -11; i <= 11; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -11; i <= 11; i++)
        {
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -9; i <= 9; i++)
        {
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }
        for (int i = -9; i <= 9; i++)
        {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        
        //初始化玩家
        GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().creatPlayer = true;
        
        //产生敌人
        CreateItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);
        
        InvokeRepeating("CreateEnemy", 4, 5);
        
        //实例化地图
        for (int i = 0; i < 60; i++)
        {
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 15; i++)
        {
            CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 15; i++)
        {
            CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 10; i++)
        {
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
        }
    }
    
    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }
    
    //产生随机位置的方法
    private Vector3 CreateRandomPosition()
    {
        //不生产x=-10,10两列和 y=-8,8 两行的位置
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9,10), Random.Range(-7,8), 0);
            if (!HasThePosition(createPosition))
            {
                return createPosition;
            }
        }
    }
    
    //用来判断位置列表中是否有这个位置
    private bool HasThePosition(Vector3 createPos)
    {
        for (int i = 0; i < itemPositionList.Count; i++)
        {
            if (createPos==itemPositionList[i])
            {
                return true;
            }
        }

        return false;
    }
    
    //产生敌人的方法
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        if (num == 0)
        {
            EnemyPos = new Vector3(-10, 8, 0);
        }
        if (num == 1)
        {
            EnemyPos = new Vector3(0, 8, 0);
        }
        if (num == 2)
        {
            EnemyPos = new Vector3(10, 8, 0);
        }
        CreateItem(item[3], EnemyPos, Quaternion.identity);
        
    }
}
