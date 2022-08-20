import React from 'react';
import {useAuth0} from "@auth0/auth0-react";
import Box from '@mui/material/Box';
import { Swiper, SwiperSlide } from "swiper/react";
import Button from '@mui/material/Button';
import Slide from "../../components/Slide";
import Modal from '@mui/material/Modal';
import App from "../../assets/logos/safe-mail.png";
import {
    Autocomplete,
    CircularProgress,
    Container,
    Grid,
    TextField,
    Typography,
    useMediaQuery,
    useTheme
} from "@mui/material";
import { useRef, useState } from "react";
import Woman from '../../assets/logos/woman.svg'
import artLover from '../../assets/logos/artLover.svg'
import nature from '../../assets/logos/nature.svg'
// Import Swiper React components

// Import Swiper styles
import "swiper/css";
import "swiper/css/pagination";
import "swiper/css/navigation";
    import "swiper/css/free-mode";
import "swiper/css/thumbs";


// import required modules
import { Parallax, Pagination, Navigation } from "swiper";

import "./styles.css";
import CustomSwiper from "../../components/Swiper";
import ArtistButton from "../../components/ArtistButton";
import * as PropTypes from "prop-types";
import Pullout from "../../components/LandingPage/Pullout";
import CardSlider from "../../components/LandingPage/CardSlider";
import CardCollection from "../../components/LandingPage/CardCollection";
import ToutOverlap from "../../components/LandingPage/ToutOverlap";
import VerticalLine from "../../components/LandingPage/VerticalLine";
import BigTextLittleText from "../../components/LandingPage/BigTextLittleText";
import ToutContent from "../../components/LandingPage/ToutContent";
import {ParallaxProvider} from "react-scroll-parallax";
import {ARTIST_GREEN} from "../../utils/constants";
import {useHistory} from "react-router-dom";
import GanCard from "../../components/GanCard";
import {hooks} from "../../query";
import useAuth0Query from "../../hooks/useAuth0Query";

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 550,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
};



const Landing = () => {
    const [open, setOpen] = React.useState(true);
    const handleClose = () => setOpen(false);
    const theme = useTheme()
    const [thumbsSwiper, setThumbsSwiper] = useState(null);
    const {push} = useHistory()
    const isSmall = useMediaQuery(theme.breakpoints.down('md'));
    const { token } = useAuth0Query()
    const { isAuthenticated } = useAuth0()
    const { data: ganPainting, isLoading: isLoadingGAN } = hooks.useGANPainting(
        {isAuthenticated,
            token:token()
        })



    const {
        error,
        loginWithRedirect,
    } = useAuth0();

    function getSwiper() {
        if(ganPainting){
            return <>
                {ganPainting.map((e, i) => {
                    return (
                        <SwiperSlide key={i}>
                            <GanCard
                                data={e}
                            />
                        </SwiperSlide>
                    )
                })}
            </>;

        } else {
            return (
                <div style={{height:'calc(100vh - 74px)',width:'100%',display:'flex'}}>
                    <CircularProgress sx={{margin:'auto'}} />
                </div>
            )
        }
    }

    if (error && error.error_description === 'Please verify your email before logging in.') {
        return (
            <div>
                <Modal
                    open={open}
                    onClose={handleClose}
                    aria-labelledby="modal-modal-title"
                    aria-describedby="modal-modal-description"
                >
                    <Box sx={style} >
                        <Typography id="modal-modal-title" variant="h4" component="h4" style={{display: "flex", justifyContent: "center"}}>
                            Email Verification
                        </Typography>
                        <div style={{display: "flex", justifyContent: "center"}}>
                        <img alt="facebook-logo" src={App} height={100} width={100} className="social-media-button"/>
                        </div>

                        <Typography id="modal-modal-description" variant="h5" sx={{ mt: 2 }} style={{display: "flex", justifyContent: "center"}}>
                            We have sent you an email for verification.
                        </Typography>
                    </Box>
                </Modal>
            </div>
        )
    } else {
        return (
            <div>
                <Swiper
                    style={{
                        "--swiper-navigation-color": "#fff",
                        "--swiper-pagination-color": "#fff",
                    }}
                    speed={600}
                    parallax={true}
                    pagination={{
                        clickable: true,
                    }}
                    navigation={true}
                    modules={[Parallax, Pagination, Navigation]}
                    className="mySwiper"
                >
                    <div
                        slot="container-start"
                        className="parallax-bg"
                        style={{
                            "background-image":
                                "url(https://www.dunamis.co.id/wp-content/uploads/2020/01/Dunamis-Web-Banner-1280x360-7H-H2.png)",
                        }}
                        data-swiper-parallax="-23%"
                    ></div>
                    <SwiperSlide>
                        <div style={{paddingLeft: "40px"}}>
                            <div className="title" data-swiper-parallax="-300">
                                Find Your Artist Now
                            </div>

                            <div className="text" data-swiper-parallax="-100">
                                <Typography variant={"h6"} sx={{color: 'grey'}}>
                                    It hasn't been any easier to reach out to artists of all levels and specialties,
                                    check their latest works, and connect with them.
                                </Typography>

                            </div>
                        </div>
                    </SwiperSlide>
                    <SwiperSlide>
                        <div style={{paddingLeft: "40px"}}>
                        <div className="title" data-swiper-parallax="-300">
                            Find Your Painting
                        </div>

                        <div className="text" data-swiper-parallax="-100">
                            <Typography variant={"h6"} sx={{color: 'grey'}}>
                                What kind of painting are you looking for?
                                Take a look on what kind of painting our painters published
                            </Typography>
                        </div>
                        </div>
                    </SwiperSlide>
                    <SwiperSlide>

                        <div style={{display: "flex", justifyContent: "flex-end", paddingRight: "30px"}}
                             className="title" data-swiper-parallax="-300">
                            Generate Your Custom Art
                        </div>
                        <div style={{display: "flex", justifyContent: "flex-end", paddingRight: "50px", textAlignLast: "center"}}>
                            <div style={{display: "flex", justifyContent: "flex-end", paddingLeft: "30px"}}
                                 className="text" data-swiper-parallax="-100">
                                <Typography variant={"h6"} sx={{color: 'grey'}}>
                                    Need a little extra inspiration for your next painting? Using text only,
                                    our AI painting assistant will help you generate unique paintings that will surely
                                    impress!
                                </Typography>
                            </div>
                        </div>

                    </SwiperSlide>
                </Swiper>


                <div>
                    <br/>
                    <Grid container spacing={2} sx={{flexDirection: {md: "row-reverse"}}}>
                        <Grid item sm={12} md={6} sx={{paddingRight: {md: "5%"}}}>
                            <div>
                                <Typography style={{display: "flex", justifyContent: "center"}} variant={"h3"}
                                            color={"black"} sx={{fontWeight: 700}} mt={5}>
                                    EVERYTHING ABOUT
                                </Typography>
                                <Typography style={{display: "flex", justifyContent: "center"}} variant={"h3"}
                                            color={"primary"} sx={{fontWeight: 700}}>
                                    Artist Block
                                </Typography>
                                <Typography style={{display: "flex", justifyContent: "center", textAlignLast: "center"}}
                                            variant={"h5"} mt={4} textAlign={"start"}>
                                    Imagine if your favourite masterpieces were as much a part of your life as your
                                    favourite movies or music? If you didn't have to visit
                                    a gallery every time you wanted to see them?
                                    If you could bring this classic art into your home or your office, sharing your
                                    artistic vision and taste with family and friends, colleagues and clients?
                                </Typography>
                                <div style={{display: "flex", justifyContent: "center", paddingTop: "15px"}}>
                                    <Button
                                        style={{color: "white", backgroundColor: ARTIST_GREEN}}
                                        variant={"contained"}
                                        onClick={() => push(`/hire-a-painter?page=1&min=0&max=5000`)}
                                    >
                                        Search Now
                                    </Button>
                                </div>
                            </div>
                        </Grid>

                        <Grid item md={6}>
                            <div>
                                <img src={Woman} alt={"Artist Platform"} height={"100%"} width={"100%"}/>
                            </div>
                        </Grid>

                    </Grid>
                    <Grid container spacing={2} sx={{flexDirection: {md: "row-reverse"}}}>
                        <Grid item sm={12} md={6} sx={{paddingRight: {md: "5%"}}}>
                            <div>
                                <img src={artLover} alt={"Artist Platform"} height={"100%"} width={"100%"}/>
                            </div>

                        </Grid>

                        <Grid item md={6}>
                            <div>
                                <Typography style={{display: "flex", justifyContent: "center"}} variant={"h3"}
                                            color={"black"} sx={{fontWeight: 700}} mt={5}>
                                    AI In Image Generation
                                </Typography>
                                <Typography style={{display: "flex", justifyContent: "center"}} variant={"h3"}
                                            color={"primary"} sx={{fontWeight: 700}}>
                                    GAN?
                                </Typography>
                                <Typography style={{
                                    display: "flex",
                                    justifyContent: "center",
                                    textAlignLast: "center",
                                    paddingLeft: "50px"
                                }} variant={"h5"} mt={4} textAlign={"start"}>
                                    Create beautiful artwork using the power of AI.
                                    Enter a prompt, pick an art style and watch Artist Block turn your idea into an
                                    AI-powered painting in seconds.
                                </Typography>
                                <div style={{display: "flex", justifyContent: "center", paddingTop: "15px",  paddingBottom: "15px"}}>
                                    <Button
                                        style={{color: "white", backgroundColor: ARTIST_GREEN}}
                                        variant={"contained"}
                                        onClick={() => push(`/gan`)}
                                    >
                                        Generate Yours
                                    </Button>
                                </div>
                            </div>

                            <Swiper
                                effect={"coverflow"}
                                grabCursor={true}
                                // autoplay={{
                                //     delay: 1000,
                                // }}
                                height={300}
                                loop={true}
                                centeredSlides={true}
                                slidesPerView={5}
                                coverflowEffect={{
                                    rotate: 50,
                                    stretch: 0,
                                    depth: 100,
                                    modifier: 1,
                                    slideShadows: false,
                                }}
                                pagination={true}
                                // navigation={true}
                                className="mySwiper"
                            >

                                {getSwiper()}
                            </Swiper>

                        </Grid>
                        <br/>
                    </Grid>
                    <Grid container spacing={2} sx={{flexDirection: {md: "row-reverse"}}}>
                        <Grid item sm={12} md={6} sx={{paddingRight: {md: "5%"}}}>
                            <div>
                                <Typography style={{display: "flex", justifyContent: "center"}} variant={"h3"}
                                            color={"black"} sx={{fontWeight: 700}} mt={5}>
                                    Make Your Collection Richer
                                </Typography>
                                <Typography style={{display: "flex", justifyContent: "center"}} variant={"h3"}
                                            color={"primary"} sx={{fontWeight: 700}}>
                                    Generate & Buy
                                </Typography>
                                <Typography style={{display: "flex", justifyContent: "center", textAlignLast: "center"}} variant={"h5"} mt={4}
                                            textAlign={"start"}>
                                    Our platform allows you to build a rich collection! Generate or Buy either make
                                    make sure to have the best collection between your connections!
                                </Typography>
                                <div style={{display: "flex", justifyContent: "center", paddingTop: "15px"}}>
                                    <Button
                                        style={{color: "white", backgroundColor: ARTIST_GREEN}}
                                        variant={"contained"}
                                        onClick={() => push(`/profile`)}
                                    >
                                        Check Your Profile
                                    </Button>
                                </div>
                            </div>
                        </Grid>

                        <Grid item md={6}>
                            <div>
                                <img src={nature} alt={"Artist Platform"} height={"100%"} width={"100%"}/>
                            </div>
                        </Grid>

                    </Grid>
                </div>
            </div>
        )
    }
    ;

};

export default Landing;
