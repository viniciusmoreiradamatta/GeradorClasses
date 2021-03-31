using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeradorClasse.Base
{
    public abstract class ClassGeneratorBase
    {
        public string NameSpace { get; init; }
        public string DiretorioFinal { get; init; }

        private readonly string diretorioBase = Environment.CurrentDirectory;

        protected ClassGeneratorBase()
        {
            NameSpace = "NameSpacePadrao";
            DiretorioFinal = $"{DateTime.Now:yyyyMMdd}/";
        }

        protected ClassGeneratorBase(string nameSpace, string diretorioFinal)
        {
            NameSpace = nameSpace;
            DiretorioFinal = diretorioFinal;
        }

        protected abstract void MontarTodasClasses();

        protected abstract List<string> ListaNomeTodasTabelas();

        protected abstract List<PropriedadesTabela> ListaTodasColunasTabela(string nomeTabela);

        protected abstract string MontarPropriedade(string dataType, int precision, bool isNullable);

        public void CriarClasse(List<PropriedadesTabela> listaPropriedades, string nomeTabela)
        {
            List<string> linhasClass = new List<string>();

            var montagemClasse = new StringBuilder();

            montagemClasse.Append("using System;\n");

            montagemClasse.Append("\n namespace " + NameSpace + "\n{");

            montagemClasse.Append("\n public class " + nomeTabela + "\n{\n");

            if (listaPropriedades != null)
            {
                foreach (var item in listaPropriedades)
                {
                    int precisao = int.TryParse(item.precision, out int precisaoX) ? precisaoX : 0;

                    var prop = MontarPropriedade(item.data_type, precisao, false);

                    var nome = item.column_name.Equals(nomeTabela) ? "_" + item.column_name : item.column_name;

                    var propriedade = $"\t \t public {prop} {nome} " + "{get;set;}";

                    linhasClass.Add(propriedade);
                }

                var diretorioAtual = diretorioBase + "/" + DiretorioFinal;

                if (!Directory.Exists(diretorioAtual))
                    Directory.CreateDirectory(diretorioAtual);

                var nomeArquivo = diretorioAtual + $"/{nomeTabela}.cs";

                montagemClasse.Append(string.Join("\n", linhasClass));
                montagemClasse.Append("\n \t}");
                montagemClasse.Append("\n}");

                File.WriteAllText(nomeArquivo, montagemClasse.ToString());
            }
        }

        public void CriarUmaClasse(string nomeTable)
        {
            var lista = ListaTodasColunasTabela(nomeTable);

            CriarClasse(lista, nomeTable);
        }
    }
}