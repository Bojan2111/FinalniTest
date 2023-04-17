import React, { useState } from "react";
import StartScreen from "./StartScreen";
import Kuriri from "../kuriri/Kuriri";

const Content = () => {
  const [jwtToken, setJwtToken] = useState("");

  return (
    <div>
      {jwtToken === "" && <StartScreen />}
      <Kuriri token={jwtToken} />
    </div>
  );
};

export default Content;
