/*
import React, {useMemo} from 'react';
import {Grid} from "@mui/material";
import UploadIcon from "@mui/icons-material/Upload";
import {useDropzone} from "react-dropzone";

const baseStyle = {
    flex: 1,
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    padding: "20px",
    borderWidth: 2,
    borderRadius: 2,
    borderColor: "#eeeeee",
    // borderStyle: 'dashed',
    backgroundColor: "#fafafa",
    color: "#bdbdbd",
    outline: "none",
    transition: "border .24s ease-in-out",
};

const focusedStyle = {
    borderColor: "#2196f3",
};

const acceptStyle = {
    borderColor: "#00e676",
};

const rejectStyle = {
    borderColor: "#ff1744",
};

const UploadImage  = () => {

    const {
        acceptedFiles,
        getRootProps,
        getInputProps,
        isFocused,
        isDragAccept,
        isDragReject,
    } = useDropzone({
        accept: "image/!*",
        maxFiles: 1,
        onDrop: (acceptedFiles) => {
            console.log(acceptedFiles)
            // handleFile(acceptedFiles);
        },
    });

    const style = useMemo(
        () => ({
            ...baseStyle,
            ...(isFocused ? focusedStyle : {}),
            ...(isDragAccept ? acceptStyle : {}),
            ...(isDragReject ? rejectStyle : {}),
        }),
        [isFocused, isDragAccept, isDragReject]
    );

    return (
        <div>

            <Grid item xs={12} mt={4}>
                <div style={{ margin: "10px 0" }}>
                    <div {...getRootProps({ style })}>
                        <input {...getInputProps()} />
                        <div style={{ cursor: "pointer" }}>
                            <UploadIcon />
                        </div>
                        <p>Upload unrevealed picture</p>
                    </div>
                </div>
            </Grid>
        </div>
    );
};

export default UploadImage ;*/
