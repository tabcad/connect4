import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { Login } from "./User/Login";
import { Signup } from "./User/Signup";
import ConnectFour from "./Game/ConnectFour";
import { Nav } from "./Components/Nav";
import { Provider } from "react-redux";
import { store } from "./Redux/store";
import { Home } from "./Home";
import { StatsBoard } from "./User/StatsBoard";
import { ReduxCounter } from "./study-redux/ReduxCounter";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Nav />,
    children: [
      { path: "/", element: <Home /> },
      { path: "/login", element: <Login /> },
      { path: "/signup", element: <Signup /> },
      { path: "/stats", element: <StatsBoard /> },
      { path: "/game/:id", element: <ConnectFour /> },
      { path: "/redux", element: <ReduxCounter /> },
    ],
  },
]);

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <Provider store={store}>
      <RouterProvider router={router} />
    </Provider>
  </React.StrictMode>
);
