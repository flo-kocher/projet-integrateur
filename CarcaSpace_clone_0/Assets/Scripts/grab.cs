using UnityEngine;
/*pour le merge*/
public class grab : MonoBehaviour {

    private GameObject selectedObject;

    public Camera camera;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if(selectedObject == null) {
                RaycastHit hit = CastRay();

                if(hit.collider != null) {
                    if (!hit.collider.CompareTag("drag")) {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
            } else {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = camera.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, 0f, worldPosition.z);

                selectedObject = null;
                Cursor.visible = true;
            }
        }

        if(selectedObject != null) {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = camera.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, .25f, worldPosition.z);
            

            if (Input.GetMouseButtonDown(1)) {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                    selectedObject.transform.rotation.eulerAngles.x +90f,
                    selectedObject.transform.rotation.eulerAngles.y ,
                    selectedObject.transform.rotation.eulerAngles.z +180f));
            }
        }
    }

    private RaycastHit CastRay() {
        Vector3 screenMousePosFar = new Vector3( Input.mousePosition.x, Input.mousePosition.y,
            camera.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            camera.nearClipPlane);
        Vector3 worldMousePosFar = camera.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = camera.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}

