import React, { useState, useEffect } from 'react';
import 'semantic-ui-css/semantic.min.css';
import './alladvisor.css';

import axios from 'axios';
import { useLocation, useNavigate } from 'react-router-dom';

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
  Table,
  TableRow,
  TableHeaderCell,
  TableHeader,
  TableCell,
  TableBody,
  Header,
} from 'semantic-ui-react';

const Allstrategy = () => {
  const location = useLocation();
  const [strategyData, setStrategyData] = useState([]);
  const navigate = useNavigate();
  const advisor = location.state.advisor;
  useEffect(() => {
    const fetchData = async () => {
      try {
        
        console.log("hey ->"+JSON.stringify(advisor));
        const response = await axios.post(`http://localhost:5236/api/Strategy/Showstrategy?advisorId=${advisor.id}`);
        const data = await response.data;
        setStrategyData(data);
      } catch (error) {
        console.error('Error fetching strategy:', error);
      }
    };

    fetchData();
  }, [advisor]); // Dependency array ensures fetch happens only when advisorID changes

  const renderStrategy = () => {
    return strategyData.map((strategy) => (
      <TableRow key={strategy.strategyID}>
        <TableHeaderCell>Stocks</TableHeaderCell>
        <TableCell>{strategy.stockPercentage}%</TableCell>
        <TableHeaderCell>Mutual Funds</TableHeaderCell>
        <TableCell>{strategy.mfPercentage}%</TableCell>
        <TableHeaderCell>Bonds</TableHeaderCell>
        <TableCell>{strategy.bondsPercentage}%</TableCell>
        {/*need to make a call to advisory request*/}
        <Button basic color='green' href='/Alladvisor'>
              Select this
            </Button>
      </TableRow>
    ));
  };

  return (
    <div className='strategy-cont' style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
      <Card style={{ innerWidth: '50vw' }}>
        <CardContent>
          <Image floated='right' size='mini' src='https://react.semantic-ui.com/images/avatar/large/steve.jpg' />
          <CardHeader>All stratergies of Mr. {advisor.firstName}</CardHeader>
          <CardDescription>
            {/* Replace with relevant information from the API response (optional) */}
            {advisor.emailAddress}
          </CardDescription>
          <Table celled>
            <TableHeader>
              <TableHeaderCell>Asset Class</TableHeaderCell>
              <TableHeaderCell>Percentage</TableHeaderCell>
            </TableHeader>
            <TableBody>{renderStrategy()}</TableBody>
          </Table>
        </CardContent>
        <CardContent extra>
          <div className='ui two buttons'>
            
            <Button basic color='red'>
              Go Back
            </Button>
          </div>
        </CardContent>
      </Card>
    </div>
  );
};

export default Allstrategy;
