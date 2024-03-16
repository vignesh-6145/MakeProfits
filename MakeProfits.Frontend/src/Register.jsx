import React, { useState } from 'react';
import logo from './assets/logo (2).png';
import { jwtDecode } from 'jwt-decode';
import { GoogleOAuthProvider } from '@react-oauth/google';
import "./user.css";
import { GoogleLogin } from '@react-oauth/google';

import axios from 'axios';
 
const UserRegistrationForm = () => {
  const [formData, setFormData] = useState({
   
    firstname: '',
    email: '',
    password: '',
    phno:null
  });
  const [showPassword, setShowPassword] = useState(false); // State to track password visibility

  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword); // Toggle the state
  };


const handleSubmit = async (event) => {
  event.preventDefault();

  try {
    const response = await axios.post('https://localhost:7274/api/User/Register', formData, {
      headers: {
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
      },
    });
    
    console.log('Registration successful:', response.data);
    // You can handle the response here (e.g., show a success message)
  } catch (error) {
    console.error('Error registering user:', error);
    // You can handle errors here (e.g., show an error message)
  }
};

 
  const handleChange = (event) => {
    setFormData({
      ...formData,
      [event.target.name]: event.target.value,
    });
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
               <h3 >New Member</h3>
      <form onSubmit={handleSubmit}>
         
         <div className='InputContainer'>
         
          <input type="text"  name="firstname" className='input' placeholder='' value={formData.firstname} onChange={handleChange} required />
          <label className="place" >Name</label>
        </div>
        <div className='InputContainer'>
       
        <input type="email" className='input' name="email" placeholder="" value={formData.email} onChange={handleChange} required />
        <label className="place" >Username</label>
         </div>
         <div className='InputContainer'>
       
        <input type="input" className='input' name="phno" placeholder="" value={formData.phno} onChange={handleChange} required />
        <label className="place" >Phone number</label>
         </div>
         <div className='InputContainer'>
       
        <input  type={showPassword ? "text" : "password"} className='input' name="password" placeholder='' value={formData.password} onChange={handleChange} required />
        <label  className="place" >Password</label>
      
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
        <input required style={{height:'15px',width:"15px"}} type="checkbox" name="terms" id="" /><p style={{fontSize:"12px",display:'inline'}}> Check All Terms And Condition</p>
        <div  style={{display:'flex',justifyContent:'center',width:'70%',marginTop:'5px',marginBottom:'5px'}}>
        <button type="submit" className='userbtn'>Sign Up</button>

        </div>
              
      </form>
      <div style={{display:'flex',alignItems: 'center',
      margin: '0px',textAlign:'center',width:'70%',justifyContent:'center',color:'gray'}}>
        OR
        </div>
        <GoogleOAuthProvider clientId="365247809255-3dl2cakpj169krcqb9dbcgc4qvchjrof.apps.googleusercontent.com">
         
          <GoogleLogin width={'100%'}
  onSuccess={credentialResponse => {
    var cred=jwtDecode(credentialResponse.credential);
    console.log('email verified:',cred.email_verified);
    setFormData({
      'firstname':cred.name,
      'email':cred.email
 
    })
 
   
  }}
  onError={() => {
    console.log('Login Failed');
  }}
/>
         
       
          </GoogleOAuthProvider>
      </div>
      <div className='TextContainer'>
          <h1>
              HELLO
          </h1>
         <p className='usertext'>WELCOME TO MakeProfit</p>  
         <h6 style={{fontSize:'12px',color:"#ffffffb0"}}>
         </h6>
      </div>
      </div>
     
    </div>
  );
};
 
export default UserRegistrationForm;