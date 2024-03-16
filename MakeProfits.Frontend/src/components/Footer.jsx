import React from 'react';

const Footer = () => {
  return (
    <footer style={footerStyle}>
      <h1>This is our footer</h1>
    </footer>
  );
};

// Styles
const footerStyle = {
  backgroundColor: '#000',
  fontfamily:"poppins",
  color: '#fff',
  padding: '20px 0',
  textAlign: 'center',
};

const containerStyle = {
  display: 'flex',
  justifyContent: 'space-around',
};

const infoStyle = {
  flex: 1,
  margin: '0 20px',
};

const copyrightStyle = {
  marginTop: '20px',
};

export default Footer;
