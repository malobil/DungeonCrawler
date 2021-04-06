using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CustomCell
{
    public CellType CurrentCellType = CellType.Classic;

    public Vector3 CellWorldPosition;
    public Vector2 CellPosition;
    public GameObject CellPrefab;    
}

public enum CellType { Classic, Wall }

