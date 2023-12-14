import { Box, Typography } from "@mui/material";
import ResultTable from "./ResultTable";
import { AppContext } from "../App";
import React from "react";

export default function ProcessingResult() {
    const { currentProjectName } = React.useContext(AppContext);
    return (
        <Box>
            <Typography>提取结果</Typography>
            <Typography>WebService地址: "http://43.163.205.191:8080/"</Typography>
            <Typography>当前XML文件: {currentProjectName}</Typography>
            <ResultTable />
        </Box>
    )
}
