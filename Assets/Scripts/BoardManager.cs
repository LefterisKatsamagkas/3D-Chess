using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }
    private bool[,] allowedMoves { set; get; }
    public Chessman[,] Chessmans { get; set; }
    private Chessman selectedChessman;

    private const float TILE_SIZE = 1.0f;    //θα χρησιμοποιηθούν παρακάτω για την μετατροπή των συντεταγμένων στο κέντρο
    private const float TILE_OFFSET = 0.5f;     //των τετραγώνων

    private int selectionX = -1;   
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;
    private Quaternion blackRot = Quaternion.Euler(0, 180, 0);  //περιστροφή των μαύρων πιονιών επειδή ηταν τοποθετημένα αναποδα
    private Quaternion whiteRot = Quaternion.identity;
    public bool isWhiteTurn = true;
    public bool conditionswap = true;

    private void Start()
    {
        Instance = this;
        SpawnAllChessmans();
    }

    private void DrawChessboard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }

        }

        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX, Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            Debug.DrawLine(Vector3.forward * (selectionY + 1) + Vector3.right * selectionX, Vector3.forward * selectionY + Vector3.right * (selectionX + 1));

        }


    }

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;

        }





    }

    private void Update()
    {
        UpdateSelection();
        DrawChessboard();
        if (Input.GetMouseButtonDown(0))
        {
            if(selectionX>=0 && selectionY >= 0)
            {
                if (selectedChessman == null)
                {
                    SelectChessman(selectionX,selectionY);

                }
                else
                {
                    MoveChessman(selectionX, selectionY);
                }
            }
        }

    }

    private Vector3 GetTileCenter(int x, int z)
    {
        Vector3 origin = Vector3.zero;
        origin.x += x * TILE_SIZE + TILE_OFFSET;
        origin.z += z * TILE_SIZE + TILE_OFFSET;
        return origin;

    }

    private void SpawnChessman(int index, int x, int y, Quaternion rot)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x, y), rot) as GameObject;
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPosition(x, y);
        activeChessman.Add(go);
    }

    private void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[8, 8];
        //Άσπρα πιόνια
        //Βασιλιάς
        SpawnChessman(0, 4, 0, whiteRot);

        //Βασίλισσα
        SpawnChessman(1, 3, 0, whiteRot);

        //Πύργος
        SpawnChessman(2, 0, 0, whiteRot);
        SpawnChessman(2, 7, 0, whiteRot);

        //Αξιωματικός
        SpawnChessman(3, 2, 0, whiteRot);
        SpawnChessman(3, 5, 0, whiteRot);

        //Άλογο
        SpawnChessman(4, 1, 0, whiteRot);
        SpawnChessman(4, 6, 0, whiteRot);

        //Στρατιωτάκια
        for (int k = 0; k <= 7; k++)
        {
            SpawnChessman(5, k, 1, whiteRot);
        }
        //Μαύρα πιόνια
        //Βασιλιάς
        SpawnChessman(6, 4, 7, blackRot);

        //Βασίλισσα
        SpawnChessman(7, 3, 7, blackRot);

        //Πύργος
        SpawnChessman(8, 0, 7, blackRot);
        SpawnChessman(8, 7, 7, blackRot);

        //Αξιωματικός
        SpawnChessman(9, 2, 7, blackRot);
        SpawnChessman(9, 5, 7, blackRot);

        //Άλογο
        SpawnChessman(10, 1, 7, blackRot);
        SpawnChessman(10, 6, 7, blackRot);

        //Στρατιωτάκια
        for (int k = 0; k <= 7; k++)
        {
            SpawnChessman(11, k, 6, blackRot);
        }











    }

    private void SelectChessman(int x,int y)
    {
        if (Chessmans[x, y] == null)
            return;
        if (Chessmans[x, y].isWhite != isWhiteTurn)
            return;
        allowedMoves = Chessmans[x, y].PossibleMove();
        selectedChessman = Chessmans[x, y];
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);


    }

    private void MoveChessman(int x, int y)
    {
        if (allowedMoves[x,y])
        {
            Chessman c = Chessmans[x, y];
            if(c!=null && (c.isWhite != isWhiteTurn))
            {
                if (c.GetType() == typeof(King))             //Αν εξουδετερωθεί ο βασιλιάς το παιχνίδι τελείωσε
                {
                    EndGame();
                    return;
                }
                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }

            if (selectedChessman.GetType() == typeof(Pawn)){           //προαγωγή στρατιωτάκι σε βασίλισσα
                if (y == 7)
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    SpawnChessman(1,x,y,whiteRot);
                    selectedChessman = Chessmans[x, y];
                }else if (y == 0)
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    SpawnChessman(7, x, y, blackRot);
                    selectedChessman = Chessmans[x, y];
                }
            }
             
            
            if (selectedChessman.GetType() == typeof(King))                //αλλαγή μεταξύ βασιλιά-πύργο
            {
                
                Chessman WhiteLeftRook = Chessmans[0, 0];
                if(x==2 && y == 0 && conditionswap==true)
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    activeChessman.Remove(WhiteLeftRook.gameObject);
                    Destroy(selectedChessman.gameObject);
                    Destroy(WhiteLeftRook.gameObject);
                    SpawnChessman(0, 2, 0, whiteRot);
                    SpawnChessman(2, 3, 0, whiteRot);
                    selectedChessman = Chessmans[x, y];
                    
                }
                Chessman WhiteRightRook = Chessmans[7, 0];
                if (x == 6 && y == 0 && conditionswap == true)
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    activeChessman.Remove(WhiteRightRook.gameObject);
                    Destroy(selectedChessman.gameObject);
                    Destroy(WhiteRightRook.gameObject);
                    SpawnChessman(0, 6, 0, whiteRot);
                    SpawnChessman(2, 5, 0, whiteRot);
                    selectedChessman = Chessmans[x, y];
                }
                Chessman BlackLeftRook = Chessmans[0, 7];
                if (x == 2 && y == 7 && conditionswap == true)
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    activeChessman.Remove(BlackLeftRook.gameObject);
                    Destroy(selectedChessman.gameObject);
                    Destroy(BlackLeftRook.gameObject);
                    SpawnChessman(6, 2, 7, blackRot);
                    SpawnChessman(8, 3, 7, blackRot);
                    selectedChessman = Chessmans[x, y];

                }
                Chessman BlackRightRook = Chessmans[7, 7];
                if (x == 6 && y == 7 && conditionswap == true)
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    activeChessman.Remove(BlackRightRook.gameObject);
                    Destroy(selectedChessman.gameObject);
                    Destroy(BlackRightRook.gameObject);
                    SpawnChessman(6, 6, 7, whiteRot);
                    SpawnChessman(8, 5, 7, whiteRot);
                    selectedChessman = Chessmans[x, y];
                }

                conditionswap = false;
            }


                Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
            selectedChessman.transform.position = GetTileCenter(x, y);
            selectedChessman.SetPosition(x, y);
            Chessmans[x, y] = selectedChessman;
            isWhiteTurn =! isWhiteTurn;
        }
        BoardHighlights.Instance.Hidehighlights();
        selectedChessman = null;

    }

    


    

    

    

   

    private void EndGame()
    {
        if (isWhiteTurn)
            Debug.Log("White Winner!");
        else
            Debug.Log("Black Winner!");
        foreach (GameObject go in activeChessman)
            Destroy(go);
        isWhiteTurn = true;
        BoardHighlights.Instance.Hidehighlights();
        SpawnAllChessmans();

    }
}
