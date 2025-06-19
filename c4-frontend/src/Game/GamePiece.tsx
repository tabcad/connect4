import { keyframes } from "@emotion/react";
import styled from "@emotion/styled";
import { useState } from "react";

export type PlayerColor = 'red' | 'yellow' | 'blue' | 'green' | 'purple';

export interface PieceProps {
  color: PlayerColor
}

const slideDown = keyframes`
  0% {
    transform: translateY(0px);
  }
  40% {
    transform: translateY(622px);
  }
  65% {
    transform: translateY(594px);
  }
  75% {
    transform: translateY(622px);
  }
  90% {
    transform: translateY(615px);
  }
  100% {
    transform: translateY(622px);
  }
`;

export const colorThemes = {
  red: {
    outer: "#FF4D4D",
    inner: "#FF3333",
    shadow: "#B30000",
  },
  yellow: {
    outer: "#FFD700",
    inner: "#FFC300",
    shadow: "#CC9A00",
  },
  blue: {
    outer: "#4DA6FF",
    inner: "#007BFF",
    shadow: "#0056B3",
  },
  green: {
    outer: "#66E066", 
    inner: "#28A745", 
    shadow: "#1C6B2D", 
  },
  purple: {
    outer: "#B266FF", 
    inner: "#8A2BE2", 
    shadow: "#5A189A", 
  },
};

export const GamePiece = ({ color }: PieceProps) => {
  const [hasPlaced, setHasPlaced] = useState(false);
  const theme = colorThemes[color]
  
  return (
    <GamePieceStyled piece={theme} hasPlaced={hasPlaced} onClick={() => setHasPlaced(true)}>
      <span></span>
    </GamePieceStyled>
  );
};

interface ColorTheme {
  outer: string;
  inner: string;
  shadow: string;
}

const GamePieceStyled = styled.div<{ piece: ColorTheme; hasPlaced: boolean }>(
  ({ piece, hasPlaced }) => ({
    width: "90px",
    height: "90px",
    borderRadius: "50%",
    backgroundColor: piece.outer,
    position: "relative",
    border: "none",
    // ...(hasPlaced && {
    //   animation: `${slideDown} 1.5s ease-out forwards`,
    // }),

    span: {
      borderRadius: "50%",
      position: "absolute",
      top: 10,
      left: 10,
      width: "70px",
      height: "70px",
      backgroundColor: piece.inner,
      boxShadow: `inset 3px 4px 7px  ${piece.shadow}`,
    },
  })
);
