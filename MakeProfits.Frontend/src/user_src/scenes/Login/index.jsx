
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Box, Typography, Dialog, DialogTitle, DialogContent, DialogActions, Button } from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import { tokens } from "../../theme";
import { useTheme } from "@mui/material/styles";
import { mockDataTeam } from "../../data/mockData";
import Header from "../../components/Header";
import CalendarTodayOutlinedIcon from "@mui/icons-material/CalendarTodayOutlined";
import AssignmentOutlinedIcon from '@mui/icons-material/AssignmentOutlined';
import { Grid,Paper, Avatar, TextField,Link } from '@mui/material'
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import { GoogleLogin } from '@react-oauth/google';
import { useNavigate } from 'react-router-dom';
const Logins = ({handleLogin}) => {
  const theme = useTheme();
  const navigate = useNavigate();
  const colors = tokens(theme.palette.mode);
  const [selectedClient, setSelectedClient] = useState(null);
  const [showInvestmentInfo, setShowInvestmentInfo] = useState(false);
  
  const baseURL = 'http://localhost:5236/api/Advisor/6/Clients';
  const paperStyle={padding :20,height:'70vh',width:280, margin:"20px auto"};
    const avatarStyle={backgroundColor:'#1bbd7e'};
    const btnstyle={margin:'8px 0'};

  return (
    <Grid>
    <Paper elevation={10} style={paperStyle}>
        <Grid align='center'>
             <Avatar style={avatarStyle}><LockOutlinedIcon/></Avatar>
            <h2>Sign In</h2>
        </Grid>
        <GoogleLogin
          onSuccess={credentialResponse => {
            handleLogin(true);
          }}
          onError={() => {
            console.log('Login Failed');
            handleLogin(false);
          }}
          useOneTap
        />;
        <TextField label='Username' placeholder='Enter username' variant="outlined" fullWidth required/>
        <TextField label='Password' placeholder='Enter password' type='password' variant="outlined" fullWidth required/>
        <FormControlLabel
            control={
            <Checkbox
                name="checkedB"
                color="primary"
            />
            }
            label="Remember me"
         />
        <Button type='submit' color='primary' variant="contained" style={btnstyle} fullWidth>Sign in</Button>
        <Typography >
             <Link href="#" >
                Forgot password ?
        </Link>
        </Typography>
        <Typography > Do you have an account ?
             <Link href="#" >
                Sign Up 
        </Link>
        </Typography>
    </Paper>
</Grid>
  );
};

export default Logins;

