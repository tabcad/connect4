import React, { ReactNode } from "react";
import styled from "@emotion/styled";
import { colors } from "../assets/colors";

export const Button: React.FC<{
  children: ReactNode;
  onClick: () => void;
  disabled: boolean;
}> = ({children, disabled = true, onClick}) => {
  return (
    <ButtonStyled disabled={disabled} onClick={onClick}>{children}</ButtonStyled>
  )
};

const ButtonStyled = styled.button({
  width: "120px",
  height: "35px",
  fontSize: "28px",
  background: "#bdcad5",
  border: "2px solid black",
  boxShadow: `inset 3px -5px 2px ${colors.neutral.main}, inset -2px 5px 2px ${colors.neutral.light}`,
});
