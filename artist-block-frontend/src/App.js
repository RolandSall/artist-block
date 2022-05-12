import React, {useEffect} from "react";
import {Route, Switch} from "react-router-dom";
import {ROUTES} from "./utils/constants";
import './App.css'
import MainLayout from "./layouts/MainLayout";
import useAuth0Query from "./hooks/useAuth0Query";
import {useAuth0} from "@auth0/auth0-react";
import Auth0Route from "./routers/Auth0";
import {hooks} from "./query";

const App = () => {

    const { isAuthenticated } = useAuth0();
    const { token } = useAuth0Query()

    const { data: member, isLoading: isLoadingCurrentMember} = hooks.useCurrentMember(
        {isAuthenticated,token: token()})

    const { data: artist, isLoading: isLoadingArtist} = hooks.useCurrentArtist({
        isArtist: member?.role==="Painter",
        token:token()
    })

    return (
        <Switch>
            <MainLayout>
                {
                    ROUTES.map(({path,component,scrollable,protectedRoute, exact},index) => {
                            return (
                                protectedRoute?
                                    <Auth0Route key={`route-${index}`} path={path} exact component={component} />
                                    :
                                    <Route strict={false} key={`route-${index}`} path={path} exact={exact} component={component} />
                            )
                        }
                    )
                }
            </MainLayout>
        </Switch>
    );
}

export default App;
