using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverDetection : MonoBehaviour
{
    private bool isMouseOver = false;
    public Color originalColor;
    public Color newColor;
    private Renderer objectRenderer;
    public GameObject tempObject = null;

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    private void OnMouseEnter()
    {
        int index = InGameManager.instance.getValueClicked();
        if (index>-1 && !isMouseOver)
        {
            isMouseOver = true;
            //GetComponent<Outline>().enabled = true;
            //objectRenderer.material.color = newColor;
            tempObject = Instantiate(InGameManager.instance.piecesNoColl[index], transform.position, Quaternion.identity);
            float xSize = transform.localScale.x * InGameManager.instance.boardGame.SizeX / 3.2f;
            float zSize = transform.localScale.z * InGameManager.instance.boardGame.SizeZ / 3.2f;
            tempObject.transform.localScale = new Vector3(xSize,  (zSize + xSize), zSize);
        }
    }

    private void OnMouseExit()
    {
        if (isMouseOver)
        {
            isMouseOver = false;
            //GetComponent<Outline>().enabled = false;
            //objectRenderer.material.color = originalColor;
            if(tempObject!= null)
            {
                Destroy(tempObject);
                tempObject = null;
            }
        }
    }
    private void Update()
    {
        if(isMouseOver && Input.GetMouseButtonDown(0))
        {
            int index = InGameManager.instance.getValueClicked();
            GameObject newTile = Instantiate(InGameManager.instance.piecesColl[index],transform.position, Quaternion.identity);
            float xSize = transform.localScale.x * InGameManager.instance.boardGame.SizeX / 3.2f;
            float zSize = transform.localScale.z * InGameManager.instance.boardGame.SizeZ / 3.2f;
            newTile.transform.localScale = new Vector3(xSize, (zSize + xSize), zSize);
            InGameManager.instance.pieceButtons[index].click();
            isMouseOver = false;
            if (tempObject != null)
            {
                Destroy(tempObject);
                tempObject = null;
            }
            InGameManager.instance.navMesh.BuildNavMesh();
            gameObject.SetActive(false);
        }
    }
}
