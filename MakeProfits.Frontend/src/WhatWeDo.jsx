// WhatWeDo.js

import React from 'react';
import './WhatWeDo.css'; // Import the CSS file for WhatWeDo
import whatWeDoImage from './assets/exploreimg.jpg'; // Import the image

const WhatWeDo = () => {
  return (
    <div className="what-we-do-container" id='what-we-do'>
      <div className="what-we-do-content-wrapper">
        <div className="what-we-do-image">
          <img src={whatWeDoImage} alt="What We Do" />
          <div className="explore-button">Explore More</div>
        </div>
        <div className="what-we-do-content">
          <div className='what-we-do-header'>
          <h2>What We Do?</h2>
          </div>
          <div className="divider"></div>
          <p>
            At MakeProfit, our mission is to empower investors to achieve their financial 
            goals through personalized investment strategies tailored to their unique needs and aspirations. 
            Through our subscription-based service, clients gain access to a team of seasoned advisors who provide 
            expert guidance and recommendations designed to optimize their investment portfolios.
          </p>
          <p>
            We offer subscription plans that provide clients with exclusive access to our team of dedicated advisors.
            Upon subscribing, clients unlock a wealth of resources and personalized support to navigate the 
            complexities of the financial markets with confidence.
          </p>
        </div>
      </div>
    </div>
  );
};

export default WhatWeDo;
