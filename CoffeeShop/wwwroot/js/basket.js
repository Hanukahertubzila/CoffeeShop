const addBtnEl = document.getElementById("add-btn-el")
const substractBtnEl = document.getElementById("substract-btn-el")
const finalPriceEl = document.getElementById("final-price-el")
const quantityEl = document.getElementById("quantity-el")
const modelLabelEl = document.getElementById("ModalLabel")

let productPrice = 0
let quantity = 1

function setModal(price, productName) {
    productPrice = Number(price.replace(/[^0-9]/g, '')) / 100
    quantity = 1
    finalPriceEl.textContent = productPrice.toFixed(2)
    quantityEl.textContent = quantity
    modelLabelEl.textContent = productName
}

addBtnEl.addEventListener("click", function () {
    quantity += 1
    quantityEl.textContent = quantity
    finalPriceEl.textContent = (productPrice * quantity).toFixed(2)
})

substractBtnEl.addEventListener("click", function () {
    if (quantity > 1) {
        quantity -= 1
        quantityEl.textContent = quantity
        finalPriceEl.textContent = (productPrice * quantity).toFixed(2)
    }
})
