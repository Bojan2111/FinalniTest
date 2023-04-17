import React, { useState } from "react";

const KuririTelo = () => {
  const [logged, setLogged] = useState(true);

  return (
    <tbody>
      <tr>
        <td>nesto 1a</td>
        <td>nesto 2a</td>
        <td>nesto 3a</td>
        <td>nesto 4a</td>
        {logged && (
          <td>
            <button>Delete</button>
          </td>
        )}
      </tr>
      <tr>
        <td>nesto 1b</td>
        <td>nesto 2b</td>
        <td>nesto 3b</td>
        <td>nesto 4b</td>
        {logged && (
          <td>
            <button>Delete</button>
          </td>
        )}
      </tr>
      <tr>
        <td>nesto 1c</td>
        <td>nesto 2c</td>
        <td>nesto 3c</td>
        <td>nesto 4c</td>
        {logged && (
          <td>
            <button>Delete</button>
          </td>
        )}
      </tr>
    </tbody>
  );
};

export default KuririTelo;
