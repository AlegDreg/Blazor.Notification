window.NewAlert = function (message, fromLogin) {
    toastr.success(message, "От: " + fromLogin, { timeOut: 5000 })
}

window.NewError = function (text, title) {
    toastr.error(text, title, { timeOut: 5000 })
}