import React, {useEffect, useMemo, useState} from 'react';
import {hooks, useMutation} from "../../query";
import SwiperCore, {EffectCoverflow, Navigation, Pagination, Autoplay} from "swiper";
import {useAuth0} from "@auth0/auth0-react";
import { Swiper, SwiperSlide } from "swiper/react";
import useAuth0Query from "../../hooks/useAuth0Query";
import {CircularProgress, Grid, InputAdornment, TextField, Typography} from "@mui/material";
import PainterCards from "../../components/PainterCards";
import GanCard from "../../components/GanCard";
import ArtistButton from "../../components/ArtistButton";
import Button from "@mui/material/Button";
import {ARTIST_GREEN, REGISTRATION_ARTIST_ROUTE} from "../../utils/constants";
import {Link, useHistory} from "react-router-dom";
import { useDropzone } from "react-dropzone";
import CloudUploadIcon from '@mui/icons-material/CloudUpload';
import UploadIcon from '@mui/icons-material/Upload';
import SaleCountdown from "../../components/SaleCountdown";
import Paypal from "../../components/PayPal";
import AdapterMoment from "@mui/lab/AdapterMoment";
import {DatePicker} from "@mui/lab";
import moment from "moment";
import LocalizationProvider from "@mui/lab/LocalizationProvider";
import {CustomTextField} from "../RegistrationArtist";
import {MUTATION_KEYS} from "../../query/config/keys";
import axios from "axios";
import Box from "@mui/material/Box";


SwiperCore.use([EffectCoverflow, Pagination, Autoplay, Navigation]);

const baseStyle = {
    flex: 1,
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    padding: "20px",
    borderWidth: 2,
    borderRadius: 2,
    borderColor: "#eeeeee",
    // borderStyle: 'dashed',
    color: "#bdbdbd",
    outline: "none",
    transition: "border .24s ease-in-out",
    zIndex: 100000
};

const focusedStyle = {
    borderColor: "#2196f3",
};

const acceptStyle = {
    borderColor: "#00e676",
};

const rejectStyle = {
    borderColor: "#ff1744",
};

const toBase64 = (file) =>
    new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = (error) => reject(error);
    });

const UserProfile = () => {

    const {push} = useHistory()
    const { isAuthenticated } = useAuth0()
    const { token } = useAuth0Query()
    const [file,setFile] = useState()
    const [base64Img, setBase64Img] = useState('')

    const [name,setName] = useState('')
    const [description,setDescription] = useState('')
    const [price,setPrice] = useState('')
    const [year,setYear] = useState('')
    const [bidYear,setBidYear] = useState('')


    const {
        acceptedFiles,
        getRootProps,
        getInputProps,
        isFocused,
        isDragAccept,
        isDragReject,
    } = useDropzone({
        accept: "image/*",
        maxFiles: 1,
        onDrop:async (acceptedFiles) => {
            setFile(acceptedFiles)

            // handleFile(acceptedFiles);
        },
    });

    //console.log(file,base64Img)

    useEffect(() => {
        if(file){
            toBase64(file[0]).then((temp) => {
                setBase64Img(temp)
            })
        }
    },[file])

    const style = useMemo(
        () => ({
            ...baseStyle,
            ...(isFocused ? focusedStyle : {}),
            ...(isDragAccept ? acceptStyle : {}),
            ...(isDragReject ? rejectStyle : {}),
        }),
        [isFocused, isDragAccept, isDragReject]
    );

    const { data: memberData, isLoading  } = hooks.useCurrentMember(
        {isAuthenticated,
            token:token()
        })

    const { data: ownPaintings, isLoading: isLoadingOwn } = hooks.useOwnPainting(
        {isAuthenticated,
            token:token()
        })

    const { data: ganPainting, isLoading: isLoadingGAN } = hooks.useGANPainting(
        {isAuthenticated,
            token:token()
        })

    const { data: painterPainting, isLoading: isLoadingPainter } = hooks.usePainterPainting(
        {isAuthenticated,
            isArtist: memberData?.role==="Painter",
            token:token()
        })

    const { data: artistData, isLoading: isLoadingArtist } =
        hooks.useCurrentArtist({
            isArtist: memberData?.role==="Painter",
            token:token()
        })


    const { mutateAsync: mutateAsyncPainting} = useMutation(MUTATION_KEYS.POST_PAINTING);
    const { mutateAsync: mutateAsyncImage } = useMutation(MUTATION_KEYS.POST_IMAGE);

    if (isLoading ||
        isLoadingArtist ||
        isLoadingGAN ||
        isLoadingOwn ||
        isLoadingPainter
    ) {
        return (
            <div style={{height:'calc(100vh - 74px)',width:'100%',display:'flex'}}>
                <CircularProgress sx={{margin:'auto'}} />
            </div>
        )
    }

    function getSwiper() {
        console.log()
        if (ownPaintings.length == 0){
            return(
            <div>

                <div className="page">
                    <div className="registration-customer-introduction-section" style={{backgroundColor: "white", paddingTop: "15px"}}>
                        <Grid container>
                            <Grid item xs={12}>
                                <Typography variant={"h4"}>
                                    You are Missing Out! Find Your Paintings Now
                                </Typography>
                                <Typography variant={"h6"} sx={{color: 'grey'}}>
                                    {/*Please note that you are registering as a member, if you're an artist register*/}
                                </Typography>
                                <div style={{justifyContent: "center", display: "flex", paddingTop: "15px"}}>
                                    <Button
                                        style={{color: "white", backgroundColor: ARTIST_GREEN}}
                                        variant={"contained"}
                                        onClick={() => push(`/hire-a-painter?page=1&min=0&max=5000`)}
                                    >
                                        Find Yours
                                    </Button>
                                </div>
                            </Grid>
                        </Grid>
                    </div>
                </div>

            </div>
            )
        }
        return <>
            {ownPaintings.map((e, i) => {
                return (
                    <SwiperSlide key={i}>
                        <PainterCards
                            data={e}
                        />
                    </SwiperSlide>
                )
            })}
        </>;
    }

    function getTypographyForOwnedItems() {
        if (ownPaintings.length != 0) {
            return <> Your Own Items</>;
        }
    }

    function getPaintingsUploaded() {
        // console.log(memberData)
        // if(painterPainting.length != 0) {
        //     return <> Your Paintings</>;
        // }
        return (
            <div className="page">
                <div  style={{backgroundColor: "white", paddingTop: "15px"}}>
                    <Grid container>
                        <Grid item xs={12}>
                            {/*<Typography variant={"h4"} style ={{color: "black"}}>*/}
                            {/*    What Are You Waiting For! Share Your Paintings With The World*/}
                            {/*</Typography>*/}
                            <Typography variant={"h6"} sx={{color: 'grey'}}>
                                {/*Please note that you are registering as a member, if you're an artist register*/}
                            </Typography>
                            <div style={{justifyContent: "center", display: "flex", paddingTop: "15px"}}>
                                <div>
                                    <Grid container padding={'30px 120px'} spacing={2}>
                                        <Grid item xs={12} sm={12} md={12} lg={12}>
                                            <div>
                                                <div className={"frame-settings"}>
                                                    <Grid container spacing={2}>
                                                        <Grid item xs={12} sm={12} md={6} lg={6} >
                                                            <img
                                                                src={base64Img}
                                                                style={{
                                                                    borderRadius:'20px',
                                                                    width:'100%',

                                                                }} className={
                                                                "paintingImage"
                                                            } />

                                                            <div className={"details-container"}>
                                                                <div style={{ margin: "10px 0" }}>
                                                                    <div {...getRootProps({ style })}>
                                                                        <input {...getInputProps()} />
                                                                        <div style={{ cursor: "pointer" }}>
                                                                            <CloudUploadIcon />
                                                                        </div>
                                                                        <p>Upload unrevealed picture</p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </Grid>
                                                        <Grid item xs={12} sm={12} md={6} lg={6} >
                                                            <div className={"details-container"} style={{height:'100%'}}>
                                                                <CustomTextField
                                                                    style={{
                                                                        marginTop:'16px'
                                                                    }}
                                                                    variant={"filled"}
                                                                    label={"Name"}

                                                                    value={name}
                                                                    placeholder={"Name"}
                                                                    onChange={(e) => setName(e.target.value)}
                                                                />
                                                                <CustomTextField
                                                                    style={{
                                                                        marginTop:'16px'
                                                                    }}
                                                                    variant={"filled"}
                                                                    label={"Description"}
                                                                    value={description}
                                                                    placeholder={"Description"}
                                                                    onChange={(e) => setDescription(e.target.value)}
                                                                />
                                                                <CustomTextField
                                                                    style={{
                                                                        marginTop:'16px'
                                                                    }}
                                                                    variant={"filled"}
                                                                    label={"Year"}
                                                                    value={year}
                                                                    placeholder={"Year"}
                                                                    onChange={(e) => setYear(e.target.value)}
                                                                />
                                                                <CustomTextField
                                                                    style={{
                                                                        marginTop:'16px'
                                                                    }}
                                                                    variant={"filled"}
                                                                    label={"Price"}
                                                                    value={price}
                                                                    InputProps={{
                                                                        startAdornment: <InputAdornment position="start">$</InputAdornment>,
                                                                    }}
                                                                    placeholder={"Price"}
                                                                    onChange={(e) => setPrice(e.target.value)}
                                                                />
                                                                <LocalizationProvider dateAdapter={AdapterMoment}>
                                                                    <DatePicker
                                                                        disablePast
                                                                        label="Bid Date"
                                                                        openTo="year"
                                                                        views={['year', 'month', 'day']}
                                                                        format="mm/dd/yyyy"
                                                                        value={bidYear}
                                                                        onChange={value => {
                                                                            setBidYear( moment(value).format("MM/DD/YYYY"))
                                                                        }}
                                                                        renderInput={(params) =>
                                                                            <CustomTextField
                                                                                id="birthDate"

                                                                                style={{
                                                                                    marginTop:'16px'
                                                                                }}
                                                                                // onBlur={handleBlur}
                                                                                variant={"filled"}
                                                                                fullWidth
                                                                                {...params} />
                                                                        }
                                                                    />
                                                                </LocalizationProvider>
                                                                <ArtistButton
                                                                    variant={'contained'}
                                                                    style={{
                                                                        marginTop:'16px'
                                                                    }}
                                                                    disabled={
                                                                        !name ||
                                                                        !description ||
                                                                        ! year ||
                                                                        !price ||
                                                                        !bidYear ||
                                                                        !file
                                                                    }
                                                                    onClick={() => {
                                                                        mutateAsyncPainting({
                                                                            token: token(),
                                                                            payload: {
                                                                                "paintingName": name,
                                                                                "paintingDescription": description,
                                                                                "paintedYear": year,
                                                                                "paintingPrice": parseInt(price),
                                                                                "paintingStatus": 'For Sale',
                                                                                "buyTimeStamp": moment(bidYear).format("YYYY-MM-DD")
                                                                            },
                                                                            painterId: artistData.painterId
                                                                        }).then(async (e) => {

                                                                           const formData = new FormData()
                                                                           const blob = new Blob([file[0]],{type: file[0].type})

                                                                            console.log(blob)

                                                                            formData.append("image",blob)

                                                                            console.log(formData)

                                                                            //const formData = new FormData()
                                                                            // formData.append("image", ganResult.blob);
                                                                            // console.log(formData.get('image'))
                                                                            const t = await token()
                                                                            await axios.post('https://artist-block-account-service.herokuapp.com/api/v1/create-painting/image/'+e.paintingId,
                                                                                formData
                                                                                , {
                                                                                    headers: {

                                                                                        Authorization: `Bearer ${t}`,
                                                                                    }
                                                                                })

                                                                            //
                                                                            // mutateAsyncImage({
                                                                            //     token: token(),
                                                                            //     paintingId: e.paintingId,
                                                                            //     formData: formData
                                                                            //
                                                                            // })
                                                                        })


                                                                    }}
                                                                >
                                                                    Submit
                                                                </ArtistButton>
                                                            </div>
                                                        </Grid>
                                                    </Grid>
                                                </div>
                                            </div>
                                        </Grid>
                                    </Grid>
                                </div>
                            </div>
                        </Grid>
                    </Grid>
                </div>
            </div>
        )
    }

    const getGanTypography = () => {
        if(ganPainting.length == 0){
            return (
            <div>

                <div className="page">
                    <div className="registration-customer-introduction-section" style={{backgroundColor: "white", paddingTop: "15px"}}>
                        <Grid container>
                            <Grid item xs={12}>
                                <Typography variant={"h4"}>
                                    What? You Haven't Tried Our Gan Yet?
                                </Typography>
                                <Typography variant={"h6"} sx={{color: 'grey'}}>
                                    {/*Please note that you are registering as a member, if you're an artist register*/}
                                </Typography>
                                <div style={{justifyContent: "center", display: "flex", paddingTop: "15px"}}>
                                    <Button
                                        style={{color: "white", backgroundColor: ARTIST_GREEN}}
                                        variant={"contained"}
                                        onClick={() => push(`/gan`)}
                                    >
                                        Check It Out Now
                                    </Button>
                                </div>
                            </Grid>
                        </Grid>
                    </div>
                </div>

            </div>
            )
        } else {
            return <Typography variant={"h6"} color={"primary"} textAlign={"center"}>
                Your GAN Generated Images
            </Typography>;
        }
    }

    return (
        <div>
            <Grid container px={'5vw'} mt={2}>
                <Grid item xs={12}>
                    <Typography variant={"h2"} textAlign={"center"} color={"primary"}>
                        Welcome to your Personal Museum
                    </Typography>
                </Grid>
                <Grid item xs={12} mt={4}>
                    <Typography variant={"h6"} color={"primary"} textAlign={"center"}>
                        {getTypographyForOwnedItems()}
                    </Typography>
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
                <Grid item xs={12} mt={4}>
                    {getGanTypography()}
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
                        {ganPainting.map((e, i) => {
                            return (
                                <SwiperSlide key={i}>
                                    <GanCard
                                        data={e}
                                    />
                                </SwiperSlide>
                            )
                        })}
                    </Swiper>
                </Grid>
                {
                    memberData?.role === "Painter" &&
                    <Grid item xs={12} mt={4}>
                        <Typography variant={"h6"} color={"primary"} textAlign={"center"}>
                            Your Art
                        </Typography>
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
                            {painterPainting.map((e, i) => {
                                return (
                                    <SwiperSlide key={i}>
                                        <PainterCards
                                            data={e}
                                        />
                                    </SwiperSlide>
                                )
                            })}
                        </Swiper>
                        {getPaintingsUploaded()}
                    </Grid>
                }


            </Grid>



        </div>
    );
};

export default UserProfile;
