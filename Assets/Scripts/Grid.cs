using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour
{
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
 


    public int xDim;
    public int yDim;

    public PiecePrefab[] piecePrefabs;
    public GameObject backgroundPerfab;
    private Dictionary<PieceType, GameObject> piecePtefaDict;

    private GamePiece[,] pieces;
    void Start()
    {

        Debug.Log("Position:" + transform.position);
        piecePtefaDict = new Dictionary<PieceType, GameObject>();
        for(int i = 0; i < piecePrefabs.Length; i++)
        {
            if (!piecePtefaDict.ContainsKey(piecePrefabs[i].type))
            {
                piecePtefaDict.Add(piecePrefabs[i].type, piecePrefabs[i].prefab);
            }
        }
        for (int x = 0; x < xDim; x++)
        {
            for(int y = 0; y < yDim; y++)
            {
                GameObject background = (GameObject)Instantiate(backgroundPerfab, GetWorldPosition(x,y), Quaternion.identity);
                background.transform.parent = transform;
            }
        }
        pieces = new GamePiece[xDim, yDim];
        for(int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                GameObject newPiece=(GameObject)Instantiate(piecePtefaDict[PieceType.NORMAL], Vector3.zero, Quaternion.identity);
                newPiece.name="Piece("+x+"," + y + ")";
                newPiece.transform.parent = transform;
                pieces[x, y] = newPiece.GetComponent<GamePiece>();
                pieces[x, y].Init(x, y, this, PieceType.NORMAL);
                if (pieces[x, y].IsMovable())
                {
                    pieces[x, y].MovableComponet.Move(x, y);
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Vector2 GetWorldPosition(int x,int y)
    {
        return new Vector2(transform.position
            .x - xDim / 2.0f + x, transform.position
            .y - yDim / 2.0f - y);
    }
}
