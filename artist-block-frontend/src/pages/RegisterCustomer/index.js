import React, {useCallback, useEffect, useRef, useState} from 'react';
import {Avatar, Box, Button, Grid, MenuItem, Modal, TextField, Typography} from "@mui/material";
import LocalizationProvider from '@mui/lab/LocalizationProvider';
import AdapterMoment from '@mui/lab/AdapterMoment';
import styled from "@emotion/styled";
import {COUNTRIES, ARTIST_GREEN, HOME_ROUTE, REGISTRATION_LAWYER_ROUTE, TITLES} from "../../utils/constants";
import {useAuth0} from "@auth0/auth0-react";
import {Autocomplete, DatePicker} from "@mui/lab";

import MuiPhoneNumber from "material-ui-phone-number";
import {hooks, useMutation} from "../../query";
import {MUTATION_KEYS} from "../../query/config/keys";
import useAuth0Query from "../../hooks/useAuth0Query";
import {Link, Redirect} from "react-router-dom";
import {LANGUAGES} from "../../utils/constants/languages";
import './styles.scss';
import {useFormik} from "formik";
import {customerInitialValue, customerSchema} from "../../utils/schemas/customer";
import moment from "moment";
import {GENDERS} from "../../utils/constants/gender";
import PersonIcon from '@mui/icons-material/Person';
import { useHistory } from 'react-router-dom'
import ReactCrop, {centerCrop, makeAspectCrop} from "react-image-crop";
import {cropPreview} from "../../utils/helpers/cropPreview";
import 'react-image-crop/dist/ReactCrop.css'
import ArtistButton from "../../components/ArtistButton";

export const CustomTextField = styled(TextField)({
    '& .MuiFilledInput-root': {
        borderRadius: '10px !important',
    },
    '& .MuiFilledInput-underline:before': {
        borderBottom: 'none !important',
    },
    '& .MuiFilledInput-underline:after': {
        borderBottom: 'none !important',
    },
});

const Input = styled('input')({
    display: 'none',
});

const RegistrationCustomer = () => {

    const {mutate, isLoading} = useMutation(MUTATION_KEYS.REGISTER_MEMBER)
    const {mutateAsync: mutateImage, isLoading: isLoadingImage} = useMutation(MUTATION_KEYS.POST_MEMBER_IMAGE)

    const {user} = useAuth0()
    const {token} = useAuth0Query()
    const { push } = useHistory()
    const {data} = hooks.useCurrentMember({isAuthenticated: false, token:token()})
    const imgRef = useRef(null)
    const [uploaded,setUploaded] = useState(false)
    const previewCanvasRef = useRef(null)
    const previewCanvasPageRef = useRef(null)
    const [open, setOpen] = useState(false);
    const [ imageError,setImageError] = useState('')
    const [crop, setCrop] = useState()
    const [imgSrc, setImgSrc] = useState('')
    const [previewCanvasState, setPreviewCanvasState] = useState(null)
    const [completedCrop, setCompletedCrop] = useState()
    const [scale, setScale] = useState(1)
    const [rotate, setRotate] = useState(0)

    const handleOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const {
        handleSubmit,
        handleChange,
        values,
        errors,
        handleBlur,
        setFieldValue,
        setFieldTouched,
        isSubmitting,
        isValid,
        touched,

    } = useFormik({
        validationSchema: customerSchema,
        initialValues: customerInitialValue,
        onSubmit: (values) => {
            // setUploaded(true)

            console.log("efveveve",values)
            // console.log({
            //         payload: {
            //             "firstName": values.firstName,
            //             "lastName": values.lastName,
            //             "title": values.title,
            //             "nationality": values.nationality,
            //             // "email": user.email,
            //             // "phoneNumber": values.phoneNumber.number,
            //             // "birthDate": moment(values.birthDate).format("YYYY-MM-DD"),
            //             }
            //     })
        }
    });

    const updateCropPreview = useCallback(() => {
        if (completedCrop && previewCanvasRef.current && imgRef.current && previewCanvasPageRef.current) {
            cropPreview(imgRef.current, previewCanvasRef.current, completedCrop, scale, rotate)
            cropPreview(imgRef.current, previewCanvasPageRef.current, completedCrop, scale, rotate)
        }
        previewCanvasPageRef?.current?.toBlob((e) => console.log(e))
    }, [completedCrop, scale, rotate])

    useEffect(() => {
        updateCropPreview()
    }, [updateCropPreview])

    useEffect(() => {
        previewCanvasPageRef?.current?.toBlob((e) => console.log(e))
    },[previewCanvasPageRef?.current])


    useEffect(() => {
        if(uploaded && !isLoading){
            mutateImage({
                token:token(),
                formData: previewCanvasState
            }).then(() =>
                push(HOME_ROUTE.path)
            )
        }
    },[uploaded, isLoading])

    if (data && !uploaded) {
        return <Redirect to={HOME_ROUTE.path} />
    }

    const onSelectFile = (e) => {
        if (e.target.files && e.target.files.length > 0) {
            if (e.target.files[0].size > 4e6) {
                setImageError('Please upload a file smaller than 4 MB')
            }
            else{
                setCrop(undefined) // Makes crop preview update between images.
                const reader = new FileReader()
                setImageError('')
                reader.addEventListener('load', () => setImgSrc(reader.result.toString() || ''))
                reader.readAsDataURL(e.target.files[0])
            }

        }
    }

    const onImageLoad = (e)=>  {
        imgRef.current = e.currentTarget

        const { width, height } = e.currentTarget

        // This is to demonstate how to make and center a % aspect crop
        // which is a bit trickier so we use some helper functions.
        const crop = centerCrop(
            makeAspectCrop(
                {
                    unit: 'px',
                    width: 150,
                },
                1,
                width,
                height,
            ),
            width,
            height,
        )

        setCrop(crop)
    }



    return (
        <div className="page">
            <div className="registration-customer-introduction-section">
                <Grid container>
                    <Grid item xs={12}>
                        <Typography variant={"h3"}>
                            Register to Artist Block
                        </Typography>

                        <Typography variant={"h6"} sx={{color: '#808080'}}>
                            Please note that you are registering as a member, if you're an artist register <Link
                            to={REGISTRATION_LAWYER_ROUTE.path} style={{color: ARTIST_GREEN}}>here</Link>
                        </Typography>
                    </Grid>
                </Grid>
            </div>
            <div className={"registration-customer-section"}>
                <Modal
                    open={open}
                    onClose={handleClose}
                    aria-labelledby="parent-modal-title"
                    aria-describedby="parent-modal-description"
                >
                    <Box sx={{
                        position: 'absolute',
                        top: '50%',
                        left: '50%',
                        transform: 'translate(-50%, -50%)',
                        width: 400,
                        bgcolor: '#f8f8f8',
                        boxShadow: 24,
                        pt: 2,
                        px: 4,
                        pb: 3,}}>
                        <Box>
                            <Typography variant={"h5"} textAlign={'center'}>
                                Please Choose a Profile Picture
                            </Typography>
                            <div style={{margin:'5px 0'}}>
                                <div style={{display:'flex',width:'100%',placeContent:'center'}}>
                                    <label htmlFor="contained-button-file">
                                        <Input accept="image/*" id="contained-button-file" onChange={onSelectFile} type="file" />
                                        <ArtistButton variant="contained" component="span">
                                            Choose a Picture
                                        </ArtistButton>
                                    </label>
                                </div>
                                { imageError!=="" &&
                                <div style={{display:'flex',width:'100%',placeContent:'center'}}>
                                    <Typography variant={"body2"}>
                                        {imageError}
                                    </Typography>
                                </div>
                                }
                            </div>

                            {
                                Boolean(imgSrc) &&
                                <div>
                                    <ReactCrop
                                        crop={crop}
                                        onChange={(_, percentCrop) => setCrop(percentCrop)}
                                        onComplete={(c) => setCompletedCrop(c)}
                                        aspect={1}
                                    >
                                        <img
                                            alt="Crop me"
                                            src={imgSrc}
                                            style={{ transform: `scale(${scale}) rotate(${rotate}deg)`, maxHeight:'60vh' }}
                                            onLoad={onImageLoad}
                                        />
                                    </ReactCrop>

                                    <div style={{display:'flex'}}>
                                        <canvas
                                            ref={previewCanvasRef}
                                            style={{
                                                width: Math.floor(100),
                                                height: Math.floor(100),
                                                margin:'auto'
                                            }}
                                        />
                                    </div>

                                    <div style={{display:'flex', placeContent:'space-around'}}>
                                        <ArtistButton style={{margin: '10px auto'}} variant={"outlined"}
                                                      onClick={() => {
                                                          setImgSrc('')
                                                          handleClose()
                                                      }}>
                                            Cancel
                                        </ArtistButton>
                                        <ArtistButton style={{margin: '10px auto'}} variant={"contained"}
                                                      onClick={() => {
                                                          previewCanvasPageRef.current.toBlob(function (blob) {
                                                              let formData = new FormData();
                                                              formData.append("image", blob);
                                                              setPreviewCanvasState(formData)
                                                          },"image/jpeg",0.6)
                                                          handleClose()
                                                      }}>
                                            Save Image
                                        </ArtistButton>
                                    </div>
                                </div>
                            }
                        </Box>
                    </Box>
                </Modal>
                <form onSubmit={handleSubmit}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={12} lg={12}>
                            {
                                Boolean(imgSrc)?
                                    <div style={{display:'flex'}}>
                                        <canvas
                                            ref={previewCanvasPageRef}
                                            style={{
                                                width: Math.floor(100 ),
                                                height: Math.floor(100),
                                                margin:'auto',
                                                borderRadius:'100px',
                                                border:`3px solid ${ARTIST_GREEN}`,
                                                cursor:'pointer'
                                            }}
                                            onClick={handleOpen}

                                        />
                                    </div>
                                    :
                                    <Avatar
                                        variant={"circular"}
                                        sx={{
                                            width:'100px',
                                            height:'100px',
                                            margin:'auto',
                                            border: `3px solid ${ARTIST_GREEN}`,
                                            cursor:'pointer'
                                        }}
                                        onClick={handleOpen}
                                    >
                                        <PersonIcon />
                                    </Avatar>
                            }

                        </Grid>
                        <Grid item xs={12} md={6} lg={6}>
                            <CustomTextField
                                id="firstName"
                                label="First Name"
                                variant={"filled"}
                                required
                                value={values.firstName}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                helperText={touched.firstName ? errors.firstName : ""}
                                error={touched.firstName && Boolean(errors.firstName)}
                                fullWidth
                            />
                        </Grid>
                        <Grid item xs={12} md={6} lg={6}>
                            <CustomTextField
                                id="lastName"
                                label="Last Name"
                                variant={"filled"}
                                required
                                value={values.lastName}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                helperText={touched.lastName ? errors.lastName : ""}
                                error={touched.lastName && Boolean(errors.lastName)}
                                fullWidth
                            />
                        </Grid>
                        <Grid item xs={12} md={5} lg={5}>
                            <CustomTextField
                                id="title"
                                label="Title"
                                variant={"filled"}
                                select
                                required
                                value={values.title}
                                onChange={handleChange("title")}
                                onBlur={()=> setFieldTouched("title")}
                                helperText={touched.title ? errors.title : ""}
                                error={touched.title && Boolean(errors.title)}
                                fullWidth
                            >
                                {
                                    TITLES.map((e) => (
                                            <MenuItem key={e} value={e}>{e}</MenuItem>
                                        )
                                    )
                                }

                            </CustomTextField>
                        </Grid>
                        <Grid item xs={12} sm={12} md={7} lg={7}>
                            <LocalizationProvider dateAdapter={AdapterMoment}>
                                <DatePicker
                                    disableFuture
                                    label="Birth Date"
                                    openTo="year"
                                    views={['year', 'month', 'day']}
                                    format="MM/dd/yyyy"

                                    value={values.birthDate}
                                    onChange={value => {
                                        setFieldValue("birthDate",moment(value))
                                        setFieldTouched("birthDate")
                                    }}
                                    renderInput={(params) =>
                                        <CustomTextField
                                            variant={"filled"}
                                            fullWidth
                                            id={"birthDate"}
                                            onBlur={handleBlur}
                                            required
                                            value={values.birthDate}
                                            helperText={touched.birthDate ? errors.birthDate : ""}
                                            error={touched.birthDate && Boolean(errors.birthDate)}
                                            {...params} />
                                    }
                                />
                            </LocalizationProvider>
                        </Grid>
                        <Grid item xs={12} md={12} lg={12}>
                            <CustomTextField
                                variant={"filled"}
                                label={"Email"}
                                fullWidth
                                value={user?.email}

                                disabled
                            />
                        </Grid>
                        <Grid item xs={12} md={12} lg={12}>
                            <Autocomplete
                                id="nationality"
                                options={COUNTRIES.sort((a,b) => (a.label > b.label) ? 1 : ((b.label > a.label) ? -1 : 0))}
                                autoHighlight
                                fullWidth
                                onChange={(event, newValue) => {
                                    setFieldValue("nationality", newValue.label)
                                }}
                                onBlur={handleBlur}
                                getOptionLabel={(option) => option.label}
                                renderOption={(props, option) => (
                                    <Box component="li" sx={{'& > img': {mr: 2, flexShrink: 0}}} {...props}>
                                        <img
                                            loading="lazy"
                                            width="20"
                                            src={`https://flagcdn.com/w20/${option.code.toLowerCase()}.png`}
                                            srcSet={`https://flagcdn.com/w40/${option.code.toLowerCase()}.png 2x`}
                                            alt=""
                                        />
                                        {option.label}
                                    </Box>
                                )}
                                renderInput={(params) => (
                                    <CustomTextField
                                        {...params}
                                        required
                                        variant={"filled"}
                                        label="Nationality"
                                        value={values.nationality}
                                        name={"nationality"}
                                        id={"nationality"}
                                        fullWidth
                                        helperText={touched.nationality ? errors.nationality : ""}
                                        error={touched.nationality && Boolean(errors.nationality)}
                                        inputProps={{
                                            ...params.inputProps,
                                            autoComplete: 'new-password', // disable autocomplete and autofill
                                        }}
                                    />
                                )}
                            />
                        </Grid>
                        <Grid item xs={12} md={12} lg={12}>
                            <MuiPhoneNumber
                                variant={"filled"}
                                fullWidth
                                required
                                defaultCountry={'lb'}
                                id={"phoneNumber"}
                                onBlur={handleBlur}
                                className={"phone-field"}
                                value={values.phoneNumber?.number}
                                onChange={(e, newValue) => {
                                    setFieldValue("phoneNumber", {
                                        number:e,
                                        metadata:newValue
                                    })
                                }}
                                label={"Phone Number"}
                                helperText={touched.phoneNumber ? errors.phoneNumber : ""}
                                error={touched.phoneNumber && Boolean(errors.phoneNumber)}

                            />
                        </Grid>
                        <Grid item xs={12} md={12} lg={12}>
                            <div style={{
                                margin: '0 auto',
                                width: '100%',
                                display: 'flex',
                                placeContent: 'center'
                            }}>
                                <ArtistButton variant={"contained"} type={"submit"}
                                              onClick={() => {
                                                  mutate({
                                                      token:token(),
                                                      payload:  {
                                                          "firstName": values.firstName,
                                                          "lastName": values.lastName,
                                                          "title": values.title,
                                                          "nationality": values.nationality,
                                                          "email": user.email,
                                                          "phoneNumber": values.phoneNumber.number,
                                                          "birthDate": moment(values.birthDate).format("YYYY-MM-DD"),
                                                      }
                                                  })
                                              }}
                                              disabled={
                                                  !(values.firstName &&
                                                      values.lastName &&
                                                      values.birthDate &&
                                                      values.title &&
                                                      values.nationality &&
                                                      values.phoneNumber &&
                                                      isValid)
                                              }
                                >
                                    Submit
                                </ArtistButton>
                            </div>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </div>
    );
};

export default RegistrationCustomer;
