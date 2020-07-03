using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrag : MonoBehaviour
{
    public LayerMask layerA;
    public Vector3 backPos;
    Vector2 firstPos;
    private void OnMouseDown()
    {
        firstPos = this.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hip = Physics2D.Raycast(firstPos, Vector2.zero, 0f, layerA);
    }
    void OnMouseDrag()
    {
            Vector3 mousePosition
                = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            //마우스 좌표를 스크린 투 월드로 바꾸고 이 객체의 위치로 설정해 준다.
            this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseUp()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        Debug.Log("누름종료");
        //특정 오브젝트랑 겹치는지 확인할때 사용
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, layerA);
        Collider2D[] otherArr = Physics2D.OverlapCircleAll(mousePos, 0.1f);
        List<Collider2D> otherList = new List<Collider2D>();
        if (hit.collider != null)
        {

        }
    }
}
