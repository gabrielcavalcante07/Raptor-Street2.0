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

// mascara de CPF
function formatarCPF(campo) {
    let cpf = campo.value.replace(/\D/g, '');
    if (cpf.length > 11) cpf = cpf.slice(0, 11);

    cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
    cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
    cpf = cpf.replace(/(\d{3})(\d{1,2})$/, '$1-$2');

    campo.value = cpf;
}

// mascara telefone
function formatarTelefone(campo) {
    let tel = campo.value.replace(/\D/g, '');
    if (tel.length > 11) tel = tel.slice(0, 11);

    tel = tel.replace(/^(\d{2})(\d)/, '($1) $2');
    tel = tel.replace(/(\d{5})(\d{1,4})$/, '$1-$2');

    campo.value = tel;
}

//mascara cep
function formatarCEP(campo) {
    let cep = campo.value.replace(/\D/g, '').slice(0, 8);
    if (cep.length >= 6) {
        cep = cep.replace(/(\d{5})(\d)/, '$1-$2');
    }
    campo.value = cep;
}

// mudar visibilidade da senha
function toggleSenha(inputId, iconId) {
    const input = document.getElementById(inputId);
    const icon = document.getElementById(iconId);

    if (input.type === "password") {
        input.type = "text";
        icon.classList.remove('fa-eye-slash');
        icon.classList.add('fa-eye');
    } else {
        input.type = "password";
        icon.classList.remove('fa-eye');
        icon.classList.add('fa-eye-slash');
    }
}

// qnd carregar a pagina, mostrar os dados formatados ;)
document.addEventListener('DOMContentLoaded', function () {
    const cpfInput = document.querySelector('[name="CPF"]');
    const telInput = document.querySelector('[name="Telefone"]');
    const cepInput = document.querySelector('[name="CEP"]');

    if (cpfInput) formatarCPF(cpfInput);
    if (telInput) formatarTelefone(telInput);
    if (cepInput) formatarCEP(cepInput);
});

// limpar a formataçao css
function limparFormatacao() {
    const cpf = document.querySelector('[name="CPF"]');
    const tel = document.querySelector('[name="Telefone"]');
    const cep = document.querySelector('[name="CEP"]');

    if (cpf) cpf.value = cpf.value.replace(/\D/g, '');
    if (tel) tel.value = tel.value.replace(/\D/g, '');
    if (cep) cep.value = cep.value.replace(/\D/g, '');
}

// mensagem flutuante

window.onload = function () {
    const msg = document.getElementById("loginMessage");
    if (msg) {
        setTimeout(() => {
            msg.style.opacity = '0';
            setTimeout(() => msg.remove(), 500);
        }, 4000);
    }
};