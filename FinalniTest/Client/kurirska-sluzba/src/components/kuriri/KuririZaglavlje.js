import React, { useState } from "react";

const KuririZaglavlje = () => {
  const [logged, setLogged] = useState(true);
  return (
    <thead>
      <tr>
        <td>Svasta 1</td>
        <td>Svasta 2</td>
        <td>Svasta 3</td>
        <td>Svasta 4</td>
        {logged && <td>Akcija</td>}
      </tr>
    </thead>
  );
};

export default KuririZaglavlje;
