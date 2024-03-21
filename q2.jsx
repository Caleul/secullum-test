import React from 'react';

class Produtos extends React.Component {
    constructor(props) {
        super(props);
        this.state = { produtos: [] };

        // É necessário fazer um bound nesse método
        // Para vincular o evento ao componente e garantir que ele reflita a instancia do componente
        this.handleAddClick = this.handleAddClick.bind(this);
    } 
    
    handleAddClick() { 
        const { produtos } = this.state; 
        
        const novoProduto = {
            // O id do produto não era único
            // A maneira implementada abaixo funciona como um autoincremento em relação ao id do produto anterior
            id: produtos.length > 0 ? produtos[produtos.length - 1].id + 1 : 1,
            descricao: 'Banana'
        };

        // Ao invés de adicionar o produto novo na variavel antiga, é necessário criar um novo array com o produto novo e os antigos
        // Para não violar a imutabilidade de estado em React, não usamos o produtos.push e sim o setState()
        const novosProdutos = [...produtos, novoProduto];

        // Atualizamos o stado com a nova lista de produtos
        this.setState({ produtos: novosProdutos }); 
    } 
    
    render() { 
        const { produtos } = this.state; 
        return ( 
            <div>
                <ul> 
                    { produtos.map(p => {
                        // Crio uma referência única para cada item
                        // esse referência é o id, que agora é único para cada item
                        <li key={p.id}>{p.descricao}</li>
                    }) }
                </ul>
                {/* 
                    Para que o método funcione corretamente, você deve passar um evento e com esse evento chamar a função
                    Porém, como o método não está usando dados do evento, ele funcionaria sem problemas
                */}
                <button onClick={(e) => this.handleAddClick(e)}>+</button>
            </div>
        );
    }
}
