import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { Provider } from "react-redux";
import { PersistGate } from "redux-persist/integration/react";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { persistor, store } from "./Store/store";
import "./index.css";
import { Nav } from "./Components/Nav";
import { SinglePlayerRoom } from "./Room/SinglePlayerRoom";
import { UserForm } from "./Components/UserForm";
import { UsersRooms } from "./Room/UsersRooms";
import { Leaderboard } from "./Pages/Leaderboard";
import { Welcome } from "./Pages/Welcome";
import { NewRoom } from "./Room/NewRoom";

const queryClient = new QueryClient();

const router = createBrowserRouter([
  {
    path: "/",
    element: <Nav />,
    children: [
      { path: "/", element: <Welcome /> },
      { path: "/login", element: <UserForm /> },
      { path: "/create", element: <NewRoom /> },
      { path: "/room/:id", element: <SinglePlayerRoom /> },
      // { path: "/room/:id", element: <TwoPlayerRoom /> },
      { path: "/rooms", element: <UsersRooms /> },
      { path: "/stats", element: <Leaderboard /> },
    ],
  },
]);

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <QueryClientProvider client={queryClient}>
          <RouterProvider router={router} />
        </QueryClientProvider>
      </PersistGate>
    </Provider>
  </React.StrictMode>
);
