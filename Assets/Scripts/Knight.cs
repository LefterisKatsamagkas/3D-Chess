using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        //Άλογο
        //πάνω δεξιά
        if (CurrentX < 7 && CurrentY < 6)
        {


            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 2];
            if(c==null || (c!=null && (c.isWhite != isWhite)))
                r[CurrentX+1, CurrentY + 2] = true;
        }

        //πάνω αριστερά
        if (CurrentX > 0 && CurrentY < 6)
        {


            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 2];
            if (c == null || (c != null && (c.isWhite != isWhite)))
                r[CurrentX - 1, CurrentY + 2] = true;
        }

        //δεξιά πάνω
        if (CurrentX < 6 && CurrentY < 7)
        {


            c = BoardManager.Instance.Chessmans[CurrentX + 2, CurrentY + 1];
            if (c == null || (c != null && (c.isWhite != isWhite)))
                r[CurrentX + 2, CurrentY + 1] = true;
        }

        //δεξιά κάτω
        if (CurrentX < 6 && CurrentY > 0)
        {


            c = BoardManager.Instance.Chessmans[CurrentX + 2, CurrentY - 1];
            if (c == null || (c != null && (c.isWhite != isWhite)))
                r[CurrentX + 2, CurrentY - 1] = true;
        }

        //κάτω δεξιά
        if (CurrentX < 7 && CurrentY > 1)
        {


            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 2];
            if (c == null || (c != null && (c.isWhite != isWhite)))
                r[CurrentX + 1, CurrentY - 2] = true;
        }
        //κάτω αριστερά
        if (CurrentX > 0 && CurrentY > 1)
        {


            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 2];
            if (c == null || (c != null && (c.isWhite != isWhite)))
                r[CurrentX - 1, CurrentY - 2] = true;
        }
        //αριστερά πάνω
        if (CurrentX > 1 && CurrentY < 7)
        {


            c = BoardManager.Instance.Chessmans[CurrentX - 2, CurrentY + 1];
            if (c == null || (c != null && (c.isWhite != isWhite)))
                r[CurrentX - 2, CurrentY + 1] = true;
        }
        //αριστερά κάτω
        if (CurrentX > 1 && CurrentY > 0)
        {


            c = BoardManager.Instance.Chessmans[CurrentX - 2, CurrentY - 1];
            if (c == null || (c != null && (c.isWhite != isWhite)))
                r[CurrentX - 2, CurrentY - 1] = true;
        }
        return r;
    }

}
