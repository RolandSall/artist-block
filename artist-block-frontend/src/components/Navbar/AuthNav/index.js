import React from "react";
import {useAuth0} from "@auth0/auth0-react";
import LoginButton from "../LoginButton";
import MyArtistButton from "../MyArtistButton";
import {Avatar, CircularProgress, Fade, Menu, MenuItem} from "@mui/material";
import './styles.scss';
import {ARTIST_GREEN} from "../../../utils/constants";
import {useHistory} from "react-router-dom";

const AuthNav = ({authButton=true}) => {
    const { isLoading, isAuthenticated, logout } = useAuth0();

    const auth = () => isAuthenticated ? <MyArtistButton /> : <LoginButton />;

    const { push} = useHistory()
    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);
    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const logoutButton = () => {
        logout({
            returnTo: window.location.origin,
        })

        handleClose()

    }
    const handleClose = () => {
        setAnchorEl(null);
    };

    if(isLoading){
        return (
            <CircularProgress />
        )
    }

    return (
        <div style={{display:'flex',placeItems:'center'}}>
            {
                authButton &&
                auth()
            }
            {isAuthenticated && (
                <div  onMouseLeave={handleClose}>
                    <Avatar sx={{marginLeft:'10px', cursor:'pointer'}} onMouseOver={handleClick}/>
                    <Menu
                        id="fade-menu"
                        MenuListProps={{
                            'aria-labelledby': 'fade-button',
                        }}
                        anchorOrigin={{
                            vertical: 'bottom',
                            horizontal: 'center',
                        }}
                        transformOrigin={{
                            vertical: 'top',
                            horizontal: 'center',
                        }}
                        className={"dropdown-user-menu"}
                        anchorEl={anchorEl}
                        open={open}
                        onClose={handleClose}
                        TransitionComponent={Fade}
                    >
                        {/*<MenuItem onClick={() => {*/}
                        {/*    handleClose()*/}
                        {/*}} sx={{color: ARTIST_GREEN}}>Setting</MenuItem>*/}
                        {/*<MenuItem onClick={handleClose}>My account</MenuItem>*/}
                        <MenuItem
                            onClick={logoutButton}
                            sx={{backgroundColor:'rgba(255, 0, 0, 0.1)',color:'#FF0000'}}
                        >
                            Logout
                        </MenuItem>
                    </Menu>
                </div>
            )}
        </div>
    )
};


export default AuthNav;
