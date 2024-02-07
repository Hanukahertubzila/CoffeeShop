const searchIptEl = document.getElementById("search-ipt-el")
const searchBtnEl = document.getElementById("search-btn-el")

searchBtnEl.addEventListener("click", function () {
    console.log('hei')
    let search = searchIptEl.value
    search = search.trim()
    window.location.href = '/Product/GetProductByName?name=' + search
})

