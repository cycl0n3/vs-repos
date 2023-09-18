import axios from "axios";

const API_URL = "http://localhost:5068/";

const login = async (username, password) => {
  return axios
    .post(API_URL + "login", {
        username,
        password,
      }, {
        headers: {
          "Content-Type": "application/json",
        },
      }
    )
    .then((response) => {
      if (response.data) {
        localStorage.setItem("user", JSON.stringify(response.data));
      }

      return response.data;
    });
};

export const net = {
  login,
};
