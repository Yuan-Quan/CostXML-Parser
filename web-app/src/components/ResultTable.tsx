import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { Box } from '@mui/system';
import { Button, Typography } from '@mui/material';
import { CopyToClipboard } from 'react-copy-to-clipboard';
import './ResultTable.css';
import { AppContext } from '../App';

interface ResultItem {
    name: string;
    url: string;
}
interface ProcessingResult {
    projectName: string;
    results: ResultItem[];
}
export default function ResultTable() {
    const { currentProjectName } = React.useContext(AppContext);
    const [processingResult, setProcessingResult] = React.useState<ProcessingResult>({ projectName: "", results: [] } as ProcessingResult);
    const [isLoading, setIsLoading] = React.useState<boolean>(false);
    const [error, setError] = React.useState<string>("");

    React.useEffect(() => {
        const fetchData = async () => {
            setIsLoading(true);
            try {
                var res = await fetch(`http://43.163.205.191:8080/api/reactapp/result/${currentProjectName.substring(1, 8)}`);
                if (res.status === 404) {
                    await new Promise(f => setTimeout(f, 1000)); // workaround for the server not ready
                    res = await fetch(`http://43.163.205.191:8080/api/reactapp/result/${currentProjectName.substring(1, 8)}`);
                }
                else if (res.status !== 200) {
                    throw new Error(`请求失败: ${res.status}: ${res.statusText}`);
                }
                setProcessingResult(await res.json() as ProcessingResult);
            } catch (ex: any) {
                setError(ex.toString());
                return false;
            } finally {
                setIsLoading(false);
            }
            return true;
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
                                        <Typography noWrap>
                                            {row.name}
                                        </Typography>
                                    </TableCell>
                                    <TableCell align="left">
                                        <Box sx={{
                                            display: 'flex',
                                            flexDirection: 'row',
                                        }}>
                                            <Typography sx={{ overflow: "hidden", textOverflow: "ellipsis", width: '25rem' }}>{row.url}</Typography>
                                            <CopyToClipboard text={row.url} onCopy={() => { console.log("copyed") }}>
                                                <Button variant='outlined'>复制</Button>
                                            </CopyToClipboard>
                                        </Box>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>}
        </Box>
    );
}