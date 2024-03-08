// App.js

import React, { useState } from 'react';
import { BrowserRouter as Router, Route, Link } from 'react-router-dom';
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
import LandingChart from './components/LandingChart';
function Home() {
  return (<>
    <Navbar/>
    <div className='hero'>
      <div className='herotext'>
        <h1>
         Empowering Your Financial Future: Navigate Investments with Confidence.
        </h1>
      </div>
      
    </div>
    <StockGrid/>
    <div style={{display:'flex'}}>
    <StockTable/>
    <LandingChart symbol={'AMZN'}/>
    </div>


    <AdvisorSection/>
    {/* <ClientSection/> */}
    <PopularComment/>
    <Footer/>
    
  </>
  
  );
}

export default Home;
