import styled from "@emotion/styled";
import { colors } from "../assets/colors";

interface InputFieldProps {
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  value: string;
  id: string;
  type: "text" | "password";
  label: string;
}

export const InputField = ({
  onChange,
  value,
  id,
  type,
  label,
}: InputFieldProps) => {
  return (
    <InputStyled>
      <label htmlFor={id}>{label}</label>
      <input type={type} value={value} onChange={onChange} />
    </InputStyled>
  );
};

const InputStyled = styled.div({
  label: {},
  input: {
    padding: "5px",
    width: "180px",
    fontSize: "18px",
    border: "2px solid black",
    borderRadius: "1px",
    boxShadow: `inset -5px 5px 3px ${colors.neutral.main}`,
    outline: "none",
  },
});
