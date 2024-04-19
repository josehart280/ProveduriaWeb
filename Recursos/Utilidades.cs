using System.Security.Cryptography;
using System.Text;
namespace ProveduriaWeb.Recursos
{
    public class Utilidades
    {

        public static string EncriptarClave(string contrasena)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding encoding = Encoding.UTF8;

                byte[] result = hash.ComputeHash(encoding.GetBytes(contrasena));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));


                }

                return sb.ToString();

            }



        }



    }
}
