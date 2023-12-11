import { Box, List, ListItem } from "@mui/material"

export const Main = () => {
    return (
        <Box className="mainPage">
            <h1>Main page placeholder</h1>
            <List>
                <ListItem> <a href="/app/new-project">New Project</a> </ListItem>
            </List>
        </Box>
    )
}