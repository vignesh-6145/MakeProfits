import React, { useState } from 'react';
import {
  Grid,
  Card,
  CardHeader,
  CardContent,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Button,Box,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
} from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import Header from '../../components/Header';

const StrategyGrid = () => {
  const [strategies, setStrategies] = useState([
    {
      id: 1,
      strategyType: 'Strategy Type ',
      stockId: 'STK001',
      mfId: 'MF001',
      bondsId: 'BND001',
      stockPercentage: 40,
      mfPercentage: 30,
      bondsPercentage: 30,
    },
    {
      id: 2,
      strategyType: 'Strategy Type',
      stockId: 'STK002',
      mfId: 'MF002',
      bondsId: 'BND002',
      stockPercentage: 50,
      mfPercentage: 25,
      bondsPercentage: 25,
    },
    {
      id: 3,
      strategyType: 'Strategy Type',
      stockId: 'STK003',
      mfId: 'MF003',
      bondsId: 'BND003',
      stockPercentage: 60,
      mfPercentage: 20,
      bondsPercentage: 20,
    },
  ]);
  const [openEditDialog, setOpenEditDialog] = useState(false);
  const [openAddDialog, setOpenAddDialog] = useState(false);
  const [selectedStrategy, setSelectedStrategy] = useState(null);
  const [editFormValues, setEditFormValues] = useState({
    stockPercentage: '',
    mfPercentage: '',
    bondsPercentage: '',
  });
  const [addFormValues, setAddFormValues] = useState({
    strategyType: '',
    stockId: '',
    mfId: '',
    bondsId: '',
    stockPercentage: '',
    mfPercentage: '',
    bondsPercentage: '',
  });

  const handleOpenEditDialog = (strategy) => {
    setSelectedStrategy(strategy);
    setEditFormValues({
      stockPercentage: strategy.stockPercentage,
      mfPercentage: strategy.mfPercentage,
      bondsPercentage: strategy.bondsPercentage,
    });
    setOpenEditDialog(true);
  };

  const handleCloseEditDialog = () => {
    setSelectedStrategy(null);
    setEditFormValues({
      stockPercentage: '',
      mfPercentage: '',
      bondsPercentage: '',
    });
    setOpenEditDialog(false);
  };

  const handleOpenAddDialog = () => {
    setOpenAddDialog(true);
  };

  const handleCloseAddDialog = () => {
    setAddFormValues({
      strategyType: '',
      stockId: '',
      mfId: '',
      bondsId: '',
      stockPercentage: '',
      mfPercentage: '',
      bondsPercentage: '',
    });
    setOpenAddDialog(false);
  };

  const handleSaveEdit = () => {
    // Implement save edit logic
    console.log('Saving edit:', editFormValues);
    handleCloseEditDialog();
  };

  const handleSaveAdd = () => {
    // Implement save add logic
    console.log('Saving add:', addFormValues);
    handleCloseAddDialog();
  };

  const handleDeleteStrategy = (strategyId) => {
    // Implement delete logic
    console.log('Deleting strategy with ID:', strategyId);
    // Update state or perform API call to remove the strategy from the list
  };

  return (
    <div style={{ padding: '20px' }}>
        <Box>
            <Header title="Strategy Plans"/>
        </Box>
      <Grid container spacing={3}>
        {strategies.map((strategy) => (
          <Grid item xs={12} key={strategy.id}>
            <Card>
              <CardHeader
                title={strategy.strategyType}
                action={
                  <div>
                    <Button onClick={() => handleOpenEditDialog(strategy)} startIcon={<EditIcon />} color="primary">Edit</Button>
                    <Button onClick={() => handleDeleteStrategy(strategy.id)} startIcon={<DeleteIcon />} color="primary">Delete</Button>
                  </div>
                }
              />
              <CardContent>
                <TableContainer>
                  <Table>
                    <TableHead>
                      <TableRow>
                        <TableCell>Stocks</TableCell>
                        <TableCell>MF</TableCell>
                        <TableCell>Bonds</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {/*<TableRow>
                        <TableCell>ID: {strategy.stockId}</TableCell>
                        <TableCell>ID: {strategy.mfId}</TableCell>
                        <TableCell>ID: {strategy.bondsId}</TableCell>
            </TableRow>*/}
                      <TableRow>
                        <TableCell>Percentage: {strategy.stockPercentage}%</TableCell>
                        <TableCell>Percentage: {strategy.mfPercentage}%</TableCell>
                        <TableCell>Percentage: {strategy.bondsPercentage}%</TableCell>
                      </TableRow>
                    </TableBody>
                  </Table>
                </TableContainer>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>

      {/* Add button */}
      <Button onClick={handleOpenAddDialog} startIcon={<AddIcon />} color="primary">Add</Button>

      {/* Edit dialog */}
      <Dialog open={openEditDialog} onClose={handleCloseEditDialog}>
        <DialogTitle>Edit Strategy</DialogTitle>
        <DialogContent>
  <Box mt={2}mb={2}>
    <TextField 
      label="Stock Percentage"
      value={editFormValues.stockPercentage}
      onChange={(e) => setEditFormValues({ ...editFormValues, stockPercentage: e.target.value })}
      fullWidth
    />
  </Box>
  <Box mb={2}>
    <TextField
      label="MF Percentage"
      value={editFormValues.mfPercentage}
      onChange={(e) => setEditFormValues({ ...editFormValues, mfPercentage: e.target.value })}
      fullWidth
    />
  </Box>
  <TextField
    label="Bonds Percentage"
    value={editFormValues.bondsPercentage}
    onChange={(e) => setEditFormValues({ ...editFormValues, bondsPercentage: e.target.value })}
    fullWidth
  />
</DialogContent>

        <DialogActions>
          <Button onClick={handleCloseEditDialog}>Cancel</Button>
          <Button onClick={handleSaveEdit} color="primary">Save</Button>
        </DialogActions>
      </Dialog>

      {/* Add dialog */}
      <Dialog open={openAddDialog} onClose={handleCloseAddDialog}>
        <DialogTitle>Add Strategy</DialogTitle>
        <DialogContent>
          {/* Add inputs for all form fields */}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseAddDialog}>Cancel</Button>
          <Button onClick={handleSaveAdd} color="primary">Save</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
};

export default StrategyGrid;

