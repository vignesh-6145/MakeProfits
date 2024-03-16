import React, { useEffect, useState } from 'react';
import { useTheme } from '@mui/material/styles';
import { Card, Grid, Typography, IconButton } from '@mui/material';
import ArrowUpwardIcon from '@mui/icons-material/ArrowUpward';
import ArrowDownwardIcon from '@mui/icons-material/ArrowDownward';
import ApexCharts from 'apexcharts';
import Chart from 'react-apexcharts';
import chartData from './bajaj-area-chart';

// Mock data for popular stocks
const popularStocks = [
  { name: 'APPL', value: 100, direction: 'up' },
  { name: 'GOOG', value: 90, direction: 'down' },
  { name: 'HDL', value: 80, direction: 'up' },
];

const BajajAreaChartCard = () => {
  const theme = useTheme();
  const [navType, setNavType] = useState(null);

  useEffect(() => {
    const orangeDark = theme.palette.secondary[800];
    const newSupportChart = {
      ...chartData.options,
      colors: [orangeDark],
      tooltip: {
        theme: 'light'
      }
    };
    ApexCharts.exec(`support-chart`, 'updateOptions', newSupportChart);
  }, [theme.palette.secondary, navType]);

  return (
    <Card sx={{ bgcolor: 'secondary.light' }}>
      <Grid container sx={{ p: 2, pb: 0, color: '#fff' }}>
        <Grid item xs={12}>
          <Grid container alignItems="center" justifyContent="space-between">
            <Grid item>
              <Typography variant="subtitle1" sx={{ color: theme.palette.secondary.dark }}>
                Bajaj Finery
              </Typography>
            </Grid>
            <Grid item>
              <Typography variant="h4" sx={{ color: theme.palette.grey[800] }}>
                $1839.00
              </Typography>
            </Grid>
          </Grid>
        </Grid>
        <Grid item xs={12}>
          <Typography variant="subtitle2" sx={{ color: theme.palette.grey[800] }}>
            10% Profit
          </Typography>
        </Grid>
      </Grid>
      <Chart {...chartData} />
      
      {/* Display top 3 popular stocks */}
      <Grid container justifyContent="center" mt={2}>
        {popularStocks.map((stock, index) => (
          <Grid container alignItems="center" key={index} sx={{ p: 1 }}>
            <Grid item xs={4}>
              <Typography variant="body1" sx={{ color: theme.palette.grey[800] }}>
                {stock.name}
              </Typography>
            </Grid>
            <Grid item xs={4}>
              <Typography variant="body1" sx={{ color: theme.palette.grey[800] }}>
                {stock.value}
              </Typography>
            </Grid>
            <Grid item xs={4}>
              {stock.direction === 'up' ? (
                <IconButton color="primary">
                  <ArrowUpwardIcon />
                </IconButton>
              ) : (
                <IconButton color="error">
                  <ArrowDownwardIcon />
                </IconButton>
              )}
            </Grid>
          </Grid>
        ))}
      </Grid>
    </Card>
  );
};

export default BajajAreaChartCard;
