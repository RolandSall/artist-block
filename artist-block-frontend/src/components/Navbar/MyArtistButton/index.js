import React from 'react';
import ArtistButton from "../../ArtistButton";

const MyArtistButton = ({...props}) => {
    return (
        <ArtistButton
            variant={"contained"}
            disabled
            {...props}
        >
            Artist Dashboard
        </ArtistButton>
    );
};

export default MyArtistButton;