import React from 'react';
import StarIcon from '@mui/icons-material/Star';
import StarBorderIcon from '@mui/icons-material/StarBorder';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper } from '@mui/material';

// Dummy data for client ratings and reviews
const dummyClientData = [
  { name: 'Client 1', rating: 4, color: 'gold', review: 'Great service and very helpful advice.' },
  { name: 'Client 2', rating: 3, color: 'gold', review: 'Good experience overall, but could improve communication.' },
  { name: 'Client 3', rating: 5, color: 'gold', review: 'Exceptional service! Highly recommend.' },
  { name: 'Client 4', rating: 2, color: 'red', review: 'Disappointed with the lack of follow-up on my inquiries.' },
  { name: 'Client 5', rating: 4, color: 'gold', review: 'Very satisfied with the results and professionalism.' },
];

const ClientRatings = () => {
  return (
    <div >
      <h1>Ratings and Reviews</h1>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Client Name</TableCell>
              <TableCell>Rating</TableCell>
              <TableCell>Review</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {dummyClientData.map((client, index) => (
              <TableRow key={index}>
                <TableCell>{client.name}</TableCell>
                <TableCell>
                  {Array.from({ length: client.rating }, (_, i) => (
                    <StarIcon key={i} sx={{ color: client.color }} />
                  ))}
                  {Array.from({ length: 5 - client.rating }, (_, i) => (
                    <StarBorderIcon key={i} sx={{ color: client.color }} />
                  ))}
                </TableCell>
                <TableCell>{client.review}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};

export default ClientRatings;
