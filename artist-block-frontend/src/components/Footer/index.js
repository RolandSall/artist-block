import React from 'react';

import './styles.scss';
import {Button, Grid, TextField, Typography, useMediaQuery, useTheme} from "@mui/material";
import {Link} from "react-router-dom";
import {FOOTER_ROUTES, HOME_ROUTE} from "../../utils/constants";
import styled from "@emotion/styled";
import Facebook from '../../assets/logos/facebook.svg'
import Twitter from '../../assets/logos/twitter.svg'
import App from '../../assets/logos/app.png'

export const CustomTextField = styled(TextField)({
    width: '100%',
    '&:hover': {
        borderColor: 'white',
    },
    '& .MuiOutlinedInput-root': {
        '& fieldset': {
            border: '2px solid #FFFFFF',
            borderRadius: '10px 0px 0px 10px',
            padding: '8px',
            color: 'white'
        },
        color: 'white',
        '&.Mui-focused fieldset': {
            borderColor: 'white',
        },
        '&:hover': {
            borderColor: 'white',
        },
    },
});
const CustomButton = styled(Button)({
    backgroundColor:'white',
    color:'#031926',
    borderRadius:'0px 10px 10px 0px'
});

const Footer = () => {

    const theme = useTheme()

    const smMatches = useMediaQuery(theme.breakpoints.down('md'))

    return (
        <div className="footer-container">
            <Grid container spacing={smMatches? 4: 1}>
                <Grid item xs={12} sm={12} md={2} lg={2}>
                    <div className="footer-logo flex column">
                        <Link to={HOME_ROUTE.path}>
                            <img alt="facebook-logo" src={App} height={100} width={100} className="social-media-button"/>
                        </Link>
                        <div className="flex spaced">
                            <img alt="facebook-logo" src={Facebook} height={20} width={20} className="social-media-button"/>
                            <img alt="twitter-logo"  src={Twitter} height={20} width={20} className="social-media-button"/>
                        </div>
                    </div>
                </Grid>
                <Grid item xs={12} sm={12} md={10} lg={10}>
                    <div className="flex spaced links" style={{width: '100%'}}>
                        {
                            FOOTER_ROUTES.map(({pathname,text}) => (
                                <div>
                                    <Link to={pathname} className="link">
                                        <Typography color={"white"}>
                                            {text}
                                        </Typography>
                                    </Link>
                                </div>
                            ))
                        }
                    </div>
                </Grid>
            </Grid>
        </div>
    );
};

export default Footer;
