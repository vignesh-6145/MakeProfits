import React from 'react';
import logo from '../assets/logo.png';
import './Header.css'
function Header() {
  return (
    <div className='header'>
        <div className="logo">
      <img src={logo}  alt="Logo" />
        <h1>MakeProfit</h1>
        </div>
      <h1>Hello, React!</h1>
      <p>This is a default React app.</p>
    </div>
  );
}

export default Header;