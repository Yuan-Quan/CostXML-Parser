import { is } from '@babel/types';
import { FormControl, InputLabel, MenuItem, Select, SelectChangeEvent, Typography } from '@mui/material';
import { Box } from '@mui/system';
import * as React from 'react';
import { AppContext } from '../App';

interface Selection {
    fileName: string;
    dateUploaded: string;
}

export default function ProcessingFileSelection() {
    const [selections, setSelections] = React.useState<Selection[]>([]);
    const [isLoading, setIsLoading] = React.useState<boolean>(false);
    const [error, setError] = React.useState<string>("");
    const [selectedFile, setSelectedFile] = React.useState<string>("");
    const { currentProjectName, setCurrentProjectName } = React.useContext(AppContext);

    React.useEffect(() => {
        const fetchData = async () => {
            var selections = [] as Selection[];
            try {
                const res = await fetch("http://43.163.205.191:8080/api/reactapp/uploaded-projects");
                selections = await res.json() as Selection[];
            } catch (ex: any) {
                setError(ex.toString());
            } finally {
                setIsLoading(false);
            }
            setSelections(selections);
            setSelectedFile(selections[0].fileName);
            setCurrentProjectName(selections[0].fileName);
        }
        fetchData();
    }, []);

    const handleSelect = (event: SelectChangeEvent) => {
        setSelectedFile(event.target.value);
        setCurrentProjectName(event.target.value);
    }

    return (
        <Box>
            {isLoading && <p>加载中...</p>}
            {error && <p>{error}</p>}
            {!isLoading && !error && selections.length > 0 &&
                <Box>
                    <FormControl fullWidth>

                        <InputLabel id="demo-simple-select-label">File</InputLabel>
                        <Select
                            labelId="demo-simple-select-label"
                            id="demo-simple-select"
                            value={selectedFile}
                            label="File"
                            onChange={handleSelect}
                        >
                            {selections.map((selection) => (
                                <MenuItem value={selection.fileName}>{selection.fileName}</MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                </Box>
            }
        </Box>
    )
}