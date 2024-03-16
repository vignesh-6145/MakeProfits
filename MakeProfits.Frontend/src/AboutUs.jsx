// AboutUs.js
import React, { useState } from 'react';
import './AboutUs.css'; // Import the CSS file for AboutUs
import aboutImage from './assets/aboutusimg.jpg'; // Import the image
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';

const AboutUs = () => {
  const [expanded, setExpanded] = useState(false);

  const toggleExpanded = () => {
    setExpanded(!expanded);
  };

  return (
    <div className={`about-us-container ${expanded ? 'expanded' : ''}`} id='about-us'>
      <div className="about-us-header">
        <h2>About Us</h2>
        <hr className="divider" />
      </div>
      <div className="about-us-content-wrapper">
        <div className="about-us-image">
          <img src={aboutImage} alt="About Us" />
        </div>
        <div className="about-us-content">
          <p className="initial-content">
            At MakeProfit, our unwavering commitment extends globally to empowering investors on
            their path to financial achievements, fostering a culture of individual success within 
            a collective framework.
          </p>
          {!expanded && (
            <button className="expand-btn" onClick={toggleExpanded}>
              Read More
            </button>
          )}
          {expanded && (
            <>
              <p>
                Leveraging pioneering methodologies, comprehensive expertise, and deeply
                personalized approach, we strive to equip investors across diverse geographies with the tools, insights, 
                and resources necessary to navigate the intricate landscape of financial markets with unwavering confidence.
                Recognizing the inherent value of each investor’s unique journey, from seasoned veterans to nascent
                enthusiasts, we firmly believe in democratizing access to sophisticated investment strategies.
                Through our relentless pursuit of excellence, we stand as a beacon of transformative change, catalyzing
                a paradigm.
              </p>
              <p>
                Join us at MakeProfit and embrace the opportunity to become a vital protagonist in our 
                global narrative where each triumphant achievement serves as a testament to our shared goal of unparalleled financial prosperity. 
              </p>
              <div className="button-container">
                <button className="expand-btn" onClick={toggleExpanded}>
                  Read Less
                </button>
                <div className="arrow-container">
                  <KeyboardArrowUpIcon className="arrow-icon" />
                </div>
              </div>
            </>
          )}
          {!expanded && (
            <div className="arrow-container">
              <KeyboardArrowDownIcon className="arrow-icon" />
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default AboutUs;



{/*import React from 'react';
import './AboutUs.css'; // Import the CSS file for AboutUs

const AboutUs = () => {
  return (
    <div className="about-us-container" id='about-us'>
      <h2>About Us</h2>
      <p>
      At MakeProfit, our unwavering commitment extends globally to empowering investors on
      their path to financial achievements, fostering a culture of individual success within 
      a collective framework. Leveraging pioneering methodologies, comprehensive expertise, and deeply
      personalized approach, we strive to equip investors across diverse geographies with the tools, insights, 
      and resources necessary to navigate the intricate landscape of financial markets with unwavering confidence.
      </p>
      <p>
      Recognizing the inherent value of each investor’s unique journey, from seasoned veterans to nascent
      enthusiasts, we firmly believe in democratizing access to sophisticated investment strategies.
      Through our relentless pursuit of excellence, we stand as a beacon of transformative change, catalyzing
      a paradigm. Join us at MakeProfit and embrace the opportunity to become a vital protagonist in our 
      global narrative where each triumphant achievement serves as a testament to our shared goal of unparalleled financial prosperity. 
      </p>
    </div>
  );
};

export default AboutUs;
*/}