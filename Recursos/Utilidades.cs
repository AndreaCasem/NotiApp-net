using System.Security.Cryptography;
using System.Text;

namespace NotiApp.Recursos
{
    public class Utilidades
    {
        // Encriptar clave con el algoritmo SHA256
        public static String EncriptarClave(String clave)
        {
            // Se crea un objeto StringBuilder para la cadena de salida
            StringBuilder sb = new StringBuilder();

            // Se crea un objeto HSA256 para hacer el calculo del hash
            using (SHA256 hash = SHA256Managed.Create())
            {
                // Se crea un objeto Encoding para codificar la clave en formato UTF8
                Encoding enc = Encoding.UTF8;

                byte[] result = hash.ComputeHash(enc.GetBytes(clave));

                // Se itera a través de los bytes del hash y los agrega al StringBuilder en formato hexadecimal
                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }

                // Devuelve la clave encriptada en hexadecimal
                return sb.ToString(); 

            }
        }
    }
}
