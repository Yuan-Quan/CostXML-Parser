import { Box, Typography } from "@mui/material";
import ResultTable from "./ResultTable";

export default function ProcessingResult() {
    return (
        <Box>
            <Typography>Result</Typography>
            <Typography>RootURL: "http://localhost:7094/"</Typography>
            <ResultTable />
        </Box>
    )
}
