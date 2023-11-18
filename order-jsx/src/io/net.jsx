import axios from "axios";

const API_URL = "http://localhost:5068/";

const login = async (email, password) => {
  return axios
    .post(API_URL + "Auth/login", {
        email,
        password,
      }, {
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
};

export const net = {
  login,
};
