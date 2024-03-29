
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

const Team = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [selectedClient, setSelectedClient] = useState(null);
  const [showInvestmentInfo, setShowInvestmentInfo] = useState(false);
  const [clients,setClients] = useState([]);
  const baseURL = 'http://localhost:5236/api/Advisor/6/Clients';
  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(baseURL, {
          headers: {
            'Access-Control-Allow-Origin': '*' // Replace with your allowed origin
          }
        });
        setClients(response.data.map((obj,idx) => ({...obj,id:idx})));
      } catch (error) {
        console.error('Error fetching clients:', error);
      }
    };
  
    fetchData();
  }, []);

  const columns = [
    // Define your columns as before
    {
      field: "firstName",
      headerName: "firstName",
      flex: 1,
      cellClassName: "name-column--cell",
    },
    {
      field: "lastName",
      headerName: "lastName",
      flex: 1,
      cellClassName: "name-column--cell",
    },
    // {
    //   field: "addressLine",
    //   headerName: "addressLine",
    //   flex: 1,
    //   cellClassName: "name-column--cell",
    // },
    {
      field: "city",
      headerName: "amount",
      value: "1000",
      flex: 1,
      cellClassName: "name-column--cell",
    },
    // // {
    // //   field: "age",
    // //   headerName: "Age",
    // //   type: "number",
    // //   headerAlign: "left",
    // //   align: "left",
    // // },
    {
      field: "phoneNumber",
      headerName: "phoneNumber",
      flex: 1,
      cellClassName: "name-column--cell",
    },
    // {
    //   field: "emailAddress",
    //   headerName: "emailAddress",
    //   flex: 1,
    // },
    // {
    //   field: "registrationDate",
    //   headerName: "Registration Date",
    //   flex: 1,
    //   renderCell: ({ row: { registrationDate } }) => {
    //     return (
    //       <Box
    //         width="60%"
    //         m="0 auto"
    //         p="5px"
    //         display="flex"
    //         justifyContent="center"
    //         backgroundColor={colors.greenAccent[700]}
    //         borderRadius="4px"
    //       >
    //         <CalendarTodayOutlinedIcon />
    //         <Typography color={colors.grey[100]} sx={{ ml: "5px" }}>
    //           {registrationDate}
    //         </Typography>
    //       </Box>
    //     );
    //   },
    // },
    // {
    //   field: "strategyPlan",
    //   headerName: "Strategy Plan",
    //   flex: 1,
    //   renderCell: ({ row: { strategyPlan } }) => {
    //     return (
    //       <Box
    //         width="60%"
    //         m="0 auto"
    //         p="5px"
    //         display="flex"
    //         justifyContent="center"
    //         backgroundColor={colors.greenAccent[700]}
    //         borderRadius="4px"
    //       >
    //         <AssignmentOutlinedIcon />
    //         <Typography color={colors.grey[100]} sx={{ ml: "5px" }}>
    //           {strategyPlan}
    //         </Typography>
    //       </Box>
    //     );
    //   },
    // },
  ];

  const handleClientClick = (client) => {
    setSelectedClient(client);
    setShowInvestmentInfo(true); // Open the dialog when a client is selected
  };

  return (
    <Box m="20px">
      <Header title="CLIENTS" subtitle="INVESTING INFORMATION" />
      <Dialog open={showInvestmentInfo} onClose={() => setShowInvestmentInfo(false)}>
        <DialogTitle>Investment Details</DialogTitle>
        <DialogContent>
          {/* Render investment details here */}
          {selectedClient && (
            <>
              <Typography variant="body1">Name: {selectedClient.name}</Typography>
              <Typography variant="body1">Investment Amount: {selectedClient.investmentAmount}</Typography>
              {/* Add more investment details as needed */}
            </>
          )}
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setShowInvestmentInfo(false)}>Close</Button>
        </DialogActions>
      </Dialog>
      {console.log("Data"+JSON.stringify(clients))}
      {/* Render the DataGrid component to display client list */}
      <DataGrid
            rows={clients}
            columns={columns}
            getRowId={(row) =>  row.id}
         />
    </Box>
  );
};

export default Team;

