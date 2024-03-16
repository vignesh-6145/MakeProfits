import React, { useState, useEffect } from 'react';
import './stockgrid.css'
const StockGrid = () => {
  // ... (same as previous code)
  const [Data, setData] = useState([
    
  ]);
  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch('https://financialmodelingprep.com/api/v3/quote/AAPL,FB,GOOG,MSFT,AMZN,TSLA?apikey=KAlAkVbTbPRQGoJyc6Mh1bryjRiWd4kz'); // Replace with the actual API endpoint
        const result = await response.json();
        setData(result);
        console.log(response);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
  

  }, []);

  return (
    <div className='stockcontainer' >
      <h1  style={{color:"red"}}>&#8606;</h1>
      {
        
        Data.map(s=>(
          <div className='stock'>
          <p style={{color:"red",fontWeight:"bold",fontSize:'1rem'}}>{s.symbol}</p>
          <h5>{s.price}</h5>          
          <p style={{color:"red" ,fontSize:'1rem'}}>{s.changesPercentage}</p>

        </div>
        )    
    )
      }
      
      <h1  style={{color:"green"}}>&#8608;</h1>
    </div>
    
    
  );
};

export default StockGrid;
