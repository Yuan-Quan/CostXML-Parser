import { Button } from '@mui/base';
import { Typography } from '@mui/material';
import { Box } from '@mui/system';
import * as React from 'react';

export default function ProjectFileUpload() {
    const [selectedFile, setSelectedFile] = React.useState<File | null>(null);

    const onFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSelectedFile(event.target.files![0]);
    }

    return (
        <Box>
            <Typography> 选择上传的XML文件</Typography>
            <Box>
                <input type="file" onChange={onFileChange} />
                <Button>上传</Button>
            </Box>
        </Box>
    )
}