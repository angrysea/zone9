function checkEmail(emailaddress) {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(emailaddress)) {
        return (true)
    }
    alert("Invalid E-mail Address! Please re-enter.")
    return (false)
}

