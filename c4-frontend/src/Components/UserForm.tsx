import { useMutation } from "@tanstack/react-query";
import { useState } from "react";
import { useDispatch } from "react-redux";
import styled from "@emotion/styled";
import { postLogin, postSignup } from "../api";
import { loginSuccess } from "../Store/authSlice";
import { AppDispatch } from "../Store/store";
import { Header } from "./Header";
import { useNavigate } from "react-router-dom";
import { Button } from "./Button";
import { InputField } from "./InputField";

export const UserForm = () => {
  const [isLoggingIn, setIsLoggingIn] = useState(true);
  const navigate = useNavigate();
  const dispatch = useDispatch<AppDispatch>();

  const fn = isLoggingIn ? postLogin : postSignup;

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const { mutate } = useMutation({
    mutationFn: () =>
      fn({
        username: username,
        password: password,
      }),
    onSuccess: (user) => {
      dispatch(loginSuccess(user));
      navigate("/");
    },
    onError: (error) => {
      console.error("Problem signing in", error);
    },
  });

  if (isLoggingIn) {
    return (
      <FormWrapper>
        <Header bgColor="yellow">WELCOME BACK</Header>

        <InputField
          onChange={(e) => setUsername(e.target.value)}
          value={username}
          id="username"
          label="USERNAME"
          type="text"
        />
        <InputField
          onChange={(e) => setPassword(e.target.value)}
          value={password}
          id="password"
          label="PASSWORD"
          type="password"
        />
        <Button
          disabled={username === "" && password === ""}
          onClick={() => mutate()}
        >
          OK
        </Button>
        <p>
          Don't have an account?{" "}
          <span onClick={() => setIsLoggingIn(false)}>Sign up here</span>
        </p>
      </FormWrapper>
    );
  }

  return (
    <FormWrapper>
      <Header bgColor="blue">CREATE ACCOUNT</Header>
      <InputField
        onChange={(e) => setUsername(e.target.value)}
        value={username}
        id="username"
        label="USERNAME"
        type="text"
      />
      <InputField
        onChange={(e) => setPassword(e.target.value)}
        value={password}
        id="password"
        label="PASSWORD"
        type="password"
      />
      <Button
        disabled={username === "" && password === ""}
        onClick={() => mutate()}
      >
        OK
      </Button>
      <p>
        Already have an account?{" "}
        <span onClick={() => setIsLoggingIn(true)}>Log in here</span>
      </p>
    </FormWrapper>
  );
};

const FormWrapper = styled.div({
  width: "650px",
  height: "280px",
  fontSize: "22px",
  display: "flex",
  flexDirection: "column",
  justifyContent: "space-between",
  alignItems: "center",
  border: "2px solid black",
  borderRadius: "1px",
  boxShadow: "-2px 2px 1px 0 black",
  paddingBottom: "20px",

  div: {
    margin: "0 auto",
    width: "300px",
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
    fontSize: "18px",
  },

  "> p:last-of-type": {
    fontSize: "16px",

    span: {
      fontWeight: 600,
      color: "black",
      cursor: "pointer",
    },
  },
});
