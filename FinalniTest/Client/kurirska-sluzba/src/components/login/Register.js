import React from "react";
import "../style.css";
const Register = (props) => {
  return (
    <div className="col-6">
      <h2 className="text-center">Registracija novog korisnika</h2>
      <form method="post" id="registrationForm">
        <table className="formular">
          <tr>
            <th>
              <label for="usernameRegister">Korisnicko Ime:</label>
            </th>
            <td>
              <input
                className="form-control"
                type="text"
                name="usernameRegister"
                id="usernameRegister"
              />
              <span
                style={{ fontSize: 10 + "px", color: "red" }}
                id="unameCheckInfo"
              ></span>
            </td>
            <td id="unameCheck"></td>
          </tr>
          <tr>
            <th>
              <label for="emailRegister">Email:</label>
            </th>
            <td>
              <input
                className="form-control"
                type="email"
                name="emailRegister"
                id="emailRegister"
              />
              <span
                style={{ fontSize: 10 + "px", color: "red" }}
                id="emailCheckInfo"
              ></span>
            </td>
            <td id="emailCheck"></td>
          </tr>
          <tr>
            <th>
              <label for="passwordRegister">Lozinka:</label>
            </th>
            <td>
              <input
                className="form-control"
                type="password"
                name="passwordRegister"
                id="passwordRegister"
              />
              <span
                style={{ fontSize: 10 + "px", color: "red" }}
                id="pwordCheckInfo"
              ></span>
            </td>
            <td id="pwordCheck"></td>
          </tr>
          <tr>
            <th>
              <label for="confirmPasswordRegister">Ponovi lozinku:</label>
            </th>
            <td>
              <input
                className="form-control"
                type="password"
                name="confirmPasswordRegister"
                id="confirmPasswordRegister"
              />
              <span
                style={{ fontSize: 10 + "px", color: "red" }}
                id="pwordConfCheckInfo"
              ></span>
            </td>
            <td id="pwordConfCheck"></td>
          </tr>
          <tr>
            <td>
              <input
                type="submit"
                value="Registruj se"
                className="form-control btn btn-primary"
                onClick="registerUser()"
              />
            </td>
            <td className="d-flex justify-content-end">
              <button
                className="btn btn-danger"
                onClick={props.closeRegistration}
              >
                Zatvori
              </button>
            </td>
          </tr>
        </table>
      </form>
    </div>
  );
};

export default Register;
