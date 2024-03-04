import React from "react";
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
}