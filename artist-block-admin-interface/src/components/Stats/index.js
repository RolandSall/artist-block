import React, { useEffect, useState } from 'react';
import {useNavigate} from "react-router-dom";
import CustomMap from "../Charts/map";
import {Grid, Typography} from "@mui/material";
import CustomPie from "../Charts/pie";
import CustomRadar from "../Charts/radar";
import CustomBar from "../Charts/radar";
import axios from 'axios'
import iso from 'iso-3166-1'
import ReleaseGithubCalender from "../Charts/github";

const baseUrl = "https://artist-block-account-service.herokuapp.com/api/v1/"
const usersBaseUrl = baseUrl + "stats-users"
const paintingsBaseUrl = baseUrl + "stats-paintings"
const countryBaseUrl = baseUrl + "stats-country"
const statsSpecialityUrl = baseUrl + "stats-specialty"
const statsDataReleaseUrl = baseUrl + "deployments"


const View1 = () => {

const [piData , setPiData] = useState([]);
const [barData, setBardata] = useState([]);
const [mapData, setmapData] = useState([]);
const [specData, setSpecData] = useState([]);
const [releaseData, setReleaseData] = useState([]);

    // const data = [
    //     {
    //         "id": "LBN",
    //         "value": 500
    //     },
    //     {
    //         "id": "AGO",
    //         "value": 1
    //     },
    // ]

// const   piData =     [
//         {
//             "id": "Painter",
//             "label": "Painter",
//             "value": 346,
//             "color": "hsl(13, 70%, 50%)"
//         },
//             {
//                 "id": "Client",
//                 "label": "Client",
//                 "value": 421,
//                 "color": "hsl(229, 70%, 50%)"
//             },
//     ]

// const barData =    [
//     {
//         "Painting Type": "Painting",
//         "GAN": 42,
//         "hot dogColor": "hsl(314, 70%, 50%)",
//         "Real Painting": 108,
//         "burgerColor": "hsl(122, 70%, 50%)",
//     },

//         ]


const githubData = [
    {
        "value": 214,
        "day": "2016-11-02"
    },
    {
        "value": 10,
        "day": "2022-05-04"
    }
    ]

const getPiData = async() => {
    const res = await axios.get(usersBaseUrl);
    setPiData(res.data) // update pie chart component
}

const githubReleaseData = async() => {
    const res = await axios.get(statsDataReleaseUrl);



    res.data.forEach(element => {

        const [day, month, year] = element.timestamp.split('/');

        const result = [year, day, month].join('-');

        element.timestamp = result

        var obj = {

            "value": element.count,
            "day": result

        }


        releaseData.push(obj)


    });

    //setReleaseData(res.data)

    console.log("rrr" , releaseData)





}

const getBarData = async() => {
    const res = await axios.get(paintingsBaseUrl)
    // format the data correctly
    const data = [{"PaintingType": "Painting",
                  "GAN": res.data.numGans,
                  "hot dogColor": "hsl(314, 70%, 50%)",
                  "Real Painting": res.data.numPaintings,
                  "burgerColor": "hsl(122, 70%, 50%)"}];

    setBardata(data) // update bar graph component
}

const getStatsSpecialityData = async() => {
        const res = await axios.get(statsSpecialityUrl)
        // format the data correctly
        setSpecData(res.data)


    }

const getMapData = async() => {
    const res = await axios.get(countryBaseUrl)


    console.log(res)

    // change the country NAMEs to 3 letter ISO-3166
    res.data.forEach(element => {
        element.id = iso.whereCountry(element.id).alpha3;
    });

    //console.log(res.data)
    setmapData(res.data)
}

useEffect( () => {
    getPiData();
    getBarData();
    getMapData();
    getStatsSpecialityData();
    githubReleaseData();
}
, [] );


    let navigate = useNavigate();
    return (
     <div   style={{height: 500}}>
         <Typography style={{display:"flex", justifyContent: "center", fontSize: "30px"}} variant="overline" display="block" gutterBottom>
             Painters Location
         </Typography>
         <br/>
         <CustomMap data={mapData}/>
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
         <Typography style={{display:"flex", justifyContent: "center", fontSize: "30px"}} variant="overline" display="block" gutterBottom>
             Our Painters Painting Specialities
         </Typography>
         <div   style={{height: 500}}>
             <CustomPie data={specData}/>
         </div>
         <Typography style={{display:"flex", justifyContent: "center", fontSize: "30px"}} variant="overline" display="block" gutterBottom>
             Releases Dates     
         </Typography>
         <div   style={{height: 250}}>
             <ReleaseGithubCalender data={releaseData}/>
         </div>
          </div>

    );
};

export default View1;
