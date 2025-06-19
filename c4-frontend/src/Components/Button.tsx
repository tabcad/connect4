import React, { ReactNode } from "react";
import styled from "@emotion/styled";

interface ButtonProps {
  children: ReactNode;
}

export const Button = ({ children }: ButtonProps) => {
  return <ButtonStyled type="submit">{children}</ButtonStyled>;
};

const ButtonStyled = styled.button({
  fontSize: '18px',
  padding: '4px 6px',
  margin: '40px auto 0',
})
