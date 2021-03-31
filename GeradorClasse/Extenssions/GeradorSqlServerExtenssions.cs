namespace GeradorClasse.Extenssions
{
    public static class GeradorSqlServerExtenssions
    {
        public static string GetTipoPropriedadeSqlServer(this string tipo) => tipo switch
        {
            "datetimeoffset" => "DateTime",
            "date" => "DateTime",
            "datetime2" => "DateTime",
            "nvarchar" => "string",
            "bit" => "bool",
            "bigint" => "long",
            _ => tipo,
        };
    }
}