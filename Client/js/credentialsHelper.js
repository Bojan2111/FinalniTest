function formReset(idName) {
  document.getElementById(idName).reset();
}

function showRegistration() {
  window.event.preventDefault();
  formReset("loginForm");
  showHideElement("informacija", hideClass);
  showHideElement("registracija-forma", showClass);
  disableLoginForm();
	beginValidateRegistry();
}

function closeRegistration() {
  window.event.preventDefault();
  showHideElement("informacija", showClass);
  showHideElement("registracija-forma", hideClass);
  formReset("registrationForm");
  enableLoginForm();
	endValidateRegistry();
}

function loginUser() {
  window.event.preventDefault();
	var username = document.getElementById("usernameLogin").value;
	var password = document.getElementById("passwordLogin").value;

	if (validateLoginForm(username, password)) {
		let url = baseUrl + loginEndpoint;
		var sendData = { "Username": username, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					formReset("loginForm");
					console.log("Successful login");
					alert("Successful login");
					response.json().then(function (data) {
						document.getElementById("loggedUser").innerHTML = data.username;
						jwt_token = data.token;
						loginSuccessAction();
					});
				} else {
					console.log("Error occured with code " + response.status);
					alert("Error occured!");
					response.text().then(text => { console.log(text); })
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}

function loginSuccessAction() {
  showHideElement("login-forma", hideClass);
  showHideElement("registracija-dugme-div", hideClass);
  showHideElement("logged", showClass);
  showHideElement("logout-dugme-div", showClass);
  showHideElement("pretraga", showClass);
  showHideElement("uredjivanje", showClass);
  showHideElement("informacija", hideClass);
  modifyElement("footer", "footer-logged");
  formReset("loginForm");
	loadLoggedInPage();
}

function logout() {
  window.event.preventDefault();
  jwt_token = undefined;
  modifyElement("footer", "footer-logged");
  loadPage();
}

function registerUser() {
  window.event.preventDefault();
	let username = document.getElementById("usernameRegister").value;
	let email = document.getElementById("emailRegister").value;
	let password = document.getElementById("passwordRegister").value;
	let confirmPassword = document.getElementById("confirmPasswordRegister").value;

	if (validateRegisterForm(username, email, password, confirmPassword)) {
		let url = baseUrl + registerEndpoint;
		let sendData = { "Username": username, "Email": email, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					console.log("Successful registration");
          alert("Successful registration");
					endValidateRegistry();
					loadPage();
				} else {
					console.log("Error occured with code " + response.status);
					alert("Desila se greska!");
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}