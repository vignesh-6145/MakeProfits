import React from 'react';
import { Box, Typography, useTheme } from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';
import { tokens } from '../../theme';
import { mockInvestmentData } from '../../data/mockInvdata';
import Header from '../../components/Header';
import AssignmentOutlinedIcon from '@mui/icons-material/AssignmentOutlined';

const ClientTable = ({ client }) => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);

  const columns = [
    { field: 'id', headerName: 'ID', width: 90 },
    { field: 'type', headerName: 'Investment Type', flex: 1 },
    { field: 'currentValue', headerName: 'Current Value', flex: 1 },
    { field: 'initialValue', headerName: 'Initial Value', flex: 1 },
    { field: 'changePercent', headerName: 'Change (%)', flex: 1 },
  ];

  const rows = mockInvestmentData.map((investment) => ({
    id: investment.id,
    type: investment.type,
    currentValue: investment.currentValue,
    initialValue: investment.initialValue,
    changePercent: investment.changePercent,
  }));

  return (
    <Box m="20px">
      <Header title="Investment Portfolio" subtitle="Client's Investment Information" />
      <Box
        m="40px 0 0 0"
        height="75vh"
        sx={{
          '& .MuiDataGrid-root': {
            border: 'none',
          },
          '& .MuiDataGrid-cell': {
            borderBottom: 'none',
          },
          '& .MuiDataGrid-columnHeaders': {
            backgroundColor: colors.blueAccent[700],
            borderBottom: 'none',
          },
          '& .MuiDataGrid-virtualScroller': {
            backgroundColor: colors.primary[400],
          },
          '& .MuiDataGrid-footerContainer': {
            borderTop: 'none',
            backgroundColor: colors.blueAccent[700],
          },
          '& .MuiCheckbox-root': {
            color: `${colors.greenAccent[200]} !important`,
          },
        }}
      >
        <DataGrid rows={rows} columns={columns} pageSize={5} />
      </Box>
    </Box>
  );
};

export default ClientTable;
