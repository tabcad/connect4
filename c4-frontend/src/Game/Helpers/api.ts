import { Move, NewGameRequest } from "./types";

export async function createGame({ playerOne, playerTwo }: NewGameRequest) {
  const url = "https://localhost:7018/api/game";
  const response = await fetch(url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      playerOne: playerOne,
      playerTwo: playerTwo,
    }),
  });

  if (!response.ok) {
    const fallbackError = "Error creating game";

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
