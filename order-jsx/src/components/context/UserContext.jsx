// UserContext.jsx

import React, { createContext, useEffect } from "react";

export const UserContext = createContext();

export const UserProvider = ({ children }) => {

  const [user, setUser] = React.useState(null);

  useEffect(() => {
    let u = null;
    
    try {
      u = JSON.parse(localStorage.getItem("user"));
    } catch (error) {
      console.error(error);
    }

    if (u) {
      setUser(u);
    }
  }, []);
  
  const login = (user) => {
    localStorage.setItem("user", JSON.stringify(user));
    setUser(user);
  };

  const logout = () => {
    localStorage.removeItem("user");
    setUser(null);
  };

  return (
    <UserContext.Provider value={{ login, logout, user }}>
      {children}
    </UserContext.Provider>
  );
};

export const useUser = () => {
  const context = React.useContext(UserContext);
  if (context === undefined) {
    throw new Error("useUser must be used within a UserProvider");
  }
  return context;
};
