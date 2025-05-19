document.addEventListener("DOMContentLoaded", function () {
    setTimeout(() => {
        const track = document.querySelector('.product-carousel-track');
        const cards = document.querySelectorAll('.product-card');

        // Verificações de segurança
        if (!track) {
            console.warn("Elemento .product-carousel-track não encontrado.");
            return;
        }

        if (!cards.length) {
            console.warn("Nenhum .product-card encontrado.");
            return;
        }

        const cardWidth = cards[0].offsetWidth + 16; // 16 = margem/gap
        let index = 0;

        // Clona os cards para fazer rotação infinita
        cards.forEach(card => {
            const clone = card.cloneNode(true);
            track.appendChild(clone);
        });

        function moveNext() {
            index++;
            track.style.transform = `translateX(-${index * cardWidth}px)`;

            if (index >= cards.length) {
                setTimeout(() => {
                    track.style.transition = 'none';
                    index = 0;
                    track.style.transform = 'translateX(0)';
                    void track.offsetWidth; // força reflow
                    track.style.transition = 'transform 0.5s ease';
                }, 500);
            }
        }

        function movePrev() {
            if (index === 0) {
                track.style.transition = 'none';
                index = cards.length;
                track.style.transform = `translateX(-${index * cardWidth}px)`;
                void track.offsetWidth;
                track.style.transition = 'transform 0.5s ease';
            }
            index--;
            track.style.transform = `translateX(-${index * cardWidth}px)`;
        }

        const nextBtn = document.querySelector('.product-carousel-button.next');
        const prevBtn = document.querySelector('.product-carousel-button.prev');

        if (nextBtn) nextBtn.addEventListener('click', moveNext);
        if (prevBtn) prevBtn.addEventListener('click', movePrev);
    }, 100);
});

const produtos = {
    jordan4: {
        nome: "Jordan 4",
        preco: "899,99",
        imagem: "/assets/tenis/air-jordan4.png",
        tamanhos: ["Escolha seu tamanho", "38", "39", "40", "41"],
        cores: ["Escolha a cor", "Preto/Vermelho", "Preto/Cinza"],
        descricao: "Tênis Jordan 4 com detalhes em camurça e estilo retrô."
    },
    shadow: {
        nome: "Tênis Shadow",
        preco: "R$1700,00",
        imagem: "/assets/tenis/tenis-shadow.2.jpeg",
        tamanhos: ["Escolha seu tamanho", "37", "38", "39", "42"],
        cores: ["Branco/Vermelho"],
        descricao: "Sim! Nós capturamos o shadow, o rouliço, após ele mijar na esposa do Dr Eggman."
    },

    jordan5: {
        nome: "Air Max 90",
        preco: "R$720,00",
        imagem: "/assets/tenis/tenis-airjordan5.jpg",
        tamanhos: ["Escolha seu tamanho", "37", "38", "39", "42"],
        cores: ["Escolha a cor", "Branco/Azul", "Cinza/Verde"],
        descricao: "Air Max 90 com amortecimento máximo e design moderno."
    }

   
};

function carregarProduto() {

    const params = new URLSearchParams(window.location.search);
    const id = params.get('produto')
    

    const produto = produtos[id];
    if (!produto) return;


    document.getElementById("nome").innerText = produto.nome;
    document.getElementById("preco").innerText = produto.preco;
    document.getElementById("imagem").src = produto.imagem;
    document.getElementById("descricao").innerText = produto.descricao;

    const tamanhoSelect = document.getElementById("tamanhos");
    tamanhoSelect.innerHTML = "";
    produto.tamanhos.forEach(tamanho => {
        const option = document.createElement("option");
        option.value = tamanho;
        option.textContent = tamanho;
        tamanhoSelect.appendChild(option);
    });

    const corSelect = document.getElementById("cores");
    corSelect.innerHTML = "";
    produto.cores.forEach(cor => {
        const option = document.createElement("option");
        option.value = cor;
        option.textContent = cor;
        corSelect.appendChild(option);
    });
}

carregarProduto();