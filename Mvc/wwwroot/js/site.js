document.addEventListener("DOMContentLoaded", function () {
    const createButton = document.getElementById("createButton");

    createButton.addEventListener("click", function () {
        const orderData = [];
        const cardElements = document.querySelectorAll(".card");
        cardElements.forEach(function (card) {
            const bookId = card.id;
            const quantityInput = card.querySelector("input[name='quantity']");
            const quantity = quantityInput.value;

            if (quantity > 0) {
                orderData.push({ bookId, quantity });
            }
        });

        console.log(JSON.stringify(orderData));
        fetch("/Order/NewOrder", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(orderData),
        }).then(() => {
            location.reload(); 
        })
            .catch(error => {
                console.error("An error occurred:", error);
        });

    });
});