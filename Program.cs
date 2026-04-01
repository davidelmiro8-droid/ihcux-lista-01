/*
Heurísticas de Nielsen aplicadas no sistema:

1. Visibilidade do Status do Sistema:
O sistema informa claramente em qual etapa o usuário está através das mensagens
"[Passo 1 de 3]", "[Passo 2 de 3]" e "[Passo 3 de 3]".
Além disso, há feedback visual com cores e a mensagem "Processando pedido..."
antes da finalização.

3. Controle e Liberdade do Usuário:
O usuário pode digitar "voltar" para retornar à etapa anterior e corrigir dados,
ou "cancelar" para encerrar o pedido a qualquer momento do fluxo.

9. Ajuda no Reconhecimento e Diagnóstico de Erros:
O sistema apresenta mensagens de erro específicas e claras, como quando o código
do produto está fora do intervalo permitido (1 a 10) ou quando a quantidade é inválida,
orientando o usuário sobre como corrigir.
*/

using System;
using System.Threading;

class Program
{
    static void Main()
    {
        int codigoProduto = 0;
        int quantidade = 0;
        int passo = 1;

        string[] produtos = {
            "Coxinha - R$5",
            "Pastel - R$6",
            "Pão de queijo - R$4",
            "Refrigerante - R$5",
            "Suco - R$4",
            "Hambúrguer - R$10",
            "Batata frita - R$8",
            "Pizza - R$12",
            "Café - R$3",
            "Chocolate - R$6"
        };

        double[] precos = {5,6,4,5,4,10,8,12,3,6};

        while (true)
        {
            while (passo == 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== CANTINA UNA ===");
                Console.ResetColor();

                Console.WriteLine("\n[Passo 1 de 3] - Escolha seu produto\n");

                for (int i = 0; i < produtos.Length; i++)
                {
                    Console.WriteLine($"{i + 1} - {produtos[i]}");
                }

                Console.WriteLine("\nDigite o código do produto:");
                Console.WriteLine("(ou 'cancelar')");

                string input = Console.ReadLine().ToLower();

                if (input == "cancelar")
                {
                    Cancelar();
                }

                if (int.TryParse(input, out codigoProduto))
                {
                    if (codigoProduto >= 1 && codigoProduto <= 10)
                    {
                        passo = 2;
                    }
                    else
                    {
                        Erro($"Código {codigoProduto} não encontrado. Use valores de 1 a 10.");
                    }
                }
                else
                {
                    Erro("Digite apenas números.");
                }
            }

            while (passo == 2)
            {
                Console.Clear();
                Console.WriteLine("[Passo 2 de 3] - Quantidade\n");

                Console.WriteLine($"Produto selecionado: {produtos[codigoProduto - 1]}");

                Console.WriteLine("\nDigite a quantidade:");
                Console.WriteLine("(ou 'voltar' / 'cancelar')");

                string input = Console.ReadLine().ToLower();

                if (input == "cancelar") Cancelar();

                if (input == "voltar")
                {
                    passo = 1;
                    break;
                }

                if (int.TryParse(input, out quantidade) && quantidade > 0)
                {
                    passo = 3;
                }
                else
                {
                    Erro("Quantidade inválida. Digite um número maior que zero.");
                }
            }

            while (passo == 3)
            {
                Console.Clear();
                Console.WriteLine("[Passo 3 de 3] - Confirmação\n");

                double total = quantidade * precos[codigoProduto - 1];

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Resumo do Pedido:");
                Console.ResetColor();

                Console.WriteLine($"Produto: {produtos[codigoProduto - 1]}");
                Console.WriteLine($"Quantidade: {quantidade}");
                Console.WriteLine($"Total: R${total}");

                Console.WriteLine("\nConfirmar pedido? (s/n)");
                Console.WriteLine("(ou 'voltar' / 'cancelar')");

                string input = Console.ReadLine().ToLower();

                if (input == "cancelar") Cancelar();

                if (input == "voltar")
                {
                    passo = 2;
                    break;
                }

                if (input == "s")
                {
                    Processando();
                    Sucesso("Pedido realizado com sucesso!");
                }
                else if (input == "n")
                {
                    Console.WriteLine("Pedido reiniciado...");
                    Thread.Sleep(1000);
                    passo = 1;
                    break;
                }
                else
                {
                    Erro("Digite 's' ou 'n'.");
                }
            }
        }
    }

    static void Erro(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nErro: {mensagem}");
        Console.ResetColor();
        Console.WriteLine("Pressione qualquer tecla...");
        Console.ReadKey();
    }

    static void Sucesso(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{mensagem}");
        Console.ResetColor();
        Console.ReadKey();
        Environment.Exit(0);
    }

    static void Cancelar()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nPedido cancelado.");
        Console.ResetColor();
        Console.ReadKey();
        Environment.Exit(0);
    }

    static void Processando()
    {
        Console.Write("\nProcessando pedido");
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(500);
            Console.Write(".");
        }
        Console.WriteLine();
    }
}
