import { useState } from "react";
import { Routes, Route } from "react-router-dom";
import Topbar from "./advisor_src/scenes/global/Topbar";
import Sidebar from "./advisor_src/scenes/global/Sidebar";
import Dashboard from "./advisor_src/scenes/dashboard";
import Team from "./advisor_src/scenes/team";
import Invoices from "./advisor_src/scenes/invoices";
import Contacts from "./advisor_src/scenes/contacts";
import Bar from "./advisor_src/scenes/bar";
import ProfilePage from "./advisor_src/scenes/form";
import Line from "./advisor_src/scenes/line";
import Pie from "./advisor_src/scenes/pie";
// import Logins from "./advisor_src/scenes/Login";
import FAQ from "./advisor_src/scenes/faq";
import Geography from "./advisor_src/scenes/geography";
import { CssBaseline, ThemeProvider } from "@mui/material";
import { ColorModeContext, useMode } from "./advisor_src/theme";
import Calendar from "./advisor_src/scenes/calendar/calendar";
import ClientRatings from "./advisor_src/scenes/ratings";
import StrategyGrid from "./advisor_src/scenes/strategies";
//import { GoogleOAuthProvider } from "@react-oauth/google";

function Advisor() {
  const [theme, colorMode] = useMode();
  const [isSidebar, setIsSidebar] = useState(true);
  const [loggedIn,setLoggedIn] = useState(false)
  const handleLogin = (status)=>{
    console.log("Loggin stateus change");
    setLoggedIn(status)
  }
  return (
     // <GoogleOAuthProvider clientId="6374949853-ogg50phbim1cil9jvcsbhuep0tk9kmna.apps.googleusercontent.com">
<ColorModeContext.Provider value={colorMode}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <div className="app">
          <Sidebar isSidebar={isSidebar} />
          <main className="content">
            <Topbar setIsSidebar={setIsSidebar} />
            <Routes>
            <Route path="/dashboard" element={<Dashboard />} />
              <Route path="/team" element={<Team />} />
              <Route path="/contacts" element={<Contacts />} />
             {/* <Route path="/invoices" element={<Invoices />} /> */}
              <Route path="/ratings" element={<ClientRatings/>}/>
              <Route path="/strategies" element={<StrategyGrid/>}/>
              <Route path="/form" element={<ProfilePage />} />
              <Route path="/faq" element={<FAQ />} />
            {/*  <Route path="/" element={loggedIn ? <Dashboard /> : <Logins handleLogin={handleLogin}/>} />
              <Route path="/team" element={loggedIn ? <Team /> : <Logins handleLogin={handleLogin}/>} />
              <Route path="/contacts" element={loggedIn ? <Contacts /> : <Logins handleLogin={handleLogin}/>} />
              <Route path="/invoices" element={loggedIn ? <Invoices /> : <Logins handleLogin={handleLogin}/>} />
              <Route path="/ratings" element={loggedIn ? <ClientRatings/> : <Logins handleLogin={handleLogin}/> }/>
              <Route path="/form" element={loggedIn ? <ProfilePage /> : <Logins handleLogin={handleLogin}/> } />
              <Route path="/bar" element={loggedIn ? <Bar /> : <Logins handleLogin={handleLogin}/> } />
              <Route path="/pie" element={loggedIn ? <Pie /> : <Logins handleLogin={handleLogin}/> } />
              <Route path="/line" element={loggedIn ? <Line /> : <Logins handleLogin={handleLogin}/> } />
              <Route path="/faq" element={loggedIn ? <FAQ /> : <Logins handleLogin={handleLogin}/> } />
              <Route path="/calendar" element={loggedIn ? <Calendar /> : <Logins handleLogin={handleLogin}/> } />
          */}
            </Routes>
          </main>
        </div>
      </ThemeProvider>
    </ColorModeContext.Provider>
     // </GoogleOAuthProvider>
  );
}

export default Advisor;
