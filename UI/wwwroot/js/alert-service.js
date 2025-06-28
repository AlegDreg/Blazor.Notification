window.NewAlert = function (message, fromLogin) {
    toastr.success(message, "От: " + fromLogin, { timeOut: 5000 })
}