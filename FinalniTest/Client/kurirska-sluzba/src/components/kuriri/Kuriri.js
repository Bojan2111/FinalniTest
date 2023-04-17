import React from "react";
import KuririZaglavlje from "./KuririZaglavlje";
import KuririTelo from "./KuririTelo";

const Kuriri = (props) => {
  return (
    <div>
      <table border={2}>
        <KuririZaglavlje />
        <KuririTelo />
      </table>
    </div>
  );
};

export default Kuriri;
