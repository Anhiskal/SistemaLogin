using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControLogin : MonoBehaviour
{
    public TMP_InputField usuario;
    public TMP_InputField pass;
    public TMP_Text alerta;

    void reiniciarPagina() 
    {
        usuario.text = "";
        pass.text = "";
        alerta.gameObject.SetActive(false);
    }

    void intentarLogear()
    {
        string user = usuario.text;
        string cont = pass.text;
    }

    public void loginFallido() 
    {
        pass.text = "";
        
        alerta.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        reiniciarPagina();
    }

}
