using GeradorClasse.Base;
using GeradorClasse.Extenssions;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace GeradorClasse
{
    public class ClassGeneratorSqlServer : ClassGeneratorBase
    {
        public ClassGeneratorSqlServer()
        {
        }

        public ClassGeneratorSqlServer(string nameSpace, string diretorioFinal) : base(nameSpace, diretorioFinal)
        {
        }

        public void GerarTodasClassesDoBanco() => MontarTodasClasses();

        protected override void MontarTodasClasses()
        {
            var listaNomes = ListaNomeTodasTabelas();

            foreach (var nome in listaNomes)
            {
                var propsTabela = ListaTodasColunasTabela(nome);

                CriarClasse(propsTabela, nome);
            }
        }

        protected override List<string> ListaNomeTodasTabelas()
        {
            var consulta = @"SELECT  name FROM  SYSOBJECTS WHERE xtype = 'U'";

            using var sqlClient = new SqlConnection(StringConnectionsSqlServerExtenssions.SqlConnection);

            var resultado = Dapper.SqlMapper.Query<string>(sqlClient, consulta).ToList();

            return resultado;
        }

        protected override List<PropriedadesTabela> ListaTodasColunasTabela(string nomeTabela)
        {
            var consulta = @"select schema_name(tab.schema_id) as schema_name,
                                    tab.name as table_name,
                                    col.column_id,
                                    col.name as column_name,
                                    t.name as data_type,
                                    col.max_length,
                                    col.precision
                                from sys.tables as tab
                                    inner join sys.columns as col
                                        on tab.object_id = col.object_id
                                    left join sys.types as t
                                    on col.user_type_id = t.user_type_id
                                    where tab.name= @nomeTabela
                                order by schema_name,
                                    table_name,
                                    column_id";

            using var sqlClient = new SqlConnection(StringConnectionsSqlServerExtenssions.SqlConnection);

            var resultado = Dapper.SqlMapper.Query<PropriedadesTabela>(sqlClient, consulta, new { nomeTabela }).ToList();

            return resultado;
        }

        protected override string MontarPropriedade(string dataType, int precision, bool isNullable)
        {
            return dataType.GetTipoPropriedadeSqlServer();
        }
    }
}