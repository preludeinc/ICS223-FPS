using System.Collections;
using UnityEngine;

public class RayShooter : ActiveDuringGameplay
{
    private Camera cam;
    // Start is called before the first frame update

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                // is the object the Enemy?
                if (target != null)
                {
                    target.ReactToHit();
                } else
                {
                    // visually indicate there was a hit
                    StartCoroutine(CreateTempSphereIndicator(hit.point));
                }
            }
        }
    }

    private IEnumerator CreateTempSphereIndicator(Vector3 hitPosition)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.position = hitPosition;
        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
    }
}
