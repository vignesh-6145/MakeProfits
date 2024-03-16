import React from 'react';
import { Box, Button, TextField, Typography } from '@mui/material';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { styled } from '@mui/system';

const Container = styled(Box)({
  margin: 0,
  paddingTop: 0,
  color: '#2e323c',
  background: '#f5f6fa',
  position: 'relative',
  height: '100%',
});

const UserProfile = styled(Box)({
  margin: '0 0 1rem 0',
  paddingBottom: '1rem',
  textAlign: 'center',
});

const UserAvatar = styled('div')({
  margin: '0 0 1rem 0',
});

const AvatarImage = styled('img')({
  width: '90px',
  height: '90px',
  borderRadius: '100px',
});

const UserName = styled(Typography)({
  margin: '0 0 0.5rem 0',
});

const UserEmail = styled(Typography)({
  margin: 0,
  fontSize: '0.8rem',
  fontWeight: 400,
  color: '#9fa8b9',
});

const About = styled(Box)({
  margin: '2rem 0 0 0',
  textAlign: 'center',
});

const AboutTitle = styled(Typography)({
  margin: '0 0 15px 0',
  color: '#007ae1',
});

const FormContainer = styled(Box)({
  marginBottom: '1rem',
});

const Card = styled(Box)({
  background: '#ffffff',
  borderRadius: '5px',
  border: 0,
  marginBottom: '1rem',
});

const CancelButton = styled(Button)({
  marginRight: '8px',
});

const UpdateButton = styled(Button)({
  marginLeft: '8px',
});

const UserProfileComponent = () => (
  <UserProfile>
    <UserAvatar>
      <AvatarImage src="https://bootdey.com/img/Content/avatar/avatar7.png" alt="Advisor" />
    </UserAvatar>
    <UserName variant="h5">Advisor Name</UserName>
    <UserEmail variant="h6">abc@a.com</UserEmail>
  </UserProfile>
);

const AboutComponent = () => (
  <About>
    <AboutTitle variant="h5">About</AboutTitle>
    <Typography variant="body1">
      I'm ....I am an inverstment advisor...
    </Typography>
  </About>
);

const FormComponent = () => (
  <FormContainer>
    <Typography variant="h6" mb="2" color="primary">Personal Details</Typography>
    <Box display="flex" flexWrap="wrap">
      <Box flex="0 0 50%" pr="8px">
        <TextField fullWidth id="fullName" label="Full Name" variant="outlined" />
      </Box>
      <Box flex="0 0 50%" pl="8px">
        <TextField fullWidth id="eMail" label="Email" variant="outlined" />
      </Box>
      <Box flex="0 0 50%" pr="8px" mt="8px">
        <TextField fullWidth id="phone" label="Phone" variant="outlined" />
      </Box>
      <Box flex="0 0 50%" pl="8px" mt="8px">
        <TextField fullWidth id="website" label="Website URL" variant="outlined" />
      </Box>
    </Box>
    <Typography variant="h6" mt="3" mb="2" color="primary">Address</Typography>
    <Box display="flex" flexWrap="wrap">
      <Box flex="0 0 50%" pr="8px">
        <TextField fullWidth id="Street" label="Street" variant="outlined" />
      </Box>
      <Box flex="0 0 50%" pl="8px">
        <TextField fullWidth id="ciTy" label="City" variant="outlined" />
      </Box>
      <Box flex="0 0 50%" pr="8px" mt="8px">
        <TextField fullWidth id="sTate" label="State" variant="outlined" />
      </Box>
      <Box flex="0 0 50%" pl="8px" mt="8px">
        <TextField fullWidth id="zIp" label="Zip Code" variant="outlined" />
      </Box>
    </Box>
    <Box textAlign="right" mt="2">
      <CancelButton variant="contained">Cancel</CancelButton>
      <UpdateButton variant="contained" color="primary">Update</UpdateButton>
    </Box>
  </FormContainer>
);

const ProfilePage = () => (
  <Container>
    <div className="container">
      <div className="row gutters">
        <div className="col-xl-3 col-lg-3 col-md-12 col-sm-12 col-12">
          <Card>
            <UserProfileComponent />
            <AboutComponent />
          </Card>
        </div>
        <div className="col-xl-9 col-lg-9 col-md-12 col-sm-12 col-12">
          <Card>
            <FormComponent />
          </Card>
        </div>
      </div>
    </div>
  </Container>
);

export default ProfilePage;
