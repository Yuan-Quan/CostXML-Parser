import { Checkbox, FormControl, FormControlLabel, FormGroup, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import { Box, width } from '@mui/system';
import * as React from 'react';

interface ProcessingMethod {
    processName: string;
    processDescription: string;
}

interface CheckedState {
    name: string;
    checked: boolean;
}

export default function ProcessingMethodSelection() {
    const [methods, setMethods] = React.useState<ProcessingMethod[]>([]);
    const [isLoading, setIsLoading] = React.useState<boolean>(false);
    const [error, setError] = React.useState<string>("");
    const [checkboxes, setCheckboxes] = React.useState<CheckedState[]>([]);

    React.useEffect(() => {
        const fetchData = async () => {
            setIsLoading(true);
            try {
                const res = await fetch("http://43.163.205.191:8080/api/reactapp/avaliable-processes");
                setMethods(await res.json() as ProcessingMethod[]);
            } catch (ex: any) {
                setError(ex.toString());
            } finally {
                setIsLoading(false);
            }
        }
        fetchData();
    }, []);

    React.useEffect(() => {
        if (methods.length > 0) {
            setCheckboxes(methods.map((method) => {
                return {
                    name: method.processName,
                    checked: true,
                }
            }));
        }
    }, [methods]);

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCheckboxes(checkboxes.map((item) => {
            if (item.name === event.target.name) {
                return {
                    name: item.name,
                    checked: event.target.checked,
                }
            }
            return item;
        }));
    }


    return (
        <Box sx={{}}>
            <FormGroup>
                {isLoading && <p>加载中...</p>}
                {error && <p>{error}</p>}
                {!isLoading && !error && checkboxes.map((item) => (
                    <FormControlLabel control={<Checkbox checked={item.checked} onChange={handleChange} name={item.name} />} label={item.name} />
                ))}
            </FormGroup>
        </Box>
    );


}