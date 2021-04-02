using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridManager : MonoBehaviour
{
	[SerializeField] private Vector2 m_gridHeight;
	[SerializeField] private float m_cellGap = 1f ;
	[SerializeField] private CustomGrid m_gameGrid;

	[SerializeField] private GameObject m_classicCellPrefab;

	public static GridManager Instance { get; set ; }

    private void Awake()
    {
        if(Instance == null)
        {
			Instance = this;
        }
		else
        {
			Destroy(this);
        }

		GenerateGrid(m_gridHeight, m_cellGap);
	}

    public void GenerateGrid(Vector2 gridHeight, float cellGap)
	{
		for(int i = 0; i < gridHeight.x; i++)
		{
			for(int y = 0; y < gridHeight.y; y++)
			{
				CustomCell newCell = new CustomCell() ;
				newCell.CellWorldPosition = new Vector3(i * cellGap, 0, y * cellGap);
				newCell.CellPosition = new Vector2(i, y);

				newCell.CurrentCellType = (CellType)Random.Range(0, System.Enum.GetValues(typeof(CellType)).Length);

				GameObject newCellPrefab = Instantiate(m_classicCellPrefab, newCell.CellWorldPosition,Quaternion.identity, transform);

				newCell.CellPrefab = newCellPrefab;
				m_gameGrid.Cells.Add(newCell);
			}
		}
	}

	public CustomCell GetCellByPosition(Vector2 cellPosition)
    {
		return m_gameGrid.Cells.Find(x => x.CellPosition == cellPosition);
    }

	public CustomCell GetCellByDirection(CustomCell baseCell, Vector3 targetDirection)
	{
		Vector2 cellDestination = new Vector2(baseCell.CellPosition.x + targetDirection.x, baseCell.CellPosition.y + targetDirection.z);
		return GetCellByPosition(cellDestination);
	}
}
