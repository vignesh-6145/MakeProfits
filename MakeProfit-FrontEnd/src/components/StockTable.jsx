import React from 'react';
import './stocktable.css'

const StockTable = () => {
  // Placeholder data for stocks
  const stocks = [
    { symbol: 'AAPL', price: 150.25, success: true },
    { symbol: 'GOOGL', price: 2750.50, success: false },
    { symbol: 'MSFT', price: 300.75, success: true },
    { symbol: 'AMZN', price: 3400.00, success: false },
    { symbol: 'TSLA', price: 800.25, success: true },
    { symbol: 'GOOGL', price: 2750.50, success: false },
    { symbol: 'MSFT', price: 300.75, success: true },
    { symbol: 'AMZN', price: 3400.00, success: false },
    { symbol: 'TSLA', price: 800.25, success: true },
  ];

  return (
    <div className="tablecontainer">
        
    <table className="stock-table" >
      <thead style={{backgroundColor:'black',color:'white'}}>
        <tr>
          <th>Company Name</th>
          <th>MCap</th>
          <th>Price</th>
          <th>ROE</th>
          <th>Signal</th>
        </tr>
      </thead>
      <tbody>
        {stocks.map((stock) => (
          <tr key={stock.symbol}>
            <td>{stock.symbol}</td>
            <td>2000cr</td>
            <td>{stock.price}</td>
            <td>16.78%</td>
            <td>
              <div id="signal"className={stock.success?'red':'green'}>
               
              {stock.success ? <p>Buy</p> : <p>Sell</p>}
              
               
              </div>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
    </div>
  );
};

export default StockTable;
