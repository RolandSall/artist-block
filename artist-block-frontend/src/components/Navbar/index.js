import React, { useState} from 'react'
import {
    AppBar,
    Box,
    List,
    ListItem,
    ListItemText,
    SwipeableDrawer,
    Toolbar, Typography,
    useMediaQuery,
    useTheme
} from "@mui/material";
import {Link, NavLink, useLocation} from "react-router-dom";
import {
    HOME_ROUTE,
    NAVBAR_ROUTES_DESKTOP,
    NAVBAR_ROUTES_MOBILE,
} from "../../utils/constants";
import './styles.scss';
import AuthNav from "./AuthNav";
import {useAuth0} from "@auth0/auth0-react";
import LoginButton from "./LoginButton";
import LogoutButton from "./LogoutButton";
import useAuth0Query from "../../hooks/useAuth0Query";
import MyArtistButton from "./MyArtistButton";

const Navbar = () => {


    const [open, setOpen] = useState(false);

    const { pathname } = useLocation()


    const { isAuthenticated } = useAuth0();
    const { token } = useAuth0Query();

    const theme = useTheme();

    const matchesMd = useMediaQuery(theme.breakpoints.up('md'));

    const toggleDrawer = (openE) => (event) => {
        if (
            event &&
            event.type === 'keydown' &&
            (event.key === 'Tab' || event.key === 'Shift')
        ) {
            return;
        }

        setOpen(openE)
    };


    const desktopRoutes = () => {
        return NAVBAR_ROUTES_DESKTOP.map((e) => {
            if(Array.isArray(e)) {
                return (
                    <li className={"nav-li desktop dropdown"} key={`nav-item-${e[0].name}`}>
                        <span className={"nav-link"}>{e[0].name}</span>
                        <i className="arrow down" />
                        <ul className="dropdown_menu dropdown_menu--animated dropdown_menu-6">
                            {
                                e[0].paths.map(({path, name}) => (
                                    <li key={`nav-item-${name}`} className={'dropdown-li'}>
                                        <NavLink
                                            to={path}
                                            activeClassName="active"
                                            className={'nav-link'}
                                            exact
                                        >
                                            {name}
                                        </NavLink>
                                    </li>
                                ))
                            }
                        </ul>
                    </li>

            )
            }
            else {
                const {path,name} = e
                return (
                    <li key={`nav-item-${name}`} className={'nav-li desktop'}>
                        <NavLink
                            to={path}
                            activeClassName="active"
                            className={'nav-link'}
                            exact
                        >
                            {name}
                        </NavLink>
                    </li>
                )
            }
        })
    }

    return (
        <Box style={{position: 'relative'}}>
            <AppBar position="fixed" className="header">
                <Toolbar sx={{padding:' 0 !important',height:'100%'}}>
                    <Link to={HOME_ROUTE.path} style={{display:'flex'}} >
                        {/*<img*/}
                        {/*    className={"logo"}*/}
                        {/*    src={PurpleLogo}*/}
                        {/*    height={50}*/}
                        {/*    alt="logo"*/}
                        {/*    style={{pointerEvents: "all", cursor: 'pointer'}}*/}
                        {/*/>*/}
                    </Link>
                    <Box sx={{flexGrow: 1}}/>
                    {matchesMd ?
                        <div className={"nav-container"}>
                            <ul className={"nav-container-ul"}>
                                {
                                    desktopRoutes()
                                }
                                <li
                                    key={`nav-item-login`} className={'nav-li desktop'}
                                    style={{
                                        zIndex:100000000
                                    }}
                                >
                                    <AuthNav />
                                </li>
                            </ul>
                        </div>
                        :
                        <React.Fragment key={"right"}>
                            {/*<img*/}
                            {/*    className={"logo"}*/}
                            {/*    src={MenuIcon}*/}
                            {/*    height={15}*/}
                            {/*    alt="logo"*/}
                            {/*    onClick={toggleDrawer(true)}*/}
                            {/*    style={{pointerEvents: "all", cursor: 'pointer'}}*/}
                            {/*/>*/}
                            <SwipeableDrawer
                                    anchor={'left'}
                                    className={"drawer"}
                                    open={open}
                                    onClose={toggleDrawer(false)}
                                    onOpen={toggleDrawer(true)}
                                >
                                    <Box
                                        sx={{width: '100% !important'}}
                                        onClick={toggleDrawer(false)}
                                        onKeyDown={toggleDrawer(false)}
                                    >
                                        <div className={"drawer-header"}>
                                            <div className={"drawer-image"}>
                                                {/*<img*/}
                                                {/*    className={"logo"}*/}
                                                {/*    // src={PurpleLogo}*/}
                                                {/*    height={50}*/}
                                                {/*    alt="logo"*/}
                                                {/*    style={{pointerEvents: "all", cursor: 'pointer'}}*/}
                                                {/*/>*/}
                                            </div>
                                            {
                                                isAuthenticated &&
                                                <AuthNav authButton={false} />

                                            }
                                        </div>
                                        <List className={"drawer-list"}>
                                            {
                                            NAVBAR_ROUTES_MOBILE.map(({path, name, icon}) => (
                                                <NavLink
                                                    to={path}
                                                    activeClassName="drawer-active"
                                                    // className={'drawer-active'}
                                                    key={`navlink-${name}`}
                                                    exact
                                                >
                                                    <ListItem className={"drawer-item"} button key={name}>
                                                        <img alt={`logo-nav-${name}`} src={icon} height={"20px"} width={'20px'}/>
                                                        <ListItemText className={"drawer-item-text"} primary={name} />
                                                    </ListItem>
                                                 </NavLink>
                                            ))}
                                            {
                                                isAuthenticated?(
                                                    <>
                                                        <MyArtistButton fullWidth />
                                                        <LogoutButton fullWidth />
                                                    </>

                                                    )
                                                    :
                                                    <LoginButton fullWidth />
                                            }
                                        </List>
                                    </Box>
                                </SwipeableDrawer>

                        </React.Fragment>
                    }

                </Toolbar>
            </AppBar>
        </Box>
    )
}

export default Navbar;
