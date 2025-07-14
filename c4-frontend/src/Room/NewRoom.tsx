import styled from "@emotion/styled";
import { useMutation } from "@tanstack/react-query";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { createSinglePlayerGame } from "../api";
import { Button } from "../Components/Button";
import { Header } from "../Components/Header";
import { RootState } from "../Store/store";

export const NewRoom = () => {
    const user = useSelector((state: RootState) => state.auth.user);
    const navigate = useNavigate();

    const singleRoomMut = useMutation({
      mutationFn: () => createSinglePlayerGame({ playerOne: user!.id }),

      onSuccess: (room) => {
        navigate(`/room/${room.roomId}`);
      },

      onError: (error) => {
        console.error("Failed to create room", error);
      },
    });

    const twoPlayerRoomMut = useMutation({
      mutationFn: () => createSinglePlayerGame({ playerOne: user!.id }),

      onSuccess: (room) => {
        navigate(`/room/${room.roomId}`);
      },

      onError: (error) => {
        console.error("Failed to create room", error);
      },
    });

  return (
    <NewRoomWrapper>
      <Header bgColor="green">CONNECT 4</Header>
      <Button
        onClick={() => singleRoomMut.mutate()}
        disabled={singleRoomMut.isPending}
      >
        VS bot
      </Button>
      <Button onClick={() => twoPlayerRoomMut.mutate()} disabled={twoPlayerRoomMut.isPending}>VS friend</Button>
    </NewRoomWrapper>
  );
};

const NewRoomWrapper = styled.div({
  width: "600px",
  height: "300px",
  fontSize: "22px",
  display: "flex",
  flexDirection: "column",
  justifyContent: "space-between",
  alignItems: "center",
  border: "1px solid black",
  boxShadow: "-2px 2px 1px 0 black",
  paddingBottom: "20px",
});
