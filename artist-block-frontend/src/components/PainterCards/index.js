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

const PainterCards = ({data}) => {

    const theme = useTheme()

    const isSmall = useMediaQuery(theme.breakpoints.down('sm'));

    const [prevData,setPrevData] = useState(data)

    const {push} = useHistory()

    const tableIcon = (icon) => (
        <Box flexShrink sx={{display: 'flex', margin: 'auto 0'}}>
            <img alt="table-icon" src={icon} height={'25px'} width={'25px'}/>
        </Box>
    )
    const tableText = (text) => (
        <Box sx={{display: 'flex', margin: 'auto 0', textOverflow: 'ellipsis',
            overflow: 'hidden',
            whiteSpace: 'nowrap',}}>
            <Typography ml={1} variant={"body2"} sx={{color: `${ARTIST_LIGHT_GREY} !important`}}>
                {text}
            </Typography>
        </Box>
    )

    const arrayToString = (arr,propertyName) => {
        let str = ""
        const arrTemp = []
        arr.forEach((e,index) => {
            if(!arrTemp.includes(e[propertyName])){
                if(index>0){
                    str+=", " + e[propertyName]
                }
                else{
                    str+=e[propertyName]
                }
                arrTemp.push(e[propertyName])
            }
        })
        return str
    }
    return (
        <div className={"painting-card-container"}>
            <div className={"grey-area"}>
                {/*<div style={{width:'100%',aspectRatio:'1',backgroundColor:'grey'}}/>*/}

                {
                    data.paintingUrl ?

                    <img className={"painting-image"} id={data.paintingUrl} key={data.paintingUrl} src={data.paintingUrl+"?"+ Date.now()} style={{aspectRatio:1}} alt="painting-image" width={"100%"}/>
                    :

                    <Avatar  className={"painting-image"} />
                }
                <div style={{padding:'0 5%', flexGrow:1, display:'flex'}}>
                    <Typography variant={isSmall?"h6":"h5"} margin={"auto"} color={"primary"} >
                        {data.paintingName}
                    </Typography>
                </div>
                <Typography variant={"body2"}>
                    {data.paintingStatus}
                </Typography>

                <Box flexDirection={"column"} sx={{display: 'flex', placeContent: "center", width: isSmall?'100%':'60%', margin: '10px auto 20px auto'}}>

                    <Typography variant={"body2"}>
                        {data.paintingDescription}
                    </Typography>

                </Box>
            </div>
            <div>
                <ArtistButton
                    padding={"10px 15px"}
                    variant={"outlined"}
                    className={"view-painting"}
                    onClick={() => push(`/painting/${data.paintingId}`)}
                >
                    View Painting
                </ArtistButton>
            </div>
            <Box sx={{display:'flex', placeContent:'space-evenly',padding: isSmall? "0px 10px 20px 10px": "0px 25px 20px 25px", marginTop:'-10px'}}>

                <div style={{width:'100%'}}>
                    <Typography variant={isSmall?"body1":"h6"} sx={{color:ARTIST_LIGHT_GREY}}>
                        ${data.paintingPrice}
                    </Typography>

                </div>
            </Box>
        </div>
    );
};

export default PainterCards;
