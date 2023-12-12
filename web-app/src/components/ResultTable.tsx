import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { Box } from '@mui/system';

interface ResultItem {
    name: string;
    url: string;
}
interface ProcessingResult {
    projectName: string;
    results: ResultItem[];
}
export default function ResultTable() {
    const [processingResult, setProcessingResult] = React.useState<ProcessingResult>({ projectName: "", results: [] } as ProcessingResult);
    const [isLoading, setIsLoading] = React.useState<boolean>(false);
    const [error, setError] = React.useState<string>("");

    React.useEffect(() => {
        const fetchData = async () => {
            setIsLoading(true);
            try {
                const res = await fetch("http://localhost:7094/api/reactapp/result/eebf58c");
                setProcessingResult(await res.json() as ProcessingResult);
            } catch (ex: any) {
                setError(ex.toString());
            } finally {
                setIsLoading(false);
            }
        }
        fetchData();
    }, []);

    return (
        <Box>
            {isLoading && <p>加载中...</p>}
            {error && <p>{error}</p>}
            {!isLoading && !error &&
                <TableContainer component={Paper}>
                    <Table sx={{ minWidth: 650 }} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <TableCell>表单</TableCell>
                                <TableCell align="left">WebService API Url</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {processingResult.results.map((row) => (
                                <TableRow
                                    key={row.name}
                                    sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                                >
                                    <TableCell component="th" scope="row">
                                        {row.name}
                                    </TableCell>
                                    <TableCell align="left">{row.url}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>}
        </Box>
    );
}