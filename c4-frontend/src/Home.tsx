import { useDispatch, useSelector } from "react-redux";
import { RootState } from "./Redux/store";
import { useNavigate } from "react-router-dom";
import { createGame } from "./Game/Helpers/api";
import { setGame } from "./Redux/gameSlice";

export const Home = () => {
  const user = useSelector((state: RootState) => state.auth.user);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  //test data
  const playerOne = "e38c932f-d30c-441a-a39f-f5246f9e7139";
  const playerTwo = "4b3a41fa-8a11-4d80-ae24-0c0cfbe21996";

  const handleNewGame = async () => {
    const newGame = await createGame({ playerOne, playerTwo });
    dispatch(setGame({
      gameId: newGame.gameId,
      currentTurn: newGame.currentTurn,
      board: newGame.gameBoard
    }))
    navigate(`/game/${newGame.gameId}`);
  };

  return (
    <div>
      {user && <h1>Hey, {user.username}</h1>}
      <button onClick={handleNewGame}>New Connect4 Game</button>
    </div>
  );
};
