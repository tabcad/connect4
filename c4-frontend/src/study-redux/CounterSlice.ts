import { createSlice } from "@reduxjs/toolkit"
import type { PayloadAction } from "@reduxjs/toolkit"
import { RootState, AppThunk } from "../Redux/store"

//define type for state
export interface CounterState {
  value: number
  status: 'idle' | 'loading' | 'failed'
}

//define initial value for state
const initialState: CounterState = {
  value: 0,
  status: 'idle'
}

//counterSlices contain the reducer logic
export const counterSlice = createSlice({
  name: 'counter',
  initialState,
  reducers: {
    increment: state => {   //redux toolkit allows writing "mutating" logic in reducer but mutation is not occurring
      state.value += 1      //a library called Immer does the work for changing the state immutably
    },
    decrement: state => {
      state.value -=1
    },
    incrementByAmount: (state, action: PayloadAction<number>) => { 
            // using the PayloadAction type to declare the contents of 'action.payload'
      state.value += action.payload
    },
  },
})

//export generated action creators for use in components
export const {increment, decrement, incrementByAmount} = counterSlice.actions
//export reducer for use in store config
export default counterSlice.reducer

//selector functions let you select a value from the redux root state
export const selectCount = (state: RootState) => state.counter.value
export const selectStatus = (state: RootState) => state.counter.status

export const incrementIfOdd = (amount: number): AppThunk => {
  return (dispatch, getState) => {
    const currValue = selectCount(getState())
    if (currValue % 2 === 1) {
      dispatch(incrementByAmount(amount))
    }
  }
}