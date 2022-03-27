import React from 'react';
import Button from "@mui/material/Button";
import PropTypes from 'prop-types';

const ArtistButton = ({
                        variant,
                        children,
                        padding="10px 20px",
                        onClick,
                        ...props
                    }) => {
    return (
        <Button
            variant={variant}
            onClick={onClick}
            sx={{
                color:'white',
                padding: `${padding} !important`,
                borderRadius:'17px !important'
            }}
            disableElevation
            {...props}
        >
            {children}
        </Button>
    );
};

ArtistButton.propType = {
    variant: PropTypes.oneOf(['contained','outlined','text']).isRequired,
    children: PropTypes.node.isRequired,
    padding: PropTypes.string,
    onClick: PropTypes.func
}

export default ArtistButton;