using BluetoothCourse.Bluetooth;

namespace BluetoothCourse.Views.ControlLuces;

public partial class LightControl : ContentPage
{
    private readonly BluetoothDevice _bluetoothDevice;
    public LightControl(BluetoothDevice bluetoothDevice)
	{
		InitializeComponent();
        _bluetoothDevice = bluetoothDevice;
    }

    private async void btnEncender1_Clicked(object sender, EventArgs e)
    {
        await SendCommandAsync("a");

    }
    private async void btnApagar1_Clicked(object sender, EventArgs e)
    {
        await SendCommandAsync("b");
    }

    private async void btnEncender2_Clicked(object sender, EventArgs e)
    {
        await SendCommandAsync("c");
    }

    private async void btnApagar2_Clicked(object sender, EventArgs e)
    {
        await SendCommandAsync("d");
    }

    private async void btnEncender3_Clicked(object sender, EventArgs e)
    {
        await SendCommandAsync("e");
    }

    private async void btnApagar3_Clicked(object sender, EventArgs e)
    {
        await SendCommandAsync("f");
    }

    private async void btnEncender4_Clicked(object sender, EventArgs e)
    {
        await SendCommandAsync("g");
    }

    private async void btnApagar4_Clicked(object sender, EventArgs e)
    {
        await SendCommandAsync("h");
    }


    private async Task SendCommandAsync(string command)
    {
        var characteristicUuid = new Guid("0000FFE1-0000-1000-8000-00805F9B34FB"); // Reemplaza esto con tu UUID de característica
        var data = System.Text.Encoding.UTF8.GetBytes(command);
        var success = await _bluetoothDevice.Write(characteristicUuid, data);

        if (success)
        {
            await DisplayAlert("Success", $"Command '{command}' sent successfully", "OK");
        }
        else
        {
            await DisplayAlert("Error", $"Failed to send command '{command}'", "OK");
        }
    }

}