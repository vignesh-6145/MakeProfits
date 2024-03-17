import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Box, Typography, Dialog, DialogTitle, DialogContent, DialogActions, Button } from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import { tokens } from "../../theme";
import { useTheme } from "@mui/material/styles";
import Header from "../../components/Header";
import CalendarTodayOutlinedIcon from "@mui/icons-material/CalendarTodayOutlined";
import AssignmentOutlinedIcon from '@mui/icons-material/AssignmentOutlined';

const InvestmentTable = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [selectedInvestment, setSelectedInvestment] = useState(null);
  const [showInvestmentInfo, setShowInvestmentInfo] = useState(false);
  const [investments, setInvestments] = useState([]);
  const baseURL = '';

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(baseURL, {
          headers: {
            'Access-Control-Allow-Origin': '*' // Replace with your allowed origin
          }
        });
        setInvestments(response.data.map((obj,idx) => ({...obj,id:idx})));
      } catch (error) {
        console.error('Error fetching investments:', error);
      }
    };
  
    fetchData();
  }, []);

  const columns = [
    {
      field: "clientName",
      headerName: "Client Name",
      width: 200,
      cellClassName: "name-column--cell",
    },
    {
      field: "investmentType",
      headerName: "Type",
      width: 120,
      cellClassName: "name-column--cell",
    },
    {
      field: "quantity",
      headerName: "Quantity",
      width: 120,
      cellClassName: "name-column--cell",
    },
    {
      field: "purchasePrice",
      headerName: "Purchase Price",
      width: 150,
      cellClassName: "name-column--cell",
    },
    {
      field: "InvestedDate",
      headerName: "Invested Date",
      width: 150,
      cellClassName: "name-column--cell",
    },
  ];
  

  const handleInvestmentClick = (investment) => {
    setSelectedInvestment(investment);
    setShowInvestmentInfo(true);
  };

  return (
    <Box m="20px" height="300px" width="70%" >
      <Header title="INVESTMENTS" subtitle="INVESTING INFORMATION" />
      <Dialog open={showInvestmentInfo} onClose={() => setShowInvestmentInfo(false)}>
        <DialogTitle>Investment Details</DialogTitle>
        <DialogContent>
          {selectedInvestment && (
            <>
              <Typography variant="body1">Client Name: {selectedInvestment.clientName}</Typography>
              <Typography variant="body1">Type: {selectedInvestment.investmentType}</Typography>
              <Typography variant="body1">Quantity: {selectedInvestment.quantity}</Typography>
              <Typography variant="body1">Purchase Price: {selectedInvestment.purchasePrice}</Typography>
              <Typography variant="body1">Invested date: {selectedInvestment.investedDate}</Typography>
              {/* Add more investment details as needed */}
            </>
          )}
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setShowInvestmentInfo(false)}>Close</Button>
        </DialogActions>
      </Dialog>
      <div style={{ height: '100%', width: '100%', overflowX: 'auto', scrollbarWidth: 'thin', scrollbarColor: 'black' }}>
        <DataGrid
          rows={investments}
          columns={columns}
          getRowId={(row) => row.id}
          onRowClick={(row) => handleInvestmentClick(row.row)}
          autoHeight
        />
      </div>
    </Box>
  );
};

export default InvestmentTable;
