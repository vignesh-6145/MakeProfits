// src/Login.js
import React, { useState } from 'react';
import logo from './assets/logo (2).png';
import { GoogleLogin } from '@react-oauth/google';
import { GoogleOAuthProvider } from '@react-oauth/google';
import './user.css';
import { jwtDecode } from 'jwt-decode';
const Login = () => {
  const [Username, setUsername] = useState('');
  const [Password, setPassword] = useState('');

  const handleLogin = async (e) => {
    try {
      // Prevent the default form submission
      e.preventDefault();
  
      // Get the values of Username and Password (assuming they are stored in state variables)
      const username = encodeURIComponent(Username);
      const password = encodeURIComponent(Password);
  
      // Make API call to check credentials
      const url = `https://localhost:7131/api/User/Login`;
      
      const response = await fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ Username: username, Password: password }),
      });
  
      const data = await response.json();
  
      console.log('Login response:', data);
  
      if (data === "not a user") {
        alert('Incorrect credentials');
      } else {
        alert('Logged in');
        // You can perform further actions after successful login
      }
    } catch (error) {
      console.error('Error during login:', error);
    }
  };
  
  const handleGoogleLogin = async (Username) => {
    try {
      // Make API call to check credentials
      const url = `https://localhost:7131/api/User/Loginwithgoogle?Username=${encodeURIComponent(Username)}`;
     
      fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json', 
        },
      })
      .then(response => response.json())
      .then(data => {
        console.log('Login successful:', data);
        
        if(!data)
        {
          alert('your Id is verified but you dont have account');
        }
        else{
          alert('logged in');
        }

      })
      .catch(error => {
        console.error('Error during login:', error);
      });
    } catch (error) {
      console.error('Error during login:', error);
    }
  };

  return (
    
  <div className='RegisterContainer'>

      <div className='register'>
      
      <div className="formCont">
        <div style={{marginBottom:'20px',display:'flex'}}>
        <img src={logo}alt="" height="50px"srcset="" />
        <span style={{color: '#6947a1',    fontWeight: '900',
    fontSize: 'larger',margin:'10px' }}>MakeProfit</span>

        </div>
               <h3>Our Investor</h3>
      <form >
      <div className='InputContainer'>
         
         <input type="text" className='input' name='Username'
            value={Username}
            onChange={(e) => setUsername(e.target.value)} required />
         <label className="place" >Username</label>
       </div>
       <div className='InputContainer'>
         
         <input type="password" className='input' name='Password'
            value={Password}
            onChange={(e) => setPassword(e.target.value)} required />
         <label className="place" >Password</label>
       </div>
         
       <div  style={{display:'flex',justifyContent:'center',width:'70%',marginTop:'5px',marginBottom:'5px'}}>
        
            <button type="submit" className='userbtn' onClick={handleLogin}>Login</button>
        </div>
      </form>
      <div style={{display:'flex',alignItems: 'center',
      margin: '0px',textAlign:'center',width:'70%',justifyContent:'center',color:'gray'}}>
        OR
        </div>
        <GoogleOAuthProvider clientId="365247809255-3dl2cakpj169krcqb9dbcgc4qvchjrof.apps.googleusercontent.com">
          
          <GoogleLogin 
  onSuccess={credentialResponse => {
    var cred=jwtDecode(credentialResponse.credential);
    console.log('email verified:',cred.email_verified);
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
         <h6 style={{fontSize:'12px',color:"#ffffffb0"}}>
         </h6>
      </div>
      </div>
      
    </div>
  );
};

export default Login;
