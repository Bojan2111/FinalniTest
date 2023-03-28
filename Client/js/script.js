// Osnovni podaci
const port = 44321;
let baseUrl = `https://localhost:${port}/api/`;

// Auth endpoints
let registerEndpoint = "authentication/register";
let loginEndpoint = "authentication/login";

// Entity API endpoints
let paketiEndpoint = "paketi/";
let kuririEndpoint = "kuriri/";

// Search endpoints
let searchPaketiEndpoint = "pretraga";
let searchDostaveEndpoint = "dostave?granica=";
let searchPrijemEndpoint = "paketi/trazi?prijem=";
let kuririImeEndpoint = "kuriri/nadji?ime=";

// Stanje endpoints
let stanjeEndpoint = "stanje"

// Working variables
let formAction = "Create";
let editingId;
let jwt_token;

function loadPage() {
  showHideElement("logged", hideClass);
  showHideElement("logout-dugme-div", hideClass);
  showHideElement("login-forma", showClass);
  showHideElement("registracija-dugme-div", showClass);
  showHideElement("data", showClass);
  showHideElement("informacija", showClass);
  showHideElement("pretraga", hideClass);
  showHideElement("uredjivanje", hideClass);
  showHideElement("registracija-forma", hideClass);
  formReset("loginForm");
  enableLoginForm();
  formReset("registrationForm");
  loadPaketi();
}

function loadPaketi()
{
	let requestUrl = baseUrl + paketiEndpoint;
	let headers = {};
	console.log(headers);
	fetch(requestUrl)
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setPaketi);
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));
};

// Ovde izmeni samo header stringove.
function setPaketi(data) {
	let container = document.getElementById("data");
	container.innerHTML = "";
	let div = document.createElement("div");
	
	div.appendChild(createHeading(1, "Paketi"));

	let table = document.createElement("table");
	table.setAttribute("class", "table table-bordered table-striped table-hover");
	let header = tableHeader("Posiljalac", "Primalac", "Tezina", "Postarina", "Kurir");
	table.append(header);

	table.append(tableBody(data, editPaket, deletePaket));

	div.appendChild(table);

	container.appendChild(div);
};

// NIJE automatizovano
function setPaketiSearch(data) {
	let container = document.getElementById("showSearchTezina");
	container.innerHTML = "";

	let table = document.createElement("table");
	table.setAttribute("class", "table table-bordered table-striped table-hover");
	let header = tableHeader("Posiljalac", "Primalac", "Tezina", "Postarina", "Kurir", "NOBUTTON");
	table.append(header);

	table.append(tableBody(data));

	container.appendChild(table);
};
console.log(sessionStorage);
function deletePaket() {
	let deleteID = this.name;
	let url = baseUrl + paketiEndpoint + deleteID.toString();
	let headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
	fetch(url, { method: "DELETE", headers: headers})
		.then((response) => {
			if (response.status === 200 || response.status === 204) {
				console.log("Record deleted successfully");
				loadPaketi();
			} else {
				console.log("Error occured with code " + response.status);
				alert("Desila se greska!");
			}
		})
		.catch(error => console.log(error));
}

function searchPaketTezina() {
  window.event.preventDefault();
	let startTezina = document.getElementById("start").value;
	let endTezina = document.getElementById("kraj").value;

	let url = baseUrl + searchPaketiEndpoint;
	httpAction = "POST";
	godine = {
		"najmanje": parseInt(startTezina),
		"najvise": parseInt(endTezina)
	};
	let headers1 = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers1.Authorization = 'Bearer ' + jwt_token;
	}
	fetch(url, { method: httpAction, headers: headers1, body: JSON.stringify(godine) })
		.then((response) => {
			if (response.status === 200 || response.status === 201) {
				response.json().then(setPaketiSearch);
			} else {
				console.log("Error occured with code " + response.status);
				alert("Desila se greska!");
			}
		})
		.catch(error => console.log(error));
	return false;
}

function loadKuriri() {
	let requestUrl = baseUrl + kuririEndpoint;
	let headers = {};
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;
	}
	fetch(requestUrl, { headers: headers })
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setKurir);
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));
}

function setKurir(data) {
	let dropdown = document.getElementById("kurirId");
  dropdown.innerHTML = "";
	for (let i = 0; i < data.length; i++) {
		let option = document.createElement("option");
		option.value = data[i].id;
		let text = document.createTextNode(data[i].ime);
		option.appendChild(text);
		dropdown.appendChild(option);
	}
}

function submitPaketForm() {
	let posiljalac = document.getElementById("posiljalac").value;
	let primalac = document.getElementById("primalac").value;
	let tezina = document.getElementById("tezina").value;
	let postarina = document.getElementById("postarina").value;
	let kurirId = document.getElementById("kurirId").value;
	let httpAction;
	let sendData;
	let url;

	if (formAction === "Create") {
		httpAction = "POST";
		url = baseUrl + paketiEndpoint;
		sendData = {
			"posiljalac": posiljalac,
			"primalac": primalac,
			"tezina": tezina,
			"postarina": postarina,
			"kurirId": kurirId
		};
	}
	else {
		httpAction = "PUT";
		url = baseUrl + paketiEndpoint + editingId.toString();
		sendData = {
			"id": editingId,
			"posiljalac": posiljalac,
			"primalac": primalac,
			"tezina": tezina,
			"postarina": postarina,
			"kurirId": kurirId
		};
	}

	let headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;	
	}
  if(validatePaketForm(posiljalac, primalac, tezina, postarina)) {
    fetch(url, { method: httpAction, headers: headers, body: JSON.stringify(sendData) })
      .then((response) => {
        if (response.status === 200 || response.status === 201) {
          console.log("Record submitted successfully");
          formAction = "Create";
          document.getElementById("izmeniDodaj").innerHTML = "Dodaj";
          cancelForm();
        } else {
          console.log("Error occured with code " + response.status);
          alert("Desila se greska!");
        }
      })
      .catch(error => console.log(error));
    return false;
  }
}

function editPaket() {
	let editId = this.name;

	let url = baseUrl + paketiEndpoint + editId.toString();
	let headers = { };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;	
	}
	fetch(url, { headers: headers})
		.then((response) => {
			if (response.status === 200) {
				console.log("Record obtained successfully for editing");
				response.json().then(data => {
					document.getElementById("posiljalac").value = data.posiljalac;
					document.getElementById("primalac").value = data.primalac;
					document.getElementById("tezina").value = data.tezina;
					document.getElementById("postarina").value = data.postarina;
					document.getElementById("kurirId").value = data.kurirId;
          document.getElementById("izmeniDodaj").innerHTML = "Izmeni";
					
					editingId = data.id;
					formAction = "Update";
				});
			} else {
				formAction = "Create";
				console.log("Error occured with code " + response.status);
				alert("Desila se greska!");
			}
		})
		.catch(error => console.log(error));
}

function searchKuririIme() {
	let ime = document.getElementById("kurirIme").value;
	let requestUrl = baseUrl + kuririImeEndpoint + ime;
	let headers = {};
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;
	}
	axios.get(requestUrl, { headers: headers })
		.then(function (response) {
			// handle success
			setKurirIme(response.data);
		}).catch (function (error) {
			// handle error
			console.log(`Error occured with code ${error.response.status}`);
			console.log(error);
			showError();
		})
}

function setKurirIme(data) {
	let tabelaDiv = document.getElementById("kuririImeTabela");
	tabelaDiv.innerHTML = "";

	let table = document.createElement("table");
	table.setAttribute("class", "table table-bordered table-striped table-hover");
	let header = tableHeader("Ime", "Rodjenje", "NOBUTTON");
	table.append(header);

	table.append(tableBody(data));

	tabelaDiv.appendChild(table);
}

function searchDostave() {
	let granica = document.getElementById("searchTezina").value;
	let requestUrl = baseUrl + searchDostaveEndpoint + granica;
	let headers = {};
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;
	}
	axios.get(requestUrl, { headers: headers })
		.then(function (response) {
			// handle success
			setDostave(response.data);
		}).catch (function (error) {
			// handle error
			console.log(`Error occured with code ${error.response.status}`);
			console.log(error);
			showError();
		})
}

function setDostave(data) {
	let tabelaDiv = document.getElementById("dostaveTabela");
	tabelaDiv.innerHTML = "";

	let table = document.createElement("table");
	table.setAttribute("class", "table table-bordered table-striped table-hover");
	let header = tableHeader("ID", "Ime kurira", "Ukupna tezina", "NOBUTTON");
	table.append(header);

	table.append(tableBody(data));

	tabelaDiv.appendChild(table);
}

function searchPrijem() {
	let primaoc = document.getElementById("searchPrijem").value;
	let requestUrl = baseUrl + searchPrijemEndpoint + primaoc;
	let headers = {};
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;
	}
	axios.get(requestUrl, { headers: headers })
		.then(function (response) {
			// handle success
			setPrijem(response.data);
		}).catch (function (error) {
			// handle error
			console.log(`Error occured with code ${error.response.status}`);
			console.log(error);
			showError();
		})
}

function setPrijem(data) {
	let tabelaDiv = document.getElementById("prijemTabela");
	tabelaDiv.innerHTML = "";

	let table = document.createElement("table");
	table.setAttribute("class", "table table-bordered table-striped table-hover");
	let header = tableHeader("Posiljalac", "Primalac", "Tezina", "Postarina", "Kurir", "NOBUTTON");
	table.append(header);

	table.append(tableBody(data));

	tabelaDiv.appendChild(table);
}

function showStanje() {
	let requestUrl = baseUrl + stanjeEndpoint;
	let headers = {};
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;
	}
	fetch(requestUrl, { headers: headers })
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setStanje);
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));
}

function setStanje(data) {
	let tabelaDiv = document.getElementById("stanjeTabela");
	let grafikonDiv = document.getElementById("stanjeGrafikon");
	tabelaDiv.innerHTML = "";
	grafikonDiv.innerHTML = "";

	let table = document.createElement("table");
	table.setAttribute("class", "table table-bordered table-striped table-hover");
	let header = tableHeader("ID", "Kurir", "Broj Paketa", "NOBUTTON");
	table.append(header);

	table.append(tableBody(data));

	tabelaDiv.appendChild(table);

	setD3Chart(data);
}

function setD3Chart(data) {
	let graphMax = Math.floor(Math.max(...data.map(d=>d.brojPaketa)));
	let margin = { top: 30, right: 30, bottom: 70, left: 60 },
		width = 360 - margin.left - margin.right,
		height = 300 - margin.top - margin.bottom;
	
	// append the svg object to the body of the page
	let svg = d3.select("#stanjeGrafikon")
		.append("svg")
		.attr("width", width + margin.left + margin.right)
		.attr("height", height + margin.top + margin.bottom)
		.append("g")
		.attr("transform",
			"translate(" + margin.left + "," + margin.top + ")");

	// X axis
	let x = d3.scaleBand()
		.range([0, width])
		.domain(data.map(function (d) { return d.kurirIme; }))
		.padding(0.2);
	svg.append("g")
		.attr("transform", "translate(0," + height + ")")
		.call(d3.axisBottom(x))
		.selectAll("text")
		.attr("transform", "translate(-10,0)rotate(-45)")
		.style("text-anchor", "end");

	// Add Y axis
	let y = d3.scaleLinear()
		.domain([0, graphMax])
		.range([height, 0]);
	svg.append("g")
		.call(d3.axisLeft(y));
		
		// Bars
	svg.selectAll("mybar")
		.data(data)
		.enter()
		.append("rect")
		.attr("x", function (d) { return x(d.kurirIme); })
		.attr("y", function (d) { return y(d.brojPaketa); })
		.attr("width", x.bandwidth())
		.attr("height", function (d) { return height - y(d.brojPaketa); })
		.attr("fill", "#4f1670")
		.append('title')
		.text((d) => `${d.kurirIme} - ukupno paketa: ${d.brojPaketa}`);
}

function showKuriri() {
	let requestUrl = baseUrl + kuririEndpoint;
	let headers = {};
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;
	}
	axios.get(requestUrl, { headers: headers })
		.then(function (response) {
			// handle success
			setKuriri(response.data);
		}).catch (function (error) {
			// handle error
			console.log(`Error occured with code ${error.response.status}`);
			console.log(error);
			showError();
		})
}

function setKuriri(data) {
	let tabelaDiv = document.getElementById("kuririTabela");
	tabelaDiv.innerHTML = "";

	let table = document.createElement("table");
	table.setAttribute("class", "table table-bordered table-striped table-hover");
	let header = tableHeader("Ime", "Rodjenje");
	table.append(header);

	table.append(tableBody(data, editKurir, deleteKurir));

	tabelaDiv.appendChild(table);
}

function submitKurirForm() {
	let kurirNaziv = document.getElementById("kurirNaziv").value;
	let rodjenje = document.getElementById("kurirRodjenje").value;
	let httpAction;
	let sendData;
	let url;

	if (formAction === "Create") {
		httpAction = "POST";
		url = baseUrl + kuririEndpoint;
		sendData = {
			"ime": kurirNaziv,
			"rodjenje": parseInt(rodjenje)
		};
	}
	else {
		httpAction = "PUT";
		url = baseUrl + kuririEndpoint + editingId.toString();
		sendData = {
			"id": editingId,
			"ime": kurirNaziv,
			"rodjenje": parseInt(rodjenje)
		};
	}

	let headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;
		console.log(headers, jwt_token)
	}
  if(validateKurirForm(kurirNaziv, rodjenje)) {
    fetch(url, { method: httpAction, headers: headers, body: JSON.stringify(sendData) })
      .then((response) => {
        if (response.status === 200 || response.status === 201) {
          console.log("Record submitted successfully");
          formAction = "Create";
          document.getElementById("izmeniDodajKurir").innerHTML = "Dodaj";
          cancelKurirForm();
        } else {
          console.log("Error occured with code " + response.status);
          alert("Desila se greska!");
        }
      })
      .catch(error => console.log(error));
    return false;
  }
}

function editKurir() {
	let editId = this.name;

	let url = baseUrl + kuririEndpoint + editId.toString();
	let headers = { };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;	
	}
	fetch(url, { headers: headers})
		.then((response) => {
			if (response.status === 200) {
				console.log("Record obtained successfully for editing");
				response.json().then(data => {
					document.getElementById("kurirNaziv").value = data.ime;
					document.getElementById("kurirRodjenje").value = data.rodjenje;
          document.getElementById("izmeniDodajKurir").innerHTML = "Izmeni";
					
					editingId = data.id;
					formAction = "Update";
					document.getElementById("profile-tab").click();
				});
			} else {
				formAction = "Create";
				console.log("Error occured with code " + response.status);
				alert("Desila se greska!");
			}
		})
		.catch(error => console.log(error));
}

function deleteKurir() {
	let deleteID = this.name;
	let url = baseUrl + kuririEndpoint + deleteID.toString();
	let headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;
	}
	fetch(url, { method: "DELETE", headers: headers})
		.then((response) => {
			if (response.status === 200 || response.status === 204) {
				console.log("Record deleted successfully");
				loadPaketi();
				showKuriri();
			} else {
				console.log("Error occured with code " + response.status);
				alert("Desila se greska!");
			}
		})
		.catch(error => console.log(error));
}

function cancelForm() {
	document.getElementById("posiljalac").value = null;
	document.getElementById("primalac").value = null;
	document.getElementById("tezina").value = null;
	document.getElementById("postarina").value = null;
	document.getElementById("izmeniDodaj").innerHTML = "Dodaj";
  formAction = "Create";

	loadPaketi();
	loadKuriri();
	showKuriri();
}

function cancelKurirForm() {
	document.getElementById("kurirNaziv").value = null;
	document.getElementById("kurirRodjenje").value = null;
	document.getElementById("izmeniDodajKurir").innerHTML = "Dodaj";
  formAction = "Create";
	document.getElementById("home-tab").click();

	loadPaketi();
	loadKuriri();
	showKuriri();
}

function loadLoggedInPage() {
	loadPaketi();
  loadKuriri();
	showKuriri();
}