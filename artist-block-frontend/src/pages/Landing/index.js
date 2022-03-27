import React from 'react';
import {useAuth0} from "@auth0/auth0-react";
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Modal from '@mui/material/Modal';
import App from "../../assets/logos/safe-mail.png";

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 550,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
};

const Landing = () => {
    const [open, setOpen] = React.useState(true);
    const handleClose = () => setOpen(false);

    const {
        error,
        loginWithRedirect,
    } = useAuth0();
    if (error && error.error_description === 'Please verify your email before logging in.') {
        return (
            <div>
                <Modal
                    open={open}
                    onClose={handleClose}
                    aria-labelledby="modal-modal-title"
                    aria-describedby="modal-modal-description"
                >
                    <Box sx={style} >
                        <Typography id="modal-modal-title" variant="h4" component="h4" style={{display: "flex", justifyContent: "center"}}>
                            Email Verification
                        </Typography>
                        <div style={{display: "flex", justifyContent: "center"}}>
                        <img alt="facebook-logo" src={App} height={100} width={100} className="social-media-button"/>
                        </div>

                        <Typography id="modal-modal-description" variant="h5" sx={{ mt: 2 }} style={{display: "flex", justifyContent: "center"}}>
                            We have sent you an email for verification.
                        </Typography>
                    </Box>
                </Modal>
            </div>
        )
    } else {
        return (
            <div>
                Landing page
            </div>
        )
    }
    ;

};

export default Landing;
