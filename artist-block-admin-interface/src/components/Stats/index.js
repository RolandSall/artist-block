import React from 'react';
import {useNavigate} from "react-router-dom";
import CustomMap from "../Charts/map";
import {Typography} from "@mui/material";



const View1 = () => {

    const data = [
        {
            "id": "LBN",
            "value": 500
        },
        {
            "id": "AGO",
            "value": 1
        },
    ]
    let navigate = useNavigate();
    return (
     <div   style={{height: 500}}>
         <Typography style={{display:"flex", justifyContent: "center", fontSize: "30px"}} variant="overline" display="block" gutterBottom>
             Painters Location
         </Typography>
         <br/>
         <CustomMap data={data}/>

          </div>

    );
};

export default View1;
