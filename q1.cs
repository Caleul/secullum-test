using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    { 
        Registro[] listaRegistros = new Registro[14];
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

        Dictionary<DateTime, Dictionary<string, List<string>>> registrosAgrupadosPorDataEFuncionario = AgruparRegistrosPorDataEFuncionario(listaRegistros);

        printaTodosRegistros(registrosAgrupadosPorDataEFuncionario);

        List<TempoTrabalhadoPorDia> tempoTrabalhadoPorDia = CalculaTempoTrabalhadoPorDia(registrosAgrupadosPorDataEFuncionario);

        printaTotalHorasTrabalhada(tempoTrabalhadoPorDia);
    }

    static void printaTotalHorasTrabalhada(List<TempoTrabalhadoPorDia> tempoTrabalhadoPorDia)
    {
        foreach (var item in tempoTrabalhadoPorDia)
        {
            Console.WriteLine($"Funcionario: {item.Funcionario}, Data: {item.Data}, Total: {item.Total}");
        }
        
    }

    static void printaTodosRegistros(Dictionary<DateTime, Dictionary<string, List<string>>> registrosAgrupadosPorDataEFuncionario)
    {
        foreach (KeyValuePair<DateTime, Dictionary<string, List<string>>> data in registrosAgrupadosPorDataEFuncionario)
        {            
            Console.WriteLine($"Data: {data.Key.ToShortDateString()}");
            
            foreach (KeyValuePair<string, List<string>> funcionario in data.Value)
            {
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

        foreach (Registro registro in registros)
        {
            if (!registrosAgrupados.ContainsKey(registro.Data))
            {
                registrosAgrupados[registro.Data] = new Dictionary<string, List<string>>();
            }

            Dictionary<string, List<string>> registrosPorFuncionario = registrosAgrupados[registro.Data];

            if (!registrosPorFuncionario.ContainsKey(registro.Funcionario))
            {
                registrosPorFuncionario[registro.Funcionario] = new List<string>();
            }

            registrosPorFuncionario[registro.Funcionario].Add(registro.Hora);
        }

        return registrosAgrupados;
    }

    static List<TempoTrabalhadoPorDia> CalculaTempoTrabalhadoPorDia(Dictionary<DateTime, Dictionary<string, List<string>>> registrosAgrupadosPorDataEFuncionario)
    {
        List<TempoTrabalhadoPorDia> tempoTrabalhadoPorDia = new List<TempoTrabalhadoPorDia>();

        foreach (KeyValuePair<DateTime, Dictionary<string, List<string>>> data in registrosAgrupadosPorDataEFuncionario)
        {            
            foreach (KeyValuePair<string, List<string>> funcionario in data.Value)
            {
                if (funcionario.Value.Count > 0 && funcionario.Value.Count % 2 == 0)
                {
                    TimeSpan totalHorasTrabalhada = TimeSpan.Zero;

                    for (int i = 0; i < funcionario.Value.Count; i += 2)
                    {
                        string horaInicio = funcionario.Value[i];
                        string horaFim = funcionario.Value[i + 1];

                        TimeSpan diferenca = TimeSpan.Parse(horaFim) - TimeSpan.Parse(horaInicio);

                        totalHorasTrabalhada += diferenca;
                    }

                    string totalHorasTrabalhadaFormatada = $"{totalHorasTrabalhada.Hours:00}:{totalHorasTrabalhada.Minutes:00}";
                    tempoTrabalhadoPorDia.Add(new TempoTrabalhadoPorDia { Funcionario = funcionario.Key, Data = data.Key.ToString("yyyy-MM-dd"), Total = totalHorasTrabalhadaFormatada});
                }
                else 
                {
                    Console.WriteLine($"Pela quantidade de registros do funcionário {funcionario.Key}, não é possível calcular a quantidade de horas trabalhadas no dia {data.Key.ToString("yyyy-MM-dd")}; Favor corrigir a quantidade de registros");
                }
            }
        }
    
        return tempoTrabalhadoPorDia.OrderBy(item => item.Funcionario).ThenBy(item => item.Data).ToList();
    }
} 

class Registro 
{ 
    public DateTime Data; 
    public string Hora; 
    public string Funcionario; 
}

class TempoTrabalhadoPorDia
{
    public string Funcionario;
    public string Data;
    public string Total;
}
