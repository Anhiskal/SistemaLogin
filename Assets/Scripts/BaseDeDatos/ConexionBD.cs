using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;

public class ConexionBD : MonoBehaviour
{
    //Servidor
    string urlBase = "http://localhost/ServerLogin/Scripts/";

    //Acciones a reliazar en el servidor
    string urlLogear = "Logear.php";
    string urlRegistrar= "RegistrarUsuario.php";
    string urlActualizarDatos = "ActualizarInformacionDelUsuario.php";
    string urlCargarInformacionDeOtros = "CargarInformacionDeLosOtrosUsuario.php";
    string urlVerificarNombreUsuario = "ValidarNombreDeUsuario.php";


    //Llamado de ejecucion a los comandos de la baseDeDatos
    public void ejecutarGuardarInformacion(string usuario, string nombre, string apellido, string informacion, byte[] foto = null) 
    {
        StartCoroutine(guardarInformacionDelUsuario(usuario, nombre, apellido, informacion, foto));
    }

    public void ejecutarRegistrarCuenta(string nombreUsuario, string pass) 
    {
        StartCoroutine(registrarCuenta(nombreUsuario, pass));
    }

    public void ejecutarIntentarLogear(string usuario, string pass)
    {
        StartCoroutine(intentarLogear( usuario,  pass));
    }

    public void ejecutarVerificarUsuario(string nombreUsuario) 
    {
        StartCoroutine(verificarNombreUsuario(nombreUsuario));
    }

    public void ejecutarObtenerInformacion() 
    {
        StartCoroutine(cargarListaDeInformacion());
    }


    //Llamado al servidor para realizar las siguientes acciones
    IEnumerator guardarInformacionDelUsuario(string usuario, string nombre, string apellido, string informacion, byte[] foto = null)
    {
        WWWForm informacionAEnviar = new WWWForm();
        informacionAEnviar.AddField("usuario", usuario);
        informacionAEnviar.AddField("nombre", nombre);
        informacionAEnviar.AddField("apellido", apellido);
        informacionAEnviar.AddField("masDeMi", informacion);

        using (UnityWebRequest www = UnityWebRequest.Post(urlBase + urlActualizarDatos, informacionAEnviar)) 
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                yield break;
            }
        }       
    }    

    IEnumerator registrarCuenta(string nombreUsuario, string pass)
    {
        WWWForm informacionAEnviar = new WWWForm();
        informacionAEnviar.AddField("nombreUsuario", nombreUsuario);
        informacionAEnviar.AddField("passUsuario", pass);

        using (UnityWebRequest www = UnityWebRequest.Post(urlBase + urlRegistrar, informacionAEnviar)) 
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                yield break;
            }
        }            
    }

    IEnumerator intentarLogear(string usuario, string pass)
    {
        WWWForm informacionAEnviar = new WWWForm();
        informacionAEnviar.AddField("usuario", usuario);
        informacionAEnviar.AddField("pass", pass);

        using (UnityWebRequest www = UnityWebRequest.Post(urlBase + urlLogear, informacionAEnviar)) 
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                yield break;
            }
            else 
            {
                string resultado = www.downloadHandler.text;
            }
        }           
    }

    IEnumerator verificarNombreUsuario(string nombreUsuario)
    {
        string url = urlVerificarNombreUsuario + "?" + "usuario" + UnityWebRequest.EscapeURL(nombreUsuario);
        UnityWebRequest verificarNombre = new UnityWebRequest(url);

        yield return verificarNombre;

        if (verificarNombre.isNetworkError || verificarNombre.isHttpError)
        {
            Debug.Log("Error en la base de datos");
            yield break;
        }
        else
        {
            //Resultado obtenido
            string resultado = verificarNombre.downloadHandler.text;
        }
    }

    IEnumerator cargarListaDeInformacion() 
    {
        UnityWebRequest obtenerInformacion = UnityWebRequest.Get(urlBase + urlCargarInformacionDeOtros);

        yield return obtenerInformacion.SendWebRequest();
        // obtenerURL.text
        if (obtenerInformacion.isNetworkError || obtenerInformacion.isHttpError)
        {
            Debug.Log("Error en la base de datos");
            yield break;
        }

        JSONNode informacionDeLosUsuarios = JSON.Parse(obtenerInformacion.downloadHandler.text);
        Debug.Log(obtenerInformacion.downloadHandler.text);
        /*
        for (int i = 0; i < informacionDeLosUsuarios.Count; i++)
        {
            string nombre = informacionDeLosUsuarios["nombre"];
            string apellido = informacionDeLosUsuarios["apellido"];
            string masDeMi = informacionDeLosUsuarios["masDeMi"];
            string urlImagen = informacionDeLosUsuarios["foto"];

            UnityWebRequest imagenDeUsuario = UnityWebRequestTexture.GetTexture(urlImagen);
            yield return imagenDeUsuario.SendWebRequest();

            if (imagenDeUsuario.isNetworkError || imagenDeUsuario.isHttpError)
            {
                yield break;
            }
            else 
            {
                
            }
        }*/
        


    }


}
