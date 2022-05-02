using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public class Move : NetworkBehaviour {

  public PlayerManager PlayerManager;
  private GameObject go;         // GameObject sur lequel on clique
  private bool dragging = false; // Booléen qui controle le suivi de la souris
  public float speed;            // vitesse de lévé et de pose de la tuile
  private bool anim1 = false;    // Leve de la tuile
  private bool anim2 = false;    // Pose de la tuile
  private GameObject disapear; // GameObject de la grille en dessous de la ou on veut poser
  private rotateZ r;

  private bool clickedOnStar = false;

  public static List<GameObject> plateau = new List<GameObject>();
  public static int nb_of_struct_roads;
  public static List<PlayerManager.CurrentRoads> list_of_struct_roads = new List<PlayerManager.CurrentRoads>();

  // Start is called before the first frame update
  void Start() {  }

  // Update is called once per frame
  void Update() {
    dynamic i = test(); 
    r = this.GetComponent<rotateZ>();
    float x = transform.position.x - (transform.position.x % 1);
    float y = transform.position.y - (transform.position.y % 1);

    if (Input.GetMouseButtonDown(0) && !(anim1 || anim2) &&
        !(r.leve || r.couche || r.tourne)) {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit, 100)) {
        go = hit.transform.gameObject;
        if (go == this.gameObject) {
          dragging = !dragging;
          if (clickedOnStar) {
            MoveMeeple.rmMeeple(); ///////////////// à faire en version Network
            clickedOnStar = false;
          }
          if (!dragging) {
            if (this.GetComponent<Constraints>().verif(
                    i.haut,
                    i.bas,
                    i.droite,
                    i.gauche)) {

              anim2 = true;
              Type_land haut = i.haut;
              Type_land bas = i.bas;
              Type_land gauche = i.gauche;
              Type_land droite = i.droite;
              Type_land milieu = i.milieu;
              // Il faut un bouton de validation
              this.GetComponent<rotateZ>().enabled = false;
              this.GetComponent<tile_type>().enabled = false;
              disapear = GameObject.Find((int)x + "/" + (int)y);
              this.GetComponent<Constraints>().enabled = false;

              NetworkIdentity networkIdentity = NetworkClient.connection.identity;
              PlayerManager = networkIdentity.GetComponent<PlayerManager>();
              // client envoie une requête au serveur pour générer une tuile
              PlayerManager.CmdDealMove(disapear, haut, bas, gauche, droite, milieu, (int)x, (int)y);
              // Move.plateau.Add(go);

              // Type_land tg = tiles[z].haut;
       
              // lancer est_complet
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
    if (dragging) {
      Vector3 position =
          new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                      Camera.main.WorldToScreenPoint(transform.position).z);
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
      transform.position =
          new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }
    if (anim1) {
      MoveMeeple.rmStars(); ///////////////// Faire en version Network

      if (disapear != null) {
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
      if (finish <= 0.001f) {
        this.GetComponent<Constraints>().enabled = true;
        this.GetComponent<rotateZ>().enabled = true;
        anim1 = false;
      }
    }
    if (anim2) {
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
        //bool[] tabExample = new bool[]{ false, true, false, true, false };

        // on récupère l'Pidentifiant du network
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        // faire spawn les étoiles sur le serveur et les clients
        PlayerManager.CmdSpawnStars(disapear.GetComponent<Constraints>().posePossible, x, y);



        // APPELS DES FONCTIONS DE VERIFICATION DE CLOTURE

        //cloture de chemins
        // Debug.Log(disapear.name);
        // disapear.name = go.name;  
        PlayerManager.roadIsClosed_Struct(disapear);
        PlayerManager.checkAllStruct();
        PlayerManager.seeStruct();
        // Debug.Log("liste des structs "+list_of_struct_roads.Count);
        // for(int k = 0; k < list_of_struct_roads.Count; k++)
        // {
        //   Debug.Log("nb d'elt dans  la structure "+k+ " : "+list_of_struct_roads[k].CurrentTiles.Count);
        //   //Debug.Log("var isClosed = "+PlayerManager.list_of_struct_roads[k].isClosed);
        // }
        
        //Debug.Log("test estFermante " +go.GetComponent<Constraints>().estFermante);
        

        //Debug.Log("structure "+PlayerManager.list_of_struct_roads[k].Name+ " : "+ PlayerManager.list_of_struct_roads[k].CurrentTiles.Count+ " elems"+" tag : "+PlayerManager.list_of_struct_roads[k].tag);
        

        /*
        PlayerManager.resetVisite();
        PlayerManager.drawshit(go);
        PlayerManager.resetVisite();
        */
        //cloture d'abbayes
        /*
        if(go.GetComponent<Constraints>().milieu == Type_land.Abbaye)
          PlayerManager.abbeyes.Add(go);
        PlayerManager.abbeyIsClose();
        */

      }
    }
  }

  dynamic test() {
    var mm = this.gameObject.GetComponents(typeof(Component));
    foreach(object i in mm)
    {
      if (i.GetType().Name.Contains("tile_type"))
        return i;
    }
    return null;
  }
}
