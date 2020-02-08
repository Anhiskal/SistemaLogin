using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlRegistrarse : MonoBehaviour
{

    public TMP_InputField usuario;
    public TMP_InputField pass;
    public TMP_InputField confirmacion;
    public TMP_Text alertasPass;
    public TMP_Text aletaNombre;
 
    bool verificarNombreDeUsuario()
    {
        return true;
    }

    public bool confirmarPass()
    {
        bool sonIguales = false;
        return sonIguales = pass.text.Equals(confirmacion.text) ? true : false;
    }    

    public void reiniciarPagina() 
    {
        usuario.text = "";
        pass.text = "";
        confirmacion.text = "";

        alertasPass.gameObject.SetActive(false);
        aletaNombre.gameObject.SetActive(false);
    }

    public void reiniciarUsuario() 
    {
        usuario.text = "";
        aletaNombre.gameObject.SetActive(true);
    }


    public void reiniciarContraseñas()
    {
        alertasPass.gameObject.SetActive(true);
        //Reinicia los input
        pass.text = "";
        confirmacion.text = "";
    }

    private void OnEnable()
    {
        reiniciarPagina();
    }

}
