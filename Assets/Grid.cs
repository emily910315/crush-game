using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    }

    [SerializeField] PiecePrefab[] piecePrefabs;
    [SerializeField] GameObject backgroundPerfab;

    Dictionary<PieceType, GameObject> piecePtefaDict;
    GameObject[,] pieces;
    void Start()
    {
        Debug.Log("Position:" + transform.position);

        piecePtefaDict = new Dictionary<PieceType, GameObject>();
        for (int i = 0; i < piecePrefabs.Length; i++)
        {
            if (!piecePtefaDict.ContainsKey(piecePrefabs[i].type))
            {
                piecePtefaDict.Add(piecePrefabs[i].type, piecePrefabs[i].prefab);
            }
        }

        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                GameObject background = (GameObject)Instantiate(backgroundPerfab, GetworldPosition(x, y), Quaternion.identity);
                background.transform.SetParent(transform);
            }
        }

        pieces = new GameObject[xDim, yDim];
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                pieces[x, y] = (GameObject)Instantiate(piecePrefabs[0].prefab, GetworldPosition(x, y), Quaternion.identity);
                pieces[x, y].name = "Piece(" + x + "," + y + ")";
                pieces[x, y].transform.SetParent(transform);
            }
        }
    }

    Vector2 GetworldPosition(int x, int y)
    {
        print(xDim / 2f * size.x);
        return new Vector2(
            transform.position.x - (xDim - 1) / 2f * size.x + size.x * x,
            transform.position.y + (yDim - 1) / 2f * size.y - size.y * y
        );
    }
}
