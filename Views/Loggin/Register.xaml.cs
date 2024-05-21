namespace BluetoothCourse.Views.Loggin;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
	}

    private async void btnRegister_Clicked(object sender, EventArgs e)
    {
        string name = txtName.Text;
        string email = txtEmail.Text;
        string password = txtPassword.Text;

        try
        {
            ConexionFirebase conexionFirebase = new ConexionFirebase();
            var userCredential = await conexionFirebase.CrearUsuario(email, password);
            await DisplayAlert("Success", "User created successfully", "OK");
            await Navigation.PopAsync(); // Go back to the login page
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}