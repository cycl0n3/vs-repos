import React from "react";
import ReactDOM from "react-dom/client";

import App from "./App.jsx";

import 'semantic-ui-css/semantic.min.css';

import "./index.css";

import { UserProvider } from "./components/context/UserContext";

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <UserProvider>
      <App />
    </UserProvider>
  </React.StrictMode>
);
