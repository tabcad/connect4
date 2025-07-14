import {
  AccountDetails,
  JoinRoomInviteResponse,
  LoginDetails,
  Move,
  NewRoomRequest,
  RoomResponse,
} from "./types";

/**
 * create player vs bot room
 * @param playerOne string id 
 * @returns single player room vs a bot
 */
export async function createSinglePlayerGame({
  playerOne,
}: NewRoomRequest): Promise<RoomResponse> {
  const url = "https://localhost:7018/api/room/create";
  const response = await fetch(url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      playerOne: playerOne,
      isSinglePlayer: true,
    }),
  });

  if (!response.ok) {
    const fallbackError = "Error creating room";

    let data;
    try {
      data = await response.json();
    } catch {
      throw new Error(fallbackError);
    }

    const errorMsg = data?.detail ?? fallbackError;
    throw new Error(errorMsg);
  }

  return await response.json();
}

/**
 * create 2 player room
 * @param playerOne string id
 * @param isSinglePlayer false
 * @returns link to send to playerTwo
 */
export async function createTwoPlayerRoom({
  playerOne,
}: NewRoomRequest): Promise<JoinRoomInviteResponse> {
  const url = "https://localhost:7018/api/create";
  const response = await fetch(url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      playerOne: playerOne,
      isSinglePlayer: false,
    }),
  });

  if (!response.ok) {
    const fallbackError = "Error creating room";

    let data;
    try {
      data = await response.json();
    } catch {
      throw new Error(fallbackError);
    }

    const errorMsg = data?.detail ?? fallbackError;
    throw new Error(errorMsg);
  }

  return await response.json();
}

/**
 * get room by its id
 * @param roomId string
 * @returns details of room
 */
export async function getRoomById(roomId: string): Promise<RoomResponse> {
  const url = `https://localhost:7018/api/room/${roomId}`;
  const response = await fetch(url, { method: "GET" });

  if (!response.ok) {
    const fallbackError = "Error fetching room";

    let data;
    try {
      data = await response.json();
    } catch {
      throw new Error(fallbackError);
    }

    const errorMsg = data?.detail ?? fallbackError;
    throw new Error(errorMsg);
  }

  return await response.json();
}

/**
 * get list of rooms a user is in
 * @param roomId
 * @returns
 */
export async function getUsersRooms(roomId: string) {
  const url = `https://localhost:7018/api/user/rooms/${roomId}`;
  const response = await fetch(url, { method: "GET" });

  if (!response.ok) {
    const fallbackError = "Error fetching room list";

    let data;
    try {
      data = await response.json();
    } catch {
      throw new Error(fallbackError);
    }

    const errorMsg = data?.detail ?? fallbackError;
    throw new Error(errorMsg);
  }

  return await response.json();
}

/**
 * get game by its id
 * @param gameId string
 * @returns details of game
 */
export async function getGameById(gameId: string) {
  const url = `https://localhost:7018/api/game/${gameId}`;
  const response = await fetch(url, { method: "GET" });

  if (!response.ok) {
    const fallbackError = "Error fetching game";

    let data;
    try {
      data = await response.json();
    } catch {
      throw new Error(fallbackError);
    }

    const errorMsg = data?.detail ?? fallbackError;
    throw new Error(errorMsg);
  }

  return await response.json();
}

/**
 * players turn in a game
 * @param playerId string 
 * @param game string id
 * @param col number for column choice  
 * @returns game details
 */
export async function playersTurn({ player, game, col }: Move) {
  const url = "https://localhost:7018/api/game/move";
  const response = await fetch(url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      playerId: player,
      gameId: game,
      col: col,
    }),
  });

  if (!response.ok) {
    const fallbackError = "Error making move";

    let data;
    try {
      data = await response.json();
    } catch {
      throw new Error(fallbackError);
    }

    const errorMsg = data?.detail ?? fallbackError;
    throw new Error(errorMsg);
  }

  return await response.json();
}

/**
 * bots move in a game
 * @param gameId string  
 * @returns game details
 */
export async function botMove(gameId: string) {
  const url = `https://localhost:7018/api/game/bot/${gameId}`;
  const response = await fetch(url, {
    method: "POST",
  });

  if (!response.ok) {
    const fallbackError = "Error making move";

    let data;
    try {
      data = await response.json();
    } catch {
      throw new Error(fallbackError);
    }

    const errorMsg = data?.detail ?? fallbackError;
    throw new Error(errorMsg);
  }

  return await response.json();
}

/**
 * restart current game
 * @param gameId string id of game
 * @returns return same game but reset
 */
export async function restartGame(gameId: string) {
  const url = `https://localhost:7018/api/game/restart/${gameId}`;
  const response = await fetch(url, {
    method: "POST",
  });

  if (!response.ok) {
    const fallbackError = "Error restarting game";

    let data;
    try {
      data = await response.json();
    } catch {
      throw new Error(fallbackError);
    }

    const errorMsg = data?.detail ?? fallbackError;
    throw new Error(errorMsg);
  }

  return await response.json();
}

/**
 * get player by their id
 * @param playerId string
 * @returns details of player
 */
export async function getPlayerById(playerId: string) {
  const url = `https://localhost:7018/api/user/${playerId}`;
  const response = await fetch(url, { method: "GET" });

  if (!response.ok) {
    const fallbackError = "Error fetching game";

    let data;
    try {
      data = await response.json();
    } catch {
      throw new Error(fallbackError);
    }

    const errorMsg = data?.detail ?? fallbackError;
    throw new Error(errorMsg);
  }

  return await response.json();
}

/**
 * create new account && log in
 * @param details username and password
 * @returns token to log in with
 */
export async function postSignup(details: AccountDetails) {
  const url = "https://localhost:7018/api/user/signup";
  const response = await fetch(url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      username: details.username,
      password: details.password,
    }),
  });

  if (!response.ok) {
    const fallbackError = "Error creating account";

    let data;
    try {
      data = await response.json();
    } catch {
      throw new Error(fallbackError);
    }

    const errorMsg = data?.detail ?? fallbackError;
    throw new Error(errorMsg);
  }

  return await response.json();
}

// Error: response status is 500

// Response body
// Download
// {
//   "error": "An unexpected error has occurred",
//   "details": "Failed to create user: Passwords must have at least one non alphanumeric character., Passwords must have at least one digit ('0'-'9')., Passwords must have at least one uppercase ('A'-'Z').",
//   "type": "InvalidOperationException"
// }


/**
 * log in to account
 * @param details username and password
 * @returns token to log in with
 */
export async function postLogin(details: LoginDetails) {
  const url = "https://localhost:7018/api/user/login";
  const response = await fetch(url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      username: details.username,
      password: details.password,
    }),
  });

  if (!response.ok) {
    const fallbackError = "Error logging in";

    let data;
    try {
      data = await response.json();
    } catch {
      throw new Error(fallbackError);
    }

    const errorMsg = data?.detail ?? fallbackError;
    throw new Error(errorMsg);
  }

  return await response.json();
}