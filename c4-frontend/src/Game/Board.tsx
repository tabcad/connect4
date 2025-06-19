import styled from "@emotion/styled";
import { useEffect, useState } from "react";
import { getGameById, playersTurn, restartGame } from "./Helpers/api";
import { useNavigate, useParams } from "react-router-dom";
import { Game } from "./Helpers/types";
import { GamePiece, PlayerColor } from "./GamePiece";
import { Turn } from "./Turn";

interface Player {
  id: string;
  color: PlayerColor;
  name: string;
}

export const Board = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  if (!id) {
    navigate("/");
  }

  const [game, setGame] = useState<Game>();

  const playerOne: Player = {
    id: "cfc9fe43-e8de-4443-aa74-c360287a0013",
    color: "blue",
    name: "Nina",
  };
  const playerTwo: Player = {
    id: "c1045673-b78d-4b3f-b2b8-385736f1a6ae",
    color: "purple",
    name: "May",
  };

  const currentTurn =
    game?.currentTurn === playerOne.id ? playerOne : playerTwo;

  const fetchGame = async () => {
    const game = await getGameById(id!);
    setGame(game);
    console.log(game);
  };

  useEffect(() => {
    fetchGame();
  }, [id]);

  const makeMove = async (column: number) => {
    console.log(column);
    const response = await playersTurn({
      col: column,
      game: id!,
      player: currentTurn.id,
    });
    setGame(response);
  };

  const handleRestart = async () => {
    const response = await restartGame(id!);
    setGame(response);
  };

  return (
    <>
      {game && (
        <BoardWrapper>
          <Turn makeMove={makeMove} playerColor={currentTurn.color} />
          <GameBoard>
            {game.board.map((row, rowIdx) =>
              row.map((cell, columnIdx) => (
                <Cell
                  onClick={() => makeMove(columnIdx)}
                  key={`${rowIdx}-${columnIdx}`}
                >
                  {cell === playerOne.id ? (
                    <GamePiece color={playerOne.color} />
                  ) : cell === playerTwo.id ? (
                    <GamePiece color={playerTwo.color} />
                  ) : null}
                </Cell>
              ))
            )}
          </GameBoard>
          <button onClick={handleRestart}>restart game</button>
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

const GameBoard = styled.div({
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
