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
    //    // �ش� ��ǥ�� ��ġ�ϰ�
    //}

    //public void monspwan()
    //{
    //    // �ش� ��ǥ�� ��ġ�ϰ�
    //}

    //public void move()
    //{
    //    // �ش� ��ǥ�� ��ġ�ϰ�
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

    // TODO: ���� Ŭ���� �� �й� �� Clear ȣ��� �̺�Ʈ ����
    public static void Clear()
    {
        reservations.Clear();
    }
}
