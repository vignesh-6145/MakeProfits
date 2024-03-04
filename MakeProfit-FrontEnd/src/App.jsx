// App.js

import React, { useEffect,useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Login from './Login';
import Register from './Register';
import StockGrid from './components/StockGrid';
import './App.css'
import Navbar from './components/Navbar';
import Footer from './components/Footer';
import ClientSection from './components/Clientsection';
import AdvisorSection from './components/Advisorsection';
import PopularComment from './components/PopularComment';
import StockTable from './components/StockTable';
// import GoogleLoginButton from './GoogleLoginButton';
import Home from './Home';
import User from './User';
function App() {
  // useEffect(()=>{
  //   function start() {
  //       gapi.client.init({
  //           clientId: clientId,
  //           scope:""
  //       })
  //   };
  //   gapi.load('client:auth2',start);
  // });
  return (<>
    {/* <Navbar/>
    <div className='hero'>
      <div className='herotext'>
        <h1>
         Empowering Your Financial Future: Navigate Investments with Confidence.
        </h1>
      </div>
      
    </div>
    <StockGrid/>
    <StockTable/>
    <AdvisorSection/>
    <ClientSection/>
    <PopularComment/>
    <Footer/>
     */}
    
     <Router>
      <Routes>
        <Route path='/' element={<Home />} />
        <Route path='/login' element={<Login />} />
        <Route path='/register' element={<Register/>}/>
        <Route path='/user' element={<User/>}/>
        {/* <Route path='/' element={<Home />} /> */}
      </Routes>
     </Router>
     

  </>
  
  );
}

export default App;
