using System;

namespace GeradorClasse
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string _namespace = "CustomNameSpace";

            string directory = "Home";

            var gerador = new ClassGeneratorSqlServer(_namespace, directory);

            gerador.CriarUmaClasse("NomeTabela");

            Console.WriteLine($"Verifique a classe gerada em .\\GeradorClasse\\bin\\Debug\\net5.0\\{directory}!");
        }
    }
}