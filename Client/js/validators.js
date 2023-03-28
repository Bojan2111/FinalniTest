const registryValidator = {
	usernameValid: false,
	emailValid: false,
	passwordValid: false,
	passwordsMatch: false
}

function beginValidateRegistry() {
	document.getElementById("passwordRegister").addEventListener("keyup", checkPassword);
	document.getElementById("passwordRegister").addEventListener("keydown", validatePassword);
	document.getElementById("confirmPasswordRegister").addEventListener("keyup", validateConfPword);
	document.getElementById("emailRegister").addEventListener("keyup", validateEmail);
	document.getElementById("usernameRegister").addEventListener("keyup", validateUname);
}

function endValidateRegistry() {
	document.getElementById("passwordRegister").removeEventListener("keyup", checkPassword);
	document.getElementById("passwordRegister").removeEventListener("keydown", validatePassword);
	document.getElementById("confirmPasswordRegister").removeEventListener("keyup", validateConfPword);
	document.getElementById("emailRegister").removeEventListener("keyup", validateEmail);
	document.getElementById("usernameRegister").removeEventListener("keyup", validateUname);
}

function validateUname() {
  let uname = this.value;
  let unameCheck = document.getElementById("unameCheck");
  let unameCheckInfo = document.getElementById("unameCheckInfo");
  let patternUname = /^(?=.{3,32}$)(?![_.-])(?!.*[_.]{2})[a-zA-Z0-9._-]+(?<![_.])$/;
  if (patternUname.test(uname)) {
    unameCheck.innerHTML = "&check;";
    unameCheck.style = "color: green;";
    unameCheckInfo.innerHTML = "";
		registryValidator.usernameValid = true;
  } else {
    unameCheck.innerHTML = "&#10008;";
    unameCheck.style = "color: red;";
    unameCheckInfo.innerHTML = "Neodgovarajuci format";
		registryValidator.usernameValid = false;
  }
}

function validatePassword() {
  let pword = this.value;
  let pwordCheck = document.getElementById("pwordCheck");
  let pwordCheckInfo = document.getElementById("pwordCheckInfo");
  let patternPword = /^(?=(?:.*[A-Z]){1,})(?=(?:.*[a-z]){1,})(?=(?:.*\d){1,})(?=(?:.*[!@#$%^&*()\-_=+{};:,<.>]){1,})(?!.*(.)\1{})([A-Za-z0-9!@#$%^&*()\-_=+{};:,<.>]{6,20})$/;
  if (patternPword.test(pword)) {
    pwordCheck.innerHTML = "&check;";
    pwordCheck.style = "color: green;";
    pwordCheckInfo.innerHTML = "";
		registryValidator.passwordValid = true;
  } else {
    pwordCheck.innerHTML = "&#10008;";
    pwordCheck.style = "color: red;";
    pwordCheckInfo.innerHTML = "Lozinka ne odgovara uputstvima";
		registryValidator.passwordValid = false;
  }
}

function validateConfPword() {
  let pword = document.getElementById("passwordRegister").value;
  let pwordConfCheck = document.getElementById("pwordConfCheck");
  let pwordConfCheckInfo = document.getElementById("pwordConfCheckInfo");
  let confPword = this.value;
  if (pword === confPword) {
    pwordConfCheck.innerHTML = "&check;";
    pwordConfCheck.style = "color: green;";
    pwordConfCheckInfo.innerHTML = "";
		registryValidator.passwordsMatch = true;
  } else {
    pwordConfCheck.innerHTML = "&#10008;";
    pwordConfCheck.style = "color: red;";
    pwordConfCheckInfo.innerHTML = "Lozinke se ne podudaraju";
		registryValidator.passwordsMatch = false;
  }
}

function validateEmail() {
  let email = this.value;
  let emailCheck = document.getElementById("emailCheck");
  let emailCheckInfo = document.getElementById("emailCheckInfo");
  let patternEmail = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
  if (patternEmail.test(email)) {
    emailCheck.innerHTML = "&check;";
    emailCheck.style = "color: green;";
    emailCheckInfo.innerHTML = "";
		registryValidator.emailValid = true;
  } else {
    emailCheck.innerHTML = "&#10008;";
    emailCheck.style = "color: red;";
    emailCheckInfo.innerHTML = "Zahtevani format: mail@example.com";
		registryValidator.emailValid = false;
  }
}

function checkPassword() {
  let pLenCheck = document.getElementById("pwordLength");
  let pUppCheck = document.getElementById("pwordUpper");
  let pLowCheck = document.getElementById("pwordLower");
  let pNumCheck = document.getElementById("pwordNumber");
  let pSpeCheck = document.getElementById("pwordSpecial");
  let patternUpper = /[A-Z]/gm;
  let patternLower = /[a-z]/gm;
  let patternNumber = /[0-9]/gm;
  let patternSpecial = /[!@#$%^&*()\-_=+{};:,<.>]/gm;
  let text = this.value;
  if (text.length >= 6 && text.length <= 20) {
    pLenCheck.innerHTML = "&check;";
    pLenCheck.style = "color: green;";
  } else {
    pLenCheck.innerHTML = "&#10008;";
    pLenCheck.style = "color: red;";
  }
  if (patternUpper.test(text)) {
    pUppCheck.innerHTML = "&check;";
    pUppCheck.style = "color: green;";
  } else {
    pUppCheck.innerHTML = "&#10008;";
    pUppCheck.style = "color: red;";
  }
  if (patternLower.test(text)) {
    pLowCheck.innerHTML = "&check;";
    pLowCheck.style = "color: green;";
  } else {
    pLowCheck.innerHTML = "&#10008;";
    pLowCheck.style = "color: red;";
  }
  if (patternNumber.test(text)) {
    pNumCheck.innerHTML = "&check;";
    pNumCheck.style = "color: green;";
  } else {
    pNumCheck.innerHTML = "&#10008;";
    pNumCheck.style = "color: red;";
  }
  if (patternSpecial.test(text)) {
    pSpeCheck.innerHTML = "&check;";
    pSpeCheck.style = "color: green;";
  } else {
    pSpeCheck.innerHTML = "&#10008;";
    pSpeCheck.style = "color: red;";
  }
}

function validateLoginForm(username, password) {
	if (username.length === 0) {
		alert("Username field can not be empty.");
		return false;
	} else if (password.length === 0) {
		alert("Password field can not be empty.");
		return false;
	}
	return true;
}

function validateRegisterForm() {
	const registerFormValid = Object.values(registryValidator).every(Boolean);
	console.log(registerFormValid);
	return registerFormValid;
}

function validatePaketForm(posiljalac, primalac, tezina, postarina) {
	if(posiljalac.length === 0){
		alert("Morate uneti naziv primaoca");
		return false;
	}
	if(posiljalac.length < 2 || posiljalac.length > 90){
		alert("Naziv primaoca mora biti izmedju 2 i 90 karaktera");
		return false;
	}
	if(primalac.length === 0){
		alert("Morate uneti posiljaoca");
		return false;
	}
	if(primalac.length < 4 || primalac.length > 120){
		alert("Naziv posiljaoca mora biti izmedju 4 i 120 karaktera");
		return false;
	}
	
	if(tezina < 0.1 || tezina >= 9.99){
		alert("Tezina mora biti izmedju 0.1 i 9.99 kg");
		return false;
	}
	if(postarina <= 250 || postarina >= 10000){
		alert("Postarina mora biti izmedju 250 i 10 000");
		return false;
	}
	return true;
}

function validateKurirForm(ime, rodjenje) {
	if (ime.length === 0) {
		alert("Ime kurira ne sme biti prazno.");
		return false;
	}
  if (ime.length > 120) {
    alert("Ime kurira mora biti manje od 120 karaktera")
  }
  if (rodjenje < 1940 || rodjenje > 2005) {
		alert("Godina rodjenja mora biti izmedju 1940 i 2005.");
		return false;
	}

	return true;
}