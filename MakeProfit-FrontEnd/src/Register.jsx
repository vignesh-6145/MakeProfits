import React, { useState } from 'react';
import logo from './assets/logo (2).png';
import { jwtDecode } from 'jwt-decode';
import { GoogleOAuthProvider } from '@react-oauth/google';
import "./user.css";
import { GoogleLogin } from '@react-oauth/google';
 
 
const UserRegistrationForm = () => {
  const [formData, setFormData] = useState({
   
    name: '',
    username: '',
    password: '',
  });
 
  const handleSubmit = (event) => {
    event.preventDefault();
 
    // Make a POST request to your server (replace URL with your actual endpoint)
    fetch('https://localhost:7131/api/User/Register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(formData),
    })
      .then(response => response.json())
      .then(data => {
        console.log('Registration successful:', data);
        // You can handle the response here (e.g., show a success message)
      })
      .catch(error => {
        console.error('Error registering user:', error);
        // You can handle errors here (e.g., show an error message)
      });
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
         
          <input type="text"  name="name" className='input' placeholder='' value={formData.name} onChange={handleChange} required />
          <label className="place" >Name</label>
        </div>
        <div className='InputContainer'>
       
        <input type="email" className='input' name="username" placeholder="" value={formData.username} onChange={handleChange} required />
        <label className="place" >Username</label>
         </div>
         <div className='InputContainer'>
       
        <input type="password"  className='input' name="password" placeholder='' value={formData.password} onChange={handleChange} required />
        <label  className="place" >Password</label>
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
      'name':cred.name,
      'username':cred.email
 
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