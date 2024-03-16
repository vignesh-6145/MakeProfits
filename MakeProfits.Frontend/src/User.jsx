import { useState } from "react";
import { Routes, Route, Form } from "react-router-dom";
import Topbar from "./user_src/scenes/global/Topbar";
import Sidebar from "./user_src/scenes/global/Sidebar";
import Dashboard from "./user_src/scenes/dashboard";
import Team from "./user_src/scenes/team";
import Invoices from "./user_src/scenes/invoices";
import Contacts from "./user_src/scenes/contacts";
import Bar from "./user_src/scenes/bar";
import ProfilePage from "./user_src/scenes/form";
import Line from "./user_src/scenes/line";
import Pie from "./user_src/scenes/pie";
import Logins from "./user_src/scenes/Login";
import FAQ from "./user_src/scenes/faq";
import Geography from "./user_src/scenes/geography";
import { CssBaseline, ThemeProvider } from "@mui/material";
import { ColorModeContext,useMode } from "./user_src/theme";
import Calendar from "./user_src/scenes/calendar/calendar";
import ClientRatings from "./user_src/scenes/ratings";
import { GoogleOAuthProvider } from "@react-oauth/google";
import Quiz from "./components/quiz";

function User() {
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
              <Route path="/dashboard" element={<Dashboard /> } />
              <Route path="/team" element={<Team /> } />
              <Route path="/contacts" element={<Contacts/> } />
              <Route path="/form" element={<ProfilePage/> } />   
              <Route path='/quiz' element={<Quiz/>}/>
              {/* <Route path="/calender" element={<Calendar/> } /> */}
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

export default User;
