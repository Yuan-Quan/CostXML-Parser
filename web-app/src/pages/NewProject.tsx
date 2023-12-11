import { Box, List, ListItem, Typography } from "@mui/material"
import { Link, Outlet } from "react-router-dom"
import CreateStepper from "../components/CreateStepper"

export const NewProject = () => {
    return (
        <Box className="mainPage">
            <CreateStepper />
            <Outlet />
        </Box>
    )
}