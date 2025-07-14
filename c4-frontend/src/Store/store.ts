import { configureStore, ThunkAction, Action, combineReducers } from "@reduxjs/toolkit";
import { persistStore, persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";
import authReducer from "./authSlice";
import gameReducer from "./gameSlice";
import roomReducer from "./roomSlice";

const persistConfig = {
  key: "root",
  storage,
};

const combinedReducer = {
  auth: authReducer,
  game: gameReducer,
  room: roomReducer,
};

const persistedReducer = persistReducer(
  persistConfig,
  combineReducers(combinedReducer)
);

export const store = configureStore({
  reducer: persistedReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: false,
    }),
});

export const persistor = persistStore(store);

export type AppStore = typeof store;
export type RootState = ReturnType<AppStore["getState"]>;
export type AppDispatch = AppStore["dispatch"];
export type AppThunk<ThunkReturnType = void> = ThunkAction<
  ThunkReturnType,
  RootState,
  unknown,
  Action
>;
