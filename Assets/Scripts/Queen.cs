using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i,j;

        //μπροστά
        i = CurrentY;
        while (true)
        {
            i++;
            if (i >= 8)
                break;

            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c == null)
            {
                r[CurrentX, i] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[CurrentX, i] = true;
                    break;

                }
                else
                {
                    break;
                }
            }
        }

        //πίσω
        i = CurrentY;
        while (true)
        {
            i--;
            if (i < 0)
                break;

            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c == null)
            {
                r[CurrentX, i] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[CurrentX, i] = true;
                    break;

                }
                else
                {
                    break;
                }
            }
        }

        //δεξιά
        i = CurrentX;
        while (true)
        {
            i++;
            if (i >= 8)
                break;

            c = BoardManager.Instance.Chessmans[i, CurrentY];
            if (c == null)
            {
                r[i, CurrentY] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[i, CurrentY] = true;
                    break;

                }
                else
                {
                    break;
                }
            }
        }
        //αριστερά
        i = CurrentX;
        while (true)
        {
            i--;
            if (i < 0)
                break;

            c = BoardManager.Instance.Chessmans[i, CurrentY];
            if (c == null)
            {
                r[i, CurrentY] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[i, CurrentY] = true;
                    break;

                }
                else
                {
                    break;
                }
            }
        }
        //πάνω δεξιά
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
        //κάτω αριστερά
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
        //κάτω δεξιά
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j--;
            if (i > 7 || j < 0)
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
        //πάνω αριστερά
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
