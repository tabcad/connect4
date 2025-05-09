/** @jsxImportSource @emotion/react */
import { Link, Outlet } from 'react-router-dom'

export default function Nav() {
  return (
    <nav>
      <ul css={{display: 'flex', justifyContent: 'space-evenly'}}>
        <Link to="/">Home</Link>
        <Link to='/login'>Login</Link>
        <Link to="signup">Sign Up</Link>
        <Link to='connect4'>Connect4</Link>
      </ul>
      <Outlet />
    </nav>
  )
}
