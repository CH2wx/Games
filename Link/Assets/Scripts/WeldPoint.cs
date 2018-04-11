using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeldPoint :MonoBehaviour
{

     public string target;
    //public Vector3 finalyPosition;
    bool Assembleing = false;
    public WeldPoint(string tar)
    {
        target = tar;
        //finalyPosition = fin;

    }
    public WeldPoint()
    {


    }
    void OnTriggerEnter(Collider other)
    {

        //if (other.name == target && !other.GetComponent<MovePart>().isMoving)
        //{
        //    Destroy(other.GetComponent<MovePart>());
        //}
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag==target&& !other.GetComponent<MovePart>().isMoving )
        {
            other.GetComponent<BoxCollider>().enabled = false;              //enabled  adj.激活的
            transform.parent.GetComponent<BoxCollider>().enabled = false;
            transform.parent.GetComponent<MovePart>().stopMouseDrag();
            other.transform.parent = transform.parent;
            other.GetComponent<Part>().Parent = transform.parent.gameObject;
            iTween.MoveTo(other.gameObject, transform.position, 1f);
            StartCoroutine(wait());
        }
       /* if (other.name == target && !other.GetComponent<MovePart>().isMoving && (!transform.GetComponentInParent<MovePart>().isMoving))
        {
            other.GetComponent<BoxCollider>().enabled = false;              //enabled  adj.激活的
            transform.parent.GetComponent<BoxCollider>().enabled = false;
            transform.parent.GetComponent<MovePart>().stopMouseDrag();
            other.transform.parent =  transform.parent;
            other.GetComponent<Part>().Parent = transform.parent.gameObject;
            iTween.MoveTo(other.gameObject, transform.position, 1f);
            StartCoroutine(wait());
            //other.transform.position = transform.position;
            
        }*/

    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        transform.GetComponentInParent<BoxCollider>().enabled = true;
        this.GetComponentInParent<Part>().deleteWeldPoint(this.gameObject);
    }

    


}
