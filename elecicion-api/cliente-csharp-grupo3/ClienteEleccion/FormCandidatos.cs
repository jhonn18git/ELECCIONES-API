namespace ClienteEleccion;

public class FormCandidatos : Form
{
    DataGridView tabla = new()
    {
        Left = 20, Top = 20, Width = 560, Height = 220,
        ReadOnly = true,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    };
    TextBox txtNombres = new(), txtApellidos = new(), txtCargo = new(), txtAmbito = new();
    int idSeleccionado = 0;

    public FormCandidatos()
    {
        Text = "CRUD de Candidatos";
        ClientSize = new Size(600, 375);
        StartPosition = FormStartPosition.CenterScreen;
        Controls.Add(tabla);

        string[] etiquetas = { "Nombres", "Apellidos", "Cargo", "Ambito" };
        TextBox[] cajas = { txtNombres, txtApellidos, txtCargo, txtAmbito };
        for (int i = 0; i < cajas.Length; i++)
        {
            Controls.Add(new Label { Text = etiquetas[i], Left = 20 + i * 143, Top = 255, Width = 130 });
            cajas[i].SetBounds(20 + i * 143, 275, 130, 25);
            Controls.Add(cajas[i]);
        }

        AgregarBoton("Nuevo", 20, (s, e) => Limpiar());
        AgregarBoton("Guardar", 163, Guardar);
        AgregarBoton("Eliminar", 306, Eliminar);
        AgregarBoton("Recargar", 449, async (s, e) => await Cargar());

        tabla.CellClick += Seleccionar;
        Load += async (s, e) => await Cargar();
    }

    void AgregarBoton(string texto, int left, EventHandler accion)
    {
        var boton = new Button { Text = texto, Left = left, Top = 320, Width = 130, Height = 35 };
        boton.Click += accion;
        Controls.Add(boton);
    }

    async Task Cargar()
    {
        tabla.DataSource = await Api.Listar();
    }

    void Seleccionar(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        var c = (Candidato)tabla.Rows[e.RowIndex].DataBoundItem;
        idSeleccionado = c.Id;
        txtNombres.Text = c.Nombres;
        txtApellidos.Text = c.Apellidos;
        txtCargo.Text = c.Cargo;
        txtAmbito.Text = c.Ambito;
    }

    async void Guardar(object? sender, EventArgs e)
    {
        var c = new Candidato
        {
            Id = idSeleccionado,
            Nombres = txtNombres.Text,
            Apellidos = txtApellidos.Text,
            Cargo = txtCargo.Text,
            Ambito = txtAmbito.Text
        };

        // sin fila elegida es un candidato nuevo, con fila elegida es una edicion
        var resp = idSeleccionado == 0 ? await Api.Crear(c) : await Api.Actualizar(c);
        Mostrar(resp, "Candidato guardado");
    }

    async void Eliminar(object? sender, EventArgs e)
    {
        if (idSeleccionado == 0)
        {
            MessageBox.Show("Primero seleccione un candidato de la tabla");
            return;
        }
        var resp = await Api.Eliminar(idSeleccionado);
        Mostrar(resp, "Candidato eliminado");
    }

    async void Mostrar(HttpResponseMessage resp, string mensajeOk)
    {
        if (resp.IsSuccessStatusCode) MessageBox.Show(mensajeOk);
        else if ((int)resp.StatusCode == 403) MessageBox.Show("Solo un administrador puede hacer esto");
        else MessageBox.Show("Error " + (int)resp.StatusCode);

        Limpiar();
        await Cargar();
    }

    void Limpiar()
    {
        idSeleccionado = 0;
        txtNombres.Clear();
        txtApellidos.Clear();
        txtCargo.Clear();
        txtAmbito.Clear();
    }
}
