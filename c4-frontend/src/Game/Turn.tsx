import styled from "@emotion/styled";
import { GamePiece, PlayerColor } from "./GamePiece";

interface Props {
  playerColor: PlayerColor;
  makeMove: (column: number) => void;
}

export const Turn = ({ playerColor, makeMove }: Props) => {
  const holes = Array(7).fill("");
  return (
    <TurnWrapper>
      {holes.map((_, i) => (
        <Square key={i} onClick={() => makeMove(i)}>
          <HoverWrapper className="hover-piece">
            <GamePiece color={playerColor} />
          </HoverWrapper>
        </Square>
      ))}
    </TurnWrapper>
  );
};

const TurnWrapper = styled.span({
  display: "grid",
  gridTemplateColumns: "repeat(7, 1fr)",
  width: "700px",
});

const Square = styled.div({
  height: "130px",
  position: "relative",
  "&:hover .hover-piece": {
    opacity: 1,
    transform: "translateY(0px)",
  },
});

const HoverWrapper = styled.div({
  position: "absolute",
  top: "20px",
  opacity: 0.1,
  transition: "opacity 0.8s ease, transform 0.2s ease",
  transform: "translateY(-10px)",
  zIndex: 10,
});
