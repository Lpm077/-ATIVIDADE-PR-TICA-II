using System;
using System.Collections.Generic;
using System.Linq;

class Exercicio
{
    public string Nome { get; set; }
    public string GrupoMuscular { get; set; }
    public double Carga { get; set; }
    public int Repeticoes { get; set; }
}

class Program
{
    static Dictionary<string, Exercicio> exercicios = new Dictionary<string, Exercicio>();

    static void Main(string[] args)
    {
        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE TREINO ===");
            Console.WriteLine("1 - Adicionar exercício");
            Console.WriteLine("2 - Listar exercícios");
            Console.WriteLine("3 - Buscar exercício por nome");
            Console.WriteLine("4 - Filtrar por grupo muscular");
            Console.WriteLine("5 - Calcular carga total do treino");
            Console.WriteLine("6 - Exibir exercício mais pesado");
            Console.WriteLine("7 - Remover exercício");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");

            int.TryParse(Console.ReadLine(), out opcao);

            switch (opcao)
            {
                case 1: AdicionarExercicio(); break;
                case 2: ListarExercicios(); break;
                case 3: BuscarExercicio(); break;
                case 4: FiltrarPorGrupo(); break;
                case 5: CalcularCargaTotal(); break;
                case 6: ExercicioMaisPesado(); break;
                case 7: RemoverExercicio(); break;
            }

            Console.WriteLine("\nPressione qualquer tecla...");
            Console.ReadKey();

        } while (opcao != 0);
    }

    static void AdicionarExercicio()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome inválido.");
            return;
        }

        if (exercicios.ContainsKey(nome.ToLower()))
        {
            Console.WriteLine("Exercício já cadastrado.");
            return;
        }

        Console.Write("Grupo muscular: ");
        string grupo = Console.ReadLine();

        double carga;
        Console.Write("Carga (kg): ");
        if (!double.TryParse(Console.ReadLine(), out carga) || carga < 0)
        {
            Console.WriteLine("Carga inválida.");
            return;
        }

        int repeticoes;
        Console.Write("Repetições: ");
        if (!int.TryParse(Console.ReadLine(), out repeticoes) || repeticoes < 1)
        {
            Console.WriteLine("Repetições inválidas.");
            return;
        }

        exercicios.Add(nome.ToLower(), new Exercicio
        {
            Nome = nome,
            GrupoMuscular = grupo,
            Carga = carga,
            Repeticoes = repeticoes
        });

        Console.WriteLine("Exercício adicionado com sucesso!");
    }

    static void ListarExercicios()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado.");
            return;
        }

        foreach (var ex in exercicios.Values)
        {
            Console.WriteLine($"{ex.Nome} - {ex.GrupoMuscular} - {ex.Carga}kg - {ex.Repeticoes}");
        }
    }

    static void BuscarExercicio()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine().ToLower();

        if (exercicios.ContainsKey(nome))
        {
            var ex = exercicios[nome];
            Console.WriteLine($"{ex.Nome} - {ex.GrupoMuscular} - {ex.Carga}kg - {ex.Repeticoes}");
        }
        else
        {
            Console.WriteLine("Exercício não encontrado.");
        }
    }

    static void FiltrarPorGrupo()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        Console.Write("Grupo muscular: ");
        string grupo = Console.ReadLine().ToLower();

        var filtrados = exercicios.Values
            .Where(e => e.GrupoMuscular.ToLower() == grupo)
            .ToList();

        if (filtrados.Count == 0)
        {
            Console.WriteLine("Nenhum exercício encontrado.");
            return;
        }

        foreach (var ex in filtrados)
        {
            Console.WriteLine(ex.Nome);
        }
    }

    static void CalcularCargaTotal()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        double total = exercicios.Values.Sum(e => e.Carga);
        Console.WriteLine($"Carga total: {total} kg");
    }

    static void ExercicioMaisPesado()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        var maisPesado = exercicios.Values
            .OrderByDescending(e => e.Carga)
            .First();

        Console.WriteLine($"Mais pesado: {maisPesado.Nome} - {maisPesado.Carga} kg");
    }

    static void RemoverExercicio()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        Console.Write("Nome do exercício: ");
        string nome = Console.ReadLine().ToLower();

        if (exercicios.Remove(nome))
        {
            Console.WriteLine("Exercício removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Exercício não encontrado.");
        }
    }
}