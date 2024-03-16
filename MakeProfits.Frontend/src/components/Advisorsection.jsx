import React from 'react';
import './advisorsection.css'
const AdvisorSection = () => {
  const advisors = [
    {
      name: 'Seema',
      image: 'https://img.freepik.com/premium-photo/indian-young-woman-orange-jacket-holds-clipboard-smiles_589208-66.jpg?size=626&ext=jpg&ga=GA1.1.441251946.1706255386&semt=sph', // Replace with actual image source
      rating: 4.8,
    },
    {
      name: 'Sweety',
      image: 'https://img.freepik.com/premium-photo/indian-young-woman-orange-jacket-holds-clipboard-smiles_589208-66.jpg?size=626&ext=jpg&ga=GA1.1.441251946.1706255386&semt=sph', // Replace with actual image source
      rating: 4.5,
    },
    {
      name: 'Bhawna',
      image: 'https://img.freepik.com/premium-photo/indian-young-woman-orange-jacket-holds-clipboard-smiles_589208-66.jpg?size=626&ext=jpg&ga=GA1.1.441251946.1706255386&semt=sph', // Replace with actual image source
      rating: 4.9,
    },
  ];

  return (
  
      <div className='advisor-cont'>
        <h1 >
          Our Best Performing Advisors
          
        </h1>
        <div className="advprofile-cont">
          {advisors.map((adv)=>(
            <div className="profile">
              <img src={adv.image} alt="" srcset="" />
              <p style={{color:"black",fontWeight:'bold',marginTop:'10px'}} className='advname'>{adv.name}</p>
              <p style={{color:'gray',fontSize:'12px'}}>SEO of XYZ ltd.</p>
              <hr style={{backgroundColor:'blue',height:"3px",width:"20%",margin:"10px",border:0}}/>
              <p style={{color:'black',textAlign:'center'}}>Lorem ipsum dolor sit, amet consectetur adipisicing elit. Non eveniet neque, iusto nam sequi labore </p>
            </div>
          ))}
          
        </div>
      </div>

  );
};

// Styles

export default AdvisorSection;