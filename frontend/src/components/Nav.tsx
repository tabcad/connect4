import { Link, Outlet } from 'react-router-dom'

export default function Nav() {
  return (
    <nav>
      <ul>
        <Link to="/">Home</Link>
        <Link to='/login'>Login</Link>
        <Link to="signup">Sign Up</Link>
      </ul>
      <Outlet />
    </nav>
  )
}
