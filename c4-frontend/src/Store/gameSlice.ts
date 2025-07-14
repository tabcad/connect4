import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface GameState {
  gameId: string | null;
  currentTurn: string | null;
  board: string[][];
}

const initialState: GameState = {
  gameId: null,
  currentTurn: null,
  board: [],
};

const gameSlice = createSlice({
  name: "game",
  initialState,
  reducers: {
    setGame(state, action: PayloadAction<GameState>) {
      return action.payload;
    },
    updateBoard(state, action: PayloadAction<string[][]>) {
      state.board = action.payload;
    },
    // more reducers to be added
  },
});

export const { setGame, updateBoard } = gameSlice.actions;
export default gameSlice.reducer;
