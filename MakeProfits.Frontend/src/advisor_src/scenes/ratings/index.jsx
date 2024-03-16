import React, { useState } from 'react';
import StarIcon from '@mui/icons-material/Star';
import StarBorderIcon from '@mui/icons-material/StarBorder';
import ThumbUpIcon from '@mui/icons-material/ThumbUp';
import ThumbDownIcon from '@mui/icons-material/ThumbDown';
import Header from '../../components/Header';
import SendIcon from '@mui/icons-material/Send';
import {
  Box,
  Paper,
  Grid,
  Typography,
  IconButton,
  TextField,
  Button,
  ButtonGroup
} from '@mui/material';

const ClientRatings = () => {
  const dummyClientData = [
    { id: 1, name: 'Client 1', rating: 4, color: 'gold', review: 'Great service and very helpful advice.', likes: 0, dislikes: 0 },
    { id: 2, name: 'Client 2', rating: 3, color: 'gold', review: 'Good experience overall, but could improve communication.', likes: 0, dislikes: 0 },
    { id: 3, name: 'Client 3', rating: 5, color: 'gold', review: 'Exceptional service! Highly recommend.', likes: 0, dislikes: 0 },
    { id: 4, name: 'Client 4', rating: 2, color: 'gold', review: 'Disappointed with the lack of follow-up on my inquiries.', likes: 0, dislikes: 0 },
    { id: 5, name: 'Client 5', rating: 4, color: 'gold', review: 'Very satisfied with the results and professionalism.', likes: 0, dislikes: 0 },
  ];

  const [comments, setComments] = useState(Array(dummyClientData.length).fill(''));
  const [commentText, setCommentText] = useState('');

  const handleCommentChange = (index, text) => {
    const newComments = [...comments];
    newComments[index] = text;
    setComments(newComments);
  };

  const handleCommentSubmit = (index) => {
    console.log('Submitted comment:', comments[index]);
    setComments((prevComments) => {
      const newComments = [...prevComments];
      newComments[index] = '';
      return newComments;
    });
  };

  const handleLike = (index) => {
    console.log('Liked:', dummyClientData[index]);
  };

  const handleDislike = (index) => {
    console.log('Disliked:', dummyClientData[index]);
  };

  // Calculate average rating
  const totalRatings = dummyClientData.reduce((acc, client) => acc + client.rating, 0);
  const averageRating = totalRatings / dummyClientData.length;

  return (
    <div style={{ padding: '20px' }}>
      <Box>
        <Header title="REVIEWS" />
      </Box>

      {/* Display average rating */}
      <Box display="flex" alignItems="center" mb={2} justifyContent="center">
        <Typography variant="h4" mr={1}>
          Average Ratings:
        </Typography>
        {Array.from({ length: Math.round(averageRating) }, (_, i) => (
          <StarIcon key={i} sx={{ color: 'gold' }} />
        ))}
        {Array.from({ length: 5 - Math.round(averageRating) }, (_, i) => (
          <StarBorderIcon key={i} sx={{ color: 'gold' }} />
        ))}
        <Typography variant="body2" color="text.secondary" ml={1}>
          {averageRating.toFixed(2)}
        </Typography>
      </Box>

      <Grid container spacing={2}>
        {dummyClientData.map((client, index) => (
          <Grid item key={client.id} xs={12} sm={6} md={4}>
            <Paper elevation={3} sx={{ p: 2, height: '100%' }}>
              <Typography variant="h6" gutterBottom>
                {client.name}
              </Typography>
              <Box display="flex" alignItems="center" mb={1}>
                {Array.from({ length: client.rating }, (_, i) => (
                  <StarIcon key={i} sx={{ color: client.color }} />
                ))}
                {Array.from({ length: 5 - client.rating }, (_, i) => (
                  <StarBorderIcon key={i} sx={{ color: client.color }} />
                ))}
              </Box>
              <Typography variant="body1" gutterBottom>
                {client.review}
              </Typography>
              <ButtonGroup size="small" aria-label="like and dislike buttons" sx={{ mb: 1 }}>
                <IconButton onClick={() => handleLike(index)}>
                  <ThumbUpIcon />
                </IconButton>
                <Typography variant="body2">{client.likes}</Typography>
                <IconButton onClick={() => handleDislike(index)}>
                  <ThumbDownIcon />
                </IconButton>
                <Typography variant="body2">{client.dislikes}</Typography>
              </ButtonGroup>
              <TextField
                label="Add Comment"
                variant="standard"
                fullWidth
                value={comments[index]}
                onChange={(e) => handleCommentChange(index, e.target.value)}
                sx={{ mt: 1 }}
              />
              <IconButton onClick={() => handleCommentSubmit(index)}>
                <SendIcon />
              </IconButton>
            </Paper>
          </Grid>
        ))}
      </Grid>
    </div>
  );
};

export default ClientRatings;
