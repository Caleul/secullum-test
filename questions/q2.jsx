import React from 'react';
import { v4 as uuidv4 } from 'uuid';

class Produtos extends React.Component {
    constructor(props) {
        super(props);
        this.state = { produtos: [] };

        // É necessário fazer um bound nesse método
        // Para vincular o evento ao componente e garantir que ele reflita a instancia do componente
        this.handleAddClick = this.handleAddClick.bind(this);
        // Caso o método handleClick fosse escrito com uma arrow funcition, não seria necessário criar o trecho de código acima
    } 
    
    // Caso esse método fosse escrito como o comentário da linha abaixo, não seria necessário criar o bind para vincular o método
    // handleAddClick = () => {}
    handleAddClick() {
        const { produtos } = this.state; 
        
        const novoProduto = {
            // O id do produto não era único
            // A maneira implementada abaixo funcionaria como um autoincremento em relação ao id do produto anterior
            // id: produtos.length > 0 ? produtos[produtos.length - 1].id + 1 : 1,
            // Existe a possibilidade dessa classe produto se repetir dentro da página, gerando id repetido entre componentes e dificultando o controle
            // Uma possibilidade seria usar uuid, garantindo que mesmo diferentes instancias desse objeto não vão ter um id repetido
            id: uuidv4(),
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
                        // Essa referência é o id, que agora é único para cada item
                        return <li key={p.id}>{p.descricao}</li>
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
