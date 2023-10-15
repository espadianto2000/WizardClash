using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int SizeX;
    public int SizeZ;
    [SerializeField]
    private GameObject pieceCollider;
    [SerializeField]
    private GameObject initialPiece;
    [SerializeField]
    private GameObject magicPiece;
    [SerializeField]
    private GameObject power1Piece;
    [SerializeField] 
    private GameObject power2Piece;
    public List<GameObject> colliders = new List<GameObject>();
    private static System.Random random = new System.Random();
    void Start()
    {
        float reducedSizeX = SizeX * 0.5f;
        //4  8
        float reducedSizeZ = SizeZ * 0.5f;
        //4  8
        transform.localScale = new Vector3(reducedSizeX * 5, 1, reducedSizeZ * 5);
        //20  40
        List<Point> randomPoints = GetRandomPoints(SizeX, SizeZ);
        for (int i = 0; i < SizeX; i++)
        {
            float posX = (((SizeX - 1) / -2.0f) + i) * 10f/SizeX;
            //-3.5  -7.5
            for(int j = 0; j < SizeZ; j++)
            {
                float posZ = (((SizeZ - 1) / -2.0f) + j) * 10f/SizeZ;
                GameObject col = null;
                if (randomPoints.Contains(new Point(i, j)))
                {
                    switch (randomPoints.IndexOf(new Point(i, j)))
                    {
                        case 0:
                        case 1:
                            col = Instantiate(power1Piece, transform, false);
                            col.gameObject.name = $"PowerA_{i}-{j}";
                            break;
                        case 2:
                        case 3:
                            col = Instantiate(power2Piece, transform, false);
                            col.gameObject.name = $"PowerB_{i}-{j}";
                            break;
                        case 4:
                        case 5:
                            col = Instantiate(magicPiece, transform, false);
                            col.gameObject.name = $"Magic_{i}-{j}";
                            break;
                    }
                    col.transform.localPosition = new Vector3(posX, 0, posZ);
                    col.transform.localScale = new Vector3(10f / SizeX, 1, 10f / SizeZ);
                }
                else
                {
                    if ((i == 0 && j == 0) || (i == SizeX - 1 && j == SizeZ - 1))
                    {
                        col = Instantiate(initialPiece, transform, false);
                        col.gameObject.name = $"Start_{i}-{j}";
                        if (i > 0)
                        {
                            col.transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                        col.transform.localPosition = new Vector3(posX, 0, posZ);
                        float xSize = (10f / 3.2f)/transform.localScale.x;
                        float zSize = (10f / 3.2f)/transform.localScale.z;
                        col.transform.localScale = new Vector3(xSize, (zSize + xSize)*20, zSize);
                    }
                    else
                    {
                        col = Instantiate(pieceCollider, transform, false);
                        col.gameObject.name = $"Coll_{i}-{j}";
                        col.transform.localPosition = new Vector3(posX, 0, posZ);
                        col.transform.localScale = new Vector3(10f / SizeX, 1, 10f / SizeZ);
                    }
                }                
                
                //1.5  0.625
                colliders.Add(col);
            }
        }
        InGameManager.instance.navMesh.BuildNavMesh();
    }

    static List<Point> GetRandomPoints(int x, int y)
    {
        List<Point> randomPoints = new List<Point>();
        HashSet<Point> usedPoints = new HashSet<Point>
        {
            new Point(0, 0),
            new Point(1, 0),
            new Point(1, 1),
            new Point(0, 1),
            new Point(x - 1, y - 2),
            new Point(x - 2, y - 1),
            new Point(x - 2, y - 2),
            new Point(x - 1, y - 1)
        };

        while (randomPoints.Count < 6)
        {
            int randomX = random.Next(0, x);
            int randomY = random.Next(0, y);

            Point newPoint = new Point(randomX, randomY);

            if (!usedPoints.Contains(newPoint))
            {
                randomPoints.Add(newPoint);
                randomPoints.Add(new Point(x - 1 - newPoint.X, y - 1 - newPoint.Y));
                usedPoints.Add(newPoint);
                usedPoints.Add(new Point(x - 1 - newPoint.X, y - 1 - newPoint.Y));
            }
        }

        return randomPoints;
    }
}
public class Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        if (obj is Point otherPoint)
        {
            return X == otherPoint.X && Y == otherPoint.Y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}
