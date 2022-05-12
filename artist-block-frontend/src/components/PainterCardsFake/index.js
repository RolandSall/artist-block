import React from 'react';
import {Avatar, Box, Chip, Typography, useMediaQuery, useTheme} from "@mui/material";
// import {Rating} from "@mui/lab";
import './styles.scss';
import ArtistButton from "../ArtistButton";
import {useAuth0} from "@auth0/auth0-react";
import {ARTIST_BACKGROUND} from "../../utils/constants";

const PainterCardsFake = () => {

    const { isAuthenticated } = useAuth0()

    const theme = useTheme()

    const isSmall = useMediaQuery(theme.breakpoints.down('sm'));

    return (
        <div className={"painting-card-container"}>
            <div className={"grey-area"}>
                {/*<div style={{width:'100%',aspectRatio:'1',backgroundColor:'grey'}}/>*/}

                <Avatar  className={"painter-image"} />

                <div style={{padding:'10px 5%', flexGrow:1, display:'flex'}}>
                    <Typography variant={isSmall?"h6":"h5"} margin={"auto"}
                                style={{
                                    color: 'transparent',
                                    background: ARTIST_BACKGROUND,
                                    borderRadius: '20px',
                                    flexGrow:1
                                }}

                                color={"primary"} >
                        ''
                    </Typography>
                </div>
                <div
                    style={{
                        padding:'5px'
                    }}
                >
                    <Typography variant={"body2"}
                                style={{
                                    color: 'transparent',
                                    background: ARTIST_BACKGROUND,
                                    borderRadius: '20px',
                                }}
                    >
                        ''
                    </Typography>
                </div>
                <div
                    style={{
                        padding:'5px'
                    }}
                >
                    <Typography variant={"body1"}
                                style={{
                                    color: 'transparent',
                                    background: ARTIST_BACKGROUND,
                                    borderRadius: '20px',
                                }}
                    >
                        ''
                    </Typography>
                </div>

                {/*<Rating*/}
                {/*    name="half-rating"*/}
                {/*    defaultValue={4}*/}
                {/*    readOnly*/}
                {/*/>*/}
                <Box flexDirection={"column"} sx={{display: 'flex', placeContent: "center", width: isSmall?'100%':'60%', margin: '10px auto 20px auto'}}>
                    {
                        [0,1,2,3].map((e)=>
                            <Box key={e} sx={{display: 'flex', height:'25px', margin: '6px 0'}}>
                                <Box sx={{display: 'flex', margin: 'auto 0', flexGrow:1}}>
                                    <Typography variant={"body2"}
                                                style={{
                                                    color: 'transparent',
                                                    background: ARTIST_BACKGROUND,
                                                    borderRadius: '20px',
                                                    flexGrow:1

                                                }}
                                    >
                                        ''
                                    </Typography>
                                </Box>
                            </Box>)
                    }
                    <div style={{display:'flex',flexWrap:'wrap',placeContent:'space-around',flexDirection:'column'}}>

                        <Chip className={"painting-description"} color={"primary"}
                              // style={{
                              //     opacity: !(getSpecialityName(1)?.length>0) && "0"
                              // }}
                        />
                        <Chip className={"painting-description"} color={"secondary"}
                              // style={{
                              //     opacity: !(getSpecialityName(2)?.length>0) && "0"
                              // }}
                        />
                        <Chip className={"painting-description"}
                              // style={{
                              //     opacity: !(getSpecialityName(3)?.length>0) && "0"
                              // }}
                        />
                    </div>
                </Box>
            </div>
            <div>
                <ArtistButton
                    padding={"10px 15px"}
                    variant={"outlined"}
                    className={"view-painting"}
                >
                    {
                        isAuthenticated?
                            'Profile':
                            'Login'
                    }
                </ArtistButton>
            </div>
            <Box sx={{display:'flex', placeContent:'space-evenly',padding: isSmall? "0px 10px 20px 10px": "0px 25px 20px 25px", marginTop:'-10px'}}>

                <div style={{width:'100%'}}>
                    <Box sx={{display: 'flex'}}>
                        <Box sx={{display: 'flex', margin: 'auto 0', flexGrow:1}}>
                            <Typography variant={isSmall?"body1":"h6"}
                                        style={{
                                            color: 'transparent',
                                            background: ARTIST_BACKGROUND,
                                            borderRadius: '20px',
                                            flexGrow:1
                                        }}
                            >
                                ''
                            </Typography>
                        </Box>
                    </Box>
                    {/*<Typography variant={isSmall?"caption":"body2"} sx={{color:'#4B8F8C',fontSize:isSmall?"0.5rem":""}}>*/}
                    {/*    */}
                    {/*</Typography>*/}
                </div>
            </Box>
        </div>
    );
};

export default PainterCardsFake;
