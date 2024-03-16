import { useState } from "react";
import { Routes, Route } from "react-router-dom";
import Topbar from "./scenes/global/Topbar";
import Sidebar from "./scenes/global/Sidebar";
import Dashboard from "./scenes/dashboard";
import Team from "./scenes/team";
import Invoices from "./scenes/invoices";
import Contacts from "./scenes/contacts";
import Bar from "./scenes/bar";
import ProfilePage from "./scenes/form";
import Line from "./scenes/line";
import Pie from "./scenes/pie";
import Logins from "./scenes/Login";
import FAQ from "./scenes/faq";
import Geography from "./scenes/geography";
import { CssBaseline, ThemeProvider } from "@mui/material";
import { ColorModeContext, useMode } from "./theme";
import Calendar from "./scenes/calendar/calendar";
import ClientRatings from "./scenes/ratings";
import { GoogleOAuthProvider } from "@react-oauth/google";

function App() {
  const [theme, colorMode] = useMode();
  const [isSidebar, setIsSidebar] = useState(true);
  const [loggedIn,setLoggedIn] = useState(false)
  const handleLogin = (status)=>{
    console.log("Loggin stateus change");
    setLoggedIn(status)
  }
  return (
      <GoogleOAuthProvider clientId="6374949853-ogg50phbim1cil9jvcsbhuep0tk9kmna.apps.googleusercontent.com">
<ColorModeContext.Provider value={colorMode}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <div className="app">
          <Sidebar isSidebar={isSidebar} />
          <main className="content">
            <Topbar setIsSidebar={setIsSidebar} />
            <Routes>
              
              {/* <Route path="/" element={loggedIn ? <Dashboard /> : <Logins handleLogin={handleLogin}/>} /> */}
              <Route path="/" element={<Dashboard /> } />
              <Route path="/team" element={<Team /> } />
              <Route path="/contacts" element={<Contacts/> } />
              <Route path="/form" element={<ProfilePage/> } />
              <Route path="/calender" element={<Calendar/> } />
              <Route path="/faq" element={<FAQ/> } />
              {/* <Route path="/team" element={loggedIn ? <Team /> : <Logins handleLogin={handleLogin}/>} /> */}
              {/* <Route path="/contacts" element={loggedIn ? <Contacts /> : <Logins handleLogin={handleLogin}/>} />
              <Route path="/invoices" element={loggedIn ? <Invoices /> : <Logins handleLogin={handleLogin}/>} />
              <Route path="/ratings" element={loggedIn ? <ClientRatings/> : <Logins handleLogin={handleLogin}/> }/>
              <Route path="/form" element={loggedIn ? <ProfilePage /> : <Logins handleLogin={handleLogin}/> } />
              <Route path="/bar" element={loggedIn ? <Bar /> : <Logins handleLogin={handleLogin}/> } />
              <Route path="/pie" element={loggedIn ? <Pie /> : <Logins handleLogin={handleLogin}/> } />
              <Route path="/line" element={loggedIn ? <Line /> : <Logins handleLogin={handleLogin}/> } />
              <Route path="/faq" element={loggedIn ? <FAQ /> : <Logins handleLogin={handleLogin}/> } />
              <Route path="/calendar" element={loggedIn ? <Calendar /> : <Logins handleLogin={handleLogin}/> } />
              <Route path="/geography" element={loggedIn ? <Geography /> : <Logins handleLogin={handleLogin}/> } /> */}
            </Routes>
          </main>
        </div>
      </ThemeProvider>
    </ColorModeContext.Provider>
      </GoogleOAuthProvider>
  );
}

export default App;
