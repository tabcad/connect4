export interface NewRoomRequest {
  playerOne: string | undefined;
  playerTwo?: string;
}

export interface RoomResponse {
  roomId: string
  game: Game
  createdAt: string
  isActive: boolean
}

export interface Game {
  gameId: string;
  playerOneId: string
  playerTwoId: string
  currentTurnId: string;
  board: string[][];
  winnerId: string | null
  isSinglePlayer: boolean;
  playerOne: Player
  playerTwo: Player
  currentTurn: Player
  winner: Player
}

export interface Player {
  id: string;
  username: string;
  wins: number
  losses: number
}

export interface JoinRoomInviteResponse {
  inviteLink: string
  roomId: string
}

export interface LoginDetails {
  username: string;
  password: string;
}

export interface AccountDetails {
  username: string;
  password: string;
}

export interface ProblemResponse {
  detail: string;
  instance: string;
  exception?: string;
  status: number;
  title: string;
  type: string;
}

// not in use
export type Move = {
  player: string;
  game: string;
  col: number;
};
