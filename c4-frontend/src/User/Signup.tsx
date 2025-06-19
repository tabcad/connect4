import React, { useState } from "react";
import styled from "@emotion/styled";
import { AccountDetails, postSignup } from "./Helpers/api";
import { InputField } from "../Components/InputField";
import { Button } from "../Components/Button";
import { useDispatch } from "react-redux";
import { AppDispatch } from "../Redux/store";
import { loginSuccess } from "../Redux/authSlice";
import { useNavigate } from "react-router-dom";

export function Signup() {
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();

  const [details, setDetails] = useState<AccountDetails>({
    username: "",
    password: "",
  });

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const response = await postSignup(details);
    localStorage.setItem("token", response.token);

    const user = {
      id: response.id,
      username: response.username,
    };

    dispatch(loginSuccess(user));
    navigate("/");
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setDetails((prev) => ({
      ...prev,
      [name]: value,
    }));
  };
  return (
    <FormStyled onSubmit={handleSubmit}>
      <InputField
        name="username"
        label="Username"
        id="username"
        type="text"
        onChange={handleChange}
      />
      <InputField
        name="password"
        label="Password"
        id="password"
        type="password"
        onChange={handleChange}
      />
      <Button>Sign Up</Button>
    </FormStyled>
  );
}

const FormStyled = styled.form({
  width: "300px",
  height: "300px",
  margin: "100px auto",
  fontFamily: "Inter, sans-serif",
});
