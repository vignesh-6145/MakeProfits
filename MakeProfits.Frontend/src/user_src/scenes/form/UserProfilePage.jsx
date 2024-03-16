import React from 'react';
import ProfileUpdateForm from './ProfileUpdateForm';

const UserProfilePage = () => {
  // Define initial values for the user's profile
  const initialValues = {
    firstName: 'John',
    lastName: 'Doe',
    email: 'john.doe@example.com',
    contact: '1234567890',
    address1: '123 Main Street',
    address2: 'Apt 101',
    qualifications: 'Bachelor of Science in Engineering',
    certifications: 'Certified Professional Engineer',
  };

  // Define a function to handle form submission
  const handleSubmit = (values) => {
    // Handle form submission logic here
    console.log('Form values:', values);
  };

  return (
    <div>
      <h1>User Profile</h1>
      <ProfileUpdateForm initialValues={initialValues} onSubmit={handleSubmit} />
    </div>
  );
};

export default UserProfilePage;
