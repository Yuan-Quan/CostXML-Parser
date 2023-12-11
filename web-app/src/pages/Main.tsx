import { Box, List, ListItem } from "@mui/material"
import { Link } from "react-router-dom"

export const Main = () => {
    return (
        <Box className="mainPage">
            <h1>Main page placeholder</h1>
            <List>
                <ListItem> <Link to='new/upload'>创建新的XML文件解析</Link> </ListItem>
            </List>
        </Box>
    )
}