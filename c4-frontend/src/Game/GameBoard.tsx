import styled from "@emotion/styled";
import { useMutation } from "@tanstack/react-query";
import { GamePiece } from "./GamePiece";
import { Game } from "../types";
import { botMove, playersTurn, restartGame } from "../api";

export const GameBoard: React.FC<{
  gameState: Game;
  refetchGame: () => void;
}> = ({ gameState, refetchGame }) => {
  const delay = (ms: number) =>
    new Promise((resolve) => setTimeout(resolve, ms));

  const makeMoveMutation = useMutation({
    mutationFn: (column: number) =>
      playersTurn({
        col: column,
        game: gameState!.gameId,
        player: gameState!.currentTurnId,
      }),
    onSuccess: async () => {
      refetchGame();

      // const isSinglePlayerGame = gameState?.isSinglePlayer;
      // const isWinner = gameState?.winnerId !== null;

      await delay(1000);
      await botMove(gameState!.gameId);
      refetchGame();
    },
  });

  const handleMove = (column: number) => {
    if (!gameState) return;
    makeMoveMutation.mutate(column);
  };

  return (
    <>
      {gameState?.board && (
        <BoardWrapper>
          <BoardStyled>
            {gameState!.board.map((row, rowIdx) =>
              row.map((cell, columnIdx) => (
                <Cell
                  onClick={() => handleMove(columnIdx)}
                  key={`${rowIdx}-${columnIdx}`}
                >
                  {cell === gameState.playerOneId ? (
                    <GamePiece color="blue" />
                  ) : cell === gameState.playerTwoId ? (
                    <GamePiece color="green" />
                  ) : null}
                </Cell>
              ))
            )}
          </BoardStyled>
        </BoardWrapper>
      )}
    </>
  );
};

const BoardWrapper = styled.div({
  position: "relative",
  width: "700px",
  height: "600px",
  margin: "auto",
});

const BoardStyled = styled.div({
  backgroundColor: "transparent",
  width: "700px",
  height: "600px",
  display: "grid",
  gridTemplateColumns: "repeat(7, 1fr)",
  borderRadius: "5px",
  zIndex: 50,
  position: "relative",
  border: "1px solid",
});

const Cell = styled.div({
  width: "90px",
  height: "90px",
  borderRadius: "50%",
  backgroundColor: "transparent",
  margin: "auto",
  border: "1px solid",
});
