import styled from "@emotion/styled";
import { Link, Outlet } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../Store/store";
import { logoutSuccess } from "../Store/authSlice";
import { Header } from "./Header";

export const Nav = () => {
  const user = useSelector((state: RootState) => state.auth.user);

  const dispatch = useDispatch<AppDispatch>();

  const handleLogout = () => {
    dispatch(logoutSuccess());
  };

  return (
    <AppWrapper>
      <NavBar>
        {user && (
          <Header canClose={false} fontSize="36px" bgColor="purple">
            Hey {user.username}
          </Header>
        )}
        <NavLinksWrapper>
          <LinkStyled to="/">Home</LinkStyled>

          {user ? (
            <>
              <LinkStyled to="/rooms">My Rooms</LinkStyled>
              <LinkStyled to="/stats">Leaderboard</LinkStyled>
              <LinkStyled to="/create">Create Room</LinkStyled>
              <LinkStyled to="/invite">Join A Room</LinkStyled>
              <LinkStyled to="/" onClick={handleLogout}>
                Log Out
              </LinkStyled>
            </>
          ) : (
            <LinkStyled to="/login">Sign In</LinkStyled>
          )}
        </NavLinksWrapper>
      </NavBar>
      <OutletWrapper>
        <Outlet />
      </OutletWrapper>
    </AppWrapper>
  );
};

const AppWrapper = styled.nav({
  display: "flex",
  height: "100vh",
});

const NavBar = styled.div({
  width: "350px",
  height: "100vh",
  border: "1px solid",
});

const NavLinksWrapper = styled.ul({
  display: "flex",
  flexDirection: "column",
  height: "600px",
  padding: "30px",
  gap: "80px",
});

const LinkStyled = styled(Link)({
  textDecoration: "none",
});

const OutletWrapper = styled.div({
  display: "flex",
  justifyContent: "center",
  alignItems: "center",
  width: "70%",
  height: "80%",
});
