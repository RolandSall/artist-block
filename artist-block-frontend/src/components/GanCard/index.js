import React, {useState} from 'react';
import {Avatar, Box, Chip, Typography, useMediaQuery, useTheme} from "@mui/material";
// import {Rating} from "@mui/lab";
// import JohnDoe from '../../assets/core/john-doe.png';
// import Location from '../../assets/icons/location.svg';
// import Briefcase from '../../assets/icons/briefcase.svg';
// import Global from '../../assets/icons/global.svg';
// import Map from '../../assets/icons/map.svg';
// import Teacher from '../../assets/icons/teacher.svg';
import './styles.scss';
import {ARTIST_LIGHT_GREY} from "../../utils/constants";
import ArtistButton from "../ArtistButton";
import {useHistory} from "react-router-dom";

const GanCard = ({data}) => {

    const theme = useTheme()

    const isSmall = useMediaQuery(theme.breakpoints.down('sm'));


    const {push} = useHistory()

    console.log(data.imageUrl)


    return (
        <div className={"painting-card-container"}>
            <div className={"grey-area"}>
                {/*<div style={{width:'100%',aspectRatio:'1',backgroundColor:'grey'}}/>*/}

                {
                    data.imageUrl ?

                        <img className={"painter-image"} id={data.imageUrl} key={data.imageUrl} src={data.imageUrl+"?"+ Date.now()} style={{aspectRatio:1}} alt="painting-image" width={"100%"}/>
                        :

                        <Avatar  className={"painter-image"} />
                }
                <div style={{padding:'0 5%', flexGrow:1, display:'flex'}}>
                    <Typography variant={isSmall?"h6":"h5"} margin={"auto"} color={"primary"} >
                      GAN Creation
                    </Typography>
                </div>

                <Box flexDirection={"column"} sx={{display: 'flex', placeContent: "center", width: isSmall?'100%':'60%', margin: '10px auto 20px auto'}}>

                    <Typography variant={"body2"}>
                        {data.description}
                    </Typography>
                </Box>
            </div>
            <div>
                <ArtistButton
                    padding={"10px 15px"}
                    variant={"outlined"}
                    className={"view-painting"}
                    onClick={() => push(`/gan-painting/${data.ganImageId}`)}
                >
                    View Painting
                </ArtistButton>
            </div>
            <Box sx={{display:'flex', placeContent:'space-evenly',padding: isSmall? "0px 10px 20px 10px": "0px 25px 20px 25px", marginTop:'-10px'}}>
                <div style={{width:'100%'}}>
                    <Typography variant={isSmall?"body1":"h6"} sx={{color:ARTIST_LIGHT_GREY}}>
                        Your Style Our Design
                    </Typography>
                    {/*<Typography variant={isSmall?"caption":"body2"} sx={{color:'#4B8F8C',fontSize:isSmall?"0.5rem":""}}>*/}
                    {/*    */}
                    {/*</Typography>*/}
                </div>
            </Box>
        </div>
    );
};

export default GanCard;
