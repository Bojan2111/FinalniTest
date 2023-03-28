// definisanje naziva klasa za sakrivanje i pokazivanje elemenata - mora odgovarati nazivima klasa u CSS fajlu
const showClass = "prikazi";
const hideClass = "sakrij";

// dodaje zeljenu klasu 'elClass' i uklanja suprotnu klasu sa elementa - prikazivanje i sakrivanje elementa
function showHideElement(elId, elClass) {
  if (elClass === hideClass) {
    document.getElementById(elId).classList.remove(showClass);
    document.getElementById(elId).classList.add(hideClass);
  } else {
    document.getElementById(elId).classList.remove(hideClass);
    document.getElementById(elId).classList.add(showClass);
  }
}

// Dodavanje ili oduzimanje odredjene klase - koristeno za footer element, ali moze da se koristi svuda.
// parametri: elemId - id atribut elementa, modifyingClass - style klasa koju zelimo ubaciti ili izbaciti iz elementa.
function modifyElement(elemId, modifyingClass) {
  if (document.getElementById(elemId).classList.contains(modifyingClass)) {
    document.getElementById(elemId).classList.remove(modifyingClass);
  } else {
    document.getElementById(elemId).classList.add(modifyingClass);
  }
}

// Kreira naslov sekcije -> size: 1-6 i.e. h1-h6; title: string - npr. myHeading = createHeading(2, "Moj Naslov")
function createHeading(size, title) {
	let heading = document.createElement(`h${size}`);
	let headingText = document.createTextNode(title);
  heading.classList.add("text-center")
	heading.appendChild(headingText);
	return heading;
}

// Ubacuje atribut 'disabled' u input polja za login formu, kao i na dugme za pozivanje forme za registraciju
function disableLoginForm() {
  document.getElementById("usernameLogin").setAttribute("disabled", "true");
  document.getElementById("passwordLogin").setAttribute("disabled", "true");
  document.getElementById("login-submit").setAttribute("disabled", "true");
  document.getElementById("registracija-dugme").setAttribute("disabled", "true");
}

// Brise atribut 'disabled' iz input polja za login formu, kao i sa dugmeta za pozivanje forme za registraciju
function enableLoginForm() {
  document.getElementById("usernameLogin").removeAttribute("disabled");
  document.getElementById("passwordLogin").removeAttribute("disabled");
  document.getElementById("login-submit").removeAttribute("disabled");
  document.getElementById("registracija-dugme").removeAttribute("disabled");
}