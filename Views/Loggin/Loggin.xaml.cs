using Firebase.Auth;

namespace BluetoothCourse.Views.Loggin;

public partial class Loggin : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public Loggin(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
    }

    private async void btnInicio_Clicked(object sender, EventArgs e)
    {
        ConexionFirebase conexionFirebase = new ConexionFirebase();
        string email = txtEmail.Text;
        string password = txtPassword.Text;

        try
        {
            var Credenciales = await conexionFirebase.CargarUsuario(email, password);

            var Uid = Credenciales.User.Uid;


            if (email == txtEmail.Text && password == txtPassword.Text)
            {
                var bluetoothScanner = _serviceProvider.GetRequiredService<BluetoothCourse.Bluetooth.BluetoothScanner>();
                await Navigation.PushAsync(new Scan.ScanResults(bluetoothScanner));
            }
            else
            {
                await DisplayAlert("Alert", "Usuario o contraseña incorrecta", "Ok");
                txtEmail.Text = "";
                txtPassword.Text = "";
            }

        }
        catch (FirebaseAuthException ex) when (ex.Reason == AuthErrorReason.WrongPassword)
        {
            await DisplayAlert("Error", "Contraseña incorrecta", "Ok");
        }
        catch (FirebaseAuthException ex) when (ex.Reason == AuthErrorReason.UnknownEmailAddress)
        {
            await DisplayAlert("Error", "Correo no registrado", "Ok");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Usuario o password incorrectos", "Ok");
        }


    }


    private async void btnForgotPassword_Clicked(object sender, EventArgs e)
    {
        string email = await DisplayPromptAsync("Recuperar Contraseña", "Ingresa tu correo electrónico:", "Enviar", "Cancelar", "Correo", -1, Keyboard.Email);

        if (!string.IsNullOrEmpty(email))
        {
            try
            {
                ConexionFirebase conexionFirebase = new ConexionFirebase();
                await conexionFirebase.EnviarCorreoRecuperacion(email);
                await DisplayAlert("Éxito", "Correo de recuperación enviado. Por favor, revisa tu correo.", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudo enviar el correo de recuperación. Por favor, intenta de nuevo.", "Ok");
            }
        }

    }

    private void btnRegistrar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.Loggin.Register());
    }
}