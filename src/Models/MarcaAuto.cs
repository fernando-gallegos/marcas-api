namespace MarcasApi.Models;

public class MarcaAuto
{
    /// <summary>
    /// Llave primaria de la tabla
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la marca
    /// </summary>
    public string Nombre { get; set; } = string.Empty;
}
