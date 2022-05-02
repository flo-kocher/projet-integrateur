using UnityEngine;

public class CameController : MonoBehaviour {

  // vitesse de déplacement de la caméra
  public float panSpeed = 20f;
  public float panBorderThickness = 10f;
  public float scrollSpeed = 20f;

  // a remplir quand taille du plateau connut
  public Vector2 panLimit;
  public float minZ;
  public float maxZ;
  private float gradient;

  void Update() {

    Vector3 pos = transform.position;
    // z,q,s,d touche pour déplacer la caméra (prévus pour clavier azerty)
    if (Input.GetKey("z") ||
        ((Input.mousePosition.y >= (Screen.height - panBorderThickness)) &&
         (Input.mousePosition.y < Screen.height) &&
         (Input.mousePosition.x < Screen.width) &&
         (Input.mousePosition.x > 0))) {
      gradient =
          1 - (Screen.height - Input.mousePosition.y) / panBorderThickness;
      if (Input.GetKey("z"))
        gradient = 0.5f;
      pos.y += gradient * panSpeed * Time.deltaTime;
    }
    if (Input.GetKey("s") || ((Input.mousePosition.y <= panBorderThickness) &&
                              (Input.mousePosition.y > 0) &&
                              (Input.mousePosition.x < Screen.width) &&
                              (Input.mousePosition.x > 0))) {
      gradient =
          (panBorderThickness - Input.mousePosition.y) / panBorderThickness;
      if (Input.GetKey("s"))
        gradient = 0.5f;
      pos.y -= gradient * panSpeed * Time.deltaTime;
    }
    if (Input.GetKey("d") ||
        ((Input.mousePosition.x >= (Screen.width - panBorderThickness)) &&
         (Input.mousePosition.x < Screen.width) &&
         (Input.mousePosition.y < Screen.height) &&
         (Input.mousePosition.y > 0))) {
      gradient =
          1 - (Screen.width - Input.mousePosition.x) / panBorderThickness;
      if (Input.GetKey("d"))
        gradient = 0.5f;
      pos.x += gradient * panSpeed * Time.deltaTime;
    }
    if (Input.GetKey("q") || ((Input.mousePosition.x <= panBorderThickness) &&
                              (Input.mousePosition.x > 0) &&
                              (Input.mousePosition.y < Screen.height) &&
                              (Input.mousePosition.y > 0))) {
      gradient =
          (panBorderThickness - Input.mousePosition.x) / panBorderThickness;
      if (Input.GetKey("q"))
        gradient = 0.5f;
      pos.x -= gradient * panSpeed * Time.deltaTime;
    }

    // dezoomer ou zoomer avec la roulette du clavier
    float scroll = Input.GetAxis("Mouse ScrollWheel");
    pos.z += scroll * scrollSpeed * Time.deltaTime;

    // Pour limiter la taille de l'écran
    pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
    pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);
    pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
    transform.position = pos;
  }
}
