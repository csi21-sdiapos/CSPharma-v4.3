using System.Security.Cryptography;
using System.Text;

namespace CSPharma_v4._1.Tools
{
    public class Encriptador
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dato"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encriptar(string dato, string key)
        {

            byte[] data = Encoding.UTF8.GetBytes(dato);
            //instanciamos el servicio de el encripter MD5 y TRIPLEDES

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(key));

                using (TripleDESCryptoServiceProvider tripleDES = new()
                {
                    //configuramos el tripledes con la clave personalizada y habilitar el modo encryptador del tripledes
                    Key = keys,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    //generamos un encriptador de tripledes con la configuracion asignada anteriormente
                    ICryptoTransform transform = tripleDES.CreateEncryptor();
                    //transforma el dato en un array de bytes con los datos especificados
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    //devuelve el dato encriptado en tipo string para su devida utilización
                    return Convert.ToBase64String(results, 0, results.Length);
                }

            }
        }

        public static string Desencriptar(string datos, string key)
        {
            byte[] data = Convert.FromBase64String(datos);

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider()
                {
                    Key = keys,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    //aqui declaramos en vez de un encriptador, construimos un desencriptador con la configuracion asignada y la clave personalizada
                    ICryptoTransform transform = tripleDES.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Encoding.UTF8.GetString(results, 0, results.Length);
                }
            }

        }
    }
}
