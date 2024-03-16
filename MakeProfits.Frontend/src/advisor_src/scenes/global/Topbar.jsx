import React, { useContext, useState } from 'react';
import { Box, IconButton, useTheme, Snackbar, Typography } from '@mui/material';
import { ColorModeContext, tokens } from '../../theme';
import InputBase from '@mui/material/InputBase';
import LightModeOutlinedIcon from '@mui/icons-material/LightModeOutlined';
import DarkModeOutlinedIcon from '@mui/icons-material/DarkModeOutlined';
import SettingsOutlinedIcon from '@mui/icons-material/SettingsOutlined';
import PersonOutlinedIcon from '@mui/icons-material/PersonOutlined';
import SearchIcon from '@mui/icons-material/Search';
import LogoutIcon from '@mui/icons-material/Logout';
import CloseIcon from '@mui/icons-material/Close';
import Notification from './Notification'; // Import the Notification component

const Topbar = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const colorMode = useContext(ColorModeContext);

  return (
    <Box display="flex" justifyContent="space-between" p={2}>
    <Box
  display="flex"
  backgroundColor={colors.primary[400]}
  borderRadius="3px"
  alignItems="center" // Align items vertically
>
  <InputBase
    sx={{ ml: 2, flex: 1, color: '#fff' }} // Ensure text color is white
    placeholder="Search"
    inputProps={{ style: { color: '#fff' } }} // Set text color of placeholder
  />
  <IconButton type="button" sx={{ml:'60px' , p: 1 }}>
    <SearchIcon />
  </IconButton>
</Box>


      {/* ICONS */}
      <Box display="flex" alignItems="center">
        {/* Replace the IconButton with the Notification component */}
        <Notification message="New Notification!" />
        {/* Only notification icon */}
      </Box>
    </Box>
  );
};

export default Topbar;
