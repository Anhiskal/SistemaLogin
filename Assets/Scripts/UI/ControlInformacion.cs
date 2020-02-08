using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControlInformacion : MonoBehaviour
{

    public TMP_InputField nombre;
    public TMP_InputField apellido;
    public TMP_InputField masDeMi;

    public RawImage foto;

    public string infoAnteriorNombre;
    public string infoAnteriorApellido;
    public string infoAntiguaMasDeMi;




    public void cargarInformacion(string nom, string apell, string mas, byte[] ima = null)
    {
        nombre.text = nom;
        apellido.text = apell;
        masDeMi.text = mas;
        //foto = ;


        infoAnteriorNombre = nom;
        infoAnteriorApellido = apell;
        infoAntiguaMasDeMi = mas;
    }


    public void reiniciaPagina() 
    {
        nombre.text = "";
        apellido.text = "";
        masDeMi.text = "";

        //foto.
    }

    private void OnEnable()
    {
        //reiniciaPagina();
    }

}
