using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Chessman
{
    //Πιθανές Κινήσεις για τον Αξιωματικό
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;
        //Πάνω-δεξιά
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j++;
            if (i > 7 || j > 7)
                break;
            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if(c !=null && c.isWhite != isWhite)
                {
                    r[i, j] = true;
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        //Κάτω-αριστερά
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j--;
            if (i < 0 || j < 0)
                break;
            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (c != null && c.isWhite != isWhite)
                {
                    r[i, j] = true;
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        //Κάτω-δεξιά
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j--;
            if (i >7 || j < 0)
                break;
            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (c != null && c.isWhite != isWhite)
                {
                    r[i, j] = true;
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        //Πάνω-αριστερά
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j++;
            if (i < 0 || j > 7)
                break;
            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (c != null && c.isWhite != isWhite)
                {
                    r[i, j] = true;
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        return r;
    }
}
