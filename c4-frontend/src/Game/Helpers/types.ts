export interface NewGameRequest {
  playerOne: string;
  playerTwo: string;
}

export interface Game {
  gameId: string
  currentTurn: string
  board: string[][]
}

export type Move = {
  player: string
  game: string
  col: number
}