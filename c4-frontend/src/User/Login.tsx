import React, { useState } from "react";
import { InputField } from "../Components/InputField";
import { Button } from "../Components/Button";
import { LoginDetails, postLogin } from "./Helpers/api";
import { AppDispatch } from "../Redux/store";
import { loginSuccess } from "../Redux/authSlice";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import styled from "@emotion/styled";

export function Login() {
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();

  const [details, setDetails] = useState<LoginDetails>({
    username: "",
    password: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setDetails((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const response = await postLogin(details);

    localStorage.setItem("token", response.token);

    const user = {
      id: response.id,
      username: response.username,
    };

    dispatch(loginSuccess(user));
    navigate("/");
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
      <Button>Log In</Button>
    </FormStyled>
  );
}

const FormStyled = styled.form({
  width: "300px",
  height: "300px",
  margin: "100px auto",
  fontFamily: "Inter, sans-serif",
});
