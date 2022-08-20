import React, {useEffect, useRef, useState} from 'react';
import {useHistory, useParams} from "react-router-dom";
import useAuth0Query from "../../hooks/useAuth0Query";
import {hooks} from "../../query";
import {CircularProgress, Grid, Typography} from "@mui/material";
import SaleCountdown from "../../components/SaleCountdown";
import Paypal from "../../components/PayPal";
import ArtistButton from "../../components/ArtistButton";
import {CustomTextField} from "../RegisterCustomer";
import Button from "@mui/material/Button";
import {ARTIST_GREEN} from "../../utils/constants";

const GanProfile = () => {
    const {push} = useHistory()
    const { id } = useParams();
    const [checkout, setCheckOut] = useState(false);
    const [descriptionHeight,setDescriptionHeight] = useState(0)
    const descriptionRef = useRef(null)
    const { token } = useAuth0Query()
    const { data: paintingData, isLoading} = hooks.useGanPainting({
        paintingId:id
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


    return (
        <Grid container padding={'30px 120px'} spacing={2}>
            <Grid item xs={12}>
                <div style={{justifyContent: "center", display: "flex"}}>
                    <div className={"frame"}>
                        <img src={paintingData.imageUrl} style={{
                            borderRadius:'20px',
                            width:'100%',

                        }} className={
                            "paintingImage"
                        } />

                        <div ref={descriptionRef} className={"details-container"}>
                            <Typography variant={"h5"} color={"white"} textAlign={"center"}>
                                {paintingData.description}
                            </Typography>
                            <Typography variant={"h5"} color={"white"}  textAlign={"center"} mt={2}>
                                {paintingData.paintingDescription}
                            </Typography>
                        </div>
                    </div>

                </div>
                <div style={{justifyContent: "center", display: "flex", paddingTop: "15px"}}>
                <ArtistButton
                    style={{color: "white", backgroundColor: ARTIST_GREEN}}
                    variant={"contained"}
                    onClick={() => push(`../gan`)}
                >
                    Generate More!
                </ArtistButton>
            </div>
            </Grid>
        </Grid>

    );
};

export default GanProfile;
