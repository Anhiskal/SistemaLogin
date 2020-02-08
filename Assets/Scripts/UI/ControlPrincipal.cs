using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPrincipal : MonoBehaviour
{
    public GameObject prefabDeInformacion;
    public Transform contenedorDeInformacion;

    public void cargarInformacionDeUsuarios(List<EstructuraInformacion> todaLaInfo) 
    {
        int cantidad = todaLaInfo.Count;
        for (int i = 0; i < cantidad; i++)
        {
            GameObject informacion =  Instantiate(prefabDeInformacion, contenedorDeInformacion);
            informacion.GetComponent<InformacionBasicaUsuario>().nombreYApellido.text = todaLaInfo[i].nombre + " " + todaLaInfo[i].apellido;
            informacion.GetComponent<InformacionBasicaUsuario>().informacionExtra.text = todaLaInfo[i].sobreMi;
            informacion.gameObject.SetActive(true);
        }
    }

    public void cargarInformacion(EstructuraInformacion usuarioNuevo)
    {
        GameObject informacion = Instantiate(prefabDeInformacion, contenedorDeInformacion);
        informacion.GetComponent<InformacionBasicaUsuario>().nombreYApellido.text = usuarioNuevo.nombre + " " + usuarioNuevo.apellido;
        informacion.GetComponent<InformacionBasicaUsuario>().informacionExtra.text = usuarioNuevo.sobreMi;
        informacion.gameObject.SetActive(true);
    }

    public void reiniciarPagina() 
    {
        eliminarTodosLosElementos();
    }

    public void agregarUnUsuario()
    { }

    public void eliminarTodosLosElementos() 
    {
        int numeroDeInfo = contenedorDeInformacion.childCount;
        if (numeroDeInfo > 0) 
        {
            foreach (Transform informacion in contenedorDeInformacion)
            {
                GameObject.Destroy(informacion.gameObject);
            }              
            
        }
        
    }

}
