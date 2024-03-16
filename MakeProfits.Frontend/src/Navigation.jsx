import { Link , useLocation } from 'react-router-dom';

const MyComponent = () => {
    const location = useLocation();

  // Hide navigation links on login and register pages
  if (location.pathname === '/' ||location.pathname==='/login' || location.pathname === '/register' || location.pathname === '/forgot-password') {
    return null;
  }
  return (
    <nav>
        <ul>
            <li><Link to="/"></Link></li>
            <li><Link to="/login">Login</Link></li>
            <li><Link to="/register">Register</Link></li>
        
      
         </ul>
    </nav>
  );
};
export default MyComponent;