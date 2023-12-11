import React from 'react';
import './App.css';
import { BrowserRouter as BRouter, Route, Routes } from "react-router-dom";
import { NotFound } from './pages/NotFound';
import { Main } from './pages/Main';

function App() {
  return (
    <>
      <div className="App">
        <BRouter basename='/app'>
          <Routes>
            <Route path="/" element={<Main />} />
            <Route path="*" element={<NotFound />} />
          </Routes>
        </BRouter>
      </div>
    </>
  );
}

export default App;
