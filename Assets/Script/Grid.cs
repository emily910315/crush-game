using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Grid : MonoBehaviour
{
    [SerializeField] int xDim;
    [SerializeField] int yDim;
    [SerializeField] Vector2 size;

    public enum PieceType
    {
        NORMAL,
        COUNT,
    };

    [System.Serializable]
    public struct PiecePrefab
    {
        public PieceType type;
        public GameObject prefab;
        public ColorPiece.ColorType color; // 添加颜色属性
    }

    public enum ColorType
    {
        GRAY,
        RED,
        GREEN,
        ANY,
        COUNT
    };

    [SerializeField] PiecePrefab[] piecePrefabs;
    [SerializeField] GameObject backgroundPrefab;

    Dictionary<PieceType, GameObject> piecePrefabDict;
    GameObject[,] pieces;

    GameObject GetRandomPrefab()
    {
        PiecePrefab randomPrefab = piecePrefabs[UnityEngine.Random.Range(0, piecePrefabs.Length)];
        GameObject prefabToInstantiate = randomPrefab.prefab;
        //ColorPiece.ColorType randomColor = randomPrefab.color;

        // 实例化方块
        GameObject newPiece = Instantiate(prefabToInstantiate, Vector3.zero, Quaternion.identity);

        // 设置颜色
        ColorPiece colorComponent = newPiece.GetComponent<ColorPiece>();
        if (colorComponent != null)
        {
            colorComponent.SetColor(randomPrefab.color);
        }

        return newPiece;
    }
    void Start()
    {
        Debug.Log("Position:" + transform.position);

        piecePrefabDict = new Dictionary<PieceType, GameObject>();
        for (int i = 0; i < piecePrefabs.Length; i++)
        {
            if (!piecePrefabDict.ContainsKey(piecePrefabs[i].type))
            {
                piecePrefabDict.Add(piecePrefabs[i].type, piecePrefabs[i].prefab);
                // 添加颜色组件到预制件，如果存在
                ColorPiece colorComponent = piecePrefabs[i].prefab.GetComponent<ColorPiece>();
                if (colorComponent != null)
                {
                    colorComponent.SetColor(piecePrefabs[i].color);
                }
            }
        }

        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                GameObject background = Instantiate(backgroundPrefab, GetWorldPosition(x, y), Quaternion.identity);
                background.transform.SetParent(transform);
            }
        }
        

        pieces = new GameObject[xDim, yDim];
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {

                GameObject newPiece = GetRandomPrefab(); // 使用 GetRandomPrefab 方法来获取随机方块
                newPiece.name = "Piece(" + x + "," + y + ")";
                newPiece.transform.SetParent(transform);

                pieces[x, y] = newPiece;

                
              

                // 获取当前方块的GamePiece组件
                GamePiece gamePiece = newPiece.GetComponent<GamePiece>();



                // 确保GamePiece存在并且具有ColorComponet
                if (gamePiece != null && gamePiece.IsColored())
                {
                    ColorPiece colorComponent = gamePiece.ColorComponet;
                    if (colorComponent != null)
                    {
                        // 随机设置颜色
                        ColorPiece.ColorType randomColor = (ColorPiece.ColorType)UnityEngine.Random.Range(0, colorComponent.NumColors);
                        colorComponent.SetColor(randomColor);
                    }
                }
            }
        }
    }

    public Vector2 GetWorldPosition(int x, int y)
    {
        print(xDim / 2f * size.x);
        return new Vector2(
            transform.position.x - (xDim - 1) / 2f * size.x + size.x * x,
            transform.position.y + (yDim - 1) / 2f * size.y - size.y * y
        );
    }

   
}
