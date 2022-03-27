import React from 'react';
import Navbar from "../../components/Navbar";
import Footer from "../../components/Footer";
import './styles.scss';
import ScrollableView from "../../components/ScrollableView";
import {NAVBAR_HEIGHT, ROUTES} from "../../utils/constants";
import {Link, useLocation} from "react-router-dom";
import {Typography, useMediaQuery, useTheme} from "@mui/material";
// import {hooks} from "../../query";
import useAuth0Query from "../../hooks/useAuth0Query";
import {useAuth0} from "@auth0/auth0-react";
import { matchPath } from "react-router";

const MainLayout = ({children}) => {

    const { pathname } = useLocation()

    const route = ROUTES.find((e) => matchPath(pathname,{path:e.path,exact:true}))



    const { isAuthenticated } = useAuth0();
    const { token } = useAuth0Query();
    // const { data, error, isLoading } = hooks.useCurrentMember({isAuthenticated,token: token()})

    const theme = useTheme()

    const isSmall = useMediaQuery(theme.breakpoints.down('md'));

    const scrollable = route.scrollable
    const footer = route.footer
    //


    return (
        <div style={{paddingTop:NAVBAR_HEIGHT}}>
            <Navbar/>
            {
                scrollable && !isSmall ?
                    <>
                        <ScrollableView>
                            <div className="main-container"
                                 // style={{
                                 //     marginTop:((isAuthenticated && !isLoading && !data && ) ||  !isAuthenticated && hireAnArtist)? '50px':'',
                                 //     minHeight: ((isAuthenticated && !isLoading && !data && ) ||  !isAuthenticated && hireAnArtist)?'calc( 100vh - 124px )':''
                                 // }}
                            >
                                {children}
                            </div>
                            {
                                footer &&
                                <Footer/>
                            }
                        </ScrollableView>
                    </> :
                    <>
                        <div>

                            <div className="main-container"
                                 // style={{
                                 //     marginTop:((isAuthenticated && !isLoading && !data && ) ||  !isAuthenticated &&hireAnArtist )? '50px':'',
                                 //     minHeight: ((isAuthenticated && !isLoading && !data && ) ||  !isAuthenticated && hireAnArtist)?'calc( 100vh - 124px )':''
                                 // }}
                            >
                                {children}
                            </div>
                            {
                                footer &&
                                <Footer/>
                            }
                        </div>
                    </>
            }
        </div>
    );
}

export default MainLayout;
