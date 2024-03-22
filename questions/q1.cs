using System;
using System.Collections.Generic;
using System.Linq;

// Para correto funcionamento da coleções genéricas (Dictionary e List) é usado "using System.Collections.Generic;"
// Para correto funcionamento de funções genéricas (OrderBy e ThenBy) é usado "using System.Linq;"

class Program
{
    static void Main(string[] args)
    { 
        Registro[] listaRegistros = new Registro[14];

        // O código funciona para listas de registro de qualquer tamanho, desde que todos os itens da lista sejam instâncias da classe Registro
        
        listaRegistros[0] = new Registro { Data = new DateTime(2019, 10, 1), Hora = "08:01", Funcionario = "Joao" };
        listaRegistros[1] = new Registro { Data = new DateTime(2019, 10, 1), Hora = "07:56", Funcionario = "Maria" };
        listaRegistros[2] = new Registro { Data = new DateTime(2019, 10, 1), Hora = "12:02", Funcionario = "Joao" };
        listaRegistros[3] = new Registro { Data = new DateTime(2019, 10, 1), Hora = "12:01", Funcionario = "Maria" };
        listaRegistros[4] = new Registro { Data = new DateTime(2019, 10, 1), Hora = "13:01", Funcionario = "Joao" };
        listaRegistros[5] = new Registro { Data = new DateTime(2019, 10, 1), Hora = "12:59", Funcionario = "Maria" };
        listaRegistros[6] = new Registro { Data = new DateTime(2019, 10, 1), Hora = "18:02", Funcionario = "Joao" };
        listaRegistros[7] = new Registro { Data = new DateTime(2019, 10, 1), Hora = "17:58", Funcionario = "Maria" };
        listaRegistros[8] = new Registro { Data = new DateTime(2019, 10, 2), Hora = "08:09", Funcionario = "Joao" };
        listaRegistros[9] = new Registro { Data = new DateTime(2019, 10, 2), Hora = "12:01", Funcionario = "Joao" };
        listaRegistros[10] = new Registro { Data = new DateTime(2019, 10, 2), Hora = "12:54", Funcionario = "Joao" };
        listaRegistros[11] = new Registro { Data = new DateTime(2019, 10, 2), Hora = "12:58", Funcionario = "Maria" };
        listaRegistros[12] = new Registro { Data = new DateTime(2019, 10, 2), Hora = "18:02", Funcionario = "Joao" };
        listaRegistros[13] = new Registro { Data = new DateTime(2019, 10, 2), Hora = "18:30", Funcionario = "Maria" };

        // Os nomes das funções são autoexplicativos para facilitar a leitura, compartilhamento e edição do código

        Dictionary<DateTime, Dictionary<string, List<string>>> registrosAgrupadosPorDataEFuncionario = AgruparRegistrosPorDataEFuncionario(listaRegistros);

        printaTodosRegistros(registrosAgrupadosPorDataEFuncionario);

        List<TempoTrabalhadoPorDia> tempoTrabalhadoPorDia = CalculaTempoTrabalhadoPorDia(registrosAgrupadosPorDataEFuncionario);

        printaTotalHorasTrabalhada(tempoTrabalhadoPorDia);
    }

    static void printaTotalHorasTrabalhada(List<TempoTrabalhadoPorDia> tempoTrabalhadoPorDia)
    {
        // A lista de horas trabalhadas é simplesmente uma lista, facilitando sua impressão em comparação com os registros ordenados e separados

        foreach (var item in tempoTrabalhadoPorDia)
        {
            Console.WriteLine($"Funcionario: {item.Funcionario}, Data: {item.Data}, Total: {item.Total}");
        }
        
    }

    static void printaTodosRegistros(Dictionary<DateTime, Dictionary<string, List<string>>> registrosAgrupadosPorDataEFuncionario)
    {
        // Embora pareça mais complexo, o funcionamento ainda é simples: ele percorre todos os registros e imprime seus valores

        foreach (KeyValuePair<DateTime, Dictionary<string, List<string>>> data in registrosAgrupadosPorDataEFuncionario)
        {
            // Começando pelo dia, que é a chave do registro chave-valor recebido pela função. Criado com AgruparRegistrosPorDataEFuncionario()

            Console.WriteLine($"Data: {data.Key.ToShortDateString()}");
            
            foreach (KeyValuePair<string, List<string>> funcionario in data.Value)
            {
                // Então mostra o funcionário, que é o valor de registrosAgrupadosPorDataEFuncionario e também a chave de Dictionary<string, List<string>>
                // Sabendo que o funcionário é a chave, para mostrar ele no console foi usado funcionario.Key
                // E para mostrar o horário de registro foi usado funcionario.Value

                Console.WriteLine($"  - Funcionario: {funcionario.Key}");

                foreach (string hora in funcionario.Value)
                {
                    Console.WriteLine($"    - Hora: {hora}");
                }
            }
        }   
    }

    static Dictionary<DateTime, Dictionary<string, List<string>>> AgruparRegistrosPorDataEFuncionario(Registro[] registros)
    {
        Dictionary<DateTime, Dictionary<string, List<string>>> registrosAgrupados = new Dictionary<DateTime, Dictionary<string, List<string>>>();

        // A função agrupa os registros em ordem por data e funcionário, criando um mapeamento onde cada data é associada a um mapeamento de funcionários e registros

        foreach (Registro registro in registros)
        {
            if (!registrosAgrupados.ContainsKey(registro.Data))
            {
                // Se não existir registro da data criado, ele inicia o mesmo na variavel Dictionary<DateTime, Dictionary<string, List<string>>>
                // Onde o valor da chave é a data e o valor do conteúdo é mapeamento valor-chave de funcionário e horário
                registrosAgrupados[registro.Data] = new Dictionary<string, List<string>>();
            }

            // Cria o mapeamento Dictionary<string, List<string>>
            // Atribui os valores desse mapeamento à variavel
            Dictionary<string, List<string>> registrosPorFuncionario = registrosAgrupados[registro.Data];

            if (!registrosPorFuncionario.ContainsKey(registro.Funcionario))
            {
                // Se nessa variavel que recebeu atribuição do valor de registros agrupados não houver registro do funcionário, ele será criado
                // Essa lista representa os horários de registro desse funcionário específico nesse dia específico
                registrosPorFuncionario[registro.Funcionario] = new List<string>();
            }

            // atribuo a lista desse funcionario nesse dia específico o horário de registro
            registrosPorFuncionario[registro.Funcionario].Add(registro.Hora);

            // Ao editar o valor de registrosPorFuncionario, também estamos alterando registrosAgrupados porque ambos são referências ao mesmo objeto
            // Isso ocorre porque tipos como Dictionary ou List são atribuídos por referência, não por valor
            // Portanto, o código funcionaria corretamente se substituíssemos registrosPorFuncionario por registrosAgrupados[registro.Data]

        }

        return registrosAgrupados;
    }

    static List<TempoTrabalhadoPorDia> CalculaTempoTrabalhadoPorDia(Dictionary<DateTime, Dictionary<string, List<string>>> registrosAgrupadosPorDataEFuncionario)
    {
        List<TempoTrabalhadoPorDia> tempoTrabalhadoPorDia = new List<TempoTrabalhadoPorDia>();

        // A função vai receber o mapeamento Dictionary<DateTime, Dictionary<string, List<string>>> registrosAgrupadosPorDataEFuncionario
        // Atraves dele calcular quantas horas por dia cada funcionario trabalhou

        foreach (KeyValuePair<DateTime, Dictionary<string, List<string>>> data in registrosAgrupadosPorDataEFuncionario)
        {
            // Dentro de cada dia do registro

            foreach (KeyValuePair<string, List<string>> funcionario in data.Value)
            {
                // Dentro de cada funcionário
                // Valida se tem um número maior de zero e par de elementos, dessa forma garante que tem registro de entrada e saída

                if (funcionario.Value.Count > 0 && funcionario.Value.Count % 2 == 0)
                {
                    TimeSpan totalHorasTrabalhada = TimeSpan.Zero;

                    for (int i = 0; i < funcionario.Value.Count; i += 2)
                    {
                        // O loop for avança em incrementos de dois para processar os pares de registros de entrada e saída consecutivos
                        // Dessa forma ele sabe que o primeiro é de entrada e o segundo de saída, então avança para a próxima entrada
                        // Ele calcula todas as horas trabalhadas no dia, independentemente de quantos registros existam

                        string horaInicio = funcionario.Value[i];
                        string horaFim = funcionario.Value[i + 1];

                        TimeSpan diferenca = TimeSpan.Parse(horaFim) - TimeSpan.Parse(horaInicio);

                        totalHorasTrabalhada += diferenca;
                    }

                    string totalHorasTrabalhadaFormatada = $"{totalHorasTrabalhada.Hours:00}:{totalHorasTrabalhada.Minutes:00}";

                    // Adiciona à lista o objeto no formato definido pelo teste

                    tempoTrabalhadoPorDia.Add(new TempoTrabalhadoPorDia { Funcionario = funcionario.Key, Data = data.Key.ToString("yyyy-MM-dd"), Total = totalHorasTrabalhadaFormatada});
                }
                else 
                {
                    Console.WriteLine($"Pela quantidade de registros do funcionário {funcionario.Key}, não é possível calcular a quantidade de horas trabalhadas no dia {data.Key.ToString("yyyy-MM-dd")}; Favor corrigir a quantidade de registros");
                }
            }
        }

        // Ordena e retorna a lista em ordem
        // Dando preferência pelo nome do funcionário e então pelo dia do registro
    
        return tempoTrabalhadoPorDia.OrderBy(item => item.Funcionario).ThenBy(item => item.Data).ToList();

        // Apenas um adendo, a questão pede que ordene por funcionário e então por data, mas mostra que Maria veio antes de João
        // Porém J vem antes do M no alfabeto
        // Por conta disso a ordem vai estar diferente da mostrada no exercício
    }
} 

class Registro 
{ 
    public DateTime Data; 
    public string Hora; 
    public string Funcionario; 
}

// Declara o tipo de objeto que será o resultado final da função
// Nome da classe criada buscam ser auto explicativos, para facilitar leitura, compartilhamento e edição do código

class TempoTrabalhadoPorDia
{
    public string Funcionario;
    public string Data;
    public string Total;
}
