import React, {useEffect, useRef, useState} from 'react';
import {useAuth0} from "@auth0/auth0-react";
import useAuth0Query from "../../hooks/useAuth0Query";
import {hooks, useMutation} from "../../query";
import Paypal from "../../components/PayPal";
import {Box, Grid, LinearProgress, TextField, Typography} from "@mui/material";
import {CustomTextField} from "../RegisterCustomer";
import ArtistButton from "../../components/ArtistButton";
import {MUTATION_KEYS} from "../../query/config/keys";
import {ga} from "react-ga";
import axios from "axios";

function LinearProgressWithLabel(props) {
    return (
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
            <Box sx={{ width: '100%', mr: 1 }}>
                <LinearProgress variant="determinate" {...props} />
            </Box>
            <Box sx={{ minWidth: 35 }}>
                <Typography variant="body2" color="text.secondary">{`${Math.round(
                    props.value,
                )}%`}</Typography>
            </Box>
        </Box>
    );
}

const GAN = () => {

    const polling = useRef(null)

    const [ganResult,setGanResult] = useState()
    const { isAuthenticated } = useAuth0();
    const { token } = useAuth0Query()
    const [description,setDescription] = useState('')
    const [submitted,setSubmitted] = useState(false)

    const { mutateAsync: postGan, isLoading } = useMutation(MUTATION_KEYS.POST_GAN)
    console.log(submitted,isLoading)
    const { data: progressBar, refetch: refetchProgressBar } = hooks.useGanImageProgressBar({enabled:submitted && isLoading})
    const { data: imgProgress, refetch: refetchImgProgress  } = hooks.useGanImageProgress({enabled:submitted && isLoading})

    // const { data: member, isLoading: isLoadingCurrentMember} = hooks.useCurrentMember(
    //     {isAuthenticated,token: token()})
    //
    // const { data: artist, isLoading: isLoadingArtist} = hooks.useCurrentArtist({
    //     isArtist: member?.role==="Painter",
    //     token:token()
    // })

    useEffect(() => {
        if(submitted && isLoading){
            polling.current = setInterval(()=>{
                refetchProgressBar()
                refetchImgProgress()
            },500);
        }
        else{
            clearInterval(polling.current)
        }
        // const intervalId = setInterval(() => {
        //     setCount(prevCount => prevCount + 1);
        // }, 1000);
        //
        // return () => clearInterval(intervalId);
    }, [submitted, isLoading]);


    useEffect(async () => {
        if(ganResult){
            const f = new File([ganResult], 'image.jpeg', {
                type: ganResult.type,
            });


            // console.log(f)
        }
    },[ganResult])


    console.log(progressBar,imgProgress, ganResult)
    return (
        <Grid container padding={'30px 120px'} spacing={2}>
            <Grid item xs={12}>
                <div style={{display:'flex',placeContent:'center'}}>
                    <div className={"frame"}>
                        <img
                            src={
                                Boolean(ganResult) && !isLoading?
                                    ganResult?.image:

                                submitted && isLoading? imgProgress?.image : null }
                            // src={paintingData.paintingUrl}

                             style={{
                                 borderRadius:'20px',
                                 width:'100%',
                                 minWidth: '300px',
                                 aspectRatio: 1
                             }}
                             className={"paintingImage"} />
                        <div style={{marginTop:'10px'}}>
                            <CustomTextField
                                value={description}
                                fullWidth
                                onChange={(e) => setDescription(e.target.value)}
                                variant={"filled"}
                                placeholder={"blue flowers in a green sky"}
                                label={"Insert Text"}
                                mt={2}
                            />
                        </div>
                        <div>
                        <div style={{marginTop:'10px', display: "flex"}}>
                            {
                                progressBar && submitted &&
                                <LinearProgressWithLabel value={parseInt(progressBar)} />
                            }
                            <div>
                            <ArtistButton
                                padding={"10px 15px"}
                                variant={"contained"}
                                mt={2}
                                onClick={() => {
                                    setSubmitted(true)
                                    postGan({
                                        description
                                    }).then((e) => {
                                        setGanResult(e)

                                    })
                                }}
                                disabled={description==="" || isLoading}
                            >

                                Create Image
                            </ArtistButton>
                            </div>
                            <div style={{marginLeft: "25px"}}>
                            <ArtistButton
                                padding={"10px 15px"}

                                variant={"contained"}
                                mt={2}
                                onClick={async () => {
                                    // const f = new File([ganResult], 'image.jpeg', {
                                    //     type: ganResult.type,
                                    // });
                                    console.log(ganResult)

                                    const formData = new FormData()
                                    formData.append("image", ganResult.blob);
                                    console.log(formData.get('image'))
                                    const t = await token()
                                    const responseId = await axios.post('https://artist-block-account-service.herokuapp.com/api/v1/account-service/gan-image/claim',
                                        {
                                            description
                                        }, {
                                            headers: {
                                                Authorization: `Bearer ${t}`
                                            }
                                        })

                                    const imageId = responseId.data.ganImageId

                                    console.log(imageId)
                                    console.log(ganResult.blob)



                                    await axios.post('https://artist-block-account-service.herokuapp.com/api/v1/account-service/gan-image/upload/'+imageId,
                                                formData
                                            , {
                                                headers: {

                                                    Authorization: `Bearer ${t}`,
                                                }
                                            })




                                }}
                                disabled={description==="" || isLoading}
                            >

                                Save Image
                            </ArtistButton>
                            </div>
                            </div>

                        </div>
                    </div>
                </div>
            </Grid>
        </Grid>
    )
};

export default GAN;
