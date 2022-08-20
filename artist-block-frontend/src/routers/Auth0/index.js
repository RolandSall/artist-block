
import React from "react";
import {Redirect, Route} from "react-router-dom";
import {useAuth0} from "@auth0/auth0-react";
import {CircularProgress} from "@mui/material";
import ProtectedRoute from "../Protected";



const Auth0Route = ({ component, ...args }) => {

    const { isLoading, isAuthenticated, loginWithRedirect, user } = useAuth0();

    return (<Route
        render={(props) =>
        {
                if(isLoading) {
                    return (
                        <div style={{height: 'calc(100vh - 74px)', width: '100%', display: 'flex'}}>
                            <CircularProgress sx={{margin: 'auto'}}/>
                        </div>
                    )
                }
                else{
                    if(isAuthenticated){
                        if(user){
                            if(user.email_verified){
                                return <ProtectedRoute component={component} {...props}/>
                            }
                            else {
                                return <>Verify Your Email</>
                            }
                        }
                    }
                    else{
                        return <Redirect to={loginWithRedirect()} />
                    }
                }
            }
        }

        {...args}
    />)
};

export default Auth0Route;

