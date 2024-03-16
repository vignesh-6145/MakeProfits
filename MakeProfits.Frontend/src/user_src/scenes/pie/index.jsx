import { Box } from "@mui/material";
import Header from "../../components/Header";
import PieArt from "../../components/PieChart";

const Pie = () => {
  return (
    <Box m="20px">
      <Header title="Pie Chart" subtitle="Simple Pie Chart" />
      <Box height="75vh">
        <PieArt />
      </Box>
    </Box>
  );
};

export default Pie;
