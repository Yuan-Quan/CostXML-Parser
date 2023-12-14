import React from 'react';
import './App.css';
import { BrowserRouter as BRouter, Route, Routes } from "react-router-dom";
import { NotFound } from './pages/NotFound';
import { Main } from './pages/Main';
import { NewProject } from './pages/NewProject';
import ProjectFileUpload from './components/ProjectFileUpload';
import ProcessingConfiguration from './components/ProcessingConfiguration';
import ProcessingResult from './components/ProcessingResult';

interface IAppContext {
  currentProjectName: string;
  setCurrentProjectName: React.Dispatch<React.SetStateAction<string>>;
}

export const AppContext = React.createContext<IAppContext>(null!);

function App() {
  const [currentProjectName, setCurrentProjectName] = React.useState<string>(() => {
    const hash = localStorage.getItem("currentProjectHash");
    return hash ? hash : "";
  });

  React.useEffect(() => {
    localStorage.setItem("currentProjectHash", currentProjectName);
  }, [currentProjectName]);

  return (
    <>
      <div className="App">
        <AppContext.Provider value={{ currentProjectName, setCurrentProjectName }}>
          <BRouter basename='/'>
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
        </AppContext.Provider>
      </div>
    </>
  );
}

export default App;
