using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public static class TileReservation
{
    private static Dictionary<Vector2Int, GameObject> reservations;// = new();

    
    public static bool IsReserved(Vector2Int pos)
    {
        if(reservations == null)
        {
            reservations = new();
        }
        return reservations.ContainsKey(pos);
    }
    //public void hero()
    //{
    //    // 해당 좌표에 위치하게
    //}

    //public void monspwan()
    //{
    //    // 해당 좌표에 위치하게
    //}

    //public void move()
    //{
    //    // 해당 좌표에 위치하게
    //}


    public static bool Reserve(Vector2Int pos, GameObject reserver)
    {
        if (reservations.ContainsKey(pos)) return false;

        reservations[pos] = reserver;
        return true;
    }

    public static void RemoveReserve(Vector2Int pos)
    {
        if (reservations.ContainsKey(pos))
            reservations.Remove(pos);
    }

    // TODO: 라운드 클리어 및 패배 시 Clear 호출용 이벤트 구독
    public static void Clear()
    {
        reservations.Clear();
    }
}
