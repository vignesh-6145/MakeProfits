import React, { useState, useEffect } from 'react';
import 'semantic-ui-css/semantic.min.css';
import './alladvisor.css';
import { useNavigate } from 'react-router-dom';

import {
  CardMeta,
  CardHeader,
  CardGroup,
  CardDescription,
  CardContent,
  Button,
  Card,
  Image,
  Popup,
  Rating,
  PopupHeader,
  PopupContent,
} from 'semantic-ui-react';

const Alladvisor = () => {
  const [advisors, setAdvisors] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch('http://localhost:5236/api/Advisor/getAllAdvisors');
        const data = await response.json();
        console.log(data);
        setAdvisors(data);
      } catch (error) {
        console.error('Error fetching advisors:', error);
      }
    };

    fetchData();
  }, []);

  const handleApprove = (advisor) => {
    // Implement logic for approving the advisor (e.g., navigate to another page)
    console.log('Approved advisor:', advisor);
    navigate('/Allstrategy',{state:{advisor : advisor}}); // Assuming Allstrategy is the approval page
  };

  const handleDecline = (advisor) => {
    // Implement logic for declining the advisor
    console.log('Declined advisor:', advisor);
  };

  const renderAdvisors = () => {
    return advisors.map((advisor,key) => (
      <Card key={advisor.userName}>
        <CardContent>
          <Image floated='right' size='mini' src='https://react.semantic-ui.com/images/avatar/large/steve.jpg' />
          <CardHeader>{advisor.firstName} {advisor.lastName}</CardHeader>
          <CardMeta>Analyst</CardMeta>
          <CardDescription>
            Customers awarded him with {advisor.rating} Rating
          </CardDescription>
        </CardContent>
        <CardContent extra>
          <div className='ui two buttons'>
            <Button basic color='green' onClick={() => handleApprove(advisor)}>
              Approve
            </Button>
            <Button basic color='red' onClick={() => handleDecline(advisor)}>
              Decline
            </Button>
          </div>
        </CardContent>
        
      </Card>
    ));
  };

  return (
    <div className='adv-body'>
      <div className='adv-container'>
        <h1>SELECTED ADVISORS FOR YOU</h1>
        <CardGroup>{renderAdvisors()}</CardGroup>
      </div>
    </div>
  );
};

export default Alladvisor;
