using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman

{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        

        //μπροστά
        if (CurrentY < 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
            if(c==null || (c !=null && (c.isWhite != isWhite)))
            {
                r[CurrentX, CurrentY + 1] = true;
            }
        }
        //πισώ
        if (CurrentY > 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
            if (c == null || (c != null && (c.isWhite != isWhite)))
            {
                r[CurrentX, CurrentY - 1] = true;
            }
        }
        //δεξιά
        if (CurrentX < 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
            if (c == null || (c != null && (c.isWhite != isWhite)))
            {
                r[CurrentX + 1, CurrentY] = true;
            }
        }
        //αριστερά
        if (CurrentX > 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
            if (c == null || (c != null && (c.isWhite != isWhite)))
            {
                r[CurrentX - 1, CurrentY] = true;
            }
        }
        //πάνω δεξια
        if (CurrentX < 7 && CurrentY < 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
            if (c == null || (c != null && (c.isWhite != isWhite)))
            {
                r[CurrentX + 1, CurrentY + 1] = true;
            }
        }
        //πάνω αριστερά
        if (CurrentX > 0 && CurrentY < 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
            if (c == null || (c != null && (c.isWhite != isWhite)))
            {
                r[CurrentX - 1, CurrentY + 1] = true;
            }
        }
        //κάτω δεξια
        if (CurrentX < 7 && CurrentY > 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
            if (c == null || (c != null && (c.isWhite != isWhite)))
            {
                r[CurrentX + 1, CurrentY - 1] = true;
            }
        }
        //κάτω αριστερα
        if (CurrentX > 0 && CurrentY > 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
            if (c == null || (c != null && (c.isWhite != isWhite)))
            {
                r[CurrentX - 1, CurrentY - 1] = true;
            }
        }
        //αλλαγή μεταξύ σε βασιλιά-πύργο
        //άσπρα πιόνια
        if (CurrentX == 4 && CurrentY == 0 && BoardManager.Instance.conditionswap == true)
        {
            Chessman Left4Piece = BoardManager.Instance.Chessmans[CurrentX - 4,CurrentY];
            Chessman Right3Piece = BoardManager.Instance.Chessmans[CurrentX + 3,CurrentY];
            Chessman Left1 = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
            Chessman Left2 = BoardManager.Instance.Chessmans[CurrentX - 2, CurrentY];
            Chessman Left3 = BoardManager.Instance.Chessmans[CurrentX - 3, CurrentY];
            Chessman Right1 = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
            Chessman Right2 = BoardManager.Instance.Chessmans[CurrentX + 2, CurrentY];
           
                if (Left4Piece.GetType() == typeof(Rook) && Left1 == null && Left2 == null && Left3 == null)
                {
                    r[2, 0] = true;
                }
                if (Right3Piece.GetType() == typeof(Rook) && Right1 == null && Right2 == null)
                {
                    r[6, 0] = true;
                }
            
        }
        //μαύρα πιόνια
        if (CurrentX == 4 && CurrentY == 7 && BoardManager.Instance.conditionswap == true)
        {
            Chessman Left4Piece = BoardManager.Instance.Chessmans[CurrentX - 4, CurrentY];
            Chessman Right3Piece = BoardManager.Instance.Chessmans[CurrentX + 3, CurrentY];
            Chessman Left1 = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
            Chessman Left2 = BoardManager.Instance.Chessmans[CurrentX - 2, CurrentY];
            Chessman Left3 = BoardManager.Instance.Chessmans[CurrentX - 3, CurrentY];
            Chessman Right1 = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
            Chessman Right2 = BoardManager.Instance.Chessmans[CurrentX + 2, CurrentY];

            if (Left4Piece.GetType() == typeof(Rook) && Left1 == null && Left2 == null && Left3 == null)
            {
                r[2, 7] = true;
            }
            if (Right3Piece.GetType() == typeof(Rook) && Right1 == null && Right2 == null)
            {
                r[6, 7] = true;
            }

        }
        return r;
    }
}
