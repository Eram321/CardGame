using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerOnMapController : MonoBehaviour {

    [SerializeField] float speed = 10f;

    Camera cam;
    Vector3 dir;
    private void Start()
    {
        cam = Camera.main;

        transform.position = Game.Instance.PlayerData.PositionOnMap;

        dir = transform.position;
    }

    private void FixedUpdate()
    {
        if (Menu.Instance.IsOpen) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f);
            if (hit)
                dir = hit.point;
        }
        transform.position = Vector3.MoveTowards(transform.position, dir, Time.deltaTime * speed);
    }
}
