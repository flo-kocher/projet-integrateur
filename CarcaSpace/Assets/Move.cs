using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
  private GameObject go; // Le GO sur lequel on clic
  private bool dragging =
      false;          // Booléen qui indique si le go doit suivre la souris
  public float speed; // Vitesse de la pose de la tuile
  private bool anim1 = false; // Animation pour lever la tuile
  private bool anim2 = false; // Animation pour la pose de la tuile
  private GameObject
      disapear; // GO de la grid (X/Y) sur lequel on va poser la tuile
  private rotateZ r;

  // Bool pour supprimer le dernier meeple si on veut rebouger la tuile.
  private bool clickedOnStar = false;

  // Start is called before the first frame update
  void Start() { plateau = this.transform.parent.GetComponent<Board>(); }

  // Update is called once per frame
  void Update() {
    r = this.GetComponent<rotateZ>();
    // Alignement des coordonées x et y pour se positionner sur une tuile de la
    // grille la plus proche
    float x = transform.position.x - (transform.position.x % 1);
    float y = transform.position.y - (transform.position.y % 1);

    if (Input.GetMouseButtonDown(0) && !(anim1 || anim2) &&
        !(r.leve || r.couche || r.tourne)) {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      // Si on clique sur un GameObject
      if (Physics.Raycast(ray, out hit, 100)) {
        go = hit.transform.gameObject;
        if (go == this.gameObject) {
          if (clickedOnStar) {
            MoveMeeple.rmMeeple();
            clickedOnStar = false;
          }
          dragging = !dragging;
          if (!dragging) {

            if (this.GetComponent<Constraints>().verif(
                    this.GetComponent<tile_type>().haut,
                    this.GetComponent<tile_type>().bas,
                    this.GetComponent<tile_type>().droite,
                    this.GetComponent<tile_type>().gauche)) {
              disapear = GameObject.Find((int)x + "/" + (int)y);

              anim2 = true;
              // Il faut un bouton de validation
              this.GetComponent<rotateZ>().enabled = false;
              // Type_land tg = tiles[z].haut;
              tile_type_1 dd = new tile_type_1();
              disapear.GetComponent<Constraints>().haut =
                  this.GetComponent<tile_type>().haut;
              disapear.GetComponent<Constraints>().bas =
                  this.GetComponent<tile_type>().bas;
              disapear.GetComponent<Constraints>().gauche =
                  this.GetComponent<tile_type>().gauche;
              disapear.GetComponent<Constraints>().droite =
                  this.GetComponent<tile_type>().droite;
              this.GetComponent<Constraints>().enabled = false;
            } else {
              if (this.GetComponent<AccessDenied>().testRefuse()) {
                this.GetComponent<NotifDenied>().pushNotif(
                    "La tuile ne peut pas être posée ici");
                this.GetComponent<AccessDenied>().animRefuse();
              }

              dragging = !dragging;
            }
          } else {
            anim1 = true;
          }
        } else if (go.name.Contains("Star")) {
          go.GetComponent<CreateMeeple>().function();
          clickedOnStar = true;
        }
      }
    }
    // La tuile suit la souris
    if (dragging) {
      Vector3 position =
          new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                      Camera.main.WorldToScreenPoint(transform.position).z);
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
      transform.position =
          new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }
    if (anim1) { // Leve la tuile
      if (disapear !=
          null) { // Fait aparaitre la tuile de la grille la ou on veut poser
        // disapear.SetActive(true);
        Material mat = disapear.GetComponent<Renderer>().material;
        Color c_target = mat.color;
        c_target.a = 1;
        mat.color = Color.Lerp(mat.color, c_target, speed * Time.deltaTime);
      }
      Vector3 target =
          new Vector3(transform.position.x, transform.position.y, -0.2f);
      transform.position =
          Vector3.Slerp(transform.position, target, speed * Time.deltaTime);
      float finish = Vector3.Angle(transform.position, target);
      if (finish <= 0.001f) { // Une fois l'animation fini
        this.GetComponent<Constraints>().enabled = true;
        this.GetComponent<rotateZ>().enabled = true;
        MoveMeeple.rmStars();
        anim1 = false;
      }
    }
    if (anim2) { // Pose de la tuile
      if (disapear != null) {
        Material mat = disapear.GetComponent<Renderer>().material;
        Color c_target = mat.color;
        c_target.a = 0;
        mat.color = Color.Lerp(mat.color, c_target, speed * Time.deltaTime);
      }
      Vector3 target = new Vector3(x + 0.5f, y + 0.5f, 0f);
      transform.position =
          Vector3.Slerp(transform.position, target, speed * Time.deltaTime);
      float finish = Vector3.Angle(transform.position, target);
      if (finish <= 0.001f) {
        anim2 = false;
        // Exemple pour créer les meeples
        bool[] tabExample = { false, true, false, true, false };
        MoveMeeple.makeStars(tabExample, x, y);
        if (disapear != null) {
          plateau.board.Add(disapear);
          // disapear.SetActive(false);
        }
      }
    }
  }
}
