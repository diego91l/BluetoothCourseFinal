using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;

namespace BluetoothCourse
{
    public class ConexionFirebase
    {
        public static FirebaseAuthClient ConectarFirebase()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "\r\nAIzaSyDKktPUL_dgGkjd0hfsRj-yfy1jEEm0Bno",
                AuthDomain = "projectblt-7fa25.web.app",
                Providers = new FirebaseAuthProvider[]
            {
                // Add and configure individual providers
                new GoogleProvider().AddScopes("email"),
                new EmailProvider()
                // ...
            },

            };

            var client = new FirebaseAuthClient(config);

            return client;
        }

        public async Task<UserCredential> CargarUsuario(string Email, string Password)
        {
            try
            {
                var cliente = ConectarFirebase();
                var userCredential = await cliente.SignInWithEmailAndPasswordAsync(Email, Password);

                return userCredential;
            }
            catch (FirebaseAuthException ex)
            {

                throw new Exception("Usuario o contraseña incorrecta.", ex);
            }

        }
        public async Task<UserCredential> CrearUsuario(string Email, string Password)
        {
            try
            {
                var cliente = ConectarFirebase();
                var userCredential = await cliente.CreateUserWithEmailAndPasswordAsync(Email, Password);
                return userCredential;

            }
            catch (Exception ex)
            {

                throw new Exception("Error no se puede crear usuario.", ex);
            }


        }

        public async Task EnviarCorreoRecuperacion(string correo)
        {
            var cliente = ConectarFirebase();
            await cliente.ResetEmailPasswordAsync(correo);
        }


    }
}
