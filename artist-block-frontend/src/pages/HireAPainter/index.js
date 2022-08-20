import React, {useEffect, useState} from 'react';
import {Box, CircularProgress, Grid, Slider, SwipeableDrawer, Typography, useMediaQuery, useTheme} from "@mui/material";
import {ARTIST_GREEN, ARTIST_LIGHT_GREY, HIRE_A_PAINTER_ROUTE, NAVBAR_HEIGHT} from "../../utils/constants";
import PainterCards from "../../components/PainterCards";
import {Pagination} from "@mui/lab";
import ScrollableView from "../../components/ScrollableView";
import './styles.scss';
import {useHistory, useLocation} from "react-router-dom";
import {LANGUAGES} from "../../utils/constants/languages";
import {hooks} from "../../query";
import {useAuth0} from "@auth0/auth0-react";
import {CustomTextField} from "../RegisterCustomer";
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import {scroller} from "react-scroll";
import ArtistButton from "../../components/ArtistButton";
import PainterCardsFake from "../../components/PainterCardsFake";
import * as ReactGA from "react-ga";
import {AdapterDateFns} from '@mui/x-date-pickers/AdapterDateFns';
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider';
import {StaticDatePicker} from "@mui/x-date-pickers";

function useQuery() {
    const { search } = useLocation();

    return React.useMemo(() => new URLSearchParams(search), [search]);
}

const HireAPainter = () => {

    const minDate = new Date('1990-01-01T00:00:00.000');
    const maxDate = new Date();

    const [year,setYear] = useState('')
    const { isLoading: isLoadingAuth, isAuthenticated,getAccessTokenSilently} = useAuth0()
    const theme = useTheme()
    const { push } = useHistory()
    const query = useQuery()
    const params = Object.fromEntries(query.entries());
    const isSmall = useMediaQuery(theme.breakpoints.down('md'));
    const [pageNum,setPageNum] = useState(() =>{
        if(params.page){
            return parseInt(params.page)
        }
        return 1
    })
    const [openDrawer,setOpenDrawer] = useState(false)
    const [openSidebar, setOpenSidebar] = useState(false)
    const [speciality,setSpeciality] = useState(() =>{
        if(params.speciality){
            return params.speciality
        }
        return ''
    })
    const [search,setSearch] = useState(() =>{
        if(params.search){
            return params.search
        }
        return ''
    })
    const [rate, setRate] = React.useState(()=>{
        if(params.min && params.max){
            return [params.min,params.max]
        }
        return [0,5000]
    });

    const [languages,setLanguages] = useState(() => {
        const languages = query.getAll('languages')

        const obj = [];
        LANGUAGES.map((e)=> {
            if(languages.includes(e.name)){
                obj.push(e)
            }
        })
        return obj

    })



    const getNavbarHeight = () => {
        return ((isAuthenticated && !isLoading && !data) ||  !isAuthenticated)? NAVBAR_HEIGHT + 50:NAVBAR_HEIGHT
    }

    const searchParams = () => {
        const obj = {pageNum,pageSize:isSmall? 6: 10,rate}
        const countriesArr = []
        const languagesArr = []
        // Object.keys(countries).forEach((e)=>countries[e] && countriesArr.push(e))
        // languages.forEach((e)=>  languagesArr.push(e.name))
        // if(languagesArr.length!==0){
        //     obj.languages = languagesArr
        // }
        // if(countriesArr.length!==0){
        //     obj.countries = countriesArr
        // }

        if(rate[1]>=5000){
            obj.rate= [rate[0],undefined]
        }
        else {
            obj.rate= [rate[0],rate[1]]
        }
        if(speciality.length!==0){
            obj.speciality = speciality.toLowerCase()
        }

        if(search.length!==0){
            obj.search = search
        }

        if(year!==""){
            obj.year = year.getFullYear()
        }
        return obj
    }

    console.log(year)
    const checkAuth = () => {
        return isAuthenticated && !isLoadingAuth? getAccessTokenSilently() : undefined
    }

    const { data,refetch,isLoading,isRefetching } = hooks.useSearch({
            payload: {...searchParams()},
            token: checkAuth()
        }
    )

    const {data:currentUser, isLoading: isLoadingUser} = hooks.useCurrentMember({isAuthenticated: isAuthenticated, token:checkAuth()})

    const toggleDrawer = (open) => (event) => {
        setOpenDrawer(open)
    }

    // const handleCountryChange = (event) => {
    //     setCountries({
    //         ...countries,
    //         [event.target.name]: event.target.checked,
    //     });
    //
    // };

    const handleLanguageChange = (event) => {
        setLanguages(event);

    };

    const handleSpecialityChange = (event) => {
        setSpeciality(event? event: '')
    }

    const handleSearchChange = (event) => {
        setSearch(event.target.value)
    }

    const handleRateChange = (event, newValue) => {
        setRate(newValue);

    };

    const handleOpenSideBar = () => {
        setOpenSidebar(!openSidebar)
    }


    useEffect(() => {
        refetch()
    },[query.toString(), isLoadingAuth, currentUser, isAuthenticated])

    useEffect(() => {
        let searchParam= new URLSearchParams()

        search.length>0 && searchParam.append('search',search)
        searchParam.append('page',""+pageNum)
        languages.forEach((e) => {
            // if(languages[e]){
            searchParam.append('languages',e.name)
            // }
        })

        // Object.keys(countries).forEach((e) => {
        //     if(countries[e]){
        //         searchParam.append('countries',e)
        //     }
        // })


        searchParam.append('min',""+rate[0])
        searchParam.append('max',""+rate[1])

        speciality.length>0 && searchParam.append('speciality',speciality)

        push({
            pathname: HIRE_A_PAINTER_ROUTE.path,
            search: "?" + searchParam.toString()
        })
        refetch()
    },[search, pageNum, currentUser])

    const saveFilters = () => {
        ReactGA.event({
            category: 'Hire A Painter',
            action: 'Save Filter',
        });
        let searchParam= new URLSearchParams()
        languages.forEach((e) => {
            // if(languages[e]){
            searchParam.append('languages',e.name)
            // }
        })

        // Object.keys(countries).forEach((e) => {
        //     if(countries[e]){
        //         searchParam.append('countries',e)
        //     }
        // })


        searchParam.append('min',""+rate[0])
        searchParam.append('max',""+rate[1])

        searchParam.append('page',""+1)

        speciality.length>0 && searchParam.append('speciality',speciality)
        search.length>0 && searchParam.append('search',search)

        setPageNum(1)
        push({
            pathname: HIRE_A_PAINTER_ROUTE.path,
            search: "?" + searchParam.toString()
        })
        if(!isLoadingAuth){
            refetch()
        }
    }




    const searchMain = () => (
        <Box
            component="main"
            sx={{ flexGrow: 1, p: 3 }}
        >
            <Grid container spacing={3} padding={"1% 3%"}>
                <Grid item xs={12} sm={12} md={6} lg={6}>
                    <Typography variant={isSmall?"h4":"h3"}>
                        Search for your <span style={{color: ARTIST_GREEN}}>Paintings</span>
                    </Typography>
                </Grid>
                <Grid item xs={12} sm={12} md={6} lg={6}>
                    <CustomTextField
                        value={search}
                        fullWidth
                        onChange={handleSearchChange}
                        variant={"filled"}
                        placeholder={"Search by name"}
                        label={"Search"}
                        />
                </Grid>
                <Grid item xs={12}>

                    <div style={{display:'flex',placeContent:'space-between'}}>
                        <Typography variant={isSmall?"body2":"body1"}>
                            {data?.pagination?.TotalCount} results
                        </Typography>
                        {
                            isSmall &&
                            <Typography onClick={() => setOpenDrawer(true)} variant={"body2"} sx={{color: ARTIST_LIGHT_GREY}}>
                                Filter
                            </Typography>
                        }
                    </div>
                </Grid>
                <Grid item width={"100%"}>
                    <Grid container spacing={isSmall? 2: 4} sx={{placeContent:'space-evenly'}}>
                        {
                                !isRefetching && data?.artists?.map((e,index) => (
                                <Grid item key={index}  xs={6} sm={6} md={4} lg={'auto'}>
                                    <div style={{height:'100%'}}>
                                        <PainterCards data={e} />
                                    </div>
                                </Grid>
                            ))
                        }
                        {
                            isRefetching &&
                            Array.from(Array(isSmall? 3:7).keys()).map((e, index)=>(
                                    <Grid item key={'loading-'+index}  xs={6} sm={6} md={4} lg={'auto'}>
                                        <div className={"loading-item"} style={{height:'100%'}}>
                                            <PainterCardsFake />
                                        </div>
                                    </Grid>
                                )

                            )                        }
                        {
                            data?.artists?.length===0 &&
                                <Typography variant={"h5"}>
                                    No Painting found with the selected criteria
                                </Typography>
                        }

                    </Grid>
                </Grid>
                <Grid item xs={12}>
                    <Pagination page={pageNum}
                                onChange={((event, page) => {
                                    setPageNum(page)
                                    scroller.scrollTo("ScrollableContainer", {
                                        containerId: "scroll-id",
                                        hashSpy: true,
                                        spy: true,
                                        smooth: true,
                                        offset: 0,
                                    });
                                })}
                                color="primary"
                                count={currentUser? data?.pagination?.TotalPages : 1}
                                size="small"
                                sx={{display:'flex',placeContent:'center'}} />
                </Grid>
            </Grid>
        </Box>
    )

    const sidePanel = () => (
        <Box
            flex
            // width={drawerWidth}
            bgcolor={'rgb(248, 248, 248)'}
            sx={{ display: 'flex' }}
        >
            <Box width={"fit-content"} sx={{ margin: 'auto',flexDirection:'column',placeContent:'center'}}>

                <ChevronLeftIcon
                    onClick={handleOpenSideBar}
                    cursor={"pointer"}
                    className={`filterIcon ${openSidebar && "open"}`}
                />
            </Box>
            <Box className={`side-panel-filters ${openSidebar && "open"}`} >
                <div>
                    <Typography color={"primary"} variant={"h5"}>
                        Filter By
                    </Typography>

                    <Typography variant={"body1"} sx={{margin:'20px 0 15px 0',color:`${ARTIST_LIGHT_GREY} !important`}}>
                        Year
                    </Typography>
                    <LocalizationProvider dateAdapter={AdapterDateFns}>
                        <StaticDatePicker
                            views={['year']}
                            displayStaticWrapperAs="desktop"
                            maxDate={maxDate}
                            label="Basic example"
                            value={year}
                            onChange={(newValue) => {
                                setYear(newValue);
                            }}
                            // renderInput={(params) => <TextField {...params} />}
                        />
                    </LocalizationProvider>
                    {/*<Autocomplete*/}
                    {/*    multiple*/}

                    {/*    value={languages}*/}
                    {/*    aria-required*/}
                    {/*    onChange={(e, newValue) => {*/}
                    {/*        handleLanguageChange(newValue)*/}
                    {/*    }}*/}
                    {/*    options={LANGUAGES}*/}
                    {/*    getOptionLabel={(e) => e.name}*/}
                    {/*    renderInput={(params) => (*/}
                    {/*        <CustomTextField*/}
                    {/*            {...params}*/}
                    {/*            variant={"filled"}*/}
                    {/*            label="Languages"*/}
                    {/*        />*/}
                    {/*    )}*/}
                    {/*/>*/}
                        {/*<FormLabel sx={{color:`${ARTIST_LIGHT_GREY} !important`, marginTop:'20px'}}>Languages</FormLabel>*/}
                        {/*<FormGroup>*/}
                        {/*    {*/}
                        {/*        LANGUAGES.map((e) => (*/}
                        {/*            <FormControlLabel*/}
                        {/*                control={*/}
                        {/*                    <Checkbox checked={languages[e.name]} onChange={handleLanguageChange} name={e.name} />*/}
                        {/*                }*/}
                        {/*                label={e.name}*/}
                        {/*            />*/}
                        {/*        ))*/}
                        {/*    }*/}
                        {/*</FormGroup>*/}
                    {/*    <Autocomplete*/}
                    {/*        value={languages}*/}
                    {/*        aria-required*/}
                    {/*        onChange={(e, newValue) => {*/}
                    {/*            handleLanguageChange(newValue)*/}
                    {/*        }}*/}
                    {/*        options={LANGUAGES}*/}
                    {/*        getOptionLabel={(e) => e.name}*/}
                    {/*        renderInput={(params) => (*/}
                    {/*            <CustomTextField*/}
                    {/*                {...params}*/}
                    {/*                variant={"filled"}*/}
                    {/*                label="Languages"*/}
                    {/*            />*/}
                    {/*        )}*/}
                    {/*    />*/}
                    {/*</FormControl>*/}
                    {/*<Typography variant={"body1"} sx={{marginTop:'20px',color:`${ARTIST_LIGHT_GREY} !important`}}>*/}
                    {/*    Rating*/}
                    {/*</Typography>*/}
                    {/*<Rating*/}
                    {/*    name="half-rating"*/}
                    {/*    defaultValue={2.5}*/}
                    {/*    precision={0.5}*/}
                    {/*    value={rating}*/}
                    {/*    onChange={(event, newValue) => {*/}
                    {/*        setRating(newValue);*/}
                    {/*    }}*/}
                    {/*/>*/}

                    {/*<Typography variant={"body1"} sx={{margin:'20px 0',color:`${ARTIST_LIGHT_GREY} !important`}}>*/}
                    {/*    Specialization*/}
                    {/*</Typography>*/}
                    {/*<Autocomplete*/}
                    {/*    value={speciality}*/}
                    {/*    aria-required*/}
                    {/*    onChange={(e, newValue) => {*/}
                    {/*        handleSpecialityChange(newValue)*/}
                    {/*    }}*/}
                    {/*    options={SPECIALIZATIONS}*/}
                    {/*    renderInput={(params) => (*/}
                    {/*        <CustomTextField*/}
                    {/*            {...params}*/}
                    {/*            variant={"filled"}*/}
                    {/*            label="Specialization"*/}
                    {/*        />*/}
                    {/*    )}*/}
                    {/*/>*/}
                    <Typography variant={"body1"} sx={{marginTop:'20px',color:`${ARTIST_LIGHT_GREY} !important`}}>
                        Hourly Rate
                    </Typography>
                    <Slider min={0} max={5000} value={rate} onChange={handleRateChange} aria-label="Default" valueLabelDisplay="auto" />
                    <Typography color={"primary"} textAlign={"center"} variant={"body1"} >
                        ${rate[0]} - ${rate[1]>=5000? '5000+' : rate[1]}
                    </Typography>
                    <div
                     style={{display:'flex'}}
                    >
                        <ArtistButton
                            style={{
                                margin:'15px auto'
                            }}
                            variant={"contained"}
                            onClick={saveFilters}
                        >
                            Save
                        </ArtistButton>
                    </div>
                </div>
            </Box>

        </Box>
    )

    if(!data || isLoadingAuth ){
        return(
            <div style={{height: 'calc(100vh - 74px)', width: '100%', display: 'flex'}}>
                <CircularProgress sx={{margin: 'auto'}}/>
            </div>)
    }


    console.log(!data, data, isRefetching, isLoading, isLoadingAuth)
    return (
        <>

            {
                isSmall?(
                        <React.Fragment key={'bottom'}>

                            {searchMain()}
                            <div className={"mobile-drawer-container"}>
                                <SwipeableDrawer
                                    anchor={'bottom'}
                                    open={openDrawer}
                                    onClose={toggleDrawer( false)}
                                    onOpen={toggleDrawer( true)}
                                    className={"mobile-drawer"}
                                >
                                    <Box
                                        sx={{ width: 'auto',backgroundColor:ARTIST_LIGHT_GREY, borderRadius:'20px 20px 0px 0px' }}
                                        role="presentation"
                                    >
                                        <div style={{padding:'35px 50px',backgroundColor:ARTIST_LIGHT_GREY, borderRadius:'20px 20px 0px 0px' }} >
                                            <Typography color={"primary"} variant={"h5"}>
                                                Filter By
                                            </Typography>

                                            <Typography variant={"body1"} sx={{marginTop:'20px',color:`${ARTIST_LIGHT_GREY} !important`}}>
                                                Hourly Rate
                                            </Typography>
                                            <Slider min={0} max={5000} value={rate} onChangeCommitted={handleRateChange} aria-label="Default" valueLabelDisplay="auto" />
                                            <Typography color={"primary"} textAlign={"center"} variant={"body1"} >
                                                ${rate[0]} - ${rate[1]>=5000? '5000+' : rate[1]}
                                            </Typography>
                                            <div
                                                style={{
                                                    display:'flex',
                                                    marginTop:'20px'
                                                }}
                                            >
                                                <ArtistButton
                                                    variant={"contained"}
                                                    style={{
                                                        margin:'auto'
                                                    }}
                                                    onClick={() => {
                                                        saveFilters()
                                                        setOpenDrawer(false)

                                                    }}
                                                >
                                                    Save
                                                </ArtistButton>
                                            </div>

                                        </div>
                                    </Box>
                                </SwipeableDrawer>
                            </div>
                        </React.Fragment>
                    ):
                    (
                        <Box sx={{ display: 'flex',
                            width:'100%',
                            maxHeight: !isSmall?`calc( 100vh -  ${getNavbarHeight()}px )`:'' }}>
                            <>
                                {
                                    !data?
                                        <div style={{height: 'calc(100vh - 74px)', width: '100%', display: 'flex'}}>
                                            <CircularProgress sx={{margin: 'auto'}}/>
                                        </div>
                                        :
                                        <>
                                            <ScrollableView>
                                                {searchMain()}
                                            </ScrollableView>
                                        </>

                                    // <ScrollableView >
                                        //     {searchMain()}
                                        // </ScrollableView>
                                }
                                {sidePanel()}

                            </>
                        </Box>
                    )
            }
        </>
    );
};

export default HireAPainter;
