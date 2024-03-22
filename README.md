# Teste de seleção - C# e React

No README deste projeto, foram fornecidos esclarecimentos para questões relacionadas ao teste.

Embora seja uma boa prática incluir instruções para execução do código em um ambiente local, neste caso específico, não foram fornecidas tais orientações. Também não foi criado um Dockerfile ou Docker Compose para a execução em contêineres por não ser aplicável. 

Caso queira executar o código C# online, recomendo o site [MyCompiler](https://www.mycompiler.io/pt/new/csharp)

## Questões 

> [!WARNING]
> Embora os comentários sejam úteis para esclarecer partes complexas do código ou documentar decisões, seu uso excessivo pode indicar que o código não está seguindo as melhores práticas de programação
>
> Nesse caso, os comentários foram usados apenas para esclarecer o motivo da escolha da lógica do código, compreendo que é uma quantidade muito grande de comentários e usualmente não se faz necessário
>
> Apenas quis demonstrar minha linha de pensamento através dos comentários

### Questão 1

> [!NOTE]
> A resolução da questão 1 está no arquivo questions/q1.cs
> 
> Foram deixados comentários no arquivo explicando a linha de raciocínio

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
> O código da questão 2 corrigido está no arquivo questions/q2.jsx
> 
> Nele foram deixados comentários explicando a linha de raciocínio
> 
> As respostas estão abaixo do código, ainda nessa página

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

#### Problemas encontrados

> [!IMPORTANT]
> Os problemas foram listados seguindo a ordem númerica das linhas, sem nenhum grau de gravidade dos problemas ou outro parâmetro:

1. É necessário vincular o método `handleAddClick` no construtor da classe
     - O React não vincula automáticamente um método ao contexto do componente
     - Portanto dentro do método construtor foi adicionado o trecho de código `this.handleAddClick = this.handleAddClick.bind(this)`
     - Porém, se o método fosse escrito com arrow function, não seria necessário fazer bind do mesmo `handleAddClick = () => {}`

2. O id do produto criado ao chamar o método `handleAddClick` não é único
     - Ter id único é uma regra comum no mundo da programação, e o mesmo se aplica num ambiente React
     - Para resolver isso foi adicionado a seguinte validação no código ao criar um novo produto`produtos.length > 0 ? produtos[produtos.length - 1].id + 1 : 1`
     - Existe a possibilidade dessa classe produto se repetir dentro da página, gerando id repetido entre componentes e dificultando o controle
     - Uma possibilidade seria usar uuid, garantindo que mesmo diferentes instancias desse objeto não vão ter um id repetido
  
3. Alterar uma variável de estado do React viola o princípio de imutabilidade
     - Remover o método antigo de adicionar um novo produto à lista de produtos com `produtos.push(novoProduto)` e `this.setState({ produtos })`
     - Adicionar para criar uma nova variável com os valores antigos e o valor novo `const novosProdutos = [...produtos, novoProduto]`
     - Atualizar o estado a partir da variável nova `this.setState({ produtos: novosProdutos });`
  
4. Ao criar um `<li></li>` no React, é importante passar um valor de chave
     - Caso não passe, seria disparado um aviso no console, mas o componente ainda funcionaria
     - O React teria problemas para editar essa lista, pois não haveria uma forma de excluir elementos dela
     - Caso o valor do id de cada produto fosse o mesmo, esse problema ainda existiria
     - O código foi alterado para garantir identificação da lista `{ produtos.map(p => <li key={p.id}>{p.descricao}</li>) }`
  
5. O evento `onClick={this.handleAddClick}` apresenta um potencial problema, pois não passa os dados do evento para o chamado
     - Pelo fato de o método `handleAddClick` não receber nenhum parâmetro, no momento esse trecho de código não é um problema
     - Porém o ideal seria enviar o evento ao chamar o método `handleAddClick` com `onClick={(e) => this.handleAddClick(e)}`
