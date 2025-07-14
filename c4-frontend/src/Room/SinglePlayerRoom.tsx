import { useParams } from "react-router-dom";
import { getRoomById, restartGame } from "../api";
import styled from "@emotion/styled";
import { GameBoard } from "../Game/GameBoard";
import { useSelector } from "react-redux";
import { RootState } from "../Store/store";
import { useMutation, useQuery } from "@tanstack/react-query";

export const SinglePlayerRoom = () => {
  const { id } = useParams<{ id: string }>();
  const user = useSelector((state: RootState) => state.auth.user);

  const {
    data: roomState,
    refetch: refetchRoom,
    isLoading,
    isError,
  } = useQuery({
    queryKey: ["room", id],
    queryFn: () => getRoomById(id!),
    select: (data) => data,
  });

  const restartGameMutation = useMutation({
    mutationFn: () => restartGame(roomState!.game.gameId),
    onSuccess: () => refetchRoom(),
  });

  return (
    <>
      {roomState && (
        <RoomPageWrapper>
          <h1>
            {roomState.game.playerOne.username} VS.{" "}
            {roomState.game.playerTwo.username}
          </h1>
          <GameBoard refetchGame={refetchRoom} gameState={roomState.game} />
          <GameBanner>
            {roomState.game.winnerId === null ? (
              <p>it is {roomState.game.currentTurn.username}'s turn</p>
            ) : (
              <p>{roomState.game.winner.username.toUpperCase()} WINS!</p>
            )}
            <button onClick={() => restartGameMutation.mutate()}>
              restart game
            </button>
          </GameBanner>
        </RoomPageWrapper>
      )}
    </>
  );
};

const RoomPageWrapper = styled.div({
  display: "flex",
  flexDirection: "column",
  alignItems: "center",
  alignSelf: "center",
  width: "80%",

  h1: {
    fontSize: '40px',
  }
});

const GameBanner = styled.span({
  display: "flex",
  justifyContent: "center",
  alignItems: "center",
  gap: "0 50px",
  paddingTop: "15px",
  fontSize: "30px",

  button: {
    padding: "5px 10px",
    fontSize: "30px",
  },
});
