using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace WcfCityCell
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWSUser
    {

        [OperationContract]
        int ObtenerLogin(string UserLogin, string Password);

        [OperationContract]
        int RegisterNow(string Name, string NumIndentification, string Mail, string UserId, string Password);

        [OperationContract]
        DataSet ObtenerProductos();
    }



}
