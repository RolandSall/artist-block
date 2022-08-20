import React from "react";
import {Redirect, Route, useLocation} from "react-router-dom";
import {useAuth0} from "@auth0/auth0-react";
import {CircularProgress} from "@mui/material";
import useAuth0Query from "../../hooks/useAuth0Query";
import {hooks} from "../../query";
import {REGISTRATION_CUSTOMER_ROUTE, REGISTRATION_ARTIST_ROUTE} from "../../utils/constants";
import RegistrationArtist from "../../pages/RegistrationArtist";
import RegistrationCustomer from "../../pages/RegisterCustomer";

function ProtectedRoute({ component: Component, ...restOfProps }) {

    const { user,isAuthenticated,getAccessTokenSilently } = useAuth0();
    const { token } = useAuth0Query();
    const { data, error, isLoading } = hooks.useCurrentMember({isAuthenticated
        ,
        token: getAccessTokenSilently()})
    const { pathname } = useLocation()
    console.log(user,isLoading,error)
    if (isLoading) {
        return (
            <div style={{height:'calc(100vh - 74px)',width:'100%',display:'flex'}}>
                <CircularProgress sx={{margin:'auto'}} />
            </div>
        )

    }

    if(!isLoading) {
        if(error) {
            if(pathname===REGISTRATION_CUSTOMER_ROUTE.path){
                return <RegistrationCustomer />
            }
            if(pathname===REGISTRATION_ARTIST_ROUTE.path){
                return <RegistrationArtist />
            }
        }
    }

    console.log(!isLoading,!error,data,isAuthenticated )
    return (
        <Route
            {...restOfProps}
            render={(props) =>
                !isLoading && !error && data && isAuthenticated  ? <Component {...props} /> : <Redirect to={REGISTRATION_CUSTOMER_ROUTE.path}/>
            }
        />
    );
}

export default ProtectedRoute;
