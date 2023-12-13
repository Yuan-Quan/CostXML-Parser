import { Box, Typography } from "@mui/material";
import ResultTable from "./ResultTable";

export default function ProcessingResult() {
    return (
        <Box>
            <Typography>Result</Typography>
            <Typography>RootURL: "http://43.163.205.191:8080/"</Typography>
            <ResultTable />
        </Box>
    )
}
