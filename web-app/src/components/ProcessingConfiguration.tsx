import { List, Typography } from "@mui/material";
import { Box } from "@mui/system";
import ProcessingFileSelection from "./ProcessingFileSelection";

export default function ProcessingConfiguration() {
    return (
        <Box>
            <Typography>选择提取的文件</Typography>
            <ProcessingFileSelection />
            <Typography>选择提取的方法</Typography>
        </Box>
    )
}