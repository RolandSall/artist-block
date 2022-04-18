import React from 'react';
import {useNavigate} from "react-router-dom";
import CustomMap from "../Charts/map";
import {Grid, Typography} from "@mui/material";
import CustomPie from "../Charts/pie";
import CustomRadar from "../Charts/radar";
import CustomBar from "../Charts/radar";



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

const   piData =     [
        {
            "id": "Painter",
            "label": "Painter",
            "value": 346,
            "color": "hsl(13, 70%, 50%)"
        },
            {
                "id": "Client",
                "label": "Client",
                "value": 421,
                "color": "hsl(229, 70%, 50%)"
            },
    ]

const barData =    [
    {
        "Painting Type": "Painting",
        "GAN": 42,
        "hot dogColor": "hsl(314, 70%, 50%)",
        "Real Painting": 108,
        "burgerColor": "hsl(122, 70%, 50%)",
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
         <Grid container spacing={2}>
             <Grid item xs={6}>
                 <div   style={{height: 500}}>
                 <CustomPie data={piData}/>
                 </div>
             </Grid>
             <Grid item xs={6}>
                 <div   style={{height: 500}}>
                 <CustomBar data={barData}/>
                 </div>
             </Grid>
         </Grid>

          </div>

    );
};

export default View1;