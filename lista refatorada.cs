using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Dictionary<string, (string grupo, double carga, int repeticoes)> exercicios =
        new Dictionary<string, (string, double, int)>();

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
        string nome = Console.ReadLine().ToLower();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome inválido.");
            return;
        }

        if (exercicios.ContainsKey(nome))
        {
            Console.WriteLine("Exercício já cadastrado.");
            return;
        }

        Console.Write("Grupo muscular: ");
        string grupo = Console.ReadLine();

        Console.Write("Carga (kg): ");
        if (!double.TryParse(Console.ReadLine(), out double carga) || carga < 0)
        {
            Console.WriteLine("Carga inválida.");
            return;
        }

        Console.Write("Repetições: ");
        if (!int.TryParse(Console.ReadLine(), out int repeticoes) || repeticoes < 1)
        {
            Console.WriteLine("Repetições inválidas.");
            return;
        }

        exercicios.Add(nome, (grupo, carga, repeticoes));
        Console.WriteLine("Exercício adicionado com sucesso!");
    }

    static void ListarExercicios()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado.");
            return;
        }

        foreach (var ex in exercicios)
        {
            Console.WriteLine($"{ex.Key} - {ex.Value.grupo} - {ex.Value.carga}kg - {ex.Value.repeticoes}");
        }
    }

    static void BuscarExercicio()
    {
        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine().ToLower();

        if (exercicios.ContainsKey(nome))
        {
            var ex = exercicios[nome];
            Console.WriteLine($"{nome} - {ex.grupo} - {ex.carga}kg - {ex.repeticoes}");
        }
        else
        {
            Console.WriteLine("Exercício não encontrado.");
        }
    }

    static void FiltrarPorGrupo()
    {
        Console.Write("Grupo muscular: ");
        string grupo = Console.ReadLine().ToLower();

        var filtrados = exercicios
            .Where(e => e.Value.grupo.ToLower() == grupo);

        if (!filtrados.Any())
        {
            Console.WriteLine("Nenhum exercício encontrado.");
            return;
        }

        foreach (var ex in filtrados)
        {
            Console.WriteLine(ex.Key);
        }
    }

    static void CalcularCargaTotal()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        double total = exercicios.Sum(e => e.Value.carga);
        Console.WriteLine($"Carga total: {total} kg");
    }

    static void ExercicioMaisPesado()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        var maisPesado = exercicios
            .OrderByDescending(e => e.Value.carga)
            .First();

        Console.WriteLine($"Mais pesado: {maisPesado.Key} - {maisPesado.Value.carga} kg");
    }

    static void RemoverExercicio()
    {
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
