import { Box, Button,Modal,TextField, IconButton, Typography, useTheme } from "@mui/material";
import { tokens } from "../../theme";
import { mockTransactions } from "../../data/mockData";
import DownloadOutlinedIcon from "@mui/icons-material/DownloadOutlined";
import CurrencyRupeeIcon from '@mui/icons-material/CurrencyRupee';
import EmailIcon from "@mui/icons-material/Email";
import PointOfSaleIcon from "@mui/icons-material/PointOfSale";
import PersonAddIcon from "@mui/icons-material/PersonAdd";
import TrafficIcon from "@mui/icons-material/Traffic";
import Header from "../../components/Header";
import LineChart from "../../components/LineChart";
import GeographyChart from "../../components/GeographyChart";
import BarChart from "../../components/BarChart";
import StatBox from "../../components/StatBox";
import ProgressCircle from "../../components/ProgressCircle";
import BajajAreaChartCard from "./BajajAreaChartCard";
import chartData from "./bajaj-area-chart";
import PieArt from "../../components/PieChart";
import ShowChartIcon from '@mui/icons-material/ShowChart';
import { Line, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';
import React, { useState } from 'react';
import { Link } from "react-router-dom";
import AttachMoneyIcon from '@mui/icons-material/AttachMoney';


const data = [
  { name: 'Jan', uv: 4000 },
  { name: 'Feb', uv: 3000 },
  { name: 'Mar', uv: 2000 },
  { name: 'Apr', uv: 2780 },
  { name: 'May', uv: 1890 },
  { name: 'Jun', uv: 2390 },
  { name: 'Jul', uv: 3490 },
  { name: 'Aug', uv: 4000 },
  { name: 'Sep', uv: 3000 },
  { name: 'Oct', uv: 2000 },
  { name: 'Nov', uv: 2780 },
  { name: 'Dec', uv: 1890 },
];

// const InvestmentModal = ({ isOpen, onClose }) => {
//   const [investmentAmount, setInvestmentAmount] = useState('');
//   const [selectedOption, setSelectedOption] = useState('');

//   const handleInvestmentSubmit = () => {
//     // Add logic to handle investment submission
//     console.log('Investment Amount:', investmentAmount);
//     console.log('Selected Option:', selectedOption);
//     // Close the modal after submission
//     onClose();
//   };

//   return (
//     <Modal open={isOpen} onClose={onClose}>
//       <Box sx={{ width: 400, bgcolor: 'background.paper', p: 2, marginLeft: 50, marginRight :50, marginTop :10,}}>
//         <Typography variant="h5" sx={{ mb: 2 }}>Enter Investment Details</Typography>
//         <TextField
//           label="Amount to Invest"
//           variant="outlined"
//           fullWidth
//           value={investmentAmount}
//           onChange={(e) => setInvestmentAmount(e.target.value)}
//           sx={{ mb: 2 }}
//         />
//         <Typography variant="h6" sx={{ mb: 2 }}>Select Investment Option:</Typography>
//         <Box>
//           <Button
//             variant="contained"
//             color="primary"
//             onClick={() => setSelectedOption('advisor1')}
//             sx={{ mr: 2, mb: 1 }}
//           >
//             Advisor - Gaurav
//             Mutual Funds: 40%
//             Gold: 15%
//             Stocks: 40%
//             Bonds: 5%
//           </Button>
//           <Button
//             variant="contained"
//             color="primary"
//             onClick={() => setSelectedOption('advisor2')}
//             sx={{ mr: 2, mb: 1 }}
//           >
//             Advisor - Jatin
//             Mutual Funds: 30%
//             Gold: 5%
//             Stocks: 60%
//             Bonds: 5%
//           </Button>
//           <Button
//             variant="contained"
//             color="primary"
//             onClick={() => setSelectedOption('advisor3')}
//             sx={{ mr: 2, mb: 1 }}
//           >
//             Advisor - Raghav
//             Mutual Funds: 45%
//             Gold: 10%
//             Stocks: 40%
//             Bonds: 5%
//           </Button>
//           <Button
//             variant="contained"
//             color="primary"
//             onClick={() => setSelectedOption('advisor4')}
//             sx={{ mr: 2, mb: 1 }}
//           >
//             Advisor - Abhishek
//             Mutual Funds: 25%
//             Gold: 20%
//             Stocks: 45%
//             Bonds: 10%
//           </Button>
//           {/* <Button
//             variant="contained"
//             color="primary"
//             onClick={() => setSelectedOption('bonds')}
//             sx={{ mb: 1 }}
//           >
//             Advisor
//             MF
//             Gold
//             Stocks
//             bonds
//           </Button> */}
//         </Box>
//         <Button variant="contained" color="primary" onClick={handleInvestmentSubmit} sx={{ mt: 2 }}>
//           Submit
//         </Button>
//       </Box>
//     </Modal>
//   );
// };



const Dashboard = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [isInvestmentModalOpen, setIsInvestmentModalOpen] = useState(false);

  return (
    <Box m="20px">
      {/* HEADER */}
      <Box display="flex" justifyContent="space-between" alignItems="center">
        <Header title="WELCOME"  />

       {/* <Box>
          <Button
            sx={{
              backgroundColor: colors.blueAccent[700],
              color: colors.grey[100],
              fontSize: "14px",
              fontWeight: "bold",
              padding: "10px 20px",
            }}
          >
            <DownloadOutlinedIcon sx={{ mr: "10px" }} />
            Download Reports
          </Button>
          </Box>*/}
      </Box>
    
      {/* GRID & CHARTS */}
      <Box
        display="grid"
        gridTemplateColumns="repeat(12, 1fr)"
        gridAutoRows="140px"
        gap="20px"
      >
        {/* ROW 1 */}
        <Box
          gridColumn="span 3"
          backgroundColor={colors.primary[400]}
          display="flex"
          alignItems="center"
          justifyContent="center"
        >
          <StatBox
            subtitle="TOTAL INVESTED"
            title="1cr"
           
            progress="0.75"
            increase="+14%"
            icon={
              <AttachMoneyIcon
                sx={{ color: colors.greenAccent[600], fontSize: "26px" }}
              />
            }
        
          />
        </Box>
      {/*  <Box
          gridColumn="span 3"
          backgroundColor={colors.primary[400]}
          display="flex"
          alignItems="center"
          justifyContent="center"
        >
          <StatBox
            title="431,225"
            subtitle="Sales Obtained"
            progress="0.50"
            increase="+21%"
            icon={
              <PointOfSaleIcon
                sx={{ color: colors.greenAccent[600], fontSize: "26px" }}
              />
            }
          />
          </Box>
          */}  
        <Box
          gridColumn="span 3"
          backgroundColor={colors.primary[400]}
          display="flex"
          alignItems="center"
          justifyContent="center"
          transition="transform 0.3s ease-in-out"
        >
          <StatBox
            title="60%"
            subtitle=" ADVISOR PROFIT"
            progress="0.30"
            increase="+5%"
            icon={
              <PersonAddIcon
                sx={{ color: colors.greenAccent[600], fontSize: "26px" }}
              />
            }
          />
        </Box>
        <Box
          gridColumn="span 3"
          backgroundColor={colors.primary[400]}
          display="flex"
          alignItems="center"
          justifyContent="center"
        >
          <StatBox
      title="70%"
      subtitle="MY PROFIT"
      progress="0.80"
      increase="+43%"
      icon={
        <ShowChartIcon
          sx={{ color: colors.greenAccent[600], fontSize: "26px" }}
        />
      }
    />
    
          </Box> 
          <Box
          gridColumn="span 3"
          backgroundColor='transparent'
          display="flex"
          alignItems="center"
          justifyContent="center"
        >
          {/* <Typography>Top Client</Typography> */}
          <Link to='/quiz'>
          <Button
            variant="contained"
            color="primary"
            to="/quiz"
            onClick={() => {
              setIsInvestmentModalOpen(true)
            }}
          >
            Invest Now
          </Button>
          </Link>
          </Box> 

          {/* <InvestmentModal isOpen={isInvestmentModalOpen} onClose={() => setIsInvestmentModalOpen(false)} /> */}
        

        {/* ROW 2 */}
        <Box
          gridColumn="span 8"
          gridRow="span 2"
          backgroundColor={colors.primary[400]}
        >
          <Box
            mt="25px"
            p="0 30px"
            display="flex "
            justifyContent="space-between"
            alignItems="center"
          >
            <Box>
              <Typography
                variant="h5"
                fontWeight="600"
                color={colors.grey[100]}
              >
                Investment Performance Comparison
              </Typography>
              <Typography
                variant="h3"
                fontWeight="bold"
                color={colors.greenAccent[500]}
              >
                $59,342.32
              </Typography>
            </Box>
           {/*} <Box>
              <IconButton>
                <DownloadOutlinedIcon
                  sx={{ fontSize: "26px", color: colors.greenAccent[500] }}
                />
              </IconButton>
    </Box>  */}
          </Box>
          <Box height="250px" m="-20px 0 0 0">
            <LineChart isDashboard={true} />
          </Box>
        </Box>
        <Box
          gridColumn="span 4"
          gridRow="span 2"
          backgroundColor={colors.primary[400]}
          padding="30px"
         // overflow="auto"
        >
        
            <Typography color={colors.grey[100]} variant="h5" fontWeight="600" >
              Asset Allocation
            </Typography>
            <Box  
               mt="100px"
               ml="30px"
               p="0 30px"
               display="flex "
               justifyContent="space-between"
               alignItems="center"
               >
              <ProgressCircle size={150}/>
            </Box>
          
        </Box>

        {/* ROW 3 */}
        <Box
          gridColumn="span 8"
          gridRow="span 3"
          backgroundColor={colors.primary[400]}
          p="30px"
          overflow="auto"
        >
           <Box
            display="flex"
            justifyContent="space-between"
            alignItems="center"
            borderBottom={`2px solid ${colors.primary[500]}`}
            colors={colors.grey[100]}
            p="15px"
          >
            <Typography color={colors.grey[100]} variant="h5" fontWeight="600">
             Alerts on Recent Transactions
            </Typography>
          </Box>
          {mockTransactions.map((transaction, i) => (
            <Box
              key={`${transaction.txId}-${i}`}
              display="flex"
              justifyContent="space-between"
              alignItems="center"
              borderBottom={`1px solid ${colors.primary[500]}`}
              p="15px"
            >
              <Box>
                <Typography
                  color={colors.greenAccent[500]}
                  variant="h5"
                  fontWeight="600"
                >
                  {transaction.txId}
                </Typography>
                <Typography color={colors.grey[100]}>
                  {transaction.user}
                </Typography>
              </Box>
              <Box color={colors.grey[100]}>{transaction.date}</Box>
              <Box
                backgroundColor={colors.greenAccent[500]}
                p="5px 10px"
                borderRadius="4px"
              >
                ${transaction.cost}
              </Box>
            </Box>
          ))}
         {/* <Typography variant="h5" fontWeight="600">
            Alerts on recent transactions
          </Typography>
          
         <Box m="20px">
         
       <Box height="25vh">
        <PieArt />
      </Box>
          </Box> */}
          {/*<Box
            display="flex"
            flexDirection="column"
            alignItems="center"
            mt="25px"
          >
            <PieArt />
            <Typography
              variant="h5"
              color={colors.greenAccent[500]}
              sx={{ mt: "15px" }}
            >
            
            </Typography>
            <Typography>Includes asset allocation</Typography>
          </Box> */}
        </Box>
       {/* <Box
          gridColumn="span 4"
          gridRow="span 2"
          backgroundColor={colors.primary[400]}
        >
          <Typography
            variant="h5"
            fontWeight="600"
            sx={{ padding: "30px 30px 0 30px" }}
          >
            Sales Quantity
          </Typography>
          <Box height="250px" mt="-20px">
            <BarChart isDashboard={true} />
          </Box>
        </Box> */}
        <Box gridColumn="span 4"
          gridRow="span 3"
          backgroundColor={colors.primary[400]}
          padding="30px">
             <Typography
            variant="h5"
            fontWeight="600"
            sx={{ marginBottom: "15px" }}
          >
            Popular Stock
          </Typography>
          <BajajAreaChartCard chartData={chartData}/>
        </Box>
        {/*<Box
          gridColumn="span 4"
          gridRow="span 2"
          backgroundColor={colors.primary[400]}
          padding="30px"
        >
          <Typography
            variant="h5"
            fontWeight="600"
            sx={{ marginBottom: "15px" }}
          >
            Geography Based Traffic
          </Typography>
          <Box height="200px">
            <GeographyChart isDashboard={true} />
          </Box>
          </Box>*/}
      </Box>
    </Box>
  );
};

export default Dashboard;
