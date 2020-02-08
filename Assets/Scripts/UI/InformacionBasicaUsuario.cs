using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformacionBasicaUsuario : MonoBehaviour
{

    public TMP_Text nombreYApellido;
    public TMP_Text informacionExtra;

    public RawImage foto;    


    public InformacionBasicaUsuario(string nomYApe, string informacion)
    {
        nombreYApellido.text = nomYApe;
        informacionExtra.text = informacion;
    }

    public void agregarInformacion(string nom, string info) 
    {
        nombreYApellido.text = nom;
        informacionExtra.text = info;
    }
    
}
