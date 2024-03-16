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
import AboutUs from './AboutUs';
import WhatWeDo from './WhatWeDo';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import { IconButton } from '@mui/material';
import Button from '@mui/material/Button';
import FAQ from './faq';


function Home() {
  return (<>
    <Navbar/>
    <div className='hero'>
      <div className='herotext'>
        <h1>
         Empowering Your Financial Future: Navigate Investments with Confidence.
        </h1>
        <Button sx={{ border:'2px solid #0096c7', '&:hover': {backgroundColor:"#0096c7",color:"white",}}} component={Link} to="/register" endIcon={<ArrowForwardIcon />}>
  Get Started
</Button>
      </div>
      
    </div>
    {/* <StockGrid/> */}
    <WhatWeDo/>
    
    <div style={{display:'flex'}}>
    {/* <StockTable/> */}
    <AboutUs/>
    {/* <LandingChart symbol={'AMZN'}/> */}
    
    </div>
    <div>
    <FAQ/>
    </div>

  
    <AdvisorSection/>
    {/* <ClientSection/> */}
    <PopularComment/>
    <Footer/>
    
  </>
  
  );
}

export default Home;
