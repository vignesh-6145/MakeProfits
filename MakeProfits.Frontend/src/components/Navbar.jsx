import React from "react";
import logo from '../assets/logo (2).png';
import './Navbar.css';
import Login from "../Login";
import { Link } from "react-router-dom";
import { Button } from "@mui/material";

export default function Navbar(){
    return(
        <div className="navbar">
            <div className="logo">
                <img className="logoimg" height="50px" src={logo} alt="" />
            </div>
            <ul className="linklist">
                <li><Link to="/">Home</Link></li>
                <li><a href="#what-we-do">Explore</a></li>
                <li><a href="#about-us">About Us</a></li>
                <li><a href="#faqs">FAQ</a></li>
                <li><Link to="/contact">Contact</Link></li>
            </ul>
            <div className="user">
      <Button
        component={Link}
        to="/login"
        variant="contained"
        sx={{
          borderRadius: '20px', // Adjust the border radius to create an oval shape
          width: '100px', // Adjust the width as needed
          height: '40px', // Adjust the height as needed
          textTransform: 'none', // Prevents text transformation
          fontSize: '16px', // Adjust the font size as needed
          fontWeight: '400', // Adjust the font weight as needed
          color: '#0096c7',
          
          backgroundColor: 'transparent', // Button background color
          '&:hover': {
            border:'#0096c7', // Button background color on hover
            color:'white',
          },
        }}
      >
        Login
      </Button>
      <Button
        component={Link}
        to="/register"
        variant="contained"
        sx={{
          borderRadius: '20px', // Adjust the border radius to create an oval shape
          width: '100px', // Adjust the width as needed
          height: '40px', // Adjust the height as needed
          marginLeft: '10px', // Adjust the margin as needed
          textTransform: 'none', // Prevents text transformation
          fontSize: '16px', // Adjust the font size as needed
          fontWeight: '400', // Adjust the font weight as needed
          color: '#0096c7',
          
          backgroundColor: 'transparent', // Button background color
          '&:hover': {
            border:'#0096c7', // Button background color on hover
            color:'white',
          },
        }}
      >
        Register
      </Button>
    </div>
        </div>
    );
}



{/*import React from "react";
import logo from '../assets/logo (2).png';
import './Navbar.css';
import Login from "../Login";
import { Link } from "react-router-dom";

export default function Navbar(){
    return(<div className="navbar">
        <div className="logo">
        <img class="logoimg" height="50px"src={logo} alt="" />

        </div>
        <ul className="linklist">
            <li>Home</li>
            <li>Learning Page</li>
            <li>About Us</li>
            <li>FAQ</li>
            <li>Contact</li>
        </ul>
        <div className="user">
            <button >
                <Link to="/login">Login</Link>
            </button>
            <button className="Register">
                <Link to="/register">Register</Link>
            </button>
            
        </div>
    </div>)
    } */}