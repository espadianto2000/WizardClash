using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager instance;
    [SerializeField]
    private GameObject board;
    public Board boardGame;
    [SerializeField]
    private GameObject playerPref;
    public GameObject player1;
    public GameObject player2;
    public IconClick[] pieceButtons;
    public GameObject[] piecesNoColl;
    public GameObject[] piecesColl;
    public UnityEngine.AI.NavMeshSurface navMesh;
    void Start()
    {
        InGameManager.instance = this;
        //Dev
        StartGameDev(8, 8);
        navMesh.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGameDev(int sizeX, int sizeZ)
    {
        boardGame = Instantiate(board, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Board>();
        boardGame.SizeX = sizeX;
        boardGame.SizeZ = sizeZ;
        player1 = Instantiate(playerPref, new Vector3(0, 0, 0),Quaternion.identity);
        player2 = Instantiate(playerPref, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
        float sizePlayer = (sizeX + sizeZ) / 2;
        player1.transform.localScale = new Vector3(1, sizePlayer * 0.1f, sizePlayer * 0.1f);
        player2.transform.localScale = new Vector3(1, sizePlayer * 0.1f, sizePlayer * 0.1f);
    }

    public int getValueClicked()
    {
        foreach (IconClick script in pieceButtons)
        {
            if (script.clicked)
            {
                return script.buttonValue;
            }
        }
        return -1; // or any other default value
    }
}
