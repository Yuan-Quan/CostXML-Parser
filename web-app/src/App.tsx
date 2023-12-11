import React from 'react';
import './App.css';
import { BrowserRouter as BRouter, Route, Routes } from "react-router-dom";
import { NotFound } from './pages/NotFound';
import { Main } from './pages/Main';
import { NewProject } from './pages/NewProject';

function App() {
  return (
    <>
      <div className="App">
        <BRouter basename='/app'>
          <Routes>
            <Route path="/" element={<Main />} />
            <Route path="/new-project" element={<NewProject />} />
            <Route path="*" element={<NotFound />} />
          </Routes>
        </BRouter>
      </div>
    </>
  );
}

export default App;
