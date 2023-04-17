import React, { useState } from "react";

const Login = (props) => {
  const [loginData, setLoginData] = useState({
    username: "",
    password: "",
  });

  const handleUsernameChange = (event) => {
    setLoginData((prevState) => {
      return {
        ...prevState,
        username: event.target.value,
      };
    });
  };
  const handlePasswordChange = (event) => {
    setLoginData((prevState) => {
      return {
        ...prevState,
        password: event.target.value,
      };
    });
  };

  const submitDataHandler = (event) => {
    event.preventDefault();
    setLoginData(event.target.value);
    console.log(loginData);
    setLoginData({ username: "", password: "" });
  };

  return (
    <div>
      <form onSubmit={submitDataHandler}>
        <table>
          <tr>
            <th>Korisnicko ime</th>
            <th>Lozinka</th>
          </tr>
          <tr>
            <td>
              <input
                className="form-control"
                type="text"
                onChange={handleUsernameChange}
                value={loginData.username}
              />
            </td>
            <td>
              <input
                className="form-control"
                type="password"
                onChange={handlePasswordChange}
                value={loginData.password}
              />
            </td>
          </tr>
          <tr>
            <td>
              <button className="btn btn-success" type="submit">
                Login
              </button>
            </td>
            <td>
              <button
                className="btn btn-primary"
                onClick={props.openRegistration}
              >
                Registracija
              </button>
            </td>
          </tr>
        </table>
      </form>
    </div>
  );
};

export default Login;
