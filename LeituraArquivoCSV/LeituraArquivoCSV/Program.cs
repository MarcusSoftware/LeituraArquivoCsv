using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LeituraArquivoCSV
{
    public static class Program

    {

        static void Main(string[] args)
        {
            Executar();
            Console.ReadLine();
        }

        private static void Executar()
        {
            try
            {
                StreamReader csv = new StreamReader(@"C:\projetos\teste.csv");

                string linha;
                string[] colunas = null;
                var itensCsv = new List<ObjetoCSV>();

                while ((linha = csv.ReadLine()) != null)
                {
                    colunas = linha.Split(";");
                    itensCsv.Add(new ObjetoCSV
                    {
                        Id = colunas[0],
                        Nome = colunas[1],
                        Cargo = colunas[2],
                        Local = colunas[3],
                        Quantidade = Convert.ToInt32(colunas[4]),
                        Valor = Convert.ToDecimal(colunas[5])
                    });
                }

                var listaConsultor = itensCsv.Where(c => c.Cargo.Equals("CONSULTOR")).ToList();
                var listaFreelancer = itensCsv.Where(c => c.Cargo.Equals("FREE-LANCER")).ToList();
                var listaPde = itensCsv.Where(c => c.Cargo.Equals("PDE")).ToList();

                var resultaConsultor = Calcular(listaConsultor);
                var resultadoFreelancer = Calcular(listaFreelancer);
                var resultadoPde = Calcular(listaPde);

                Console.WriteLine($"CONSULTOR_______Quantidade: {resultaConsultor.Quantidade} | Valor total: {resultaConsultor.Valor}");
                Console.WriteLine($"FREE-LANCER_____Quantidade: {resultadoFreelancer.Quantidade} | Valor total: {resultadoFreelancer.Valor}");
                Console.WriteLine($"PDE_____________Quantidade: {resultadoPde.Quantidade} | Valor total: {resultadoPde.Valor}");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
            }            
        }

        private static Resposta Calcular(List<ObjetoCSV> lista)
        {
            var quantidade = lista.Sum(x => x.Quantidade);
            var valor = lista.Sum(x => x.Valor);
            return new Resposta 
            {
                Quantidade = quantidade,
                Valor = valor
            };
        }
    }

    public class ObjetoCSV
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Local { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }

    public class Resposta
    {
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }

}
