import React from "react";
import { useAuth0 } from "@auth0/auth0-react";
import {HOME_ROUTE} from "../../../utils/routes/routes";
import ArtistButton from "../../ArtistButton";
// import {REGISTRATION_CUSTOMER_ROUTE} from "../../../utils/constants";

const LoginButton = ({...props}) => {
    const { loginWithRedirect } = useAuth0();

    const handleLogin = () => {
        loginWithRedirect({ appState: { returnTo: HOME_ROUTE.path } })
    }


    return (
        <ArtistButton
            variant={"contained"}
            onClick={handleLogin}
            {...props}
        >
            Log In
        </ArtistButton>
    );
};

export default LoginButton;
