using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;

public class ControladorDeVistas : MonoBehaviour
{
    public ControLogin pagLogin;
    public ControlRegistrarse pagRegistrarse;
    public ControlInformacion pagInformacionUsuario;
    public ControlPrincipal pagPrincipal;

    ConexionBD conexion = new ConexionBD();

    string nombreDeUsuario;
    string idUsuario = "";

    string urlConexion = "";







    //Servidor
    string urlBase = "http://localhost/ServerLogin/Scripts/";

    //Acciones a reliazar en el servidor
    string urlLogear = "Logear.php";
    string urlRegistrar = "RegistrarUsuario.php";
    string urlActualizarDatos = "ActualizarInformacionDelUsuario.php";
    string urlCargarInformacionDeOtros = "CargarInformacionDeLosOtrosUsuario.php";
    string urlMiInformacion = "MiInformacion.php";

    //Control de las vistas de usuario
    void activarLogin() 
    {
        pagPrincipal.gameObject.SetActive(false);
        pagRegistrarse.gameObject.SetActive(false);
        pagLogin.gameObject.SetActive(true);
    }

    public void activarRegistrarse() 
    {
        pagLogin.gameObject.SetActive(false);
        pagRegistrarse.gameObject.SetActive(true);
    }

    void activarInformacion() 
    {
        pagRegistrarse.gameObject.SetActive(false);
        pagPrincipal.gameObject.SetActive(false);
        pagInformacionUsuario.gameObject.SetActive(true);
    }

    void activarPrincipal() 
    {
        pagInformacionUsuario.gameObject.SetActive(false);
        pagLogin.gameObject.SetActive(false);
        pagPrincipal.gameObject.SetActive(true);

        ejecutarObtenerInformacion();
    } 


    //Acciones a realizar en las paginas


    //Acciones de la pagina Registrarse

    
    // Acciones de la pagina informacion

    private void Awake()
    {
        activarLogin();
    }





    //Llamado de ejecucion a los comandos de la baseDeDatos
    public void ejecutarGuardarInformacion()
    {        
        string nombre = pagInformacionUsuario.nombre.text;
        string apellido = pagInformacionUsuario.apellido.text;
        string informacion = pagInformacionUsuario.masDeMi.text;
        // byte[] foto = null;
        StartCoroutine(guardarInformacionDelUsuario(idUsuario, nombre, apellido, informacion));
    }

    public void ejecutarRegistrarCuenta()
    {
        if (pagRegistrarse.confirmarPass())
        {
            string nombreUsuario = pagRegistrarse.usuario.text;
            string pass = pagRegistrarse.pass.text;

            StartCoroutine(registrarCuenta(nombreUsuario, pass));
        }
        else 
        {
            pagRegistrarse.reiniciarContraseñas();
        }
    }

    public void ejecutarIntentarLogear()
    {
        string usuario = pagLogin.usuario.text;
        string pass = pagLogin.pass.text;

        StartCoroutine(intentarLogear(usuario, pass));
    }

    public void ejecutarObtenerInformacion()
    {
        StartCoroutine(cargarListaDeInformacion());
    }


    public void ejecutarActualizarMiInformacion() 
    {
        StartCoroutine(cargarMiInformacion());
        activarInformacion();
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
        };

        pagPrincipal.eliminarTodosLosElementos();
        //ejecutarObtenerInformacion();
        activarPrincipal();
    }

    IEnumerator registrarCuenta(string nombreUsuario, string pass)
    {
        WWWForm informacionAEnviar = new WWWForm();
        informacionAEnviar.AddField("usuario", nombreUsuario);
        informacionAEnviar.AddField("pass", pass);

        using (UnityWebRequest www = UnityWebRequest.Post(urlBase + urlRegistrar, informacionAEnviar))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                yield break;
            }
            else 
            {
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text.Contains("Existe"))
                {
                    pagRegistrarse.reiniciarPagina();
                    //pagRegistrarse.reiniciarContraseñas();
                    pagRegistrarse.reiniciarUsuario();
                }
                else 
                {
                    idUsuario = www.downloadHandler.text;
                    activarInformacion();
                }               
            }
        };

        
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
                Debug.Log(www.downloadHandler.text);
                //Resultado del logeo
                if (www.downloadHandler.text.Contains("error"))
                {
                    pagLogin.loginFallido();
                }
                else 
                {
                    idUsuario = www.downloadHandler.text;
                    activarPrincipal();
                    //Cargar informacion de los demas usuarios
                }
            }
        };
    }

    IEnumerator cargarListaDeInformacion()
    {
        List<EstructuraInformacion> informacion = new List<EstructuraInformacion>();
        UnityWebRequest obtenerInformacion = UnityWebRequest.Get(urlBase + urlCargarInformacionDeOtros);

        yield return obtenerInformacion.SendWebRequest();
        // obtenerURL.text
        if (obtenerInformacion.isNetworkError || obtenerInformacion.isHttpError)
        {
            Debug.Log("Error en la base de datos");
            yield break;
        }

        JSONArray informacionDeLosUsuarios = JSON.Parse(obtenerInformacion.downloadHandler.text) as JSONArray;
        //Debug.Log(obtenerInformacion.downloadHandler.text);
        
        for (int i = 0; i < informacionDeLosUsuarios.Count; i++)
        {
            EstructuraInformacion nuevaInformacion = new EstructuraInformacion();
            nuevaInformacion.nombre = informacionDeLosUsuarios[i].AsObject["nombre"];
            nuevaInformacion.apellido = informacionDeLosUsuarios[i].AsObject["apellido"];
            nuevaInformacion.sobreMi = informacionDeLosUsuarios[i].AsObject["masDeMi"];
            //string urlImagen = informacionDeLosUsuarios["foto"];
            

            informacion.Add(nuevaInformacion);
           

            /*UnityWebRequest imagenDeUsuario = UnityWebRequestTexture.GetTexture(urlImagen);
            yield return imagenDeUsuario.SendWebRequest();

            if (imagenDeUsuario.isNetworkError || imagenDeUsuario.isHttpError)
            {
                yield break;
            }
            else 
            {
                
            }*/
        }
        pagPrincipal.cargarInformacionDeUsuarios(informacion);
    }

    IEnumerator cargarMiInformacion() 
    {
        WWWForm informacionAEnviar = new WWWForm();
        informacionAEnviar.AddField("id", idUsuario);

        //UnityWebRequest obtenerInformacion = UnityWebRequest.Post(urlBase + urlMiInformacion, informacionAEnviar);


        //yield return obtenerInformacion.SendWebRequest();

        using (UnityWebRequest www = UnityWebRequest.Post(urlBase + urlMiInformacion, informacionAEnviar))
        {
            yield return www.SendWebRequest();
            // obtenerURL.text
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error en la base de datos");
                yield break;
            }

            JSONNode informacionDeLosUsuarios = JSON.Parse(www.downloadHandler.text) as JSONArray;
            Debug.Log(informacionDeLosUsuarios);

            string nombre = informacionDeLosUsuarios[0].AsObject["nombre"];
            string apellido = informacionDeLosUsuarios[0].AsObject["apellido"];
            string informacion = informacionDeLosUsuarios[0].AsObject["masDeMi"];

            Debug.Log("el nombre es : " + nombre);

            pagInformacionUsuario.cargarInformacion(nombre, apellido, informacion);
        };
        

       
    }
}
