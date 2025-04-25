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