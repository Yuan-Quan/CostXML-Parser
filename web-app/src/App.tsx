import React from 'react';
import './App.css';
import { BrowserRouter as BRouter, Route, Routes } from "react-router-dom";
import { NotFound } from './pages/NotFound';
import { Main } from './pages/Main';
import { NewProject } from './pages/NewProject';
import ProjectFileUpload from './components/ProjectFileUpload';
import ProcessingConfiguration from './components/ProcessingConfiguration';
import ProcessingResult from './components/ProcessingResult';

function App() {
  return (
    <>
      <div className="App">
        <BRouter basename='/app'>
          <Routes>
            <Route path="/" element={<Main />} />
            <Route path="/new" element={<NewProject />} >
              <Route path="upload" element={<ProjectFileUpload />} />
              <Route path='config-processing' element={<ProcessingConfiguration />} />
              <Route path='result' element={<ProcessingResult />} />
            </Route>
            <Route path="*" element={<NotFound />} />
          </Routes>
        </BRouter>
      </div>
    </>
  );
}

export default App;
