import styled from "@emotion/styled";

interface InputFieldProps {
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  name: string;
  id: string;
  type: "text" | "password";
  label: string;
}

export const InputField = ({
  onChange,
  name,
  id,
  type,
  label,
}: InputFieldProps) => {
  return (
    <InputWrapper>
      <label htmlFor={id}>{label}</label>
      <InputStyled type={type} name={name} onChange={onChange} />
    </InputWrapper>
  );
};

//styles
const InputWrapper = styled.div({
  display: "flex",
  justifyContent: "space-between",
  marginTop: '20px'
});

const InputStyled = styled.input({
  padding: "10px",
});
