import { ReactNode } from "react";
import styled from "@emotion/styled";
import { colors } from "../assets/colors";
import { Link, useNavigate } from "react-router-dom";

type Color = "yellow" | "blue" | "purple" | "green" | "red";

export const Header: React.FC<{
  children: ReactNode;
  bgColor: Color;
  fontSize?: string;
  canClose?: boolean;
}> = ({ children, bgColor, fontSize, canClose = true }) => {
  const navigate = useNavigate()
  return (
    <HeaderBar bgColor={bgColor} fontSize={fontSize}>
      <h1>{children}</h1>
      {canClose && <h2 onClick={() => navigate('/')}>x</h2>}
    </HeaderBar>
  );
};

const HeaderBar = styled.div<{ bgColor: Color; fontSize?: string }>(
  ({ bgColor, fontSize }) => ({
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
    width: "100%",
    letterSpacing: '2px',
    background: `${colors[bgColor].main}`,
    boxShadow: `inset 3px -5px 6px ${colors[bgColor].dark}, 
        inset -2px 2px 4px ${colors[bgColor].light}`,
    padding: "8px 10px 10px 18px",

    "> h1": {
      background: "none",
      color: "white",
    },
    "> h2": {
      background: "none",
      border: "none",
      fontSize: "36px",
      color: "white",
      fontWeight: 100,
      cursor: 'pointer',
    },
  })
);