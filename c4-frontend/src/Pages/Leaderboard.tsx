import styled from "@emotion/styled";
import { Header } from "../Components/Header";

export const Leaderboard = () => {
  return (
    <LeaderboardWrapper>
      <Header bgColor="yellow">LEADERBOARD</Header>
      <p>caitlin is the best</p>
    </LeaderboardWrapper>
  );
};

const LeaderboardWrapper = styled.div({
  width: "600px",
  height: "600px",
  fontSize: "22px",
  border: "2px solid black",
  borderRadius: "1px",
  boxShadow: "-2px 2px 1px 0 black",

  p: {
    textAlign: "center",
  },
});
