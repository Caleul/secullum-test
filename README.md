# Teste de seleção - C# e React

Para cada questão foi criado um arquivo representante da solução


## Questões 

### Questão 1

> [!NOTE]
> A resolução da questão 1 está no arquivo q1.cs

C#: Altere o código abaixo conforme instruções

```
using System;
class Program
{
    static void Main(string[] args)
    { 
        // Os funcionários de determinada empresa registram suas entradas e saídas em 
        // equipamentos REP (registrador eletrônico de ponto). Esses registros são 
        // salvos na base de dados no seguinte formato:
        Registro[] listaRegistros = new Registro[14];
        listaRegistros[0] = new Registro
        { Data = new DateTime(2019, 10, 1), Hora = "08:01", Funcionario = "João" };
        listaRegistros[1] = new Registro
        { Data = new DateTime(2019, 10, 1), Hora = "07:56", Funcionario = "Maria" };
        listaRegistros[2] = new Registro
        { Data = new DateTime(2019, 10, 1), Hora = "12:02", Funcionario = "João" };
        listaRegistros[3] = new Registro
        { Data = new DateTime(2019, 10, 1), Hora = "12:01", Funcionario = "Maria" };
        listaRegistros[4] = new Registro
        { Data = new DateTime(2019, 10, 1), Hora = "13:01", Funcionario = "João" };
        listaRegistros[5] = new Registro
        { Data = new DateTime(2019, 10, 1), Hora = "12:59", Funcionario = "Maria" };
        listaRegistros[6] = new Registro
        { Data = new DateTime(2019, 10, 1), Hora = "18:02", Funcionario = "João" };
        listaRegistros[7] = new Registro
        { Data = new DateTime(2019, 10, 1), Hora = "17:58", Funcionario = "Maria" };
        listaRegistros[8] = new Registro
        { Data = new DateTime(2019, 10, 2), Hora = "08:09", Funcionario = "João" };
        listaRegistros[9] = new Registro
        { Data = new DateTime(2019, 10, 2), Hora = "12:01", Funcionario = "João" };
        listaRegistros[10] = new Registro
        { Data = new DateTime(2019, 10, 2), Hora = "12:54", Funcionario = "João" };
        listaRegistros[11] = new Registro
        { Data = new DateTime(2019, 10, 2), Hora = "12:58", Funcionario = "Maria" };
        listaRegistros[12] = new Registro
        { Data = new DateTime(2019, 10, 2), Hora = "18:02", Funcionario = "João" };
        listaRegistros[13] = new Registro
        { Data = new DateTime(2019, 10, 2), Hora = "18:30", Funcionario = "Maria" };


        // Você deve criar um novo array que tenha o total de horas
        // trabalhadas para cada funcionário em cada dia, ordenado por nome do
        // funcionário e depois por data. Ele deve possuir os seguintes dados: 
        // 
        // [ 
        // { funcionario: 'Maria', data: '2019-10-01', total: '09:04' }, 
        // { funcionario: 'Maria', data: '2019-10-02', total: '05:32' }, 
        // { funcionario: 'João', data: '2019-10-01', total: '09:02' }, 
        // { funcionario: 'João', data: '2019-10-02', total: '09:00' } 
        // ]; 
    }
} 

class Registro 
{ 
    public DateTime Data; 
    public string Hora; 
    public string Funcionario; 
}
```

### Questão 2

> [!NOTE]
> O código da questão 2 corrigido está no arquivo q2.jsx

React: Quais problemas você encontra no código abaixo?

```
import React from 'react';

class Produtos extends React.Component { 
    constructor(props) {
        super(props);
        this.state = { produtos: [] }; 
    } 
    
    handleAddClick() { 
        const { produtos } = this.state; 
        const novoProduto = { id: 0, descricao: 'Banana' }; 
        produtos.push(novoProduto); 
        this.setState({ produtos }); 
    } 
    
    render() { 
        const { produtos } = this.state; 
        return ( 
            <div>
                <ul> 
                    { produtos.map(p => <li>{p.descricao}</li>) }
                </ul>
                <button onClick={this.handleAddClick}>+</button>
            </div>
        );
    }
}
```
