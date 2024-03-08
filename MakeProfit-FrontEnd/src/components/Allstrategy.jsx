import React from 'react'
import 'semantic-ui-css/semantic.min.css'
import './alladvisor.css'
import { useNavigate } from 'react-router-dom';

import {
  CardMeta,
  CardHeader,
  CardGroup,
  CardDescription,
  CardContent,
  Button,
  Card,
  Image,Popup
  ,Rating,
  PopupHeader,
  PopupContent,
  TableRow,
  TableHeaderCell,
  TableHeader,
  TableCell,
  TableBody,
  Header,
  Table,
 
} from 'semantic-ui-react'
const Allstrategy = () => 
  (
    <div className='strategy-cont' style={{display:'flex',justifyContent:'center',alignItems:'center',height:'100vh'}    }>
        
    <Card style={{innerWidth:'50vw'}}>
      <CardContent>
        <Image
          floated='right'
          size='mini'
          src='https://react.semantic-ui.com/images/avatar/large/steve.jpg'
        />
        <CardHeader>Strategy no 3</CardHeader>
        <CardMeta>advisor name</CardMeta>
        <CardDescription>
        <ul>
    <li><strong>Stocks:</strong> 40%</li>
    <li><strong>Bonds:</strong> 30%</li>
    <li><strong>Real Estate:</strong> 20%</li>
    <li><strong>Cryptocurrency:</strong> 10%</li>
  </ul>
        </CardDescription>
      </CardContent>
      <CardContent extra>
        <div className='ui two buttons'>
          <Button basic color='green' href ='/Alladvisor'>
            Approve
          </Button>
          <Button basic color='red' >
            Decline
          </Button>
        </div>
      </CardContent>
    </Card>
    
    </div>
  )
export default Allstrategy;