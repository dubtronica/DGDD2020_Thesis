using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeGrid : MonoBehaviour
{
    [SerializeField]
    private static int rows, cols;
    [SerializeField]
    private Vector2 gridSize, gridOffset;

    //cell
    [SerializeField]
    private Sprite cellSprite, goSprite;
    private Vector2 cellSize, cellScale;
    GameObject[][] mipmap = new GameObject[rows][];
       

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < mipmap.Length; i++)
        {
            mipmap[i] = new GameObject[cols];
        }

        initCells();
    }

    void initCells()
    {
        GameObject cellobject = new GameObject();
        cellobject.AddComponent<SpriteRenderer>().sprite = cellSprite;
        cellSize = cellSprite.bounds.size;

        Vector2 newCellSize = new Vector2(gridSize.x / (float)cols, gridSize.y / (float)rows);

        cellScale.x = newCellSize.x / cellSize.x;
        cellScale.y = newCellSize.y / cellSize.y;

        cellSize = newCellSize;

        cellobject.transform.localScale = new Vector2(cellScale.x, cellScale.y);

        gridOffset.x = -(gridSize.x / 2) + cellSize.x / 2;
        gridOffset.y = -(gridSize.y / 2) + cellSize.y / 2;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                insert(col, row, cellobject);
            }
        }

        Destroy(cellobject);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, gridSize);
    }

    void insert(int col, int row, GameObject go)
    {
        Vector2 gopos = new Vector2(col * cellSize.x + gridOffset.x + transform.position.x, row * cellSize.y + gridOffset.y + transform.position.y);
        GameObject cO = Instantiate(go, gopos, Quaternion.identity) as GameObject;
        cO.transform.parent = transform;
        mipmap[row][col] = go;
    }

    void remove(int col, int row)
    {
        if(mipmap[row][col] != null)
        {
            Destroy(mipmap[row][col]);
        }
    }

    private void OnMouseOver()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

    }
}
