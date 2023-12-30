using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Visibility : MonoBehaviour
{
public GameObject Objekt;


public void Show()
{
Objekt.SetActive(true);
}
public void Hide()
{
Objekt.SetActive(false);
}

public void ToggleVisibility(){
if(Objekt.activeSelf){
Objekt.SetActive(false);
}
else{
Objekt.SetActive(true);
}

}
}
