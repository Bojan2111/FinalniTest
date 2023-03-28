// Kreiranje table header-a, kao argumenti se koriste nazivi kolona
function tableHeader(...args) {
  let thead = document.createElement("thead");
  let tr = document.createElement("tr");

  for (let arg of args) {
    if (arg !== "NOBUTTON"){
      tr.appendChild(theadText(arg));
    }
  }
  if (jwt_token && args[args.length -1] !== "NOBUTTON") {
    tr.appendChild(theadText("Izmeni"))
    tr.appendChild(theadText("Obrisi"));
  }
  thead.appendChild(tr);
  return thead;
}

// popunjavanje polja zaglavlja sa nazivima kolona
function theadText(tekst) {
  let polje = document.createElement("th");
  let tekstPolja = document.createTextNode(tekst);
  polje.appendChild(tekstPolja);
  return polje;
}
// Popunjavanje polja tabele sa podacima.
function cellText(tekst) {
  let polje = document.createElement("td");
  let tekstPoljaEl = document.createTextNode(tekst);
  polje.appendChild(tekstPoljaEl);
  return polje;
}

// Kreiranje tela tabele, sa parametrima podaci (fetch-response-data), callbackEdit i callbackDelete reference za funkcije respektivno.
function tableBody(podaci, callbackEdit = "NOBUTTON", callbackDelete = "NOBUTTON") {
  let telo = document.createElement("tbody");
  for (let i = 0; i < podaci.length; i++) {
    let red = document.createElement("tr");
    let stringId;
    for (let key in podaci[i]) {
			if (Object.hasOwnProperty.call(podaci[i], key)) {
        if (key === "id") {
          continue;
        } else {
				  red.appendChild(cellText(podaci[i][key]))
        }
			}
		}
		if (jwt_token && (callbackDelete !== "NOBUTTON" && callbackEdit !== "NOBUTTON")) { // callbackEdit !== null && callbackDelete !== null
			stringId = podaci[i].id.toString();
			red.appendChild(createButton(stringId, callbackEdit, "Izmeni"));
			red.appendChild(createButton(stringId, callbackDelete, "Obrisi"));
		}
		telo.appendChild(red);
  }
	
  return telo;
}

// Kreiranje dugmeta - parametri: stringId koji postaje name attr, callback funkcija za click event, tekst dugmeta
function createButton(nameId, callback, tekst) {
	let dugme = document.createElement("button");
	let dugmeTekst = document.createTextNode(tekst);
	let dugmePolje = document.createElement("td");

	dugme.name = nameId; //.toString();
	dugme.className = `btn btn-${tekst == "Izmeni" ? "warning" : "danger"}`;
	dugme.addEventListener("click", callback)
	dugme.appendChild(dugmeTekst);
	dugmePolje.appendChild(dugme);

	return dugmePolje;
}