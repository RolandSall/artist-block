import React, {useState} from 'react';
import {useAuth0} from "@auth0/auth0-react";
import useAuth0Query from "../../hooks/useAuth0Query";
import {hooks} from "../../query";
import {Chip, CircularProgress, Grid, Typography} from "@mui/material";
import {ARTIST_LIGHT_GREY} from "../../utils/constants";
import Box from "@mui/material/Box";
import moment from "moment";

const PainterProfile = () => {
    // const [step,setStep] = React.useState(0)
    // const {isAuthenticated} = useAuth0();
    // const {token} = useAuth0Query()
    //
    // const {data: member, isLoading: isLoadingCurrentMember} = hooks.useCurrentMember(
    //     {isAuthenticated, token: token()})
    //
    // const [checkout, setCheckOut] = useState(false);
    //
    // const {data: artist, isLoading: isLoadingArtist} = hooks.useCurrentArtist({
    //     isArtist: member?.role === "Painter",
    //     token: token()
    // })

    const {token} = useAuth0Query()
    const {isAuthenticated} = useAuth0();

    const {data: member, isLoading: isLoadingCurrentMember} = hooks.useCurrentMember(
        {isAuthenticated, token: token()})

    const [checkout, setCheckOut] = useState(false);

    const {data: artist, isLoading: isLoadingArtist} = hooks.useCurrentArtist({
        isArtist: member?.role === "Painter",
        token: token()
    })

    if (isLoadingArtist
    ) {
        return (
            <div style={{height:'calc(100vh - 74px)',width:'100%',display:'flex'}}>
                <CircularProgress sx={{margin:'auto'}} />
            </div>
        )
    }


    console.log(artist)

    function getImg() {
        if(artist.client.image) {
            return <img src={artist.client.image}
                        width={"300px"} style={{
                borderRadius: '50%',
                border: '2px'
            }}/>;
        }
        return <img src="https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png"
                    width={"150px"} style={{
            borderRadius: '100%',
            border: '2px'
        }}/>;
    }

    return (
            <div className="App">
                <Grid container spacing={1}>
                    <Grid style={{justifyContent: "center", paddingTop: "35px"}} container item spacing={3}>
                        <div style={{display: 'flex', placeContent: 'center'}}>
                            <div style={{textAlign: 'center'}}>
                                <div style={{display: 'flex', paddingTop: '15px', placeContent: 'center'}}>
                                    {getImg()}
                                </div>

                                <Typography variant={'h5'} color={"primary"} mt={2}>
                                    {artist.client.firstName + " " + artist.client.lastName}
                                </Typography>
                                <Typography variant={'body2'} color={ARTIST_LIGHT_GREY}>
                                    {artist.location}
                                </Typography>
                                <div style={{
                                    display: 'flex',
                                    flexWrap: 'wrap',
                                    placeContent: 'space-around',
                                    flexDirection: 'column',
                                    width: 'unset',
                                    margin: 'auto'
                                }}>

                                    <Chip style={{marginTop: '10px'}} className={"artist-page-specialities"}
                                          color={"primary"}
                                          label={artist.painterSpecialityDtos[0].readSpecialityDto.specialityType}
                                        // style={{
                                        //     opacity: !(getSpecialityName(1)?.length>0) && "0"
                                        // }}
                                    />
                                    <Chip style={{marginTop: '10px'}} className={"artist-page-specialities"}
                                          color={"secondary"}
                                          label={artist.painterSpecialityDtos[1].readSpecialityDto.specialityType}
                                        // style={{
                                        //     opacity: !(getSpecialityName(2)?.length>0) && "0"
                                        // }}
                                    />

                                </div>
                            </div>
                        </div>

                    </Grid>


                </Grid>

                <Box sx={{ flexGrow: 1 }}>
                    <Grid container spacing={2} columns={16}>
                        <Grid item xs={8}>
                            <div> Personal Information </div>
                            <div style={{
                                display: 'flex',
                                flexWrap: 'wrap',
                                placeContent: 'space-around',
                                flexDirection: 'column',
                                width: 'unset',
                                margin: 'auto'
                            }}>
                            <Chip style={{marginTop: '10px'}} className={"artist-page-specialities"}
                                  color={"primary"}
                                  label={artist.client.phoneNumber}
                                // style={{
                                //     opacity: !(getSpecialityName(2)?.length>0) && "0"
                                // }}
                            />

                            <Chip style={{marginTop: '10px'}} className={"artist-page-specialities"}
                                  color={"secondary"}
                                  label={artist.client.email}
                                // style={{
                                //     opacity: !(getSpecialityName(2)?.length>0) && "0"
                                // }}
                            />
                                <Chip style={{marginTop: '10px'}} className={"artist-page-specialities"}
                                      color={"info"}


                                      label={moment(artist.client.birthDate).format("YYYY-MM-DD")}
                                    // style={{
                                    //     opacity: !(getSpecialityName(2)?.length>0) && "0"
                                    // }}
                                />
                            </div>
                        </Grid>
                        <Grid item xs={8}>
                            <div> Artist Information </div>
                            <div style={{
                                display: 'flex',
                                flexWrap: 'wrap',
                                placeContent: 'space-around',
                                flexDirection: 'column',
                                width: 'unset',
                                margin: 'auto'
                            }}>
                                <Chip style={{marginTop: '10px'}} className={"artist-page-specialities"}
                                      color={"primary"}
                                      label={"Years Of Experience: "+ artist.yearsOfExperience}
                                    // style={{
                                    //     opacity: !(getSpecialityName(2)?.length>0) && "0"
                                    // }}
                                />

                                <Chip style={{marginTop: '10px'}} className={"artist-page-specialities"}
                                      color={"secondary"}
                                      label={"Location: " + artist.location}
                                    // style={{
                                    //     opacity: !(getSpecialityName(2)?.length>0) && "0"
                                    // }}
                                />
                                <Chip style={{marginTop: '10px'}} className={"artist-page-specialities"}
                                      color={"info"}
                                      label={"Bio: " + artist.bio}
                                    // style={{
                                    //     opacity: !(getSpecialityName(2)?.length>0) && "0"
                                    // }}
                                />
                            </div>
                        </Grid>
                    </Grid>
                </Box>


            </div>
        )

}


export default PainterProfile;
