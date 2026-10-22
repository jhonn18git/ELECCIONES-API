namespace ClienteEleccion;

public class FormLogin : Form
{
    TextBox txtEmail = new() { Left = 20, Top = 40, Width = 240, Text = "test@example.com" };
    TextBox txtPassword = new() { Left = 20, Top = 100, Width = 240, UseSystemPasswordChar = true };
    Button btnIngresar = new() { Left = 20, Top = 140, Width = 240, Height = 32, Text = "Ingresar" };

    public FormLogin()
    {
        Text = "Login - Elecciones";
        ClientSize = new Size(280, 190);
        StartPosition = FormStartPosition.CenterScreen;

        Controls.Add(new Label { Left = 20, Top = 20, Text = "Email" });
        Controls.Add(new Label { Left = 20, Top = 80, Text = "Contraseña" });
        Controls.AddRange(new Control[] { txtEmail, txtPassword, btnIngresar });

        btnIngresar.Click += Ingresar;
    }

    async void Ingresar(object? sender, EventArgs e)
    {
        try
        {
            if (await Api.Login(txtEmail.Text, txtPassword.Text))
            {
                Hide();
                new FormCandidatos().ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("Email o contraseña incorrectos");
            }
        }
        catch
        {
            MessageBox.Show("No se pudo conectar con la API, revise que este corriendo");
        }
    }
}
