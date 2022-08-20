import React, {useEffect, useRef, useState} from 'react';
import {hooks} from "../../query";
import {Button, CircularProgress, Container, Grid, Typography} from "@mui/material";
import {ARTIST_PINK} from "../../utils/constants";
import {useHistory, useLocation, useParams} from "react-router-dom";
import {useAuth0} from "@auth0/auth0-react";
import useAuth0Query from "../../hooks/useAuth0Query";
import './styles.css';
import ArtistButton from "../../components/ArtistButton";
import Paypal from "../../components/PayPal";
import SaleCountdown from "../../components/SaleCountdown";

const PaintingProfile = () => {
    const {push} = useHistory()
    const { id } = useParams();
    const {isAuthenticated} = useAuth0();
    const [checkout, setCheckOut] = useState(false);
    const [descriptionHeight,setDescriptionHeight] = useState(0)
    const descriptionRef = useRef(null)
    const { token } = useAuth0Query()
    const { data: paintingData, isLoading} = hooks.usePainting({
        paintingId:id
    })

    const {data: member, isLoading: isLoadingCurrentMember} = hooks.useCurrentMember(
     {isAuthenticated, token: token()})


     const {data: artist, isLoading: isLoadingArtist} = hooks.useCurrentArtist({
         isArtist: member?.role === "Painter",
         token: token()
    })

    console.log(paintingData)
    useEffect(() => {
       if(descriptionRef?.current) {
           setDescriptionHeight(descriptionRef.current.clientHeight)
       }
    },[descriptionRef])

    if(isLoading){
        return(
            <div style={{height: 'calc(100vh - 74px)', width: '100%', display: 'flex'}}>
                <CircularProgress sx={{margin: 'auto'}}/>
            </div>)
    }

    function getArtistButton() {
        console.log("asdsahdasad", artist)
        if(paintingData.paintingStatus == "Sold" || paintingData.painterId == artist.painterId) {
            return <div> </div>
        }
        return <ArtistButton onClick={() => setCheckOut(true)} variant={"contained"}>
            Buy Painting
        </ArtistButton>;
    }

    return (
        <div>
            <Grid container padding={'30px 120px'} spacing={2}>
                <Grid item xs={12} sm={12} md={6} lg={6}>
                    <div>
                        <div className={"frame"}>
                            <img src={paintingData.paintingUrl} style={{
                                borderRadius:'20px',
                                width:'100%',

                            }} className={
                               "paintingImage"
                            } />

                            <div ref={descriptionRef} className={"details-container"}>
                                <Typography variant={"h5"} color={"white"} textAlign={"center"}>
                                    {paintingData.paintingName}
                                </Typography>
                                <Typography variant={"h5"} color={"white"}  textAlign={"center"} mt={2}>
                                    {paintingData.paintingDescription}
                                </Typography>
                            </div>
                        </div>
                    </div>
                </Grid>
                <Grid item xs={12} sm={12} md={6} lg={6}>
                    <div style={{
                        display:'flex',
                        flexDirection:'column',
                        height:'100%'
                    }}>
                        <div className={"details-container"}>
                            <SaleCountdown time={paintingData.buyTimeStamp} />
                            <Typography textAlign={"center"} variant={"h5"}> Painted in {paintingData.paintedYear} </Typography>
                            <Typography textAlign={"center"} variant={"h5"}> Painter: {paintingData.painterInfo.client.firstName + " " + paintingData.painterInfo.client.lastName} </Typography>

                            <Typography textAlign={"center"} variant={"h5"}> ${paintingData.paintingPrice} </Typography>
                        </div>
                        <div style={{marginTop:'10px'}}>
                            { checkout ? ( <Paypal /> )

                                : (
                                    <div style={{
                                        display: 'flex',
                                        placeContent: 'center'
                                    }}>
                                        {getArtistButton()}
                                        <div style={{paddingLeft: "50px"}}>
                                            <ArtistButton style={{paddingLeft: "50px"}}
                                                          onClick={() => push('../..//painter-profile/' + paintingData.painterId)}
                                                          variant={"contained"}>
                                                Check profile
                                            </ArtistButton>
                                        </div>

                                    </div>
                                )}
                        </div>
                    </div>
                </Grid>
            </Grid>
        </div>
    );
};

export default PaintingProfile;
