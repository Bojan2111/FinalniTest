import React, { useState } from "react";
import Login from "../login/Login";
import Register from "../login/Register";

const StartScreen = () => {
  const [registerVisible, setRegisterVisible] = useState(false);

  const handleRegisterVisible = () => {
    setRegisterVisible(!registerVisible);
  };

  return (
    <div>
      <h3>Niste ulogovani. Popunite formu za logovanje ili se registrujte.</h3>
      {!registerVisible && <Login openRegistration={handleRegisterVisible} />}
      {registerVisible && (
        <Register closeRegistration={handleRegisterVisible} />
      )}
      <hr />
      <div>Ovde ide tabela</div>
    </div>
  );
};

export default StartScreen;
