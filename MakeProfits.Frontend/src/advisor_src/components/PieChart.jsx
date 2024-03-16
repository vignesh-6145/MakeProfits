import React from 'react';
import { PieChart, Pie, Cell,Legend,Tooltip } from 'recharts';
import { mockPieData } from './mockPieData'; // Import the data from a separate file

const PieArt = () => {
  const data = mockPieData; // Assign the imported data to the 'data' variable

  // Define colors array
  const colors = ['#f4978e', '#84dcc6', '#70d6ff'];
  const CustomLegend = () => {
    return (
      <ul style={{ listStyleType: 'none', padding: 0 ,marginTop: '-10px'}}>
        {data.map((entry, index) => (
          <li key={`legend-${index}`} style={{ display: 'flex', alignItems: 'center' }}>
            <div style={{ width: '10px',height:'10px', borderRadius: '50%', backgroundColor: colors[index% colors.length], marginRight: '5px' }}></div>
            <span style={{ color: colors[index % colors.length] }}>{entry.label}</span>
          
          </li>
        ))}
      </ul>
    );
  };
  
  const CustomTooltip = ({ active, payload, viewBox }) => {
    if (active && payload && payload.length) {
      const data = payload[0].payload;
      const { x, y } = payload[0].coordinate || {};
  
      // Adjust the position of the tooltip
      const tooltipX = x + viewBox.x;
      const tooltipY = y + viewBox.y;
  
      return (
        <div
          style={{
            position: 'absolute',
            rigt: tooltipX + 10,
            bottom: tooltipY + 10, // Adjust the vertical position as needed
           // backgroundColor: 'white',
            //padding: '5px',
           // border: '1px solid black',
          
            zIndex: 1000,
          }}
        >
          <p>{data.label}: {data.value}</p>
        </div>
      );
    }
    return null;
  };
  
  
  return (
    <PieChart width={360} height={360} style={{ position: 'relative' }}>
       <Pie dataKey="value" data={data} label>
  {data.map((entry, index) => (
    <Cell key={`cell-${index}`} fill={colors[index % colors.length]} />
  ))}
</Pie>
      <Tooltip content={<CustomTooltip />} />
      <Legend content={<CustomLegend />} />
    </PieChart>
    
  );
};

export default PieArt;