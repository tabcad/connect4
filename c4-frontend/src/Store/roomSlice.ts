import { createSlice, PayloadAction } from "@reduxjs/toolkit"
import { Game } from "../types"

export interface RoomState {
  roomId: string | undefined;
  gameId: string | undefined;
  player1: string | undefined;
  player2: string | undefined;
  game: Game | null;
  isActive: boolean;
  createdAt: string | undefined;
}

const initialState: RoomState = {
  roomId: '',
  gameId: '',
  player1: '',
  player2: '',
  game: null,
  isActive: false,
  createdAt: '',
};

const roomSlice = createSlice({
  name: "room",
  initialState,
  reducers: {
    setRoom(state, action: PayloadAction<RoomState>) {
      return action.payload
    }
  }
})

export const { setRoom } = roomSlice.actions;
export default roomSlice.reducer;