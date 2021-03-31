namespace GeradorClasse
{
    public class PropriedadesTabela
    {
        public string schema_name { get; set; }
        public string table_name { get; set; }
        public string column_id { get; set; }
        public string column_name { get; set; }
        public string data_type { get; set; }
        public string max_length { get; set; }
        public string precision { get; set; }
        public bool nullable { get; set; }
    }
}