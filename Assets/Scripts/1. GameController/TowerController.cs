 using UnityEngine;
using UnityEngine.EventSystems;


public class TowerController : MonoBehaviour
{
    public static TowerController Instance { get; set; }
    [SerializeField]
    LayerMask towerPlacementLayer;
    [SerializeField]
    LayerMask towerLayer;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

    }

    RaycastHit2D hit;
    void Update()
    {
        //Debug.Log(hit.collider);
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButtonUp(0) && GameController.instance.State != State.End_Defeat)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, towerPlacementLayer);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("TowerPlace"))
                {
                    //Debug.Log(hit.collider.transform.GetSiblingIndex());
                    StoryUIController.instance.OpenBuyTowerPanel(hit.collider.transform, hit.collider.transform.GetSiblingIndex());
                }
            }
            else
            {
                StoryUIController.instance.CloseBuyTower();
                hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, towerLayer);
                //Debug.Log(hit.collider);
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Tower"))
                    {
                        StoryUIController.instance.OpenUpgrade_SellPanel(hit.collider.transform, hit.collider.transform.parent.gameObject);
                    }
                }
                else
                {
                    StoryUIController.instance.CloseUpgrade_SellPanel();
                }
            }
        }
    }
}