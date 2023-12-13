import { is } from '@babel/types';
import { FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import { Box } from '@mui/system';
import * as React from 'react';

interface Selection {
    fileName: string;
    dateUploaded: string;
}

export default function ProcessingFileSelection() {
    const [selections, setSelections] = React.useState<Selection[]>([]);
    const [isLoading, setIsLoading] = React.useState<boolean>(false);
    const [error, setError] = React.useState<string>("");
    const [selectedFile, setSelectedFile] = React.useState<string>("");

    React.useEffect(() => {
        const fetchData = async () => {
            setIsLoading(true);
            try {
                const res = await fetch("http://43.163.205.191:8080/api/reactapp/uploaded-projects");
                setSelections(await res.json() as Selection[]);
            } catch (ex: any) {
                setError(ex.toString());
            } finally {
                setIsLoading(false);
            }
        }
        fetchData();
    }, []);

    React.useEffect(() => {
        if (selections.length > 0) {
            setSelectedFile(selections[0].fileName);
        }
    }, [selections]);

    const handleSelect = (event: SelectChangeEvent) => {
        setSelectedFile(event.target.value);
    }
    return (
        <Box>
            {isLoading && <p>加载中...</p>}
            {error && <p>{error}</p>}
            {!isLoading && !error && selections.length > 0 &&
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
            }
        </Box>
    )
}