using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    None = 0,
    Health = 1,
    Ammo = 2
}

[CreateAssetMenu(menuName ="SO/Resource")]
public class ResourceDataSO : ScriptableObject
{
    public float rate; //�������� ����� Ȯ��
    public GameObject itemPrefab;

    public ResourceType resourcetype;
    public int minAmount = 1, maxAmount = 5;
    public AudioClip useSound;
    public Color popupTextColor;
    public int GetAmount()
    {
        return Random.Range(minAmount, maxAmount + 1); //int�ϱ� �������� +1�� �������
    }
}