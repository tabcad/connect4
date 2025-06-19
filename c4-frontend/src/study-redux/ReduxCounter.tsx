/** once done
 * delete this file
 * remove from main.jsx
 */

import styled from "@emotion/styled";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../Redux/store";
import { decrement, increment } from "./CounterSlice";


export const ReduxCounter = () => {
  const count = useSelector((state: RootState) => state.counter.value)
  const dispatch = useDispatch()

  return (
    <Wrapper>
      <Row>
        <button onClick={() => dispatch(decrement())}>-</button>
        <p>{count}</p>
        <button onClick={() => dispatch(increment())}>+</button>
      </Row>
      <Row>
        <input type="number" name="" id="" />
        <button>Add Amount</button>
      </Row>
      <Row>
        <button>Add Async</button>
        <button>Add If Odd</button>
      </Row>
    </Wrapper>
  );
}

const Wrapper = styled.div({
  width: '400px',
  margin: '200px auto',
})

const Row = styled.span({
  display: 'flex',
  justifyContent: 'space-evenly',
  alignItems: 'center',

  p: {
    border: '1px solid',
    padding: '10px',
    textAlign: 'center',
    fontSize: '32px',
  },

  button: {
    width: 'fit-content',
    fontSize: '24px',
    height: '50px'
  }
})

/**
 * row 1: minus, count, plus
 * row 2: value to add, add btn
 * row 3: add async btn, add if odd btn
 */