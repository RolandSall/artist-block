import React from "react";
import { useAuth0 } from "@auth0/auth0-react";
import ArtistButton from "../../ArtistButton";

const LogoutButton = ({...props}) => {
    const { logout } = useAuth0();
    return (
        <ArtistButton
            variant={"text"}
            onClick={() =>
                logout({
                    returnTo: window.location.origin,
                })
            }
            {...props}
        >
            Log Out
        </ArtistButton>
    );
};

export default LogoutButton;
