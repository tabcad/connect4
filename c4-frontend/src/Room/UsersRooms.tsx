import { useQuery } from "@tanstack/react-query";
import styled from "@emotion/styled";
import { getUsersRooms } from "../api";
import { useSelector } from "react-redux";
import { RootState } from "../Store/store";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { Header } from "../Components/Header";

type RoomSummary = {
  id: string;
  index: number;
};

export const UsersRooms = () => {
  const user = useSelector((state: RootState) => state.auth.user);
  const navigate = useNavigate();

  const {
    data: roomList,
    isLoading,
    isError,
  } = useQuery<RoomSummary[]>({
    queryKey: ["room-list", user!.id],
    queryFn: () => getUsersRooms(user!.id),
    select: (data) => data,
  });

  if (roomList?.length === 0) {
    return <p>oh no, you haven't joined any rooms yet...</p>;
  }

  return (
    <RoomsListWrapper>
      <Header bgColor="blue">MY ROOMS</Header>
      <div>
        {roomList ? (
          <ul>
            {roomList?.map((room, index) => (
              <li key={room.id} onClick={() => navigate(`/room/${room.id}`)}>
                Room {index + 1}
              </li>
            ))}
          </ul>
        ) : (
          <p>oh no, you haven't joined any rooms yet...</p>
        )}
      </div>
    </RoomsListWrapper>
  );
};

const RoomsListWrapper = styled.div({
  width: "300px",
  minHeight: "300px",
  fontSize: "22px",
  border: "2px solid black",
  borderRadius: "1px",
  boxShadow: "-2px 2px 1px 0 black",

  div: {
    padding: "10px",
  },

  ul: {
    display: "flex",
    flexDirection: "column",
    gap: "30px",
    padding: "20px 0 20px 45px",
  },

  li: {
    cursor: "pointer",
    color: "#007bff",
    ":hover": {
      color: "#0056b3",
    },
  },
});
