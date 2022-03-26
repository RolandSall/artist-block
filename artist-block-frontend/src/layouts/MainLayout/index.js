import React from 'react';
import Navbar from "../../components/Navbar";
import Footer from "../../components/Footer";
import './styles.scss';
import ScrollableView from "../../components/ScrollableView";
import {NAVBAR_HEIGHT, ROUTES} from "../../utils/constants";
import {useLocation} from "react-router-dom";
import {useMediaQuery, useTheme} from "@mui/material";

const MainLayout = ({children}) => {

    const { pathname } = useLocation()

    const route = ROUTES.find((e) => e.path === pathname)

    const theme = useTheme()

    const isSmall = useMediaQuery(theme.breakpoints.down('md'));

    const scrollable = route.scrollable
    const footer = route.footer
    return (
        <div style={{paddingTop:NAVBAR_HEIGHT}}>

            <Footer/>
        </div>
    );
}

export default MainLayout;
