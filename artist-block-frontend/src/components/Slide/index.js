import React from 'react';
import {Typography} from "@mui/material";

const Slide = ({src,name,id}) => {
    return (
        <div style={{
            width:"100%",
            height:'600px',
            background: `no-repeat center/80% url(${src}), rgba(38, 20, 72, 0.9)`,
            display:'flex',
            flexDirection:"column",
            placeContent:'end',
            padding:'10%'
        }}>
            {/*<img src={src} alt={id} />*/}
            <div style={{
                background:'black',
                padding:'10px',
                marginBottom:'10px'
            }}>
                <Typography textAlign={"center"} sx={{color:'white'}} variant={"h5"}>
                    {name}
                </Typography>
            </div>
        </div>
    );
};

export default Slide;
