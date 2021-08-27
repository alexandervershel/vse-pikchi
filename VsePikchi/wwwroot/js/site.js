
// навигация
var hamburgerLabel = document.querySelector('.hamburger-menu-label')
var hamburgerCloseLabel = document.querySelector('.hamburger-close-menu-label')
var navigation = document.querySelector('.nav-list')


hamburgerLabel.onclick = () => {
    navigation.style.left = '-30%'
}

hamburgerCloseLabel.onclick = () => {
    navigation.style.left = '-100%'
}

// копирование в буфер обмена
function copyUrlToClipboard() {
    var span = document.querySelector(".url-block").children[1];
    navigator.clipboard.writeText(span.innerText);
    alert(copyText.value);
}
