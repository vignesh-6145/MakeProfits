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
} from 'semantic-ui-react'

const Alladvisor = () => 
  (
  <div className='adv-body'>  
  <div className='adv-container'>
      <h1>SELECTED ADVISORS FOR YOU</h1>
  <CardGroup>
  <Popup
  trigger={
    <Card>
      <CardContent>
        <Image
          floated='right'
          size='mini'
          src='https://react.semantic-ui.com/images/avatar/large/steve.jpg'
        />
        <CardHeader>Abhishek jha</CardHeader>
        <CardMeta>senior Analyst</CardMeta>
        <CardDescription>
          one of the best advisor with <strong>1000+ happy customer</strong>
        </CardDescription>
      </CardContent>
      <CardContent extra>
        <div className='ui two buttons'>
          <Button basic color='green' href='/Allstrategy'>
            Approve
          </Button>
          <Button basic color='red' >
            Decline
          </Button>
        </div>
      </CardContent>
    </Card>
  }
  >
    <PopupHeader>User Rating</PopupHeader>
    <PopupContent>
      <Rating icon='star' defaultRating={4.5} maxRating={5} />
    </PopupContent>
  </Popup>
    <Card>
      <CardContent>
        <Image
          floated='right'
          size='mini'
          src='https://react.semantic-ui.com/images/avatar/large/molly.png'
        />
        <CardHeader>Abhishek jha</CardHeader>
        <CardMeta>senior Analyst</CardMeta>
        <CardDescription>
          one of the best advisor with <strong>1000+ happy customer</strong>
        </CardDescription>
      </CardContent>
      <CardContent extra>
        <div className='ui two buttons'>
          <Button basic color='green'>
            Approve
          </Button>
          <Button basic color='red'>
            Decline
          </Button>
        </div>
      </CardContent>
    </Card>
    <Card>
      <CardContent>
        <Image
          floated='right'
          size='mini'
          src='https://react.semantic-ui.com/images/avatar/large/jenny.jpg'
        />
       <CardHeader>Abhishek jha</CardHeader>
        <CardMeta>senior Analyst</CardMeta>
        <CardDescription>
          one of the best advisor with <strong>1000+ happy customer</strong>
        </CardDescription>
      </CardContent>
      <CardContent extra>
        <div className='ui two buttons'>
          <Button basic color='green'>
            Approve
          </Button>
          <Button basic color='red'>
            Decline
          </Button>
        </div>
      </CardContent>
    </Card>
  </CardGroup>

  </div>
  </div>
  )

export default Alladvisor