import * as React from 'react';
import { Button } from '@mui/base';
import { Typography } from '@mui/material';
import { Box } from '@mui/system';
import axios from "axios";

export default function ProjectFileUpload() {
    const [selectedFile, setSelectedFile] = React.useState<File | null>(null);
    const [isUploading, setIsUploading] = React.useState<boolean>(false);
    const [uploadMessage, setUploadMessage] = React.useState<string>("");

    const onFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSelectedFile(event.target.files![0]);
    }

    const onFileUpload = async () => {
        setIsUploading(true);
        // Create an object of formData
        const formData = new FormData();

        // Update the formData object
        formData.append(
            "formFile",
            selectedFile!,
        );
        formData.append(
            "fileName",
            selectedFile!.name,
        );

        // Details of the uploaded file
        console.log(selectedFile);

        // Request made to the backend api
        // Send formData object
        try {
            const res = await axios.post("http://43.163.205.191:8080/api/fileupload", formData);
            console.log(res);
            setUploadMessage("\"" + res.status + ": " + res.statusText + "\", " + res.data.message);
        } catch (error) {
            console.log(error);
            setUploadMessage("上传失败: " + error);
        }
        finally {
            setIsUploading(false);
        }
        setIsUploading(false);
    }

    return (
        <Box>
            <Typography> 选择上传的XML文件</Typography>
            <Box>
                <input type="file" onChange={onFileChange} />
                <Button onClick={onFileUpload}>上传</Button>
            </Box>
            {isUploading && <Typography>上传中...</Typography>}
            {uploadMessage && <Typography>{uploadMessage}</Typography>}
        </Box>
    )
}