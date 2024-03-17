import { Box } from "@mui/material";
import { DataGrid, GridToolbar } from "@mui/x-data-grid";
import { tokens } from "../../theme";
import { mockDataContacts } from "../../data/mockData";
import Header from "../../components/Header";
import { useTheme } from "@mui/material";
import axios from "axios";
import { useState,useEffect } from "react";

const Contacts = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [clients,setClients] = useState([]);
  {/**If user should display advisor contact details */}
  const baseURL = 'http://localhost:5236/api/Advisor/Client/5DB69961-F543-43FB-9D0F-403B707056A1';
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
    {
      field: "firstName",
      headerName: "Name",
      flex: 1,
      cellClassName: "name-column--cell",
    },
    {
      field: "phoneNumber",
      headerName: "Phone Number",
      flex: 1,
    },
    {
      field: "emailAddress",
      headerName: "Email",
      flex: 1,
    },
    {
      field: "addressLine",
      headerName: "Address",
      flex: 1,
    },
    {
      field: "city",
      headerName: "City",
      flex: 1,
    },
    {
      field: "state",
      headerName: "State",
      flex: 1,
    },
  ];

  return (
    <Box m="20px">
      <Header
        title="CONTACT DETAILS"
        subtitle="List of Clients Contact Information"
      />
      <Box
        m="40px 0 0 0"
        height="75vh"
        sx={{
          "& .MuiDataGrid-root": {
            border: "none",
          },
          "& .MuiDataGrid-cell": {
            borderBottom: "none",
          },
          "& .name-column--cell": {
            color: colors.greenAccent[300],
          },
          "& .MuiDataGrid-columnHeaders": {
            backgroundColor: colors.blueAccent[700],
            borderBottom: "none",
          },
          "& .MuiDataGrid-virtualScroller": {
            backgroundColor: colors.primary[400],
          },
          "& .MuiDataGrid-footerContainer": {
            borderTop: "none",
            backgroundColor: colors.blueAccent[700],
          },
          "& .MuiCheckbox-root": {
            color: `${colors.greenAccent[200]} !important`,
          },
          "& .MuiDataGrid-toolbarContainer .MuiButton-text": {
            color: `${colors.grey[100]} !important`,
          },
        }}
      >
        <DataGrid
          rows={clients}
          columns={columns}
          components={{ Toolbar: GridToolbar }}
        />
      </Box>
    </Box>
  );
};

export default Contacts;
