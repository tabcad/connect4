import styled from "@emotion/styled";
import { Link, Outlet } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../Redux/store";
import { logoutSuccess } from "../Redux/authSlice";

export const Nav = () => {
  const auth = useSelector((state: RootState) => state.auth.isAuthenticated);
  const user = useSelector((state: RootState) => state.auth.user);
  const dispatch = useDispatch<AppDispatch>();

  const handleLogout = () => {
    dispatch(logoutSuccess());
  };

  return (
    <NavWrapper>
      <NavLinksWrapper>
        <LinkStyled to="/">Home</LinkStyled>
        <LinkStyled to="/stats">Leaderboard</LinkStyled>
        {auth ? (
          <button onClick={handleLogout}>Log Out</button>
        ) : (
          <>
            <LinkStyled to="/login">Login</LinkStyled>
            <LinkStyled to="/signup">Sign Up</LinkStyled>
          </>
        )}
      </NavLinksWrapper>
      <Outlet />
    </NavWrapper>
  );
};

const NavWrapper = styled.nav({
  width: "1000px",
  margin: "0 auto",
  height: "100vh",
});

const NavLinksWrapper = styled.ul({
  display: "flex",
  justifyContent: "space-evenly",
  width:'600px',
  fontFamily: "Inter, sans-serif",
  margin: '0 auto 30px',
});

const LinkStyled = styled(Link)({
  textDecoration: "none",
});
