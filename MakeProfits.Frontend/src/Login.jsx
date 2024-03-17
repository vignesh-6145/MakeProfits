import React, { useState } from 'react';
import logo from './assets/logo (2).png';
import { GoogleLogin } from '@react-oauth/google';
import { GoogleOAuthProvider } from '@react-oauth/google';
import './user.css';
import { jwtDecode } from 'jwt-decode';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const Login = () => {
  const [Username, setUsername] = useState('');
  const [Password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false); // State to track password visibility
  const navigate = useNavigate();
  const handleLogin = async (e) => {
    try {
      // Prevent the default form submission
      e.preventDefault();
  
      // Get the values of Username and Password (assuming they are stored in state variables)
      const username = encodeURIComponent(Username);
      const password = encodeURIComponent(Password);
  
      // Make API call to check credentials
      const url = `http://localhost:5236/api/User/Login?Username=${username}&Password=${password}`;
  
      const response = await axios.post(url, null, {
        headers: {
          'Content-Type': 'application/json',
        },
      });
      const data = response.data;
  
      console.log('Login response:', data);
  
      if (data === "not a user") {
        alert('Incorrect credentials');
      } else {
        localStorage.setItem('username', Username);

        if(data==="client")
        navigate('/user/dashboard');
      else
      navigate('/advisor/dashboard');
        
        // You can perform further actions after successful login
      }
    } catch (error) {
      console.error('Error during login:', error);
    }
  };

  const handleGoogleLogin = async (Username) => {
    try {
      const url = `http://localhost:5236/api/User/Loginwithgoogle?Username=${encodeURIComponent(Username)}`;
      
      const response = await axios.post(url, null, {
        headers: {
          'Content-Type': 'application/json',
        },
      });
  
      console.log('Login successful:', response.data);
  
      if (response.data==='not a user') {
        alert('Your ID is verified but you don\'t have an account.');
      } else {
        if(response.data==="client")
        navigate('/user/dashboard');
      else
      navigate('/advisor/dashboard');
        
        localStorage.setItem('username', Username);
        
        
      }
    } catch (error) {
      console.error('Error during login:', error);
    }
  };
  

  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword); // Toggle the state
  };

  return (
    <div className='RegisterContainer'>
      <div className='register'>
        <div className="formCont">
          <div style={{ marginBottom: '20px', display: 'flex' }}>
            <img src={logo} alt="" height="50px" srcset="" />
            <span style={{
              color: '#6947a1', fontWeight: '900',
              fontSize: 'larger', margin: '10px'
            }}>MakeProfit</span>
          </div>
          <h3>Our Investor</h3>
          <form>
            <div className='InputContainer'>
              <input type="text" className='input' name='Username'
                value={Username}
                onChange={(e) => setUsername(e.target.value)} required />
              <label className="place">Username</label>
            </div>
            <div className='InputContainer'>
              <input type={showPassword ? "text" : "password"} className='input' name='Password'
                value={Password}
                onChange={(e) => setPassword(e.target.value)} required />
              <label className="place">Password</label>
              <div className="eye" onClick={togglePasswordVisibility}>
  {!showPassword ? (
    <svg className="h-8 w-8 text-red-500" width="24" height="24" viewBox="0 0 24 24" strokeWidth="2" stroke="currentColor" fill="none" strokeLinecap="round" strokeLinejoin="round">
      <path stroke="none" d="M0 0h24v24H0z" />
      <circle cx="12" cy="12" r="2" />
      <path d="M2 12l1.5 2a11 11 0 0 0 17 0l1.5 -2" />
      <path d="M2 12l1.5 -2a11 11 0 0 1 17 0l1.5 2" />
    </svg>
  ) : (
    <svg className="h-8 w-8 text-red-500" width="24" height="24" fill="none" viewBox="0 0 24 24" stroke="currentColor">
      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858.908a3 3 0 114.243 4.243M9.878 9.878l4.242 4.242M9.88 9.88l-3.29-3.29m7.532 7.532l3.29 3.29M3 3l3.59 3.59m0 0A9.953 9.953 0 0112 5c4.478 0 8.268 2.943 9.543 7a10.025 10.025 0 01-4.132 5.411m0 0L21 21"/>
    </svg>
  )}
</div>

            </div>
            <div style={{
              display: 'flex', justifyContent: 'center', width: '70%',
              marginTop: '5px', marginBottom: '5px'
            }}>
              <button type="submit" className='userbtn' onClick={handleLogin}>Login</button>
            </div>
          </form>
          <div style={{
            display: 'flex', alignItems: 'center',
            margin: '0px', textAlign: 'center', width: '70%', justifyContent: 'center', color: 'gray'
          }}>
            OR
          </div>
          <GoogleOAuthProvider clientId="365247809255-3dl2cakpj169krcqb9dbcgc4qvchjrof.apps.googleusercontent.com">
            <GoogleLogin
              onSuccess={credentialResponse => {
                var cred = jwtDecode(credentialResponse.credential);
                console.log('email verified:', cred.email_verified);
                handleGoogleLogin(cred.email);
              }}
              onError={() => {
                console.log('Login Failed');
              }}
            />
          </GoogleOAuthProvider>
        </div>
        <div className='TextContainer'>
          <h1>
            Welcome Back
          </h1>
          <p className='usertext'> TO MakeProfit</p>
          <h6 style={{ fontSize: '12px', color: "#ffffffb0" }}>
          </h6>
        </div>
      </div>
      {/* <script src='./users.js'></script> */}
    </div>
  );
};

export default Login;
